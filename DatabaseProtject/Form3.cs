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
        public static int hh1 = 0,mm1 = 0;
        int minuts1 = 0;
        String time1;
        String[] seats = new String[8];

        String[] train1 = {"00:00", "00:35", "00:40", "01:10", "01:15", "01:55", "02:00", "02:40", "null" };
        String[] train2 = {"00:00", "00:40", "00:45", "01:35", "01:40", "02:10", "02:15", "02:50", "null" };
        int s1 = 40, s2 = 35, s3 = 55, s4 = 45;

        private void button10_Click(object sender, EventArgs e)
        {
            mm1 += 5;
            TimeSpan span = TimeSpan.FromMinutes(mm1);
            label7.Text = span.ToString(@"hh\:mm");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor != Color.Red)
            {
                button3.BackColor = Color.Green;
                label12.Text += " " + button3.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor != Color.Red)
            {
                button4.BackColor = Color.Green;
                label12.Text += " " + button4.Text;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(button3.BackColor == Color.Green)
            {
                button3.BackColor = Color.Red;
                label12.Text = "";
            }
            if(button4.BackColor == Color.Green)
            {
                button4.BackColor = Color.Red;
                label12.Text = "";
            }
            MessageBox.Show("Booked!");
        }

        int[] seatsInt = new int[8];
        public Form3()
        {
            InitializeComponent();
            String db1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DatabaseProject.accdb; Persist Security Info=False;";
            connection.ConnectionString = db1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //time1 = hh1.ToString("00") + ":" + mm1.ToString("00"); ;
            label3.Text = "";
            minuts1 = 0;

            if (comboBox1.SelectedIndex < comboBox2.SelectedIndex)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime  " +
                        "from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID=Station.StationID" +
                        " where(StationName =? and TrainDes ='Busan')" +
                        " union select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                        " from((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID= TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID = Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID = Station.StationID" +
                        " where(StationName =? and TrainDes ='Busan')";
                    command.CommandText = qry;

                    command.Parameters.AddWithValue("@p1", comboBox1.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@p2", comboBox2.SelectedItem.ToString());

                    OleDbDataReader reader = command.ExecuteReader();

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
            }
            else if(comboBox1.SelectedIndex > comboBox2.SelectedIndex)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime  " +
                        "from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID=Station.StationID" +
                        " where(StationName =? and TrainDes ='Seoul')" +
                        " union select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime" +
                        " from((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID= TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID = Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID = Station.StationID" +
                        " where(StationName =? and TrainDes ='Seoul')";
                    command.CommandText = qry;

                    command.Parameters.AddWithValue("@p1", comboBox1.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@p2", comboBox2.SelectedItem.ToString());

                    OleDbDataReader reader = command.ExecuteReader();

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
            if(button2.BackColor != Color.Red)
            {

            }
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