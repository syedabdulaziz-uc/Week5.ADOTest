using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Week5.ADOTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text;
            string nameOnCard = txtNameOnCard.Text;
            string cardType = txtCardType.Text;
            string cvvNumber = txtCVVNumber.Text;
            DateTime expiryDate = Convert.ToDateTime(txtExpiryDate.Text);
            decimal balance = Convert.ToDecimal(txtBalance.Text);

            // Create a connection to the database
            using (SqlConnection conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["QuickKartCon"].ConnectionString))
            {
                conObj.Open();
                SqlCommand cmdObj = new SqlCommand("usp_CreateNewCardDetails", conObj);
                cmdObj.CommandType = CommandType.StoredProcedure;

                // input parameters
                cmdObj.Parameters.AddWithValue("@CardNumber", cardNumber);
                cmdObj.Parameters.AddWithValue("@NameOnCard", nameOnCard);
                cmdObj.Parameters.AddWithValue("@CardType", cardType);
                cmdObj.Parameters.AddWithValue("@CVVNumber", cvvNumber);
                cmdObj.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                cmdObj.Parameters.AddWithValue("@Balance", balance);

                // output parameter
                SqlParameter reslt = new SqlParameter("@Result", SqlDbType.Int);
                reslt.Direction = ParameterDirection.Output;
                cmdObj.Parameters.Add(reslt);

                // Execute procedure
                cmdObj.ExecuteNonQuery();

                int result = (int)reslt.Value;

                if (result >0)
                {
                    lblResult.Text = "Card details added successfully. Card number: " + cardNumber;
                }
                else
                {
                    lblResult.Text = "Error adding card details. Error code: " + result;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
