using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Concurrency.Parallelism
{
    public class UsingParallel : IExample
    {
        const string Text = @"There were two goats. Over a river there was a very narrow bridge. One day a goat was crossing this bridge. Just at the middle of the bridge he met another goat. There was no room for them to pass. 'Go back,' said one goat to the other, 'There is no room for both of us'. 'Why should I go back?' said the other goat. 'Better you must go back.' 'You must go back', said the first goat, 'because I am stronger than you.' 'You are not stronger than I', said the second goat. 'We will see about that', said the first goat and he put down his horns to fight. 'Stop!' said the second goat. 'If we fight, we shall both fall into the river and be drowned and instead I have a plan. I shall lie down and you may walk over me.' Then the wise one laid down on the bridge and the other goat walked highly over him. So they crossed the bridge comfortably and went on their ways.";

        public void Run()
        {
            DataParallelism();

            TaskParallelism();

            PLinq();
        }

        private void DataParallelism()
        {
            var sentences = Text.Split('.');
            Parallel.ForEach(sentences, sentence =>
            {
                var processedSentence = PrepareSentence(sentence);
                var goatCount = GoatCount(processedSentence);
                Console.WriteLine($"Sentence: {processedSentence}.\nGoat found {goatCount} times");
            });
            Console.WriteLine();
        }

        private static int GoatCount(string processedSentence)
        {
            var goatCount = new Regex("goat").Matches(processedSentence).Count;
            return goatCount;
        }

        private static string PrepareSentence(string sentence)
        {
            var processedSentence =
                sentence.Replace("'", "").Replace("\r", "").Replace("\t", "").Replace("\n", "").Trim().ToLowerInvariant();
            return processedSentence;
        }

        private void TaskParallelism()
        {
            Parallel.Invoke(
                () => Console.WriteLine($"Count of goats: {new Regex("goat").Matches(Text).Count}"),
                () => Console.WriteLine($"Count of bridges: {new Regex("bridge").Matches(Text).Count}"),
                () => Console.WriteLine($"Count of saids: {new Regex("said").Matches(Text).Count}"),
                () => Console.WriteLine($"Count of rivers: {new Regex("river").Matches(Text).Count}"));
        }

        private void PLinq()
        {
            Text.Split('.')
                .AsParallel()
                .Select(x => $"Sentence: {PrepareSentence(x)}.\nGoat found {GoatCount(x)} times")
                .ForAll(Console.WriteLine);
        }
    }
}