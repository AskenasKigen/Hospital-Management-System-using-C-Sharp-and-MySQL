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
    public partial class Channel : Form
    {
        public Channel()
        {
            InitializeComponent();
            Doctor();
            Patient();
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

        public void Doctor()
        {
            sql = "select * from Doctor";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtdn.Items.Add(dr["Doctor_Name"].ToString());
                txtrn.Items.Add(dr["Room_No"].ToString());
            }
            con.Close();
        }

        public void Patient()
        {
            sql = "select * from Patient";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtpn.Items.Add(dr["Patient_Name"].ToString());
            }
            con.Close();
        }
        public void Auto()
        {
            sql = "select channelno from Channel order by channelno desc";
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
            txtcn.Text = proid.ToString();

            con.Close();
        }
        public void Table()
        {
            sql = "select * from Channel";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);

            }
            con.Close();
        }
        public void getId(String id)
        {
            sql = "select * from Channel where channelno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcn.Text = dr[0].ToString();
                txtdn.Text = dr[1].ToString();
                txtpn.Text = dr[2].ToString();
                txtrn.Text = dr[3].ToString();
                txtdt.Text = dr[4].ToString();


            }
            con.Close();
        }

            private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cn = txtcn.Text;
            string dn = txtdn.Text;
            string pn = txtpn.Text;
            string rn = txtrn.Text;
            string dt = txtdt.Value.Date.ToString("yyyy-MM-dd");
            //Asigning Variable and Initialization
           
           



            if (Mode == true)
            {
                sql = "Insert into Channel (channelno, Doctor_Name,  Patient_Name, Room_No, Channel_Date)values(@channelno, @Doctor_Name,  @Patient_Name, @Room_No, @Channel_Date)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@channelno", cn);
                cmd.Parameters.AddWithValue("@Doctor_Name", dn);
                cmd.Parameters.AddWithValue("@Patient_Name", pn);
                cmd.Parameters.AddWithValue("@Room_No", rn);
                cmd.Parameters.AddWithValue("@Channel_Date", dt);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered!");


            
                txtdn.SelectedIndex = -1;
                txtpn.SelectedIndex = -1;
                txtrn.SelectedIndex = -1;
               

            }
            else
            {
                sql = "update Channel set Doctor_Name=@Doctor_Name,  Patient_Name=@Patient_Name, Room_No=@Room_No, Channel_Date=@Channel_Date where  channelno= @channelno";
                con.Open();
                cmd = new SqlCommand(sql, con);

                
                cmd.Parameters.AddWithValue("@Doctor_Name", dn);
                cmd.Parameters.AddWithValue("@Patient_Name", pn);
                cmd.Parameters.AddWithValue("@Room_No", rn);
                cmd.Parameters.AddWithValue("@Channel_Date", dt);
                cmd.Parameters.AddWithValue("@channelno", cn);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated!");
                txtcn.Enabled = true;
                Mode = true;
                txtdn.SelectedIndex = -1;
                txtpn.SelectedIndex = -1;
                txtrn.SelectedIndex = -1;
                txtdn.Focus();
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtcn.Enabled = false;
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
                sql = "delete from Channel where channelno= @id";
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
            txtdn.SelectedIndex = -1;
            txtpn.SelectedIndex = -1;
            txtrn.SelectedIndex = -1;
        }

        private void Channel_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
