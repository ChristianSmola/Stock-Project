using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Collections.Generic;
using LiveCharts.Wpf;
using LiveCharts;

namespace Stock_App
{
    public partial class Form1 : Form
    {
        public string APIKEY = "L56US0NPMBW02SL1";
        public ObservableCollection<StockInfo> StockCollection = new ObservableCollection<StockInfo>();
        public ObservableCollection<Company> CompanyCollection = new ObservableCollection<Company>();
        public ObservableCollection<PastStockData> OldDataCollection = new ObservableCollection<PastStockData>();
        public ObservableCollection<StockTrend> CompanyPercentChangeWeekly = new ObservableCollection<StockTrend>();
        public List<DateTime> OldDataDateUploadedList = new List<DateTime>();
        public static MySqlConnection sqlConn;

        public enum Industry
        {
            Automotive,
            Cosmetics,
            Food,
            Pharma,
            Tech,
            Weapons
        }

        public enum PriorityLevel
        {
            Owned,
            Tracked,
            PreviouslyTracked,
            RecentlyViewed,
            Suggestions,
        }

        public class StockTrend
        {
            public string CompanyTicker;
            public Industry industry; 
            public ObservableCollection<DataPoint> dataPointCollection = new ObservableCollection<DataPoint>();

        }

        public class DataPoint
        {
            public float PercentChanged;
            public DateTime date;
        }

        public class StockPrice
        {
            public DateTime date;
            public double price;
        }

        public class PastStockData
        {
            public string CompanyTicker;
            public ObservableCollection<StockPrice> StockPriceCollection = new ObservableCollection<StockPrice>();
        }

        public class StockInfo
        {
            public string Symbol;
            public string Name;
            public Industry industry;
            public float Price;
            public float DifferenceSinceLastEntry;
        }

        public class Company
        {
            public string Name;
            public string Symbol;
            public Industry industry;
            public string Summary;
            public DateTime DateAdded;
            public string Notes;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
            GetDataFromSQL();
            RetrieveAndParseDataFromAPI();
            DisplayDataToUser();
            AnalyzeChangeOverTime();
        }

