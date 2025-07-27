using SignalListener;
using System.IO.Pipes;

namespace SignalListenerTests;

[TestClass()]
public class SampleListenerTests
{
    private const string _pipeName = "SampleListener.TestCommand";

    [TestMethod]
    [DoNotParallelize]
    public async Task Start_ConnectsAndInvokesCallback()
    {
        // Arrange
        var listener = new SampleListener();
        var callbackEvent = new ManualResetEventSlim(false);
        Action callback = callbackEvent.Set;

        // Act
        listener.Start(_pipeName, callback);
        await Task.Delay(200);

        _ = Task.Run(async () =>
        {
            using var pipeClient = new NamedPipeClientStream(".", _pipeName, PipeDirection.Out);
            await pipeClient.ConnectAsync(2000);
        });

        bool isCallbackInvoked = callbackEvent.Wait(2500);

        // Assert
        Assert.IsTrue(isCallbackInvoked, "Callback was not invoked when signal was sent.");
    }

    [TestMethod]
    [DoNotParallelize]
    public async Task Stop_CancelsListening_CallbackNotInvoked()
    {
        // Arrange
        var listener = new SampleListener();
        var callbackEvent = new ManualResetEventSlim(false);
        Action callback = callbackEvent.Set;

        // Act
        listener.Start(_pipeName, callback);
        await Task.Delay(200);
        listener.Stop();
        bool isCallbackInvoked = callbackEvent.Wait(500);

        // Assert
        Assert.IsFalse(isCallbackInvoked, "Callback should not be invoked after Stop is called.");
    }
}