using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BenchmarkingExample
{
    [MemoryDiagnoser]
    public class CompareFormatMethods
    {
        private const int N = 1000;

        private string input = "  Tolle   Neue Straße 389324  ";

        [Benchmark(Baseline = true)]
        public string CompareFormatBaseline()
        {
            return Regex.Replace(input, @"\s+", "").ToLower();
        }

        [Benchmark]
        public string CompareFormatStringReplace()
        {
            return input.Replace(" ", string.Empty).ToLower();
        }
    }

    [MemoryDiagnoser]
    public class StringConcatMethods
    {
        private const int N = 1000;

        [Benchmark(Baseline = true)]
        public string NaiveApproach()
        {
            var result = string.Empty;

            for (int i = 0; i < 1000; i++)
            {
                result += i.ToString();
            }

            return result;
        }

        [Benchmark]
        public string WithStringConcat()
        {
            var result = string.Empty;

            for (int i = 0; i < 1000; i++)
            {
                result = string.Concat(result, i);
            }

            return result;
        }

        [Benchmark]
        public string WithStringBuilder()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < 1000; i++)
            {
                builder.Append(i);
            }

            return builder.ToString();
        }
    }

    [MemoryDiagnoser]
    public class GetSubsystem
    {
        private const int N = 1000;
        private const string Url = "/shop/api/v2/order/closed";

        [Benchmark(Baseline = true)]
        public string NaiveApproach()
        {
            var subsystem = Url.Split('/')[1].ToLower();
            return subsystem;
        }

        [Benchmark]
        public string IndexOf()
        {
            var index = Url.IndexOf('/', 1);
            
            var length = index == -1
                ? Url.Length - 1
                : index - 1;

            var subsystem = Url.Substring(1, length).ToLower();

            return subsystem;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
            Console.ReadLine();
        }
    }
}
