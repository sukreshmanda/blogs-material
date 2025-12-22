using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace InterOpTutorial.Basic;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BasicBenchmarks
{
    private string[] _logs__1_000 = Array.Empty<string>();
    private string[] _logs__10_000 = Array.Empty<string>();
    private string[] _logs__1_00_000 = Array.Empty<string>();

    private NaiveParser _naiveParser = new();

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);

        _logs__1_000 = GenerateUtils.GenerateLogs(1000, random);
        _logs__10_000 = GenerateUtils.GenerateLogs(10000, random);
        _logs__1_00_000 = GenerateUtils.GenerateLogs(100000, random);
    }


    // === Count By Level (Different Sizes) ===

    [Benchmark]
    public Dictionary<string, int> Basic_CountByLevel_1_000()
    {
        return _naiveParser.CountByLevel(_logs__1_000);
    }

    [Benchmark]
    public Dictionary<string, int> Basic_CountByLevel_10_000()
    {
        return _naiveParser.CountByLevel(_logs__10_000);
    }

    [Benchmark]
    public Dictionary<string, int> Basic_CountByLevel_1_00_000()
    {
        return _naiveParser.CountByLevel(_logs__1_00_000);
    }
}