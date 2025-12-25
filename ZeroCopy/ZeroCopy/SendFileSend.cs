using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ZeroCopy;

public class SendFileSend
{
    // macOS sendfile
    [DllImport("libc", EntryPoint = "sendfile", SetLastError = true)]
    private static extern int sendfile_macos(
        int fd, int s, long offset, ref long len, IntPtr hdtr, int flags);

    // Linux sendfile
    [DllImport("libc", EntryPoint = "sendfile", SetLastError = true)]
    private static extern long sendfile_linux(
        int out_fd, int in_fd, IntPtr offset, long count);

    public void SendFile()
    {
        var filePath = "testfile.bin";
        
        using var client = new TcpClient("localhost", 9876);
        var socket = client.Client;
        var socketFd = (int)socket.Handle;

        using var fileStream = File.OpenRead(filePath);
        var fileFd = (int)fileStream.SafeFileHandle.DangerousGetHandle();

        long fileSize = fileStream.Length;


        bool success;
        long bytesSent;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // macOS: sendfile(fd, socket, offset, &len, hdtr, flags)
            long len = fileSize;
            sendfile_macos(fileFd, socketFd, 0, ref len, IntPtr.Zero, 0);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Linux: sendfile(socket, fd, NULL, count)
            sendfile_linux(socketFd, fileFd, IntPtr.Zero, fileSize);
        }
        else
        {
            throw new PlatformNotSupportedException("SendFile only supported on Linux and macOS");
        }
        
    }

    private static string GetPlatformName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "Linux";
        if (RuntimeInformation.IsOSPlatform(OSPlatform. OSX)) return "macOS";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "Windows";
        return "Unknown";
    }
}