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
    public partial class Form3 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        int TrainID = 0;
        int hh1 = 0,mm1 = 0;
        int minuts1 = 0;
        String time1;
        String[] seats = new String[8];

        String[] train1 = {"00:00", "00:35", "00:40", "01:10", "01:15", "01:55", "02:00", "02:40", "null" };
        String[] train2 = {"00:00", "00:40", "00:45", "01:35", "01:40", "02:10", "02:15", "02:50", "null" };

        int s1 = 40, s2 = 35, s3 = 55, s4 = 45;

        int[] seatsInt = new int[8];
        public Form3()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Seerde\source\repos\DatabaseProtject\DatabaseProject.accdb; Persist Security Info=False;";
            connection.ConnectionString = db1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time1 = hh1.ToString("00") + ":" + mm1.ToString("00"); ;
            label3.Text = "";
            minuts1 = 0;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                //String qry = "select * from Train where TrainDes =? or TrainSource =?";
                String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime  " +
                    "from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                    " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                    " inner join Station on ScheduleStation.StationID=Station.StationID" +
                    " where(StationName =? and DepartureTime =?)" +
                    " union select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                    " from((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID= TrainSchedule.TrainScheduleID)" +
                    " inner join Train on TrainSchedule.TrainID = Train.TrainID)" +
                    " inner join Station on ScheduleStation.StationID = Station.StationID" +
                    " where(StationName =? and ArrivalTime =?)";
                command.CommandText = qry;

                command.Parameters.AddWithValue("@p1", comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("@p2", minuts1);
                command.Parameters.AddWithValue("@p3", comboBox2.SelectedItem.ToString());
                if(comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 4)
                {
                    minuts1 += (s1 + s2 + s3 + s4) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }else if(comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 3)
                {
                    minuts1 += (s1 + s2 + s3) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 2)
                {
                    minuts1 += (s1 + s2) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 0 && comboBox2.SelectedIndex == 1)
                {
                    minuts1 += s1 - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 0)
                {
                    minuts1 += (s1 + s2 + s3 + s4) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 1)
                {
                    minuts1 += (s2 + s3 + s4) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 2)
                {
                    minuts1 += (s3 + s4) - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }
                else if (comboBox1.SelectedIndex == 4 && comboBox2.SelectedIndex == 3)
                {
                    minuts1 += s4 - 5;
                    command.Parameters.AddWithValue("@p4", minuts1);
                }

                OleDbDataReader reader = command.ExecuteReader();
                /*
                while (reader.Read())
                {
                    label3.Text = "Train No: " +reader[0]+ " Train Type: " +reader[1]+" Train Source: " +reader[3]+ " Train Destnation: "+reader[2]+" Train Cars: "+reader[4];
                    TrainID = (int)reader[0];
                }*/
                while (reader.Read())
                {
                    label3.Text += "Train No: " + reader[0] + " Train Type: " + reader[1] + " Going to: " + reader[2] + " Departure Time: " + reader[3] + " Arrival Time: " + reader[4] + "\n";
                    TrainID = (int)reader[0];
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
            connection.Close();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String qry = "select SeatID, SeatNumber, SeatStat from (Seat inner join Car on Car.SeatID.Value=Seat.SeatID) " +
                    "inner join Train on Train.CarID.Value=Car.CarID where TrainID =?";
                command.CommandText = qry;

                command.Parameters.AddWithValue("@p1", TrainID);
                Array.Clear(seats,0,seats.Length);
                Array.Clear(seatsInt, 0, seats.Length);

                OleDbDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    if (reader[2].ToString() == "True")
                    {
                        seats[i] = reader[1].ToString();
                        seatsInt[i] = (int)reader[0];
                    }
                    else
                        seats[i] = "null";
                    i++;
                }
                if (seats[0] == "1A")
                    button2.BackColor = Color.Orange;
                else
                    button2.BackColor = Color.Red;
                if (seats[1] == "1B")
                    button3.BackColor = Color.Orange;
                else
                    button3.BackColor = Color.Red;
                if (seats[2] == "2A")
                    button4.BackColor = Color.Orange;
                else
                    button4.BackColor = Color.Red;
                if (seats[3] == "2B")
                    button5.BackColor = Color.Orange;
                else
                    button5.BackColor = Color.Red;
                if (seats[4] == "1A")
                    button6.BackColor = Color.Orange;
                else
                    button6.BackColor = Color.Red;
                if (seats[5] == "1B")
                    button7.BackColor = Color.Orange;
                else
                    button7.BackColor = Color.Red;
                if (seats[6] == "2A")
                    button8.BackColor = Color.Orange;
                else
                    button8.BackColor = Color.Red;
                if (seats[7] == "2B")
                    button9.BackColor = Color.Orange;
                else
                    button9.BackColor = Color.Red;
            }
            catch(Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "update Seat set SeatStat = False where SeatID=?";
            command.CommandText = qry;

            command.Parameters.AddWithValue("@p1", seatsInt[0]);

            command.ExecuteNonQuery();

            connection.Close();
            button2.BackColor = Color.Red;
        }
    }
}