using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace serverChatProject1
{
    internal class Client
    {
        private int id;
        private string username;
        private string[] messages;
        private int lastMessage;
        private Socket accepted;
        public static int indexOfClients = 0;

        public int Id { get { return id; } set { id = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string[] Messages { get { return messages; } set { messages = value; } }
        public int LastMessage { get { return lastMessage; } set { lastMessage = value; } }
        public Socket Accepted { get { return accepted; } set { accepted = value; } }

        public static string GetData(Socket Accepted)
        {
            byte[] Buffer = new byte[4096];
            int bytesRead = Accepted.Receive(Buffer);
            byte[] formatted = new byte[bytesRead];
            for (int i = 0; i < bytesRead; i++)
            {
                formatted[i] = Buffer[i];
            }
            string Data = Encoding.ASCII.GetString(formatted);
            return Data;
        }
        public static void SendData(Socket Accepted, string Data)
        {
            byte[] data = Encoding.ASCII.GetBytes(Data);
            Accepted.Send(data);
        }
    }
}
