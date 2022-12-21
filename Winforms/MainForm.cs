using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Services;

using Winforms.Services;

using Winforms.Services;

namespace Winforms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;
            // Await means that the UI thread is free to return/go back and
            //// return when the task "Task.Delay()" is finished!
            // await Task.Delay(TimeSpan.FromSeconds(5));
            // await WaitAsync();

            //// Task with result
            //var taskOfT = new Task_And_Task_Of_T();
            //await taskOfT.btnStart_Click(txtInput);

            //// Task with errors
            //var task = new Task_With_Errors();
            //await task.btnStart_Click(txtInput);

            //// WhenAll
            //var task = new Task_WhenAll();
            //await task.btnStart_Click();

            //// SemaphoreExample
            //var semaphore = new SemaphoreExample();
            //await semaphore.btnStart_Click();

            //// Response_Of_Task_WhenAll
            var task = new Response_Of_Task_WhenAll();
            await task.btnStart_Click();

            loadingGIF.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private async Task WaitAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}