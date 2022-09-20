using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	public partial class Warehouses_show : Form
	{
		int chosenWarehouseId=0;
		SqlConnection sqlConnection;
		SqlCommand command = null;
		SqlDataReader sqlReader = null;
		public Warehouses_show()
		{
			InitializeComponent();
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox1.Text))
			{
				command = new SqlCommand("INSERT INTO [WareHouses] (UserId, Name)VALUES(@UserId, @Name) ", sqlConnection);

				command.Parameters.AddWithValue("UserId", User.userId);
				command.Parameters.AddWithValue("Name", textBox1.Text);
				await command.ExecuteNonQueryAsync();
				MessageBox.Show("Склад создан.");
			}
			else
			{
				MessageBox.Show("Заполните поле.");
			}
		}

		private async void Warehouses_show_Load(object sender, EventArgs e)
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
			SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name AND [UserId] = @UserId", sqlConnection);

			command.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());
			command.Parameters.AddWithValue("@UserId", User.userId);
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
			if (chosenWarehouseId != 0)
			{
				dataGridView1.Rows.Clear();
				SqlCommand command = new SqlCommand("SELECT * FROM [Products] WHERE [WarehouseID] = @WarehouseID", sqlConnection);
				command.Parameters.AddWithValue("@WarehouseID", chosenWarehouseId);

				sqlReader = await command.ExecuteReaderAsync();
				while (await sqlReader.ReadAsync())
				{
					dataGridView1.Rows.Add(Convert.ToString(sqlReader["Id"]),Convert.ToString(sqlReader["Name"]),
						Convert.ToString(sqlReader["Count"]),Convert.ToString(sqlReader["CostBuy"]),
						Convert.ToString(sqlReader["CostSell"]));
				}
				if (sqlReader != null)
					sqlReader.Close();
			}
			else
			{
				MessageBox.Show("Выберите склад.");
			}

		}

	}
}
