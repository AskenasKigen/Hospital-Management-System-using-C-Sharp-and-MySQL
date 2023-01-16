using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            SetDefaultWindowSize();
            SetNewSize();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KN1ABBD; Initial Catalog = Hospital; User ID =Kigen; Password=Piajoy@6676;");
        SqlCommand cmd;
        SqlDataReader dr;
      
        string sql;
        bool Mode = true;
        string id;
        private void SetDefaultWindowSize()
        {
            int sizeW, sizeH;
            sizeW = 2300;
            sizeH = 1200;

            var size = new Size(sizeW, sizeH);

            Size = size;
            MinimumSize = size;
        }

        private void SetNewSize()
        {
            Size = new Size(Width, 30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string n = txtn.Text;
            string un = txtun.Text;
            string pass = txtpass.Text;
            string ut = txtut.SelectedItem.ToString();
            
           



            if (Mode == true)
            {
                sql = "Insert into Users (Name, Username, Password, Usertype)values(@Name, @Username,  @Password, @Usertype)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", n);
                cmd.Parameters.AddWithValue("@Username", un);
                cmd.Parameters.AddWithValue("@Password", pass);
                cmd.Parameters.AddWithValue("@Usertype", ut);
              

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered!");


                txtn.Clear();
                txtun.Clear();
                txtpass.Clear();
                txtut.SelectedIndex = -1;
               

            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
