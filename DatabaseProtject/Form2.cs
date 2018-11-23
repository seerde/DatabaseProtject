﻿using System;
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
    }
}
