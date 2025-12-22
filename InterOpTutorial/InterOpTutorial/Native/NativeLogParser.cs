namespace InterOpTutorial.Native;

using System.Runtime.InteropServices;
using System.Text;

using System. Runtime.InteropServices;
using System.Text;

[StructLayout(LayoutKind.Sequential)]
public struct LevelCounts
{
    public int Error;
    public int Warn;
    public int Info;
    public int Debug;
    public int Unknown;
    
    public int Total => Error + Warn + Info + Debug + Unknown;
    
    public override string ToString()
    {
        return $"ERROR:  {Error}, WARN: {Warn}, INFO: {Info}, DEBUG: {Debug}, UNKNOWN: {Unknown}";
    }
}

public static class NativeFFI
{
    private const string LibName = "log_parser_native";
    
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe int count_by_level_batch(
        byte* buffer,
        uint* lineOffsets,
        uint* lineLengths,
        nuint lineCount,
        LevelCounts* outCounts
    );
}

/// <summary>
/// High-performance log parser with dynamic memory allocation
/// No pre-allocation - allocates based on actual log size
/// </summary>
public class NativeParser
{
    private const int MaxBatchSize = 100_000;
    
    /// <summary>
    /// Count all log levels - allocates memory dynamically based on log size
    /// </summary>
    public unsafe LevelCounts CountByLevel(string[] logs)
    {
        if (logs. Length == 0)
            return new LevelCounts();
        
        if (logs.Length > MaxBatchSize)
            throw new ArgumentException($"Batch size exceeds {MaxBatchSize}");
        
        // Calculate exact buffer size needed
        int totalBufferSize = CalculateBufferSize(logs);
        
        // Allocate exactly what we need
        IntPtr buffer = IntPtr.Zero;
        IntPtr offsets = IntPtr.Zero;
        IntPtr lengths = IntPtr. Zero;
        
        try
        {
            buffer = (IntPtr)NativeMemory.Alloc((nuint)totalBufferSize);
            offsets = (IntPtr)NativeMemory.Alloc((nuint)(logs.Length * sizeof(uint)));
            lengths = (IntPtr)NativeMemory.Alloc((nuint)(logs.Length * sizeof(uint)));
            
            // Prepare buffer
            PrepareBuffer(logs, buffer, totalBufferSize, offsets, lengths);
            
            // Call native code
            var counts = new LevelCounts();
            
            int result = NativeFFI.count_by_level_batch(
                (byte*)buffer,
                (uint*)offsets,
                (uint*)lengths,
                (nuint)logs.Length,
                &counts
            );
            
            if (result != 0)
                throw new InvalidOperationException("Native call failed");
            
            return counts;
        }
        finally
        {
            // Always free memory
            if (buffer != IntPtr.Zero)
                NativeMemory.Free((void*)buffer);
            
            if (offsets != IntPtr. Zero)
                NativeMemory.Free((void*)offsets);
            
            if (lengths != IntPtr.Zero)
                NativeMemory.Free((void*)lengths);
        }
    }
    
    
    /// <summary>
    /// Calculate exact buffer size needed for all logs
    /// </summary>
    private static int CalculateBufferSize(string[] logs)
    {
        return logs.Sum(log => Encoding.UTF8.GetByteCount(log));
    }
    
    /// <summary>
    /// Prepare buffer with zero allocations
    /// </summary>
    private static unsafe void PrepareBuffer(
        string[] logs,
        IntPtr buffer,
        int bufferSize,
        IntPtr offsets,
        IntPtr lengths)
    {
        var bufferSpan = new Span<byte>((void*)buffer, bufferSize);
        var offsetsSpan = new Span<uint>((void*)offsets, logs.Length);
        var lengthsSpan = new Span<uint>((void*)lengths, logs.Length);
        
        int bufferPos = 0;
        
        for (int i = 0; i < logs.Length; i++)
        {
            ReadOnlySpan<char> logChars = logs[i]. AsSpan();
            
            int byteCount = Encoding.UTF8.GetByteCount(logChars);
            
            offsetsSpan[i] = (uint)bufferPos;
            lengthsSpan[i] = (uint)byteCount;
            
            Encoding.UTF8.GetBytes(logChars, bufferSpan. Slice(bufferPos, byteCount));
            
            bufferPos += byteCount;
        }
    }
}