using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Dialogflow.v2;
using Microsoft.AspNetCore.Mvc;
using test_project.Models;
using test_project.Services;

namespace test_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Hello World");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ChatBotParametrs value)
        {
            var test = value.Utterance;
            using (var client = new HttpClient())
            {
                try
                {
                    var credentials = GoogleCredential.FromFile(@"../test_project/appsettings.json");
                    var scopedCredentials = credentials.CreateScoped(DialogflowService.Scope.CloudPlatform);
                    var _oAuthToken = scopedCredentials.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;
                    client.BaseAddress = new Uri("https://dialogflow.googleapis.com");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_oAuthToken}");
                    var content = new StringContent("{\"queryInput\":{\"text\":{\"text\":\"" + test + "\",\"languageCode\":\"en\"}},\"queryParams\":{\"timeZone\":\"Europe/Kiev\"}}", Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("/v2/projects/chatbothelper-aqjiii/agent/sessions/5450f4f7-3656-b245-9124-2c1364d11cea:detectIntent", content);
                    string resultContent = await result.Content.ReadAsStringAsync();

                    return Ok(resultContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
