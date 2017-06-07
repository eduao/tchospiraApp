using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TchospiraApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(TchospiraApp.Services.TchospiraService))]
namespace TchospiraApp.Services
{
    public class TchospiraService : ITchospiraService
    {
        private const string BaseUrl = "https://tchospira.azurewebsites.net/Tables/";

        public async Task<List<Tag>> GetTagsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}tag").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Tag>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        public async Task<List<New>> GetNewsByTagIdAsync(string tagId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}new/tag?tag={tagId}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<New>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        public async Task<List<New>> GetNewsByFilterAsync(string filter)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}new/search?filter={Uri.EscapeUriString(filter)}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<New>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }
    }
}