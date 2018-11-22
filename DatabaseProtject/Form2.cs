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
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form2()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Seerde\Documents\DatabaseProject.accdb; Persist Security Info=False;";
            connection.ConnectionString = db1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.stationTableAdapter.Fill(this.databaseProjectDataSet.Station);
            label1.Text = "Welcome " + Form1.usr;

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select * from Station inner join ScheduleStation on Station.StationID=ScheduleStation.StationID";
            command.CommandText = qry;

            OleDbDataReader reader = command.ExecuteReader();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
                //dataGridView1.Rows.Add(reader[i].ToString());
                while (reader.Read())
                    dataGridView1.Rows.Add(reader[i].ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String qry = "INSERT INTO Station (StationName) VALUES (@StationName)";
                command.CommandText = qry;

                command.Parameters.AddWithValue("@StationName", textBox1.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Station Added!");

                connection.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
            connection.Close();
        }
    }
}
