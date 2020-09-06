using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.Models;

namespace WordQuiz.API
{
    public class DictionaryAPI
    {
        public async Task<Dictionary> getDictionaryMeaning(string  word)
        {
            string url = $"https://api.dictionaryapi.dev/api/v1/entries/en/" + word;

            using (HttpClient httpClient = new HttpClient())
            {
                Dictionary data = new Dictionary();
                try
                {

                    HttpResponseMessage result = await httpClient.GetAsync(url);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Dictionary>(response.Substring(1, response.Length - 2));

                    if (result.IsSuccessStatusCode && (result.StatusCode == HttpStatusCode.Accepted || result.StatusCode == HttpStatusCode.OK))
                    {
                        return data;
                    }


                }

                catch (WebException exception)
                {
                    return data;
                }
                catch (Exception exp)
                {
                    return data;
                }
            }

            return null;
        }
    }
}
