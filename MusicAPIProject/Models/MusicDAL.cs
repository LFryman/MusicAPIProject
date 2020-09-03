using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public string GetClient()
        {

            //Sets up our request 
            HttpWebRequest request = WebRequest.CreateHttp($"https://deezerdevs-deezer.p.rapidapi.com");

            //This sends to the remote server and gets a response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string output = rd.ReadToEnd();

            return output;
        }

        public  Task<List<Artist>> GetArtist()
        {
            string artist = GetClient();

            JObject json = JObject.Parse(artist);
            Artist person = JsonConvert.DeserializeObject<Artist>(json.ToString());
            return GetArtist();
        }    
    }
}
