using Fortnite_API;
using ConsumirApiOrd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumirApiOrd
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var lcosmetics = new List<Cosmetics>();
            //Inicializa el cliente
            var apiKey = string.Empty;
            var api = new FortniteApiClient(apiKey);
            Console.WriteLine("ola");
            try
            {
                // Obtén los cosméticos nuevos
                var newCosmeticsResponse = await api.V2.Cosmetics.GetBrNewAsync();

                // Asegúrate de que la respuesta y los datos sean válidos
                if (newCosmeticsResponse == null || newCosmeticsResponse.Data == null)
                {
                    Console.WriteLine("No se encontraron cosméticos nuevos.");
                    return;
                }

                // Itera sobre los cosméticos y mapea las propiedades
                foreach (var cosmeticItem in newCosmeticsResponse.Data.Items)
                {
                    var cosmetic = new Cosmetics
                    {
                        id = cosmeticItem.Id,
                        name = cosmeticItem.Name,
                        description = cosmeticItem.Description,
                        //rarity = cosmeticItem.Rarity?.Value, // Puede ser nulo
                        //image = cosmeticItem.Images?.Icon   // Puede ser nulo
                    };

                    lcosmetics.Add(cosmetic);

                    // Muestra en consola
                    Console.WriteLine($"ID: {cosmetic.id}");
                    Console.WriteLine($"Name: {cosmetic.name}");
                    Console.WriteLine($"Description: {cosmetic.description}");
                    //Console.WriteLine($"Rarity: {cosmetic.rarity}");
                    //Console.WriteLine($"Image URL: {cosmetic.image}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
