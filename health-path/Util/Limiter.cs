namespace health_path.Util;

public class Limiter : IDisposable
{
    private readonly SemaphoreSlim semaphore;

    public Limiter(int concurrencyLimit) {
        semaphore = new SemaphoreSlim(concurrencyLimit);
    }

    public void Dispose()
    {
        semaphore.Dispose();
    }

    public async Task Wrap(Action action) {
        await semaphore.WaitAsync();
        try {
            action.Invoke();
        } finally {
            semaphore.Release();
        }
    }
}
