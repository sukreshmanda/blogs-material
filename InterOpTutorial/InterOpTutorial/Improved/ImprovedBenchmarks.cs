using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace InterOpTutorial.Improved;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ImprovedBenchmark
{
    private string[] _logs__1_00_000 = Array.Empty<string>();
    private string[] _logs__1_000 = Array.Empty<string>();
    private string[] _logs__10_000 = Array.Empty<string>();

    private ImprovedParser _parser = new();

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);

        _logs__1_000 = GenerateUtils.GenerateLogs(1_000, random);
        _logs__10_000 = GenerateUtils.GenerateLogs(10_000, random);
        _logs__1_00_000 = GenerateUtils.GenerateLogs(1_00_000, random);
    }

    // === Count By Level (Different Sizes) ===

    [Benchmark]
    public Dictionary<string, int> Improved_CountByLevel_1_000()
    {
        return _parser.CountByLevel(_logs__1_000);
    }

    [Benchmark]
    public Dictionary<string, int> Improved_CountByLevel_10_000()
    {
        return _parser.CountByLevel(_logs__10_000);
    }

    [Benchmark]
    public Dictionary<string, int> Improved_CountByLevel_1_00_000()
    {
        return _parser.CountByLevel(_logs__1_00_000);
    }
}