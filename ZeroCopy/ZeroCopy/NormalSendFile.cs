using System.Diagnostics;
using System.Net.Sockets;

namespace ZeroCopy;

public class NormalSendFile
{
    public async Task SendFile(String filePath)
    {
        
        using var client = new TcpClient("localhost", 9876);
        await using var stream = client.GetStream();
        await using var fileStream = File.OpenRead(filePath);

        var stopwatch = Stopwatch.StartNew();

        // Traditional copy:  File -> Userspace buffer -> Kernel -> Network
        var buffer = new byte[64 * 1024]; // 64KB buffer
        int bytesRead;
        long totalBytes = 0;

        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            await stream.WriteAsync(buffer.AsMemory(0, bytesRead));
            totalBytes += bytesRead;
        }

        stopwatch.Stop();
    }
}