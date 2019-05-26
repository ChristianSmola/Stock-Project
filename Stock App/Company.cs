using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Microsoft.ML.Trainers;
using Microsoft.ML.EntryPoints;
using Microsoft.ML.Model;
using Microsoft.ML.Transforms;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Data.DataView;
using LiveCharts.Wpf;
using LiveCharts;

namespace Stock_App
{
    public partial class Company : Form
    {
        public string Ticker;

        public class DataPoint
        {
            public double price;
            public float PercentChanged;
            public DateTime date;
        }

        public class CompanyInfo
        {
            public string Name;
            public string Symbol;
            public Form1.Industry Industry;
            public ObservableCollection<DataPoint> DataPointCollection = new ObservableCollection<DataPoint>();
        }

        public class MLInput
        {
            
            public string Name { get; set; }
            [VectorType(14), ColumnName("Features")]
            public float[] Features { get; set; }
            [VectorType(14)]
            public DateTime[] dates { get; set; }
        }

        public class MLOutput
        {
            public string Name { get; set; }
            [VectorType(14), ColumnName("Features")]
            public float[] Features { get; set; }
            [VectorType(14)]
            public string[] dates { get; set; }
        }

        ObservableCollection<CompanyInfo> CompanyInfoCollection = new ObservableCollection<CompanyInfo>();
        ObservableCollection<CompanyInfo> IndustryCompetition = new ObservableCollection<CompanyInfo>();

        public Company(string xTicker)
        {
            InitializeComponent();
            Ticker = xTicker;

            this.Text = Ticker;
        }

        private void Company_Load(object sender, EventArgs e)
        {
            GrowthVsIndustry();
        }

        private void GrowthVsIndustry()
        {
            try
            {
                string strSQL = "Select Industry From Companies Where Symbol ='" + Ticker + "'";
                MySqlCommand cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                object industryOBJ = cmd.ExecuteScalar();

                strSQL = "Select Symbol, Name, Industry From Companies Where Industry ='" + (string)industryOBJ + "'";
                cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyInfo info = new CompanyInfo();
                    info.Symbol = reader.GetString(0);
                    info.Name = reader.GetString(1);

                    foreach (Form1.Industry industry in Enum.GetValues(typeof(Form1.Industry)))
                    {
                        if (industry.ToString() == reader.GetString(2))
                        {
                            info.Industry = industry;
                        }
                    }
                    CompanyInfoCollection.Add(info);
                }
                reader.Close();

                strSQL = "Select ";

                for (int x = 0; x < CompanyInfoCollection.Count - 2; x++)
                {
                    strSQL += CompanyInfoCollection[x].Symbol + ", ";
                }

                strSQL += CompanyInfoCollection[CompanyInfoCollection.Count - 1].Symbol + ", DateSubmitted From StockTable2019";
                cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                reader = cmd.ExecuteReader();

                int index = 0;
                while (reader.Read())
                {
                    for (int y = 0; y < CompanyInfoCollection.Count - 1; y++)
                    {
                        DataPoint point = new DataPoint();
                        point.price = reader.GetDouble(y);
                        point.date = reader.GetDateTime(CompanyInfoCollection.Count - 1);
                        if (CompanyInfoCollection[y].DataPointCollection.Count > 0)
                            point.PercentChanged = ((float)point.price / (float)CompanyInfoCollection[y].DataPointCollection[CompanyInfoCollection[y].DataPointCollection.Count - 1].price - 1) * 100;
                        CompanyInfoCollection[y].DataPointCollection.Add(point);
                        index++;
                    } 
                }
                reader.Close();

                UpdateGrowthVsIndustryChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int MainFocusIndex = 0;
        void UpdateGrowthVsIndustryChart()
        {
            List<string> xAxis = new List<string>();
            ChartValues<double> cv = new ChartValues<double> { };
            List<ChartValues<double>> cvList = new List<LiveCharts.ChartValues<double>> { };
            

            for (int z = 0; z < CompanyInfoCollection.Count - 1; z++)
            {
                if (Ticker == CompanyInfoCollection[z].Symbol)
                {
                    MainFocusIndex = z;
                    continue;
                }

                ChartValues<double> cv1 = new ChartValues<double> { };

                for (int v = 0; v < CompanyInfoCollection[z].DataPointCollection.Count; v++)
                {
                    cv1.Add(CompanyInfoCollection[z].DataPointCollection[v].PercentChanged);
                    IndustryCompetition.Add(CompanyInfoCollection[z]);
                }
                cvList.Add(cv1);
            }

            

            for (int x = 0; x < CompanyInfoCollection[MainFocusIndex].DataPointCollection.Count; x++)
            {
                xAxis.Add(CompanyInfoCollection[MainFocusIndex].DataPointCollection[x].date.ToString("MM/dd"));
                cv.Add(CompanyInfoCollection[MainFocusIndex].DataPointCollection[x].PercentChanged);
            }

            CartChartGrowthVsIndustry.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = CompanyInfoCollection[MainFocusIndex].Name, Values = cv
                }
            };

