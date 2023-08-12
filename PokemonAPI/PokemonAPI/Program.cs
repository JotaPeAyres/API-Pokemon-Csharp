using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI
{
    class Program
    {
        static void Main()
        {
            GetAllPokemon();
        }

        public static void GetAllPokemon()
        {
            var options = new RestClientOptions("https://pokeapi.co/api/v2/");

            var client = new RestClient(options);
            var request = new RestRequest("pokemon/?limit=5", Method.Get);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var results = JsonSerializer.Deserialize<ResponseJson>(response.Content);

                results.results.ForEach(f =>
                {
                    var pokemonRequest = new RestRequest($"pokemon/{f.name}", Method.Get);
                    var pokemonResponse = JsonSerializer.Deserialize<Pokemon>(client.Execute(pokemonRequest).Content);
                    Console.WriteLine("Pokemon name: " + pokemonResponse.name);
                    Console.WriteLine("Height: " + pokemonResponse.height);
                    Console.WriteLine("Weight: " + pokemonResponse.weight);
                    //Console.WriteLine("Abilities: ");
                    //pokemonResponse.abilities.ForEach(a => { Console.WriteLine("\t" + a.name); });
                });
            }

            else
                Console.WriteLine(response.ErrorMessage);

            Console.ReadKey();
        }

        public class ResponseJson
        {
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public List<Result> results { get; set; }

            public class Result
            {
                public string name { get; set; }
                public string url { get; set; }
            }
        }

        public class Pokemon
        {
            public string name { get; set; }
            public int height { get; set; }
            public int weight { get; set; }
            public List<Abilities> abilities { get; set; }
        }

        public class Abilities
        {
            public string name { get; set; }
        }
    }
}
