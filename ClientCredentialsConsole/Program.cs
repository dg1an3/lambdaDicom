using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientCredentialsConsole
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Console Client Credentials Flow";

            var response = RequestTokenAsync().Result;
            response.Show();
            Console.ReadLine();

            CallServiceAsync(response.AccessToken).Wait();
            Console.ReadLine();
        }

        static async Task<TokenResponse> RequestTokenAsync()
        {
            var disco = await DiscoveryClient.GetAsync(Constants.Authority);
            if (disco.IsError) throw new Exception(disco.Error);

            var client = new TokenClient(
                disco.TokenEndpoint,
                "client",
                "secret");

            return await client.RequestClientCredentialsAsync("api1");
        }

        static async Task CallServiceAsync(string token)
        {
            var baseAddress = Constants.SampleApi;

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var response = await client.GetStringAsync("what/is/this");

            "\n\nService claims:".ConsoleGreen();
            Console.WriteLine(response);
        }
    }
}
