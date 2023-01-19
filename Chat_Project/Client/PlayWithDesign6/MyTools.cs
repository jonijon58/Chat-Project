using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunifu.UI.WinForms.BunifuButton;
using Bunifu.UI.WinForms;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayWithDesign6
{
    public class MyTools
    {
        //public static BunifuButton[] CardContact = new BunifuButton[1000];
        public static int indexOfCardContact = 0;
        public static ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
        public static BunifuUserControl[] messageBox = new BunifuUserControl[1000];
        public static int indexOfOutMessage = 0;
        public static int locationPixelValueX = 0;
        public static Label[] textMessage = new Label[1000];
        public static Boolean lastMessageIsOut;
        public static BunifuButton CardContact1;
        public static Label UserId;
        public static void setCardContact(Panel panel,int id, string name, Form1 form)
        {
            CardContact1 = new BunifuButton();
            CardContact1.Dock = System.Windows.Forms.DockStyle.Top;
            CardContact1.Size = new System.Drawing.Size(200, 66);
            CardContact1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton1.BackgroundImage")));
            CardContact1.IdleIconLeftImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton1.IdleIconLeftImage")));
            CardContact1.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            CardContact1.Name = id.ToString();
            CardContact1.ButtonText = name;
            CardContact1.Click += new System.EventHandler(form.bunifuButton1_Click_1);
            //CardContact[indexOfCardContact].Location = new System.Drawing.Point(0, 66);
            panel.Controls.Add(CardContact1);
            indexOfCardContact++;
        }
        public static void inMessage(Panel panel, string text)
        {
            messageBox[indexOfOutMessage] = new BunifuUserControl();
            textMessage[indexOfOutMessage] = new Label();
            messageBox[indexOfOutMessage].AllowAnimations = false;
            messageBox[indexOfOutMessage].AllowBorderColorChanges = false;
            messageBox[indexOfOutMessage].AllowMouseEffects = false;
            messageBox[indexOfOutMessage].AnimationSpeed = 200;
            messageBox[indexOfOutMessage].BackColor = System.Drawing.Color.Transparent;
            messageBox[indexOfOutMessage].BackgroundColor = System.Drawing.Color.White;
            messageBox[indexOfOutMessage].BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            messageBox[indexOfOutMessage].BorderRadius = 20;
            messageBox[indexOfOutMessage].BorderStyle = Bunifu.UI.WinForms.BunifuUserControl.BorderStyles.Solid;
            messageBox[indexOfOutMessage].BorderThickness = 1;
            messageBox[indexOfOutMessage].ColorContrastOnClick = 30;
            messageBox[indexOfOutMessage].ColorContrastOnHover = 30;
            messageBox[indexOfOutMessage].Cursor = System.Windows.Forms.Cursors.Default;
            messageBox[indexOfOutMessage].Image = null;
            messageBox[indexOfOutMessage].ImageMargin = new System.Windows.Forms.Padding(0);
            if (indexOfOutMessage == 0)
            { messageBox[indexOfOutMessage].Location = new System.Drawing.Point(6, 6); }
            else { messageBox[indexOfOutMessage].Location = new System.Drawing.Point(6, messageBox[indexOfOutMessage - 1].Location.Y + 103); }
            messageBox[indexOfOutMessage].Name = "bunifuUserControl2";
            messageBox[indexOfOutMessage].ShowBorders = true;
            messageBox[indexOfOutMessage].Size = new System.Drawing.Size(394, 97);
            messageBox[indexOfOutMessage].Style = Bunifu.UI.WinForms.BunifuUserControl.UserControlStyles.Flat;
            messageBox[indexOfOutMessage].TabIndex = 5;
            messageBox[indexOfOutMessage].TabIndex = 0;
            textMessage[indexOfOutMessage].ForeColor = System.Drawing.Color.Cyan;
            textMessage[indexOfOutMessage].Size = new System.Drawing.Size(373, 100);
            textMessage[indexOfOutMessage].Font = new System.Drawing.Font("Segoe UI", 12);
            textMessage[indexOfOutMessage].Text = text;
            panel.Controls.Add(messageBox[indexOfOutMessage]);
            messageBox[indexOfOutMessage].Controls.Add(textMessage[indexOfOutMessage]);


            indexOfOutMessage++;
        }
        public static void outMessage(Panel panel, string text)
        {

            messageBox[indexOfOutMessage] = new BunifuUserControl();
            textMessage[indexOfOutMessage] = new Label();
            messageBox[indexOfOutMessage].AllowAnimations = false;
            messageBox[indexOfOutMessage].AllowBorderColorChanges = false;
            messageBox[indexOfOutMessage].AllowMouseEffects = false;
            messageBox[indexOfOutMessage].AnimationSpeed = 200;
            messageBox[indexOfOutMessage].BackColor = System.Drawing.Color.Transparent;
            messageBox[indexOfOutMessage].BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(89)))), ((int)(((byte)(205)))));
            messageBox[indexOfOutMessage].BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            messageBox[indexOfOutMessage].BorderRadius = 20;
            messageBox[indexOfOutMessage].BorderStyle = Bunifu.UI.WinForms.BunifuUserControl.BorderStyles.Solid;
            messageBox[indexOfOutMessage].BorderThickness = 1;
            messageBox[indexOfOutMessage].ColorContrastOnClick = 30;
            messageBox[indexOfOutMessage].ColorContrastOnHover = 30;
            messageBox[indexOfOutMessage].Cursor = System.Windows.Forms.Cursors.Default;
            messageBox[indexOfOutMessage].Image = null;
            messageBox[indexOfOutMessage].ImageMargin = new System.Windows.Forms.Padding(0);
            if (indexOfOutMessage == 0)
            { messageBox[indexOfOutMessage].Location = new System.Drawing.Point(260, 6); }
            else { messageBox[indexOfOutMessage].Location = new System.Drawing.Point(260, messageBox[indexOfOutMessage - 1].Location.Y + 103); }
            messageBox[indexOfOutMessage].Name = "bunifuUserControl1";
            messageBox[indexOfOutMessage].ShowBorders = true;
            messageBox[indexOfOutMessage].Size = new System.Drawing.Size(394, 97);
            messageBox[indexOfOutMessage].Style = Bunifu.UI.WinForms.BunifuUserControl.UserControlStyles.Flat;
            textMessage[indexOfOutMessage].ForeColor = System.Drawing.Color.Cyan;
            messageBox[indexOfOutMessage].TabIndex = 0;
            textMessage[indexOfOutMessage].Size = new System.Drawing.Size(373, 100);
            textMessage[indexOfOutMessage].Font = new System.Drawing.Font("Segoe UI", 12);
            textMessage[indexOfOutMessage].Text = text; 
            panel.Controls.Add(messageBox[indexOfOutMessage]);
            messageBox[indexOfOutMessage].Controls.Add(textMessage[indexOfOutMessage]);

            indexOfOutMessage++;
        }
        
    }
}
