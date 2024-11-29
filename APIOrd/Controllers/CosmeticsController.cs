using APIOrd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Fortnite_API;
using Fortnite_API.Objects;
using Newtonsoft.Json.Linq;


namespace APIOrd.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CosmeticsController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        public CosmeticsController(HttpClient httpClient) { _httpClient = httpClient; }


        [HttpGet]
        public async Task<IActionResult> GetCosmetics()
        {
            var lcosmetics = new List<Item>();
            try
            {
                var response = await _httpClient.GetAsync("https://fortnite-api.com/v2/cosmetics/new");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al llamar a la API: {response.StatusCode} ({response.ReasonPhrase}). Respuesta: {errorContent}");
                    return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<FortResponse>(jsonString);

                if (apiResponse == null || apiResponse.data.items.Count == 0)
                {
                    Console.WriteLine("No se encontraron cosméticos en la respuesta.");
                    return NotFound("No se encontraron cosméticos nuevos.");
                }

                // Procesar los ítems de la categoría "br"
                var cosmeticsList = apiResponse.data.items.ContainsKey("br")
                    ? apiResponse.data.items["br"]
                    : new List<Item>();

                var result = cosmeticsList.Select(c => new
                {
                    c.id,
                    c.name,
                    c.description,
                    Rarity = c.rarity.displayValue,
                    Image = c.images.icon
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno.");
            }            
        }
    }
}
