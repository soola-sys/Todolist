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

namespace RegistrationAndLogin
{
    public partial class Home : Form
    {
        
       
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\123\Desktop\RegistrationAndLogin\RegistrationAndLogin\Database.mdf;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                string query = "insert into CatTable values (" + CatIdTb.Text + " , '" + CatNameTb.Text + "' , '" + CatDescTb.Text + "')";
                SqlCommand com = new SqlCommand(query, cn);
                com.ExecuteNonQuery();
                MessageBox.Show("The category added successfully!");
                cn.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            
        }
        private void populate()
        {
            cn.Open();
            string qerty = "select * from CatTable";
            SqlDataAdapter sda = new SqlDataAdapter(qerty, cn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGView.DataSource = ds.Tables[0];
            cn.Close();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CatDGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGView.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGView.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGView.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try 
            {
                if (CatIdTb.Text == " ")
                {
                    MessageBox.Show("Select the category to delete");
                }
                else
                {
                    cn.Open();
                    string query = "delete from CatTable where CatId =" + CatIdTb.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success!");
                    cn.Close();
                    populate();
                    
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatIdTb.Text == " " || CatNameTb.Text == " " || CatDescTb.Text == " ")
                {
                    MessageBox.Show("Missing information!");
                }
                else {
                    cn.Open();
                    string query = "update CatTable set CatName = '" + CatNameTb.Text + "', CatDesc = '" + CatDescTb.Text + "' where CatId = " + CatIdTb.Text + "; ";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully updated!");
                    cn.Close();
                    populate();
                }
         }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
