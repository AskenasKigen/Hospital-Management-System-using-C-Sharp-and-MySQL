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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Usertype();
            SetDefaultWindowSize();
            SetNewSize();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KN1ABBD; Initial Catalog = Hospital; User ID =Kigen; Password=Piajoy@6676;");
        SqlCommand cmd;
        SqlDataReader dr;
        string sql;
      

        public void Usertype()
        {
            sql = "select * from Users";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtut.Items.Add(dr["Usertype"].ToString());
                
            }
            con.Close();
        }
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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlCommand con = new SqlCommand("select Username, Password, Usertype from Users where Username = '" + txtun.Text + "' and Password ='" + txtpass.Text + "' and Usertype = '" +txtut.Text+ "'");
                

                SqlParameter usernameParam;
                usernameParam = new SqlParameter("@Username", this.txtun.Text);
                SqlParameter passwordParam;
                passwordParam = new SqlParameter("@Password", this.txtpass.Text);
                SqlParameter usertypeParam;
                usertypeParam = new SqlParameter("@Usertype", this.txtut.SelectedIndex);

                cmd.Parameters.Add(usernameParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Parameters.Add(usertypeParam);

            cmd.Connection.Open();
            dr = cmd.ExecuteReader();

            if (txtun.Text.Length == 0)
            {
                MessageBox.Show("Please Enter The Username!");
            }
            else if (txtpass.Text.Length == 0)
            {
                MessageBox.Show("Please Enter The Pasword!");
            }
            else if (txtut.Text.Length == 0)
            {
                MessageBox.Show("Please Select The Usertype!");
            }
            else if (dr.HasRows)
            {
                MessageBox.Show("Welcome to Loyal Hopsital");
                Main m = new Main();
                m.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Password or Username!");
                txtun.Clear();
                txtpass.Clear();
                txtut.SelectedIndex = -1;
                txtun.Focus();
            }
            cmd.Connection.Close();
            }
    }
}
