using System;
using System.ComponentModel;
using System.Text;

namespace Concurrency.Multithreading
{
    public class UsingBackgorundWorker : IExample
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public void Run()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine(_sb);
            Console.WriteLine("Work's done");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _sb.AppendLine($"Current progress: {e.ProgressPercentage}%");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            double result = 0;
            double count = 100;
            for (double i = 0; i < count; i++)
            {
                result += i;
                var percent = i * 100.0 / count;
                worker?.ReportProgress((int)percent);
            }
            Console.WriteLine($"Result = {result}");
        }
    }
}