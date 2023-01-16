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
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
            Auto();
            Table();
            SetDefaultWindowSize();
            SetNewSize();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KN1ABBD; Initial Catalog = Hospital; User ID =Kigen; Password=Piajoy@6676;");
        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;


        public void Auto()
        {
            sql = "select patientno from Patient order by patientno desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");

            }
            else
                if (Convert.IsDBNull(dr))
            {
                proid = ("00000");
            }
            else
            {
                proid = ("00000");
            }
            txtpn.Text =  proid.ToString();

            con.Close();

        }
        public void Table()
        {
            sql = "select * from Patient";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);

            }
            con.Close();
        }

        public void getId(String id)
        {
            sql = "select * from Patient where patientno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtpn.Text = dr[0].ToString();
                txtpname.Text = dr[1].ToString();
                txtphone.Text = dr[2].ToString();
                txtadd.Text = dr[3].ToString();
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




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pn = txtpn.Text;
            string pname = txtpname.Text;
            string phone = txtphone.Text;
            string add = txtadd.Text;

           

            if (Mode== true)
            {
                sql = "Insert into Patient (patientno, Patient_Name, Patient_Phone, Address)values(@patientno, @Patient_Name, @Patient_Phone, @Address)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@patientno", pn);
                cmd.Parameters.AddWithValue("@Patient_Name", pname);
                cmd.Parameters.AddWithValue("@Patient_Phone", phone);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered!");
            
                txtpname.Clear();
                txtphone.Clear();
                txtadd.Clear();
                txtpname.Focus();
               


            }
            else
            {
                sql = "update Patient set Patient_Name= @Patient_Name, Patient_Phone=@Patient_Phone, Address=@Address where patientno= @patientno";
                con.Open();
                cmd = new SqlCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@Patient_Name", pname);
                cmd.Parameters.AddWithValue("@Patient_Phone", phone);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@patientno", pn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated!");
                txtpn.Enabled = true;
                Mode = true;
                txtpname.Clear();
                txtphone.Clear();
                txtadd.Clear();
                txtpname.Focus();
                
            }
            con.Close();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtpn.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getId(id);
            }
            else
            if 
                (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtpn.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from Patient where patientno= @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Succefully Deleted!");


            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Auto();
            Table();
            txtpname.Clear();
            txtphone.Clear();
            txtadd.Clear();
            txtpname.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
