﻿using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace CursachSUBD
{
    public partial class Grids : Form
    {
        public Grids()
        {
            InitializeComponent();
        }
        string connstring = String.Format("Server={0};Port={1};" + "User Id ={2};Password={3};Database={4}", "localhost", 5432, "postgres", "18201905", "systemBooking");
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        int rowIndex = -1;
        private void Grids_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private new void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from grid";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
