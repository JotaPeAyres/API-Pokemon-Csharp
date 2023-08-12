using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var request = new RestRequest("pokemon/", Method.Get);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                Console.WriteLine(response.Content);
            else
                Console.WriteLine(response.ErrorMessage);

            Console.ReadKey();
        }
    }
}
