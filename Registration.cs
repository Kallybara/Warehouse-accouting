using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	public partial class Registration : Form
	{
		SqlConnection sqlConnection;
		SqlCommand command = null;
		public Registration()
		{
			InitializeComponent();
		}

		private async void Registration_Load(object sender, EventArgs e)
		{
			string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\VSProjects\Warehouse accouting\Warehouse accouting\Database.mdf;Integrated Security=True";

			sqlConnection = new SqlConnection(connectionString);

			await sqlConnection.OpenAsync();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
				&& !string.IsNullOrEmpty(textBox3.Text) && textBox3.Text == textBox2.Text)
			{
				command = new SqlCommand("INSERT INTO [Users] (Login, Password, UserName)VALUES(@Login, @Password,@UserName) ", sqlConnection);

				command.Parameters.AddWithValue("Login", textBox1.Text);
				command.Parameters.AddWithValue("Password", textBox2.Text);
				command.Parameters.AddWithValue("UserName", textBox4.Text);
				await command.ExecuteNonQueryAsync();
				Authorization authorization = new Authorization();
				authorization.Show();
				Close();
			}
			else
			{
				MessageBox.Show("Введите все данные, проверьте совпадают ли пароли.");
			}
		}
	}
}
