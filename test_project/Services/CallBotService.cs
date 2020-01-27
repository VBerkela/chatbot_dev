using System;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace test_project.Services
{
    public class CallBotService
    {
        public static async Task<string> MakeRequest(string utterance)
        { 
            string endpoint = "westus.api.cognitive.microsoft.com";
            string appId = "ee93fff7-0001-407d-a466-ac403fe241f3";
            string key = "d7fe8d16a7a7438891925689b5390013";

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            queryString["query"] = utterance;

            queryString["verbose"] = "true";
            queryString["show-all-intents"] = "true";
            queryString["staging"] = "false";
            queryString["timezoneOffset"] = "0";

            var endpointUri = String.Format("https://{0}/luis/prediction/v3.0/apps/{1}/slots/production/predict?query={2}", endpoint, appId, queryString);

            var response = await client.GetAsync(endpointUri);

            //var strResponseContent = await response.Content.ReadAsStringAsync();

            // Display the JSON result from LUIS

            return await response.Content.ReadAsStringAsync(); ;
        }
    }
}
