using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ConcurrencyUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly TestService _service;
        private CancellationTokenSource _tokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _service = new TestService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await UseCancellation(async (token, p) =>
            {
                UxHtmlContent.Text = await _service.DownloadString(UxUrl.Text, token);
            });
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await UseCancellation(async (token, p) =>
            {
                await _service.LongOperation(p, token);
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _tokenSource?.Cancel();
        }

        private async Task UseCancellation(Func<CancellationToken, IProgress<int>, Task> action)
        {
            _tokenSource = new CancellationTokenSource();
            var progressIndicator = new Progress<int>(value => UxProgress.Value = value);
            try
            {
                await action(_tokenSource.Token, progressIndicator);
            }
            catch (TaskCanceledException)
            {
                UxHtmlContent.Text = string.Empty;
                UxProgress.Value = 0;
            }
            finally
            {
                _tokenSource.Dispose();
                _tokenSource = null;
            }
        }
    }
}
