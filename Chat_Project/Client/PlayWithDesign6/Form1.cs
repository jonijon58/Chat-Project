using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForms.BunifuButton;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Data.SqlClient;

namespace PlayWithDesign6
{
    public partial class Form1 : Form
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
        Form1 form;
        static Socket sck;
        Client client = login.instance.client;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GMCEH39\\SQLEXPRESS;Initial Catalog=Chat1;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 35, 35));

            label2.Text = client.Username;

            string ip = "127.0.0.1";
            int port = 5555;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            form = this;
            bunifuButton1.Hide();
            conn.Open();
            SendData(client.Id.ToString()+","+client.Username);
            backgroundWorker1.RunWorkerAsync();

        }
        private void SendData(string Data)
        {
            byte[] data = Encoding.ASCII.GetBytes(Data);
            sck.Send(data);
        }
        public static string GetData()
        {
            byte[] Buffer = new byte[4096];
            int bytesRead = sck.Receive(Buffer);
            byte[] formatted = new byte[bytesRead];
            for (int i = 0; i < bytesRead; i++)
            {
                formatted[i] = Buffer[i];
            }
            string Data = Encoding.ASCII.GetString(formatted);
            return Data;
        }

        string[] SplitByIndex(string str, char ch, int index)
        {
            char[] Output = new char[0];
            string[] str2 = new string[0];
            int t = 0;
            int counter = 0;
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch && counter < index)
                {
                    counter++;
                    Array.Resize(ref str2, (t + 1));
                    for (; j < i; j++)
                    {
                        str2[t] += Convert.ToString(Output[j]);
                    }
                    if (counter == index)
                    {
                        j -= 1;
                    }
                    j += 1;
                    t++;
                    continue;
                }

                Array.Resize(ref Output, (i + 1));
                Output[i] = str[i];
            }
            if (Output.Length == str.Length)
            {
                Array.Resize(ref str2, (t + 1));
                for (int r = j + 1; r < str.Length; r++)
                {
                    str2[t] += Convert.ToString(Output[r]);
                }
            }
            return str2;
        }
        public void DoSomething(string text ,int num)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DoSomething(text,num)));
            }
            else
            {
                MyTools.setCardContact(panel3,num, text,form);
                // update the ui from here, no worries
            }
        }
        public void DoSomething2(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DoSomething2(text)));
            }
            else
            {
                MyTools.inMessage(panel6,text);
                // update the ui from here, no worries
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT UserId from Users where UserName = '" + bunifuTextBox1.Text + "'", conn);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                client.Contact[int.Parse(reader["UserId"].ToString())] = new Contact();
                client.Contact[int.Parse(reader["UserId"].ToString())].Id = int.Parse(reader["UserId"].ToString());
                client.Contact[int.Parse(reader["UserId"].ToString())].Username = bunifuTextBox1.Text;
                SendData("FriendRequest," + client.Id + "," + client.Contact[int.Parse(reader["UserId"].ToString())].Id);
                MyTools.setCardContact(panel3, client.Contact[int.Parse(reader["UserId"].ToString())].Id, bunifuTextBox1.Text,form);
            }
            else
            {
                MessageBox.Show("client dosen't exist!");
            }
            reader.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuUserControl1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuUserControl2_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuUserControl1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void chatBox1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            if(Client.currentContact!=0)
            {
                panel6.AutoScrollPosition = new Point(800, int.MaxValue);
                MyTools.outMessage(panel6, bunifuTextBox2.Text);
                SendData("Chat," + client.Id + "," + Client.currentContact + "," + bunifuTextBox2.Text);
            }
            else
            {
                MessageBox.Show("You didn't pick a client!");
            }
            
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //bunifuTextBox2.Text += '\n';
                bunifuImageButton2.PerformClick();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            login.instance.Close();
            this.Close();
        }

        public void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            var userClicked = (BunifuButton)sender;
            cmd = new SqlCommand("SELECT isConnected from Users where UserId = '" +userClicked.Name+ "'", conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            if (int.Parse(reader["isConnected"].ToString())==1)
            {
                foreach (Control item in panel6.Controls.OfType<Control>().ToList())
                {
                    panel6.Controls.Remove(item);
                }
                MyTools.indexOfOutMessage = 0;
                label1.Text = userClicked.Text;
                Client.currentContact = int.Parse(userClicked.Name);
                for (int i = 0; i < client.Contact[int.Parse(userClicked.Name)].IndexOfMessages; i++)
                {
                    MyTools.inMessage(panel6, client.Contact[int.Parse(userClicked.Name)].Messages[i]);
                }
                client.Contact[int.Parse(userClicked.Name)].IndexOfMessages = 0;
            }
            else
            {
                panel3.Controls.Remove(userClicked);
                MessageBox.Show("User: " + userClicked.Text+ "is disconnected!");
                
            }
            reader.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                string[] data = SplitByIndex(GetData(), ',', 1);
                string switch_on = data[0];
                switch (switch_on)
                {
                    case "FriendRequest":
                        string[] friendRequestData = SplitByIndex(data[1], ',', 1);
                        client.Contact[int.Parse(friendRequestData[0])] = new Contact();
                        client.Contact[int.Parse(friendRequestData[0])].Id = int.Parse(friendRequestData[0]);
                        client.Contact[int.Parse(friendRequestData[0])].Username = friendRequestData[1];
                        DoSomething(friendRequestData[1], int.Parse(friendRequestData[0]));
                        break;
                    case "Chat":
                        string[] chatData = SplitByIndex(data[1], ',', 1);
                        client.Contact[int.Parse(chatData[0])].Messages[client.Contact[int.Parse(chatData[0])].IndexOfMessages] = chatData[1];
                        client.Contact[int.Parse(chatData[0])].IndexOfMessages++;
                        if(client.Contact[int.Parse(chatData[0])].Id==Client.currentContact)
                        {
                            DoSomething2(chatData[1]);
                            client.Contact[int.Parse(chatData[0])].IndexOfMessages = 0;
                        }
                        break;
                    case "Disconnect":
                        MessageBox.Show("You sunddenly disconnect!");
                        sck.Close();
                        login.instance.Close();
                        this.Close();
                        break;
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
