// See https://aka.ms/new-console-template for more information
// Activity # 5
// Calling an API
// Topic: Anime films
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using MyApp.WebAPIClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
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
                    Console.WriteLine("Enter film name. Press Enter without writing a film name to quit the program.");

                    var characterNumber = Console.ReadLine();
                    
                    
                    
                    if (string.IsNullOrEmpty(characterNumber))
                    {
                        break;
                    }
                    
                 

                    var result = await client.GetAsync("https://www.anapioficeandfire.com/api/characters/" + characterNumber);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    //*****xxxxxxxxx********

                    var got = JsonConvert.DeserializeObject<Got>(resultRead);
                    Console.WriteLine("---");
                    Console.WriteLine("Title: " + got.Gender);
                    Console.WriteLine("Producer: " + got.Culture);
                    Console.WriteLine("Score: " + got.Aliases);
                    //Console.WriteLine("Height: " + pokémon.Height);
                    Console.WriteLine("Type(s):");
                    got.Types.ForEach(t => Console.Write(" " + t.Type.Aliases));
                    Console.WriteLine("\n---");

                }

                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid character number!");
                }
            }
        }
    }

        namespace WebAPIClient
    {
        class Got
        {
            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("culture")]
            public string Culture { get; set; }
            
            [JsonProperty("aliases")]
            public string Aliases {get; set; }

            //[JsonProperty("height")]
            //public string Height { get; set; }

            public List<Types> Types { get; set; }
        }

        public class Type
        {
            [JsonProperty("aliases")]
            public string Aliases { get; set; }

        }

        public class Types
        {
            [JsonProperty("type")]
            public Type Type;
        }

    }

}
