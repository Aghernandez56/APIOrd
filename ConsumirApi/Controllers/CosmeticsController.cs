using ConsumirApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumirApi.Controllers
{
    internal class CosmeticsController
    {
        static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7056/")
        };

        List<Cosmetics> listacos = new List<Cosmetics>();

        public async Task<List<Cosmetics>> ObtenerCosmeticsAsync()
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/Cosmetics");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var cos = JsonConvert.DeserializeObject<List<Cosmetics>>(jsonString);
                    return cos;
                }
                else
                {
                    throw new Exception($"Error al consumir la API: {response.ReasonPhrase}");
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
