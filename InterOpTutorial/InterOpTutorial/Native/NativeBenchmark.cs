namespace InterOpTutorial.Native;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class NativeBenchmark
{
    private string[] _logs__1_000 = Array.Empty<string>();
    private string[] _logs__10_000 = Array.Empty<string>();
    private string[] _logs__1_00_000 = Array.Empty<string>();


    private NativeParser _nativeParser = null!;

    [GlobalSetup]
    public void Setup()
    {
        _nativeParser = new NativeParser();

        var random = new Random(42);

        _logs__1_000 = GenerateUtils.GenerateLogs(1_000, random);
        _logs__10_000 = GenerateUtils.GenerateLogs(10_000, random);
        _logs__1_00_000 = GenerateUtils.GenerateLogs(1_00_000, random);
    }

    // === Count By Level (Different Sizes) ===


    [Benchmark]
    public LevelCounts Native_CountByLevel_1_000()
    {
        return _nativeParser.CountByLevel(_logs__1_000);
    }

    [Benchmark]
    public LevelCounts Native_CountByLevel_10_000()
    {
        return _nativeParser.CountByLevel(_logs__10_000);
    }

    [Benchmark]
    public LevelCounts Native_CountByLevel_1_00_000()
    {
        return _nativeParser.CountByLevel(_logs__1_00_000);
    }
}