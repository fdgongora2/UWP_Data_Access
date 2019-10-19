using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Data_Access_REST.Models
{
    public class Tmb
    {
        public string id { get; set; }
        public string street_name { get; set; }
        public string city { get; set; }
        public string utm_x { get; set; }
        public string utm_y { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string furniture { get; set; }
        public string buses { get; set; }
    }

    public class Data
    {
        public List<Tmb> tmbs { get; set; }
    }

    public class RutasBus
    {
       
        public int code { get; set; }
        public Data data { get; set; }

      
    }

    public class GestorRutas
    {
        private static string Url = "http://barcelonaapi.marcpous.com/bus/stations.json";
        public static async Task<RutasBus> GetAllBusRoutesAsync()
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync(Url);
                return JsonConvert.DeserializeObject<RutasBus>(content);
            }
        }
    }

    
}
