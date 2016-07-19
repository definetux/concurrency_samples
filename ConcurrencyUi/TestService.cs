using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyUi
{
    public class TestService
    {
        private const int Delay = 500;

        public async Task<string> DownloadString(string url, CancellationToken token)
        {
            await Task.Delay(Delay, token);
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync($"http://{url}", token).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStringAsync();
                }
                return string.Empty;
            }
        }

        public async Task LongOperation(IProgress<int> progressIndicator, CancellationToken token)
        {
            for (var i = 0; i < 100; i++)
            {
                await Task.Delay(Delay, token);
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                progressIndicator.Report(i);
            }
        }

        public Task UseBackgroundWorker()
        {
            var tcs = new TaskCompletionSource<double>();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (o, e) =>
            {
                Thread.Sleep(2000);
                e.Result = 42;
            };
            backgroundWorker.RunWorkerCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    tcs.SetException(e.Error);
                }
                else
                {
                    tcs.SetResult((double)e.Result);
                }
            };

            return tcs.Task;
        }
    }
}