            for (int c = 0; c < cvList.Count; c++)
            {
                if (c < MainFocusIndex) {
                    CartChartGrowthVsIndustry.Series.Add(new LineSeries
                    {
                        Title = CompanyInfoCollection[c].Name,
                        Values = cvList[c],
                        LineSmoothness = 1, 
                    });
                }
                else
                {
                    CartChartGrowthVsIndustry.Series.Add(new LineSeries
                    {
                        Title = CompanyInfoCollection[c + 1].Name,
                        Values = cvList[c],
                        LineSmoothness = 1
                    });
                }
            }

            CartChartGrowthVsIndustry.AxisX.Add(new Axis
            {
                Title = "Dates",
                Labels = xAxis.ToArray(),
                Foreground = System.Windows.Media.Brushes.Black
            });

            CartChartGrowthVsIndustry.AxisY.Add(new Axis
            {
                Title = "Weekly Percent Change",
                LabelFormatter = value => String.Format("{0:P2}%", value.ToString()),
                Foreground = System.Windows.Media.Brushes.Black
            });

            CartChartGrowthVsIndustry.LegendLocation = LegendLocation.Right;
        }

        void MLtest()
        {
            try
            {
                MLContext mlContext = new MLContext();

                ObservableCollection<MLInput> input = new ObservableCollection<MLInput>();

                for (int y = 0; y < IndustryCompetition.Count; y++)
                {
                    MLInput mL = new MLInput();
                    List<float> floatlist = new List<float>();
                    List<DateTime> datelist = new List<DateTime>();
                    for (int x = 0; x < IndustryCompetition[y].DataPointCollection.Count; x++)
                    {
                        floatlist.Add(IndustryCompetition[y].DataPointCollection[x].PercentChanged);
                        datelist.Add(IndustryCompetition[y].DataPointCollection[x].date);
                    }
                    mL.Features = floatlist.ToArray();
                    mL.dates = datelist.ToArray();
                    mL.Name = IndustryCompetition[y].Name;
                    input.Add(mL);
                }

                var trainData = mlContext.Data.LoadFromEnumerable(input);

                var pipeline = mlContext.Transforms.Conversion.MapValueToKey(nameof(MLInput.Name))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: nameof(MLOutput.dates), inputColumnName: nameof(MLInput.dates), outputKind: OneHotEncodingTransformer.OutputKind.Key))
                    .Append(mlContext.Transforms.Concatenate("Features", nameof(MLInput.Features)/*, nameof(MLInput.dates)*/))
                    .Append(mlContext.MulticlassClassification.Trainers.LogisticRegression(labelColumnName: nameof(MLInput.Name), featureColumnName: "Features"))
                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(nameof(MLInput.Name)))
                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(nameof(MLOutput.dates)));



                var model = pipeline.Fit(trainData);

                MLInput mL1 = new MLInput();
                List<float> floatlist1 = new List<float>();
                List<DateTime> datelist1 = new List<DateTime>();
                for (int z = 0; z < CompanyInfoCollection[0].DataPointCollection.Count; z++)
                {
                    floatlist1.Add(CompanyInfoCollection[MainFocusIndex].DataPointCollection[z].PercentChanged);
                    datelist1.Add(CompanyInfoCollection[MainFocusIndex].DataPointCollection[z].date);
                }
                mL1.Features = floatlist1.ToArray();
                mL1.dates = datelist1.ToArray();
                //mL1.Industry = CompanyInfoCollection[MainFocusIndex].Industry.ToString();
                //mL1.Symbol = CompanyInfoCollection[MainFocusIndex].Symbol;
                mL1.Name = CompanyInfoCollection[MainFocusIndex].Name;

                
                //okay something seems to be wrong with this statement here it just spits back out the info that i pass it
                var prediction = model.CreatePredictionEngine<MLInput, MLOutput>(mlContext).Predict(mL1);

                MessageBox.Show("MLtest prediction: " + prediction.dates[11] + " , " + prediction.Features[11]);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMLTest_Click(object sender, EventArgs e)
        {
            MLtest();
        }
    }
}
