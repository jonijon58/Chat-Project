using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace PlayWithDesign6
{
    public partial class login : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GMCEH39\\SQLEXPRESS;Initial Catalog=Chat1;Integrated Security=True");
        public static login instance;
        public Client client;
        SqlCommand cmd;
        SqlDataReader reader;

        public login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));
            conn.Open();
            instance = this;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT UserName from Users where UserName = '" + bunifuTextBox1.Text + "'", conn);

            reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                reader.Close();
                MessageBox.Show("The user: " + bunifuTextBox1.Text + " is already exist! plase choose another name");
            }
            else
            {
                reader.Close();
                cmd = new SqlCommand("insert into Users(UserName,Password,isConnected)values('" + bunifuTextBox1.Text+"','"+bunifuTextBox2.Text+"',0) ", conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {   
            cmd = new SqlCommand("SELECT UserId,UserName,Password,isConnected from Users where UserName = '" + bunifuTextBox1.Text+"'",conn);

            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader["Password"].ToString() == bunifuTextBox2.Text&& int.Parse(reader["isConnected"].ToString()) == 0)
            {
                client = new Client();
                client.Username = reader["UserName"].ToString();
                client.Password = reader["Password"].ToString();
                client.Id = int.Parse(reader["UserId"].ToString());
                
                Form1 form = new Form1();
                this.Hide();
                form.Show();
                
            }
            else if (reader["Password"].ToString() == bunifuTextBox2.Text && int.Parse(reader["isConnected"].ToString()) ==1)
            {
                MessageBox.Show("You are already connected!");
            }
            else
            {
                MessageBox.Show("UserName or Password is wrong!");
            }
            reader.Close();

        }
    }
}
