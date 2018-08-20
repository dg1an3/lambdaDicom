using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace rest_client
{
    // From https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var repositories = ProcessRepositories().Result;
            foreach (var repo in repositories)
                Console.WriteLine($"{repo.Name}: {repo.Homepage} ({repo.LastPush})");
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var resultTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));
            var repositories = serializer.ReadObject(await resultTask) as List<Repository>;
            return repositories;
        }
    }
}
