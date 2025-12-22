namespace InterOpTutorial;

public class LogEntry
{
    public string Level { get; set; } = "";
    public string Message { get; set; } = "";
}

public class NaiveParser
{
    public LogEntry ParseLog(string logLine)
    {
        // String allocations everywhere
        var parts = logLine.Split(' ', 2);

        return new LogEntry
        {
            Level = parts[0],
            Message = parts.Length > 1 ? parts[1] : ""
        };
    }

    public int CountErrors(string[] logs)
    {
        int count = 0;

        foreach (var log in logs)
        {
            var entry = ParseLog(log);
            if (entry.Level == "ERROR")
            {
                count++;
            }
        }

        return count;
    }

    public Dictionary<string, int> CountByLevel(string[] logs)
    {
        var counts = new Dictionary<string, int>();

        foreach (var log in logs)
        {
            var entry = ParseLog(log);

            if (!counts.ContainsKey(entry.Level))
                counts[entry.Level] = 0;

            counts[entry.Level]++;
        }

        return counts;
    }
}