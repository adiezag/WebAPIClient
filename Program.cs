// See https://aka.ms/new-console-template for more information
// Activity # 5
// Calling an API
// Topic: GOT Houses
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

    

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a number to display the info of the Game of Thrones houses. Press Enter without a name to quit the program.");

                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://www.anapioficeandfire.com/api/houses/" + input);
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var gameofThrones = JsonConvert.DeserializeObject<Got>(resultRead);

                    Console.WriteLine("House in Game of Thrones ");
                    Console.WriteLine("Name: " + gameofThrones.Name);
                    Console.WriteLine("Region: " + gameofThrones.Region);
                    Console.WriteLine("Coat of Arms: " + gameofThrones.CoatOfArms);

                }

                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please, enter a valid number!");
                }

            }
        }
    }

    class Got
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("coatofArms")]
        public string CoatOfArms { get; set; }

    }
}
