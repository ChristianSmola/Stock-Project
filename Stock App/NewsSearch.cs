using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Stock_App
{
    public partial class NewsSearch : Form
    {
        private int ArticleIndex = 0;
        public NewsSearch()
        {
            InitializeComponent();
        }

        private ObservableCollection<Articles> ArticleCollection = new ObservableCollection<Articles>();

        public class Articles
        {
            public string author;
            public string title;
            public string url;
            public string content;
        }

        private void NewsSearch_Load(object sender, EventArgs e)
        {
            PopulateCBO();
        }

        private void PopulateCBO()
        {
            try
            {
                string strSQL = "Select Name From Companies";
                MySqlCommand cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cboSearch.Items.Add(reader.GetString(0));
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSearch_LostFocus(object sender, EventArgs e)
        {
            try
            {
                string APIKEY = "9b9eb1243fe449acb9f5343b9f43c509";

                WebRequest request = WebRequest.Create("https://newsapi.org/v2/everything?q=" + cboSearch.SelectedItem + "&" + "sortBy=relevancy&apiKey=" + APIKEY); //from=" + DateTime.Now +
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());

                string[] strArticles = reader.ReadLine().Trim().Split(new string[] {"},{"}, StringSplitOptions.None);

                foreach (string NewsArticle in strArticles)
                {
                    Articles articles = new Articles();
                    if (NewsArticle.Contains("null"))
                    {
                        continue;
                    }

                    if (NewsArticle.Contains(@"author"":"))
                    {
                        string strTemp = NewsArticle.Substring(NewsArticle.IndexOf(@"author"":"));
                        strTemp = strTemp.Substring(strTemp.IndexOf(":") + 2);
                        articles.author = strTemp.Remove(strTemp.IndexOf(@""","));
                    }
                    if (NewsArticle.Contains(@"title"":"))
                    {
                        string strTemp = NewsArticle.Substring(NewsArticle.IndexOf(@"title"":"));
                        strTemp = strTemp.Substring(strTemp.IndexOf(":") + 2);
                        articles.title = strTemp.Remove(strTemp.IndexOf(@""","));
                    }
                    if (NewsArticle.Contains(@"url"":"))
                    {
                        string strTemp = NewsArticle.Substring(NewsArticle.IndexOf(@"url"":"));
                        strTemp = strTemp.Substring(strTemp.IndexOf(":") + 2);
                        articles.url = strTemp.Remove(strTemp.IndexOf(@""","));
                    }
                    if (NewsArticle.Contains(@"content"":"))
                    {
                        string strTemp = NewsArticle.Substring(NewsArticle.IndexOf(@"content"":"));
                        strTemp = strTemp.Substring(strTemp.IndexOf(":") + 2);
                        articles.content = strTemp.Remove(strTemp.IndexOf(@""""));
                    }

                    ArticleCollection.Add(articles);
                }
                ArticleIndex = 0;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateDisplay()
        {
            textBox1.Text = "Author: " + ArticleCollection[ArticleIndex].author + "\r\n";
            textBox1.Text += "Title: " + ArticleCollection[ArticleIndex].title + "\r\n";
            textBox1.Text += "URL: " + ArticleCollection[ArticleIndex].url + "\r\n";
            textBox1.Text += "Content: " + ArticleCollection[ArticleIndex].content + "\r\n";

            if (ArticleIndex == 0)
                btnBack.Enabled = false;
            else
                btnBack.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ArticleIndex += 1;
            UpdateDisplay();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ArticleIndex -= 1;
            UpdateDisplay();
        }
    }
}
