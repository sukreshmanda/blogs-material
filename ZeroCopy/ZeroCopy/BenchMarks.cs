using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ZeroCopy;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchMarks
{
    private SendFileSend? _sendFileSend;
    private NormalSendFile? _normalSendFile;

    [GlobalSetup]
    public void Setup()
    {
        _sendFileSend = new SendFileSend();
        _normalSendFile = new NormalSendFile();
    }

    [Benchmark]
    public void SendFileSendBenchmark_100MB()
    {
        _sendFileSend?.SendFile("testfile_100mb.bin");
    }

    [Benchmark]
    public void SendFileSendBenchmark_200MB()
    {
        _sendFileSend?.SendFile("testfile_200mb.bin");
    }

    [Benchmark]
    public void SendFileSendBenchmark_500MB()
    {
        _sendFileSend?.SendFile("testfile_500mb.bin");
    }

    [Benchmark]
    public async Task NormalSendFileBenchmark_100MB()
    {
        await _normalSendFile?.SendFile("testfile_100mb.bin")!;
    }

    [Benchmark]
    public async Task NormalSendFileBenchmark_200MB()
    {
        await _normalSendFile?.SendFile("testfile_200mb.bin")!;
    }

    [Benchmark]
    public async Task NormalSendFileBenchmark_500MB()
    {
        await _normalSendFile?.SendFile("testfile_500mb.bin")!;
    }
}