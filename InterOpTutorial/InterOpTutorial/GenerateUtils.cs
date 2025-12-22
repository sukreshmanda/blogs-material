namespace InterOpTutorial;

public static class GenerateUtils
{
    public static string[] GenerateLogs(int count, Random random)
    {
        var levels = new[] { "ERROR", "WARN_", "INFO_", "DEBUG" };
        var messages = new[]
        {
            "database connection failed",
            "cache miss occurred",
            "user logged in",
            "request processed"
        };
        
        return Enumerable.Range(0, count)
            .Select(_ =>
            {
                var level = levels[random.Next(levels.Length)];
                var message = messages[random.Next(messages.Length)];
                return $"{level} {message}";
            })
            .ToArray();
    }

}