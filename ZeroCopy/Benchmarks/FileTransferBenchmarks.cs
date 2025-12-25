using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace Benchmarks;

public static class FileTransferBenchmarks
{
    public static void RunFileTransferBenchmarks()
    {
        var httpClient = new HttpClient();

        var normalScenario = Scenario.Create("normal_file_download", async context =>
            {
                var request = Http.CreateRequest("GET", "http://localhost:5077/api/download/file/normal");
                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10,
                    during: TimeSpan.FromSeconds(60)) // 10 concurrent users for 60 seconds
            );

        var improvedScenario = Scenario.Create("improved_file_download", async context =>
            {
                var request = Http.CreateRequest("GET", "http://localhost:5077/api/download/file/improved");
                var response = await Http.Send(httpClient, request);
                return response;
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10,
                    during: TimeSpan.FromSeconds(60)) // 10 concurrent users for 60 seconds
            );

        NBomberRunner
            .RegisterScenarios(normalScenario, improvedScenario)
            .Run();
    }
}