using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class SpanVsArrayBenchmark
{
    private const int ArraySize = 1000;
    private int[] array;

    public SpanVsArrayBenchmark()
    {
        array = new int[ArraySize];
        Random rand = new Random();
        for (int i = 0; i < ArraySize; i++)
        {
            array[i] = rand.Next();
        }
    }

    [Benchmark]
    public void ProcessWithSpan()
    {
        Span<int> span = new Span<int>(array);
        for (int i = 0; i < span.Length; i++)
        {
            span[i] *= 2;
        }
    }

    [Benchmark]
    public void ProcessWithArray()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] *= 2;
        }
    }

    public static void Run()
    {
        var summary = BenchmarkRunner.Run<SpanVsArrayBenchmark>();
    }
}