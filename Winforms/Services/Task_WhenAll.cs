using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Services;

namespace Winforms.Services
{
    public class Task_WhenAll
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_WhenAll(string apiURL = Constants.BASE_URI)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var cards = await GetCardsAsync(25_000);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int countTask = 0;
            try
            {
                countTask = await ProcessCardsAsync(cards);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds. Total of tasks={countTask}");
        }

        private async Task<int> ProcessCardsAsync(List<string> cards)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            await Task.Run(() =>
              {
                  foreach (var card in cards)
                  {
                      var json = JsonConvert.SerializeObject(card);
                      var content = new StringContent(json, Encoding.UTF8, "application/json");
                      var responseTask = httpClient.PostAsync($"{apiURL}/cards", content);
                      tasks.Add(responseTask);
                  }
              });
            // wait for the execution of all these tasks, before continuing processing
            // further with this method...
            await Task.WhenAll(tasks);
            return tasks.Count;
        }

        private async Task<List<string>> GetCardsAsync(int amountOfCards)
        {
            return await Task.Run(() =>
            {
                var cards = new List<string>();

                for (int i = 0; i < amountOfCards; i++)
                {
                    cards.Add(i.ToString().PadLeft(16, '0'));
                }
                return cards;
            });
        }
    }
}