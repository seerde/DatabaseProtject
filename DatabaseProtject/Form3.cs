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
        int ssIDDep = 0;
        int ssIDAri = 0;
        public static int hh1 = 0,mm1 = 0;
        int minuts1 = 0;
        String time1;
        String[] seats = new String[8];
        String[] seats2 = new String[8];
        String[] Stations = { "Seoul", "Cheonan", "Daejeon", "Daegu", "Busan" };
        String[] Stations2 = { "Busan", "Daegu", "Daejeon", "Cheonan", "Seoul" };
        public static List<String> Stations3 = new List<String>() { "Seoul", "Cheonan", "Daejeon", "Daegu", "Busan" };
        public static List<String> Stations4 = new List<String>() { "Busan", "Daegu", "Daejeon", "Cheonan", "Seoul" };
        List<String> bookSeats = new List<String>();
        List<int> ssID = new List<int>();

        String[] train1 = {"00:00", "00:35", "00:40", "01:10", "01:15", "01:55", "02:00", "02:40", "null" };
        String[] train2 = {"00:00", "00:40", "00:45", "01:35", "01:40", "02:10", "02:15", "02:50", "null" };
        int s1 = 40, s2 = 35, s3 = 55, s4 = 45;
        int price;
        String CarNumber = "";
        int BookingID;

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
            f2.StartPosition = FormStartPosition.Manual;
            f2.Location = this.Location;
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.Orange)
            {
                button3.BackColor = Color.Green;
                label12.Text += " " + button3.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button3.Text);
                CarNumber = "Car1";
            }
            else if (button3.BackColor == Color.Green)
            {
                button3.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.Orange)
            {
                button4.BackColor = Color.Green;
                label12.Text += " " + button4.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button4.Text);
                CarNumber = "Car1";
            }
            else if (button4.BackColor == Color.Green)
            {
                button4.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button4.Text);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == Color.Green)
            {
                button2.BackColor = Color.Red;
            }
            if (button3.BackColor == Color.Green)
            {
                button3.BackColor = Color.Red;
            }
            if(button4.BackColor == Color.Green)
            {
                button4.BackColor = Color.Red;
            }
            if (button5.BackColor == Color.Green)
            {
                button5.BackColor = Color.Red;
            }
            if (button6.BackColor == Color.Green)
            {
                button6.BackColor = Color.Red;
            }
            if (button7.BackColor == Color.Green)
            {
                button7.BackColor = Color.Red;
            }
            if (button8.BackColor == Color.Green)
            {
                button8.BackColor = Color.Red;
            }
            if (button9.BackColor == Color.Green)
            {
                button9.BackColor = Color.Red;
            }
            label12.Text = "";

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "insert into Booking(BookingDate, CustomerName) values(?,?)";
            command.CommandText = qry;
            command.Parameters.AddWithValue("@p1", DateTime.Now.ToString("h:mm:ss"));
            command.Parameters.AddWithValue("@p2", textBox1.Text);
            command.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            qry = "select BookingID from Booking where CustomerName ='"+ textBox1.Text +"'";
            command.CommandText = qry;
            //command.Parameters.AddWithValue("@p1", textBox1.Text);
            OleDbDataReader reader = command.ExecuteReader();
            while(reader.Read())
                BookingID = (int)reader[0];
            connection.Close();

            connection.Open();
            OleDbCommand command2 = new OleDbCommand();
            command2.Connection = connection;
            for (int i = 0; i < bookSeats.Count; i++)
            {
                qry = "insert into BookSeat(BookingID, Price, ScheduleStationID, Source, Des, SeatNumber, CarNumber) values(?,?,?,?,?,'"+ bookSeats[i] + "',?)";
                command2.CommandText = qry;
                command2.Parameters.AddWithValue("@p1", BookingID);
                command2.Parameters.AddWithValue("@p2", 10);
                if(comboBox1.SelectedIndex > comboBox2.SelectedIndex)
                    command2.Parameters.AddWithValue("@p3", ssID[1]);
                command2.Parameters.AddWithValue("@p4", comboBox1.SelectedItem);
                command2.Parameters.AddWithValue("@p5", comboBox2.SelectedItem);
                //command2.Parameters.AddWithValue("@p6", bookSeats[i]);
                command2.Parameters.AddWithValue("@p6", CarNumber);
                command2.ExecuteNonQuery();
            }
            connection.Close();
            
            MessageBox.Show("Booked!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.BackColor == Color.Orange)
            {
                button5.BackColor = Color.Green;
                label12.Text += " " + button5.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button5.Text);
                CarNumber = "Car1";
            }
            else if (button5.BackColor == Color.Green)
            {
                button5.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button5.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.BackColor != Color.Red)
            {
                button6.BackColor = Color.Green;
                label12.Text += " " + button6.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button6.Text);
                CarNumber = "Car2";
            }
            else if (button6.BackColor == Color.Green)
            {
                button6.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button6.Text);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.BackColor != Color.Red)
            {
                button7.BackColor = Color.Green;
                label12.Text += " " + button7.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button7.Text);
                CarNumber = "Car2";
            }
            else if (button7.BackColor == Color.Green)
            {
                button7.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button7.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.BackColor != Color.Red)
            {
                button8.BackColor = Color.Green;
                label12.Text += " " + button8.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button8.Text);
                CarNumber = "Car2";
            }
            else if (button8.BackColor == Color.Green)
            {
                button8.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button8.Text);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.BackColor != Color.Red)
            {
                button9.BackColor = Color.Green;
                label12.Text += " " + button9.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button9.Text);
                CarNumber = "Car2";
            }
            else if (button9.BackColor == Color.Green)
            {
                button9.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button9.Text);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(Stations3.ToArray());
            comboBox2.Items.AddRange(Stations3.ToArray());
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

                    String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime, ScheduleStationID " +
                        "from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID=Station.StationID" +
                        " where(StationName =? and TrainDes ='Busan')" +
                        " union select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime, ScheduleStationID " +
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
                        ssID.Add((int)reader[5]);
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

                    String qry = "select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime, ScheduleStationID " +
                        "from ((ScheduleStation inner join TrainSchedule on ScheduleStation.TrainScheduleID=TrainSchedule.TrainScheduleID)" +
                        " inner join Train on TrainSchedule.TrainID=Train.TrainID)" +
                        " inner join Station on ScheduleStation.StationID=Station.StationID" +
                        " where(StationName =? and TrainDes ='Seoul')" +
                        " union select Train.TrainID, TrainType, StationName, DepartureTime, ArrivalTime, ScheduleStationID " +
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
                        ssID.Add((int)reader[5]);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Error " + ee);
                }
            }
            connection.Close();
            #region
            /*
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String qry = "select SeatID, SeatNumber, SeatStat from (Seat inner join Car on Car.SeatID.Value=Seat.SeatID) " +
                    "inner join Train on Train.CarID.Value=Car.CarID where TrainID =?";
                command.CommandText = qry;

                command.Parameters.AddWithValue("@p1", TrainID);
                Array.Clear(seats, 0, seats.Length);
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
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
            connection.Close();*/
            #endregion
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                /*
                String qry = "select BookSeatID, Source, Des, SeatNumber, CarNumber, T.TrainID, TrainType" +
                    " from ((BookSeat as BS inner join ScheduleStation as SS on BS.ScheduleStationID=SS.ScheduleStationID)" +
                    " inner join TrainSchedule as TS on SS.TrainScheduleID=TS.TrainScheduleID)" +
                    " inner join Train as T on TS.TrainID=T.TrainID" +
                    " where BS.Source = '"+ comboBox1.SelectedItem.ToString() + "' and BS.Des = '" + comboBox2.SelectedItem.ToString() + "'";*/
                
                if(comboBox1.SelectedIndex > comboBox2.SelectedIndex)
                {
                    String qry = "select BookSeatID, Source, Des, SeatNumber, CarNumber, BS.TrainType" +
                " from ((BookSeat as BS inner join ScheduleStation as SS on BS.ScheduleStationID=SS.ScheduleStationID)" +
                " inner join TrainSchedule as TS on SS.TrainScheduleID=TS.TrainScheduleID)" +
                " inner join Train as T on TS.TrainID=T.TrainID where BS.TrainType='KTX3'";
                    command.CommandText = qry;

                    Array.Clear(seats, 0, seats.Length);
                    Array.Clear(seats2, 0, seats2.Length);
                    Array.Clear(seatsInt, 0, seats.Length);

                    OleDbDataReader reader = command.ExecuteReader();

                    int i = 0;
                    while (reader.Read())
                    {
                        //if (comboBox1.SelectedIndex < Array.IndexOf(Stations2, reader[2].ToString()))
                        if (comboBox1.SelectedIndex < Stations4.IndexOf(reader[2].ToString()))
                        {
                            //red
                            if (reader[4].ToString() == "Car1")
                            {
                                seats[i] = reader[3].ToString();
                            }
                            else if (reader[4].ToString() == "Car2")
                            {
                                seats2[i] = reader[3].ToString();
                            }
                        }
                        //else if (comboBox2.SelectedIndex <= Array.IndexOf(Stations2, reader[2].ToString()))
                        else if (comboBox2.SelectedIndex <= Stations4.IndexOf(reader[2].ToString()))
                        {
                            //red
                            if (reader[4].ToString() == "Car1")
                            {
                                seats[i] = reader[3].ToString();
                            }
                            else if (reader[4].ToString() == "Car2")
                            {
                                seats2[i] = reader[3].ToString();
                            }
                        }
                        i++;
                    }
                }
                else
                {
                    String qry = "select BookSeatID, Source, Des, SeatNumber, CarNumber, BS.TrainType" +
                " from ((BookSeat as BS inner join ScheduleStation as SS on BS.ScheduleStationID=SS.ScheduleStationID)" +
                " inner join TrainSchedule as TS on SS.TrainScheduleID=TS.TrainScheduleID)" +
                " inner join Train as T on TS.TrainID=T.TrainID where BS.TrainType='KTX2'";
                    command.CommandText = qry;

                    Array.Clear(seats, 0, seats.Length);
                    Array.Clear(seats2, 0, seats2.Length);
                    Array.Clear(seatsInt, 0, seats.Length);

                    OleDbDataReader reader = command.ExecuteReader();

                    int i = 0;
                    while (reader.Read())
                    {
                        //if (comboBox1.SelectedIndex < Array.IndexOf(Stations, reader[2].ToString()))
                        if (comboBox1.SelectedIndex < Stations3.IndexOf(reader[2].ToString()))
                        {
                            //red
                            if (reader[4].ToString() == "Car1")
                            {
                                seats[i] = reader[3].ToString();
                            }
                            else if (reader[4].ToString() == "Car2")
                            {
                                seats2[i] = reader[3].ToString();
                            }
                        }
                        //else if (comboBox2.SelectedIndex <= Array.IndexOf(Stations, reader[2].ToString()))
                        else if (comboBox2.SelectedIndex <= Stations3.IndexOf(reader[2].ToString()))
                        {
                            //red
                            if (reader[4].ToString() == "Car1")
                            {
                                seats[i] = reader[3].ToString();
                            }
                            else if (reader[4].ToString() == "Car2")
                            {
                                seats2[i] = reader[3].ToString();
                            }
                        }
                        i++;
                    }
                }
                
                
                if (seats.Contains("1A"))
                    button2.BackColor = Color.Red;
                else
                    button2.BackColor = Color.Orange;
                if (seats.Contains("1B"))
                    button3.BackColor = Color.Red;
                else
                    button3.BackColor = Color.Orange;
                if (seats.Contains("2A"))
                    button4.BackColor = Color.Red;
                else
                    button4.BackColor = Color.Orange;
                if (seats.Contains("2B"))
                    button5.BackColor = Color.Red;
                else
                    button5.BackColor = Color.Orange;
                if (seats2.Contains("1A"))
                    button6.BackColor = Color.Red;
                else
                    button6.BackColor = Color.Orange;
                if (seats2.Contains("1B"))
                    button7.BackColor = Color.Red;
                else
                    button7.BackColor = Color.Orange;
                if (seats2.Contains("2A"))
                    button8.BackColor = Color.Red;
                else
                    button8.BackColor = Color.Orange;
                if (seats2.Contains("2B"))
                    button9.BackColor = Color.Red;
                else
                    button9.BackColor = Color.Orange;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error " + ee);
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == Color.Orange)
            {
                button2.BackColor = Color.Green;
                label12.Text += " " + button2.Text;
                price += 10;
                label14.Text = price.ToString();
                bookSeats.Add(button2.Text);
                CarNumber = "Car1";
            }else if(button2.BackColor == Color.Green)
            {
                button2.BackColor = Color.Orange;
                price -= 10;
                label14.Text = price.ToString();
                bookSeats.Remove(button2.Text);
            }
            /*connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            String qry = "update Seat set SeatStat = False where SeatID=?";
            command.CommandText = qry;

            command.Parameters.AddWithValue("@p1", seatsInt[0]);

            command.ExecuteNonQuery();

            connection.Close();
            button2.BackColor = Color.Red;*/
        }
    }
}