using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithDesign6
{
    public class Client
    {
        private int id;
        private string username;
        private string password;
        private string[] messages;
        private int lastMessage;
        public static int indexOfClients = 0;
        private Contact[] contact = new Contact[1000];
        public static int currentContact = 0;

        public int Id { get { return id; } set { id = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string[] Messages { get { return messages; } set { messages = value; } }
        public int LastMessage { get { return lastMessage; } set { lastMessage = value; } }
        public Contact[] Contact { get { return contact; } set { contact = value; } }
        
    }
    public class Contact
    {
        private int id;
        private string username;
        private string[] messages = new string[10000];
        public int IndexOfMessages=0;
        

        public int Id { get { return id; } set { id = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string[] Messages { get { return messages; } set { messages = value; } }
        //public int IndexOfMessages { get { return IndexOfMessages; } set { IndexOfMessages = value; } }
    }
        
}
