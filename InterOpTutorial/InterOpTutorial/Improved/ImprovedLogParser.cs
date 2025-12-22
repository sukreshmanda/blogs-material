namespace InterOpTutorial.Improved;

public ref struct LogEntryRef
{
    public ReadOnlySpan<char> Level;
    public ReadOnlySpan<char> Message;
}

public class ImprovedParser
{
    private LogEntryRef ParseLog(ReadOnlySpan<char> logLine)
    {
        int spaceIndex = logLine.IndexOf(' ');
        
        if (spaceIndex == -1)
        {
            return new LogEntryRef
            {
                Level = logLine,
                Message = ReadOnlySpan<char>.Empty
            };
        }
        
        return new LogEntryRef
        {
            Level = logLine.Slice(0, spaceIndex),
            Message = logLine.Slice(spaceIndex + 1)
        };
    }
    
    public int CountErrors(string[] logs)
    {
        int count = 0;
        
        foreach (var log in logs)
        {
            var entry = ParseLog(log. AsSpan());
            if (entry.Level is "ERROR")
            {
                count++;
            }
        }
        
        return count;
    }
    
    // Optimized version with pre-allocated keys
    public Dictionary<string, int> CountByLevel(string[] logs)
    {
        var counts = new Dictionary<string, int>
        {
            ["ERROR"] = 0,
            ["WARN_"] = 0,
            ["INFO_"] = 0,
            ["DEBUG"] = 0,
            ["OTHER"] = 0
        };
        
        ReadOnlySpan<char> errorSpan = "ERROR";
        ReadOnlySpan<char> warnSpan = "WARN_";
        ReadOnlySpan<char> infoSpan = "INFO_";
        ReadOnlySpan<char> debugSpan = "DEBUG";
        
        foreach (var log in logs)
        {
            var entry = ParseLog(log.AsSpan());
            
            if (entry.Level.SequenceEqual(errorSpan))
                counts["ERROR"]++;
            else if (entry.Level.SequenceEqual(warnSpan))
                counts["WARN_"]++;
            else if (entry. Level.SequenceEqual(infoSpan))
                counts["INFO_"]++;
            else if (entry.Level.SequenceEqual(debugSpan))
                counts["DEBUG"]++;
            else
                counts["OTHER"]++;
        }
        
        return counts;
    }
}