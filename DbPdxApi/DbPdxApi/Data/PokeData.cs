using DbPdxApi.Models.PokemonModel;
using System.Text.Json;

namespace DbPdxApi.Data
{
    public class PokeData
    {
        private readonly HttpClient _httpClient;
        public PokeData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonModel> GetPokemon(String id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al obtener datos del Pokémon. Código de estado: {response.StatusCode}, Razón: {response.ReasonPhrase}");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PokemonModel>(responseContent)
                  ?? throw new Exception("La respuesta no tiene un formato válido para PokemonModel.");
        }
    }
}