        public void Initialize() 
        {
            try
            {
                sqlConn = new MySqlConnection("Server = 45.18.120.169; Port = 3306; Database = stockproject; Uid = Watchtower; Pwd = Jasper3221;");
                sqlConn.Open();
                Console.WriteLine("Connected to SQL");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void GetDataFromSQL()
        {
            try
            {
                string strSQL = "Select ifnull(Name, '') as Name, ifnull(Symbol, '') as Symbol, ifnull(Industry, '') as Industry, ifnull(Summary, '') as Summary, ifnull(DateAdded, '') as DateAdded, ifnull(Notes, '') as Notes"/*, isnull(DividendYield, 0) as DividendYield*/ + " From Companies";
                MySqlCommand cmd = new MySqlCommand(strSQL, sqlConn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Company xcompany = new Company();
                    xcompany.Name = reader.GetString(0);
                    xcompany.Symbol = reader.GetString(1);
                    foreach (Industry industry in Enum.GetValues(typeof(Industry)))
                    {
                        if (industry.ToString() == reader.GetString(2))
                        {
                            xcompany.industry = industry;
                        }
                    }
                    xcompany.Summary = reader.GetString(3);
                    xcompany.DateAdded = reader.GetDateTime(4);
                    xcompany.Notes = reader.GetString(5);
                    CompanyCollection.Add(xcompany);
                }
                
                reader.Close();

                strSQL = "Select DateSubmitted, ";
                for (int x = 0; x < CompanyCollection.Count; x++)
                {
                    if (x == CompanyCollection.Count - 1)
                        strSQL +=   CompanyCollection[x].Symbol + " From StockTable2019 Order By DateSubmitted desc";
                    else
                        strSQL +=   CompanyCollection[x].Symbol + ", ";
            
                }
                cmd = new MySqlCommand(strSQL, sqlConn);
                reader = cmd.ExecuteReader();

                int readerIndex = 0;

                while (reader.Read()) {

                    if (readerIndex == 0)
                    {
                        //needed to add one to the count to offset for the date column
                        for (int y = 0; y < CompanyCollection.Count + 1; y++)
                        {
                            PastStockData stockData = new PastStockData();

                            if (y == 0)
                            {
                                OldDataDateUploadedList.Add(reader.GetDateTime(0));
                                continue;
                            }

                            if (y != 0)
                            {
                                StockPrice price = new StockPrice();
                                price.price = (double)reader.GetDecimal(y);
                                price.date = OldDataDateUploadedList[readerIndex];
                                stockData.StockPriceCollection.Add(price);
                                stockData.CompanyTicker = CompanyCollection[y - 1].Symbol;
                                OldDataCollection.Add(stockData);
                            }
                        }
                    }
                    else
                    {
                        OldDataDateUploadedList.Add(reader.GetDateTime(0));

                        //needed to add one to the count to offset for the date column
                        for (int z = 0; z < OldDataCollection.Count + 1; z++)
                        {
                            if (z != 0)
                            {
                                StockPrice price = new StockPrice();
                                price.price = reader.GetDouble(z);
                                price.date = OldDataDateUploadedList[readerIndex];
                                OldDataCollection[z - 1].StockPriceCollection.Add(price);
                            }
                        }
                    }
                    readerIndex++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void RetrieveAndParseDataFromAPI()
        {
            try
            {
                string SymbolsForWeb= "";

                for (int x = 0; x < CompanyCollection.Count; x++)
                {
                    if (x == CompanyCollection.Count - 1)
                    {
                        SymbolsForWeb += CompanyCollection[x].Symbol;
                    }
                    else
                    {
                        SymbolsForWeb += CompanyCollection[x].Symbol + ",";
                    }
                }

                WebRequest request = WebRequest.Create("https://www.alphavantage.co/query?function=BATCH_STOCK_QUOTES&symbols=" + SymbolsForWeb + "&apikey=" + APIKEY);
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.ASCII);

                StockInfo TempStockInfo = new StockInfo();

                while(reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine().Trim();
                    
                    if (currentLine.Contains(@"symbol"":"))
                    {
                        string tempString = currentLine.Substring(currentLine.IndexOf(":") + 3);
                        tempString = tempString.Remove(tempString.LastIndexOf(@""""));
                        TempStockInfo.Symbol = tempString;
                    }
                    if (currentLine.Contains(@"price"":"))
                    {
                        string tempString = currentLine.Substring(currentLine.IndexOf(":") + 3);
                        tempString = tempString.Remove(tempString.LastIndexOf(@""""));
                        TempStockInfo.Price = float.Parse(tempString);
                    }
                    if (TempStockInfo.Price != 0.0f && TempStockInfo.Symbol != "" && TempStockInfo.Symbol != null)
                    {
                        StockCollection.Add(TempStockInfo);
                        TempStockInfo = new StockInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void DisplayDataToUser()
        {
            try
            {
                //Adds today's data to the graph
                dataGridView1.Columns.Add("colDate" + string.Format("{0:d}", DateTime.Today), string.Format("{0:d}", DateTime.Today));
                for (int x = 0; x < StockCollection.Count; x++)
                {
                    dataGridView1.Rows.Add(StockCollection[x].Symbol, StockCollection[x].Price);
                }
                
                //Adds additional columns
                for (int y = 0; y < OldDataDateUploadedList.Count; y++)
                {
                    dataGridView1.Columns.Add("colDate" + string.Format("{0:d}", OldDataDateUploadedList[y]), string.Format("{0:d}", OldDataDateUploadedList[y]));
                    PropertyInfo[] properties = typeof(PastStockData).GetProperties();
                }

                for (int w = 0; w < OldDataDateUploadedList.Count; w++)
                {
                    for (int z = 0; z < OldDataCollection.Count; z++)
                    {
                        if (OldDataCollection[z].StockPriceCollection.Count > w)
                        {
                            dataGridView1.Rows[z].Cells[w + 2].Value = OldDataCollection[z].StockPriceCollection[w].price;
                        }
                        else
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void AnalyzeChangeOverTime()
        {
            try
            {
                for (int x = 0; x < dataGridView1.RowCount; x++)
                {
                    ObservableCollection<DataPoint> dataPointCollection = new ObservableCollection<DataPoint>();

                    for (int y = 1; y < dataGridView1.Rows[x].Cells.Count - 1; y++)
                    {
                        float diff = 0.0f;
                        try
                        {
                            diff = ((float.Parse(dataGridView1.Rows[x].Cells[y].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture.NumberFormat) / float.Parse(dataGridView1.Rows[x].Cells[y + 1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture.NumberFormat)) - 1.0f) * 100;
                        }
                        catch
                        {
                            continue;
                        }
                        DataPoint point = new DataPoint();
                        point.PercentChanged = diff;
                        point.date = Convert.ToDateTime(dataGridView1.Columns[y].HeaderText);
                        dataPointCollection.Add(point);

                        if (diff < 2f && diff > -2f)
                        {
                            //Neutral change between 2% and -2%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Beige;
                        }
                        else if (diff >= 2f && diff < 6f)
                        {
                            //Slight Growth between 2% and 6%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.LightGreen;
                        }
                        else if (diff >= 6f && diff < 10f)
                        {
                            //Solid Growth between 6% and 10%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Green;
                        }
                        else if (diff >= 10f)
                        {
                            //Impressive Growth above 10%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.DarkGreen;
                        }
                        else if (diff <= -2f && diff > -6f)
                        {
                            //Slight Decay between -2% and -6%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Yellow;
                        }
                        
                        else if (diff <= -6f && diff > -10f)
                        {
                            //Solid Decay between -6% and -10%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Orange;
                        }
                        else if (diff <= -10f)
                        {
                            //Impressive Decay lower than -10%
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            //Other
                            dataGridView1.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Purple;
                            Console.WriteLine(diff + " Row:" + x + " & Cell:" + y);
                        }
                    }

                    StockTrend stock = new StockTrend();

                    stock.CompanyTicker = dataGridView1.Rows[x].Cells[0].Value.ToString();
                    stock.dataPointCollection = dataPointCollection;

                    for (int v = 0; v < CompanyCollection.Count - 1; v++)
                    {
                        if (stock.CompanyTicker == CompanyCollection[v].Symbol)
                        {
                            stock.industry = CompanyCollection[v].industry;
                            break;
                        }
                    }

                    CompanyPercentChangeWeekly.Add(stock);
                }

                UpdateCartChartWeeklyGrowth();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void UpdateCartChartWeeklyGrowth()
        {
            try
            {
                string topstock = "";
                float topAverageChange = 0.0f;
                int topstockIndex = 0;

                for (int z = 0; z < CompanyPercentChangeWeekly.Count - 1; z++)
                {
                    float CurrentAverageChange = 0.0f;

                    for (int w = 0; w < CompanyPercentChangeWeekly[z].dataPointCollection.Count - 1; w++)
                    {
                        CurrentAverageChange += CompanyPercentChangeWeekly[z].dataPointCollection[w].PercentChanged;
                    }
                    CurrentAverageChange = CurrentAverageChange / (CompanyPercentChangeWeekly[z].dataPointCollection.Count - 1);

                    if (CurrentAverageChange > topAverageChange)
                    {
                        topAverageChange = CurrentAverageChange;
                        topstock = CompanyPercentChangeWeekly[z].CompanyTicker;
                        topstockIndex = z;
                    }
                }

                List<string> CompletelyPointlessList = new List<string>();

                for (int u = OldDataDateUploadedList.Count; u > 0; u--)
                {
                    CompletelyPointlessList.Add(OldDataDateUploadedList[u - 1].ToString("MM/dd"));
                }
                
                ChartValues<double> cv = new ChartValues<double> {  };

                for (int q = CompanyPercentChangeWeekly[topstockIndex].dataPointCollection.Count; q > 0 ; q--)
                {
                    cv.Add(CompanyPercentChangeWeekly[topstockIndex].dataPointCollection[q - 1].PercentChanged);
                }

                List<ChartValues<double>> cvList = new List<LiveCharts.ChartValues<double>> { };
                List<int> IndustryIndexList = new List<int>();

                for (int u = 0; u < CompanyPercentChangeWeekly.Count; u++)
                {
                    if (CompanyPercentChangeWeekly[topstockIndex].industry == CompanyPercentChangeWeekly[u].industry)
                    {
                        ChartValues<double> cv1 = new ChartValues<double> { };
                        for (int v = CompanyPercentChangeWeekly[u].dataPointCollection.Count; v > 0; v--)
                        {
                            cv1.Add(CompanyPercentChangeWeekly[u].dataPointCollection[v - 1].PercentChanged);
                        }
                        IndustryIndexList.Add(u);
                        cvList.Add(cv1);
                    }
                }
 
                
                CartChartGrowthChange.Series = new SeriesCollection {
                    new LineSeries {
                        Title = CompanyPercentChangeWeekly[topstockIndex].CompanyTicker,
                        Values = cv, LineSmoothness = 1
                    },

                };

                for (int a = 0; a < cvList.Count; a++)
                {
                    CartChartGrowthChange.Series.Add(new LineSeries
                    {
                        Title = CompanyPercentChangeWeekly[IndustryIndexList[a]].CompanyTicker,
                        Values = cvList[a],
                        LineSmoothness = 1
                    });
                }

                CartChartGrowthChange.AxisX.Add(new Axis
                {
                    Title = "Dates",
                    Labels = CompletelyPointlessList.ToArray(),
                    Foreground = System.Windows.Media.Brushes.Black
                });

                CartChartGrowthChange.AxisY.Add(new Axis
                {
                    Title = "Weekly Percent Change",
                    LabelFormatter = value => String.Format("{0:P2}%", value.ToString()),
                    Foreground = System.Windows.Media.Brushes.Black
                }); 

                CartChartGrowthChange.LegendLocation = LegendLocation.Right;
            }
            catch
            {

            }
        }

        void SaveDataToSQL()
        {
            try
            {
                MySqlCommand cmd;
                string strSQL = "Insert Into StockTable2019(DateSubmitted, ";

                
                for(int x = 0; x < CompanyCollection.Count; x++)
                {
                    if (x == CompanyCollection.Count - 1)
                    {
                        strSQL += CompanyCollection[x].Symbol + ")";
                    }
                    else
                    {
                        strSQL += CompanyCollection[x].Symbol + ", ";
                    }
                }

                strSQL += " Values(CurDate(),'";
                for (int p = 0; p < StockCollection.Count; p++)
                {
                    if (p == StockCollection.Count - 1)
                    {
                        strSQL = String.Concat(strSQL, StockCollection[p].Price.ToString() + "')");
                    }
                    else
                    {
                        strSQL = String.Concat(strSQL, StockCollection[p].Price.ToString() + "','");
                    }
                }
                cmd = new MySqlCommand(strSQL, sqlConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public string ToNumber(string strValue)
        {
            int intIndex;
            string strTemp = "";
            if (strValue == null)
                strValue = "";

            for (intIndex = 0; intIndex <= strValue.Length - 1; intIndex++)
                strTemp += Char.IsDigit(Convert.ToChar(strValue.Substring(intIndex, 1))) ? strValue.Substring(intIndex, 1) : "";
            return strTemp;
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            try
            {
                AddCompany addCompany = new AddCompany();
                addCompany.StartPosition = FormStartPosition.CenterParent;
                addCompany.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditCompany_Click(object sender, EventArgs e)
        {
            try
            {
                EditCompany editCompany = new EditCompany("MSFT");
                editCompany.StartPosition = FormStartPosition.CenterParent;
                editCompany.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveTodaysDataToServer_Click(object sender, EventArgs e)
        {
            SaveDataToSQL();
        }

        private void btnUpdateDividendData_Click(object sender, EventArgs e)
        {
            UpdateDividendData();
        }

        private void UpdateDividendData()
        {
            try
            {
                for (int x = 0; x < CompanyCollection.Count; x++)
                {
                    if (CompanyCollection[x].industry == Industry.Automotive)
                    {
                        WebRequest request = WebRequest.Create("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=" + CompanyCollection[x].Symbol + "&apikey" + APIKEY);
                        WebResponse response = request.GetResponse();
                        System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.ASCII);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheckNews_Click(object sender, EventArgs e)
        {
            try
            {
                NewsSearch news = new NewsSearch();
                news.StartPosition = FormStartPosition.CenterParent;
                news.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Stock_App.Company company = new Stock_App.Company((string)dataGridView1.SelectedRows[0].Cells[0].Value);
                company.StartPosition = FormStartPosition.CenterParent;
                company.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
