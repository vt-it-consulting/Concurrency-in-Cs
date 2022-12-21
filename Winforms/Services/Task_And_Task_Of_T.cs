using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Services
{
    public class Task_And_Task_Of_T
    {
        private readonly HttpClient httpClient;

        public Task_And_Task_Of_T()
        {
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click(TextBox txtInput)
        {
            await Wait();
            var name = txtInput.Text;
            var greeting = await GetGreeting(name);
            MessageBox.Show(greeting);
        }

        private async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        private async Task<string> GetGreeting(string nombre)
        {
            using (var response = await httpClient.GetAsync($"{Constants.BASE_URI}/greetings/{nombre}"))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}