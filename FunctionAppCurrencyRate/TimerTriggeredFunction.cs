using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppCurrencyRate;

public class TimerTriggeredFunction
{
    private static readonly HttpClient client = new HttpClient();

    [FunctionName("TimerTriggeredFunction")]
    public async Task Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer, ILogger log)
    {
        try
        {
            log.LogInformation($"Timer triggered at: {DateTime.Now}");

            HttpResponseMessage response = await client.GetAsync("https://testapp1989-hydbctbeewbthab4.westeurope-01.azurewebsites.net/WeatherForecast/unfreeze");
            if (response.IsSuccessStatusCode)
            {
                log.LogInformation("Request succeeded.");
            }
            else
            {
                log.LogError("Request failed.");
            }
        }
        catch (Exception ex) 
        {
            log.LogError($"Error of TimerTriggeredFunction:{ex.Message}"); 
        }
    }
}

