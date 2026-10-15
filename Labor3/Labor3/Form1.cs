using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Labor3
{
    public partial class Form1 : Form
    {

        private List<string> blockedKeywords = new List<string>()
        {
            "facebook",
            "instagram",
            "tiktok"
        };
        private async Task<bool> IsBlockedAsync(string url)
        {
            return await Task.Run(() => blockedKeywords.Any(keyword => url.Contains(keyword)));        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com");
        }
        public Form1()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextWriterTraceListener("Events.txt"));

            Trace.AutoFlush = true;
        }
        private async Task LogEventAsync(string message)
        {
            await Task.Run(() =>
            {
                Trace.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")+ " - " + message);
            });
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();

            await LogEventAsync("Home button clicked. Navigated to home page.");
        }

        private async void BackButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();

            await LogEventAsync("Back button clicked. Navigated to previous page.");
        }

        private async void ForwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();

            await LogEventAsync("Forward button clicked. Navigated to next page.");
        }

        private async void NavigateButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStripTextBox1.Text);
            await LogEventAsync("URL navigated via Navigate button.");
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                webBrowser1.Navigate(toolStripTextBox1.Text);
                await LogEventAsync("URL navigated via Enter key.");
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private async void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        { 
            string url =e.Url.ToString();
            /*
            foreach (string keyword in blockedKeywords)
            {
                if (url.Contains(keyword))
                {
                    e.Cancel = true;

                    MessageBox.Show("Az oldal blokkolva van!");

                    return;
                }
            }
            */
            
           //bool blocked = blockedKeywords.Any(keyword => url.Contains(keyword));
           bool blocked = await IsBlockedAsync(url);

            if (blocked)
            {
                e.Cancel = true;
                MessageBox.Show("Az oldal blokkolva van!");
            }
            
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
