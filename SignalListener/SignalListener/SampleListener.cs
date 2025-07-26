using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.IO.Pipes;

namespace SignalListener;

// This listener listens for a signal on a named pipe and invokes a callback when the signal is received.
// It checks the SynchronizationContext to ensure that the callback is invoked on the correct thread.
// After receiving the signal, it stops listening for further signals.
// TODO: Add an option to continue listening for signals after the first one is received.
internal class SampleListener
{
    private readonly ILogger<SampleListener> _logger = NullLogger<SampleListener>.Instance;
    private readonly SynchronizationContext? _syncContext = SynchronizationContext.Current;
    private CancellationTokenSource? _stopSignalListenerCancellation;

    /// <summary>
    /// Starts the signal listener asynchronously, running in parallell on the same thread. <br/>
    /// Invokes the callback when a signal is received.
    /// </summary>
    public void Start(string pipeName, Action callback)
    {
        _stopSignalListenerCancellation = new CancellationTokenSource();
        Task.Run(async () =>
        {
            await WaitForStopSignal(pipeName, callback, _stopSignalListenerCancellation.Token);
        });
    }

    private async Task WaitForStopSignal(string pipeName, Action callback, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Listening for signal...");
            using var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In);
            await pipeServer.WaitForConnectionAsync(cancellationToken); // Throws if canceled.

            _logger.LogInformation("Signal received.");
            InvokeCallback(callback);
        }
        catch (OperationCanceledException)
        {
            _logger.LogDebug("Stopped listening for signal.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when listening for signal.");
        }
    }

    private void InvokeCallback(Action callback)
    {
        if (_syncContext is not null)
            _syncContext.Post(_ => callback.Invoke(), null); // Invoke on the calling thread.
        else
            callback.Invoke(); // Invoke on the listener thread.
    }

    public void Stop()
    {
        _stopSignalListenerCancellation?.Cancel();
        _stopSignalListenerCancellation?.Dispose();
        _stopSignalListenerCancellation = null;
    }
}
