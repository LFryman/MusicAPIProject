using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace MusicAPIProject.Models
{
    public class MusicDAL
    {
        private readonly string _apikey;

        public MusicDAL(string apiKey)
        {
            _apikey = apiKey;
        }

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deezerdevs-deezer.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", _apikey);
            return client;
        }

        public async Task<List<Artist>> GetArtist()
        {
            var client = GetClient();
            var response = await client.GetAsync($"/search?q=eminem");
            var album = await response.Content.ReadAsAsync<List<Artist>>();
            return album;
        }
    }
}
