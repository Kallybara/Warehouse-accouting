﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	public partial class Writeoff : Form
	{
		int chosenWarehouseId =0;
		SqlConnection sqlConnection;
		SqlCommand command = null;
		SqlDataReader sqlReader = null;
		public Writeoff()
		{
			InitializeComponent();
		}

		private async void Writeoff_Load(object sender, EventArgs e)
		{
			string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\VSProjects\Warehouse accouting\Warehouse accouting\Database.mdf;Integrated Security=True";

			sqlConnection = new SqlConnection(connectionString);

			await sqlConnection.OpenAsync();


			SqlCommand command = new SqlCommand("SELECT [Name] FROM [WareHouses] WHERE [UserId] = @UserId", sqlConnection);
			command.Parameters.AddWithValue("@UserId", User.userId);
			sqlReader = await command.ExecuteReaderAsync();
			while (await sqlReader.ReadAsync())
			{
				comboBox1.Items.Add(Convert.ToString(sqlReader["Name"]));
			}
			if (sqlReader != null)
				sqlReader.Close();
		}

		private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name", sqlConnection);

			command.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());
			sqlReader = await command.ExecuteReaderAsync();
			while (await sqlReader.ReadAsync())
			{

				chosenWarehouseId = Convert.ToInt32(sqlReader["Id"]);
			}
			if (sqlReader != null)
				sqlReader.Close();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox1.Text) && chosenWarehouseId != 0)
			{
				command = new SqlCommand("DELETE FROM [Products] WHERE [Id] = @Id AND [WarehouseID] = @WarehouseId", sqlConnection);

				command.Parameters.AddWithValue("Id", textBox1.Text);
				command.Parameters.AddWithValue("WarehouseID", chosenWarehouseId);
				try
				{
					await command.ExecuteNonQueryAsync();
				}
				catch (Exception)
				{
					MessageBox.Show("Введите коректное значениe");
					textBox1.Text = "";
				}
				
			}
			else
			{
				MessageBox.Show("Заполните все поля.");
			}
		}
	}
}
