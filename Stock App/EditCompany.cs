using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Stock_App
{
    public partial class EditCompany : Form
    {
        public string Symbol;

        public enum Industry
        {
            Automotive,
            Cosmetics,
            Food,
            Pharma,
            Tech,
            Weapons
        }

        public EditCompany(string symbol)
        {
            InitializeComponent();
            Symbol = symbol;
        }

        private void EditCompany_Load(object sender, EventArgs e)
        {
            PopulateCBO();
            GetDataFromSQL();
        }

        private void PopulateCBO()
        {
            foreach (Industry industry in Enum.GetValues(typeof(Industry)))
            {
                cboIndustry.Items.Add(industry);
            }
        }
        
        private void GetDataFromSQL()
        {
            try
            {
                string strSQL = "Select isnull(Name, '') as Name, isnull(Symbol, '') as Symbol, isnull(Industry, '') as Industry, isnull(DividendYield, 0) as DividendYield,  isnull(DateAdded, '') as DateAdded, isnull(Summary, '') as Summary, isnull(Notes, '') as Notes From Companies Where Symbol ='" + Symbol + "'";
                MySqlCommand cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txtCompanyName.Text = reader.GetString(0);
                    txtSymbol.Text = reader.GetString(1);
                    for (int x = 0; x < cboIndustry.Items.Count; x++)
                    {
                        cboIndustry.SelectedIndex = x;
                        if (cboIndustry.SelectedItem.ToString() == reader.GetString(2))
                        {
                            break;
                        }
                    }
                    txtDividendYield.Text = reader.GetDouble(3).ToString();
                    txtDateAdded.Text = string.Format("{0:d}", reader.GetDateTime(4));
                    txtSummary.Text = reader.GetString(5);
                    txtNotes.Text = reader.GetString(6);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Add Validation Checks

                //Here I construct the Update statement for SQL
                string strSQL = "Update Companies Set Name ='" + txtCompanyName.Text.Trim().Replace("'", "''") + "', ";
                strSQL += "Symbol = '" + txtSymbol.Text.Trim().Replace("'", "''") + "', ";
                strSQL += "Industry = '" + cboIndustry.SelectedItem.ToString() + "', ";
                strSQL += "Summary = '" + txtSummary.Text.Trim().Replace("'", "''") + "', ";
                strSQL += "Notes = '" + txtNotes.Text.Trim().Replace("'", "''") + "' ";
                strSQL += "Where Symbol = '" + Symbol + "'";
                MySqlCommand cmd = new MySqlCommand(strSQL, Form1.sqlConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
