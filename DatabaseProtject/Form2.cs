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
        int StationID;
        Form3 ff = new Form3();
        private OleDbConnection connection = new OleDbConnection();
        public Form2()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DatabaseProject.accdb; Persist Security Info=False;";
            connection.ConnectionString = db1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                " from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                " inner join Station on ScheduleStation.StationID=Station.StationID" +
                " where StationName = 'Seoul' and DepartureTime = 0";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
            }
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                " from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                " inner join Station on ScheduleStation.StationID=Station.StationID" +
                " where StationName = 'Busan' and DepartureTime = 0";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
            }
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                " from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                " inner join Station on ScheduleStation.StationID=Station.StationID" +
                " where StationName =?";
            command.CommandText = qry;
            command.Parameters.AddWithValue("@p1", comboBox1.SelectedItem.ToString());
            OleDbDataReader reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
            }
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3.mm1 += 5;
            TimeSpan span = TimeSpan.FromMinutes(Form3.mm1);
            label7.Text = span.ToString(@"hh\:mm");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.StartPosition = FormStartPosition.Manual;
            f3.Location = this.Location;
            f3.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Form3.Stations3.Insert(Form3.Stations3.IndexOf("Daegu"), "Gumi");
            //Form3.Stations4.Insert(Form3.Stations4.IndexOf("Daejeon"), "Gumi");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "select BookSeatID, Source, Des, SeatNumber, CarNumber, TrainType from BookSeat";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
            }
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String qry = "insert into Station(StationName) values('"+textBox1.Text+"')";
            command.CommandText = qry;
            command.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            qry = "select StationID from Station where StationName = '"+textBox1.Text+"'";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                StationID = (int)reader[0];
            connection.Close();

            connection.Open();
            qry = "insert into ScheduleStation(StationID, ArrivalTime, DepartureTime, TrainScheduleID) values('"+StationID+"', '"+textBox2.Text+"', '"+textBox3.Text+"', '2')";
            command.CommandText = qry;
            command.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            qry = "insert into ScheduleStation(StationID, ArrivalTime, DepartureTime, TrainScheduleID) values('" + StationID + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '3')";
            command.CommandText = qry;
            command.ExecuteNonQuery();
            connection.Close();
            if (!Form3.Stations3.Contains("Gumi") && !Form3.Stations4.Contains("Gumi"))
            {
                Form3.Stations3.Insert(Form3.Stations3.IndexOf("Daegu"), "Gumi");
                Form3.Stations4.Insert(Form3.Stations4.IndexOf("Daejeon"), "Gumi");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<String> stations = new List<String>();
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String qry = "select StationName from Station";
            command.CommandText = qry;
            OleDbDataReader reader = command.ExecuteReader();
            String wantStation = "";
            while (reader.Read())
            {
                stations.Add(reader[0].ToString());
            }
            reader.Close();
            connection.Close();

            connection.Open();
            OleDbCommand command2 = new OleDbCommand();
            command2.Connection = connection;
            OleDbDataReader reader2;
            foreach (String i in stations)
            {
                if (wantStation != i)
                {
                    wantStation = i;
                    qry = "select count(Des) from BookSeat where Des = '" + i + "'";
                    command2.CommandText = qry;
                    reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        if((int)reader2[0] > 0)
                            this.chart1.Series["Station"].Points.AddXY(i, reader2[0]);
                    }
                    reader2.Close();
                }
            }
            connection.Close();
            //this.chart1.Series["Station"].Points.AddXY("Seoul", 10);
            //this.chart1.Series["Station"].Points.AddXY("Daegu", 3);
        }
    }
}
