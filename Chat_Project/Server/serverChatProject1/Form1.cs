using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;

namespace serverChatProject1
{
    public partial class Form1 : Form
    {
        static Socket sck;
        static Socket Accepted;
        static Client[] client = new Client[10000];
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GMCEH39\\SQLEXPRESS;Initial Catalog=Chat1;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            string ip = "127.0.0.1";
            int port = 5555;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            conn.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sck.Listen(0);

            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    //Accepted = sck.Accept();
                    Thread thread2 = new Thread((object Accepted) =>
                    {
                        
                        string[] data = SplitByIndex(Client.GetData((Socket)Accepted), ',', 1);
                        client[int.Parse(data[0])] = new Client();
                        client[int.Parse(data[0])].Accepted = (Socket)Accepted;
                        client[int.Parse(data[0])].Id = int.Parse(data[0]);
                        client[int.Parse(data[0])].Username = data[1];
                        cmd = new SqlCommand("UPDATE Users SET isConnected=1 WHERE UserName='" + client[int.Parse(data[0])].Username + "'", conn);
                        cmd.ExecuteNonQuery();
                        //richTextBox1.Text += "client: " + client[int.Parse(data[0])].Username + " is connect\n";

                        while (true)
                        {
                            string[] data2 = new string[0];
                            try
                            {
                                Array.Resize(ref data2, 2);
                                data2 = SplitByIndex(Client.GetData(client[int.Parse(data[0])].Accepted), ',', 1);
                            }
                            catch (Exception)
                            {
                             //   richTextBox1.Text += "client: " + client[int.Parse(data[0])].Username + " disconnect\n";
                                cmd = new SqlCommand("UPDATE Users SET isConnected=0 WHERE UserName= '" + client[int.Parse(data[0])].Username + "'", conn);
                                cmd.ExecuteNonQuery();
                                client[int.Parse(data[0])].Accepted.Close();
                                client[int.Parse(data[0])] = null;
                                Thread.CurrentThread.Abort();
                                break;
                            }
                            //string[] data3;
                            string switch_on = data2[0];
                            switch (switch_on)
                            {
                                case "FriendRequest":
                                    string[] reqData = SplitByIndex(data2[1], ',', 1);
                                    Client.SendData(client[int.Parse(reqData[1])].Accepted, "FriendRequest," + client[int.Parse(reqData[0])].Id + "," + client[int.Parse(reqData[0])].Username);
                                   // richTextBox1.Text += client[int.Parse(reqData[0])].Username + " sent friendRequest to: " + client[int.Parse(reqData[1])].Username + "\n";
                                    break;
                                case "Chat":
                                    string[] chatData = SplitByIndex(data2[1], ',', 2);
                                    Client.SendData(client[int.Parse(chatData[1])].Accepted, "Chat," + client[int.Parse(chatData[0])].Id + "," + chatData[2]);
                                    // code block
                                    break;
                                case "clientDisconnect":
                                    //client[int.Parse(data2[1])].Accepted.Close();
                                    //Thread.CurrentThread.Abort();
                                    break;
                                default:
                                    // code block
                                    break;
                            }
                        }
                    });
                    thread2.Start(sck.Accept());
                }
          
            });
            thread1.Start();
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

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT UserId from Users where UserName ='" + textBox1.Text + "'", conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            Client.SendData(client[int.Parse(reader["UserId"].ToString())].Accepted, "Disconnect,");
            client[int.Parse(reader["UserId"].ToString())].Accepted.Close();
            reader.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
