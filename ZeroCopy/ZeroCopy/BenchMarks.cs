using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ZeroCopy;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchMarks
{

    private SendFileSend? _sendFileSend;
    private NormalSendFile? _normalSendFile ;
    
    [GlobalSetup]
    public void Setup()
    {
        _sendFileSend = new SendFileSend();
        _normalSendFile = new NormalSendFile();
    }
    
    [Benchmark]
    public void SendFileSendBenchmark()
    {
        _sendFileSend?.SendFile();
    }

    [Benchmark]
    public async Task NormalSendFileBenchmark()
    {
        await _normalSendFile?.SendFile()!;
    }
}