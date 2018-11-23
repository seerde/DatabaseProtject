using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DatabaseProtject
{
    public partial class Form1 : Form
    {
        public static String usr;
        public static int usrLvl;
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DatabaseProject.accdb; Persist Security Info=False;";
            connection.ConnectionString = db1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                label3.Text = "";
                connection.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String qry = "select [User.UserID], UserName, UserPassword, Account.AccountName, Account.AccountLevel from [User] inner join Account on Account.AccountID=User.AccountID";
            String qry = "select Count(*) from [User] inner join Account on Account.AccountID=User.AccountID where UserName=? and UserPassword=? and AccountLevel = 1";

            using (OleDbCommand cmd = new OleDbCommand(qry, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                int result = (int)cmd.ExecuteScalar();
                 if (result > 0)
                {
                    usr = textBox1.Text;
                    usrLvl = 1;
                    this.Hide();
                    Form3 f3 = new Form3();
                    Form2 f2 = new Form2();
                    //f3.ShowDialog();
                    f2.ShowDialog();
                }
                else
                    label3.Text = "Wrong Password or Username";
            }
            connection.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                String qry = "select Count(*) from [User] inner join Account on Account.AccountID=User.AccountID where UserName=? and UserPassword=? and AccountLevel = 1";

                using (OleDbCommand cmd = new OleDbCommand(qry, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                    cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        usr = textBox1.Text;
                        usrLvl = 1;
                        this.Hide();
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                    }
                    else
                        label3.Text = "Wrong Password or Username";
                }
                connection.Close();
            }
        }
    }
}
