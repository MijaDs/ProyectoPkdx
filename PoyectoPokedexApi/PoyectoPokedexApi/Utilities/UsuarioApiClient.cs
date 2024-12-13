using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PoyectoPokedexApi.Models.UsuarioModel;
using PoyectoPokedexApi.Pages.Pokemon;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;

namespace PoyectoPokedexApi.Utilities
{
    public class UsuarioApiClient
    {
        private readonly HttpClient _httpClient;

        public UsuarioApiClient(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }

        public static async Task EjecutarTask()
        {
            // URL de la API
            string url = "https://localhost:7068/Api_Pdx_DbV2/Conexion";

            try
            {
                // Crear instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Configurar el encabezado
                    client.DefaultRequestHeaders.Add("Accept", "*/*");

                    // Llamar a la API
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el cuerpo de la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Respuesta de la API: {responseBody}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al llamar a la API: {ex.Message}");
            }
        }

        public async Task<LogResponse> LoginUsuario(LogModel login)
        {
            // Serializamos el objeto login en formato JSON
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            // Realizamos la solicitud POST al API de login
            var response = await _httpClient.PostAsync("https://localhost:7068/Api_Pdx_DbV2/Usuario/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Leemos la respuesta de la API
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserializamos la respuesta en un objeto LogResponse
                var result = JsonConvert.DeserializeObject<LogResponse>(responseBody);

                // Retornamos el resultado
                return result;
            }

            return null; // Retorna null si la solicitud no es exitosa
        }

        public async Task<UsuarioModel> Registro(UsuarioModel usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("usuario", usuario);
            if (response.IsSuccessStatusCode)
            {
                // Si la respuesta es exitosa, deserializar la respuesta JSON
                var responseData = await response.Content.ReadFromJsonAsync<UsuarioModel>();
                return responseData;
            }
            throw new Exception($"Error al realizar registro: {response.ReasonPhrase}");
        }


    }
}
