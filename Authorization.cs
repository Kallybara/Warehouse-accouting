using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	public partial class Authorization : Form
	{
		SqlConnection sqlConnection;
		private bool isConnected = false;

		public Authorization()
		{
			InitializeComponent();
		}

		private async void Authoriztion_Load(object sender, EventArgs e)
		{
			string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\VSProjects\Warehouse accouting\Warehouse accouting\Database.mdf;Integrated Security=True";

			sqlConnection = new SqlConnection(connectionString);

			await sqlConnection.OpenAsync();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
			{				
				SqlDataReader sqlReader = null;

				SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE [Login] = @Login AND [Password] = @Password", sqlConnection);

				command.Parameters.AddWithValue("@Login", textBox1.Text);
				command.Parameters.AddWithValue("@Password", textBox2.Text);
				try
				{
					sqlReader =  await command.ExecuteReaderAsync();					
					while (await sqlReader.ReadAsync())
					{
						MainMenu mainMenu = new MainMenu();
						mainMenu.label1.Text = Convert.ToString(sqlReader["UserName"]);
						User.userId = Convert.ToInt32(sqlReader["Id"]);
						mainMenu.Show();
						Hide();
						isConnected = true;
					}
					if (!isConnected)
						MessageBox.Show("Такого пользователя не существует или пароль неверен.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Такого пользователя не существует или пароль неверен.", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

				}
				finally
				{
					if (sqlReader != null)
						sqlReader.Close();
				}				
			}
			else
			{
				MessageBox.Show("Заполните все поля.");
			}
		}

		private void label3_Click(object sender, EventArgs e)
		{
			Registration f = new Registration();
			f.Show();
			Hide();
		}
	}
}
