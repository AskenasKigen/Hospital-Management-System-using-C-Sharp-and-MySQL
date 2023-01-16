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
    public partial class Doctor : Form
    {
        public Doctor()
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



        public void Auto()
        {
            sql = "select doctorno from Doctor order by doctorno desc";
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
            txtdn.Text = proid.ToString();

            con.Close();
        }
        public void Table()
        {
            sql = "select * from Doctor";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);

            }
            con.Close();
        }
        public void getId(String id)
        {
            sql = "select * from Doctor where doctorno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtdn.Text = dr[0].ToString();
                txtdname.Text = dr[1].ToString();
                txtsp.Text = dr[2].ToString();
                txtq.Text = dr[3].ToString();
                txtc.Text = dr[4].ToString();
                txtpn.Text = dr[5].ToString();
                txtrn.Text = dr[6].ToString();

            }
            con.Close();
        }

        private void Doctor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dn = txtdn.Text;
            string dname = txtdname.Text;
            string sp = txtsp.Text;
            string qa = txtq.Text;
            string c = txtc.Text;
            string pn = txtpn.Text;
            string rn = txtrn.Text;



            if (Mode == true)
            {
                sql = "Insert into Doctor (doctorno, Doctor_Name, Specialization, Qualification, Channel_Fee, Phone_Number, Room_No)values(@doctorno, @Doctor_Name, @Specialization, @Qualification, @Channel_Fee, @Phone_Number, @Room_No)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@doctorno", dn);
                cmd.Parameters.AddWithValue("@Doctor_Name", dname);
                cmd.Parameters.AddWithValue("@Specialization", sp);
                cmd.Parameters.AddWithValue("@Qualification", qa);
                cmd.Parameters.AddWithValue("@Channel_Fee", c);
                cmd.Parameters.AddWithValue("@Phone_Number", pn);
                cmd.Parameters.AddWithValue("@Room_No", rn);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered!");


                txtdname.Clear();
                txtsp.SelectedIndex = -1;
                txtq.SelectedIndex = -1;
                txtc.Clear();
                txtpn.Clear();
                txtrn.SelectedIndex = -1;

            }
            else
            {
                sql = "update Doctor set Doctor_Name= @Doctor_Name, Specialization= @Specialization, Qualification=@Qualification, Channel_Fee=@Channel_Fee, Phone_Number=@Phone_Number, Room_No=@Room_No where doctorno= @doctorno";
                con.Open();
                cmd = new SqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@Doctor_Name", dname);
                cmd.Parameters.AddWithValue("@Specialization", sp);
                cmd.Parameters.AddWithValue("@Qualification", qa);
                cmd.Parameters.AddWithValue("@Channel_Fee", c);
                cmd.Parameters.AddWithValue("@Phone_Number", pn);
                cmd.Parameters.AddWithValue("@Room_No", rn);
                cmd.Parameters.AddWithValue("@doctorno", dn);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated!");
                txtdn.Enabled = true;
                Mode = true;
                txtdname.Clear();
                txtsp.SelectedIndex = -1;
                txtq.SelectedIndex = -1;
                txtc.Clear();
                txtpn.Clear();
                txtrn.SelectedIndex = -1;
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                txtdn.Enabled = false;
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
                sql = "delete from Doctor where doctorno= @id";
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
            txtdname.Clear();
            txtsp.SelectedIndex = -1;
            txtq.SelectedIndex = -1;
            txtc.Clear();
            txtpn.Clear();
            txtrn.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
