using System;
using System.Net.Http;
using System.Threading;

namespace Lamazon.HealthPerformanceCheckApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string appBaseUrl = "http://localhost:62019";

            Console.WriteLine("Choose '1' for Health check or '2' for performance check!");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                while (true)
                {
                    string healthUri = $"{appBaseUrl}/HealthPerformance/HealthCheck";

                    Console.WriteLine(
                        GetHealthPerformanceInfo(healthUri)
                    );

                    Thread.Sleep(1000);
                }
            }
            else if (answer == "2")
            {
                while (true)
                {
                    string performanceUri = $"{appBaseUrl}/HealthPerformance/PerformanceCheck";

                    Console.WriteLine(
                        GetHealthPerformanceInfo(performanceUri)
                    );

                    Thread.Sleep(1000);
                }
            }
        }

        private static string GetHealthPerformanceInfo(string uri)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage res = client.GetAsync(uri).Result;

            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
