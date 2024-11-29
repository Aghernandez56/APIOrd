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

                var apiResponse = JsonConvert.DeserializeObject<FortniteApiResponse>(jsonString);

                if (apiResponse== null)
                {
                    Console.WriteLine("No se encontraron cosméticos en la respuesta.");
                    return NotFound("No se encontraron cosméticos nuevos.");
                }

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //return Ok(lcosmetics);
            return Ok(apiResponse.Data.Items.Select(item => new
            {
                item.id,
                item.name,
                item.description,
                Rarity = item.rarity.value,
                Images = item.images.icon
            }));
        }
    }
}
