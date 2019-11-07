using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MovieApiOpgave.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace MovieApiOpgave.ApiWork
{
    public class ApiHelper
    {
        
        private string GetApiLink { get; set; } = "http://simonsmoviebooking.azurewebsites.net/api/movie";
        private string PostApiLink { get; set; } = "http://simonsmoviebooking.azurewebsites.net/api/movie";
        private string PutApiLink { get; set; } = "http://simonsmoviebooking.azurewebsites.net/api/movie/BookMovie/1";
        private string DeleteApiLink { get; set; } = "http://simonsmoviebooking.azurewebsites.net/api/movie/1";
        
        
        public List<Movie> GetApiData()
        {
            // initiate datatypes
            string json;
            List<Movie> movies = new List<Movie>();

            //get Data
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(GetApiLink);
            webRequest.Method = "GET";

            
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                 json = reader.ReadToEnd();
            }

            //serialize data
            movies = JsonConvert.DeserializeObject<List<Movie>>(json);

            return movies;
        }

        public List<Movie> GetSpecifikApiData(int id)
        {
            //IEnumerable<List<Movie>> movieWithSpecifikId = new IEnumerable<List<Movie>>();

            List<Movie> movies = GetApiData();
            List<Movie> movieWithSpecifikId = (List<Movie>)movies.Where(x => x.Id == id);
            //movieWithSpecifikId.Add(movie);

            return movieWithSpecifikId;    
        }

        public string PutApiData(int id)
        {
            string json;
            string putApiLink = "http://simonsmoviebooking.azurewebsites.net/api/movie/BookMovie/";
            putApiLink = putApiLink + id;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(putApiLink);
            webRequest.Method = "PUT";

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        public string DeleteApiData(int id)
        {
            string json;
            string deleteApiLink = "http://simonsmoviebooking.azurewebsites.net/api/movie/";
            deleteApiLink = deleteApiLink + id;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(deleteApiLink);
            webRequest.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            using(StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        public string PostApiData(Movie movie)
        {
            //laver det opject du vil poste til en json fil som er en string
            string payLoad = JsonConvert.SerializeObject(movie);
            //URI som er linket til api
            Uri u = new Uri(PostApiLink);
            
            //laver et httpcontext som indeholder dit payload, måden den skal encode det og media type.
            HttpContent c = new StringContent(payLoad, Encoding.UTF8, "application/json");

            //kald af metoden der poster som får både uri og content som parameter
           string response = PostMovieData(u, c).ToString();

            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(PostApiLink);
            //webRequest.Method = "POST";
            return response;
        }

        private async Task<string> PostMovieData(Uri u, HttpContent c)
        {
            string response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);

                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }
            return response;
        }
    }
}
