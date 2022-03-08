using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

//WARNING any changes that are made may cause Errors

namespace TCP_Client
{
    public partial class Form1 : Form
    {
        string nickname;
        TcpClient tcpClient;
        StreamWriter sWriter;
        StreamReader sReader;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        public void Button_Connect(object sender, EventArgs e)
        {
            Server_Connect();        
        }

        private void Server_Connect()
        {
            TabTerminal.Clear();
            TerminalWindow.Clear();

            string host = HostEntry.Text.Trim();
            int port = int.Parse(PortEntry.Text.Trim());
            nickname = NicknameEntry.Text.Trim();
            nickname = NicknameEntry.Text.Trim();

            nickname = nickname.Replace(" ", "_");
            NicknameEntry.Text = nickname;

            if (nickname == "")
            {
                MessageBox.Show("Invalid nickname try Again", "TCP Client");
                return;
            }
            
            tcpClient = new TcpClient(host, port);

            thread = new Thread(Message_Recv);
            thread.Start();

            
            sWriter = new StreamWriter(tcpClient.GetStream());
            sWriter.WriteLine(nickname);
            sWriter.Flush();
        }

        public void Button_Disconnect(object sender, EventArgs e)
        {
            Server_Disconnect();
        }

        private void Server_Disconnect() 
        {
            if (tcpClient != null) 
            {
                if (tcpClient.Connected)
                {
                    try
                    {
                        TerminalWindow.Clear();
                        TabTerminal.Clear();
                        TabTerminal.AppendText($"\n{nickname} ");
                        TerminalWindow.AppendText($"\n>> {nickname} left the Chat");
                        sReader.Close();
                        sWriter.Close();
                        tcpClient.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else 
                {
                    MessageBox.Show("You are not Connected to a Server", "TCP Client", MessageBoxButtons.OK);
                }
            }
        }

        private void Message_Recv()
        {
            while (true)
            {
                try 
                {
                    sReader = new StreamReader(tcpClient.GetStream());
                    string message = sReader.ReadLine();
                    if(message != null)
                    {
                        SetText(message);
                    }
                    
                }
                catch (Exception)
                {
                    if (!tcpClient.Connected) 
                    {
                        return;
                    }
                }
            }
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.TerminalWindow.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                
                text = text.Replace("%EOL%", "\n");
                if (text.StartsWith("PASSWORD"))
                {
                    string password = PasswordEntry.Text.Trim();
                    sWriter.WriteLine($"{password}");
                    sWriter.Flush();
                }
                else if (text.StartsWith("MESSAGE"))
                {
                    string message_arg = text.Replace("MESSAGE ", "");
                    MessageBox.Show(message_arg, "TCP Client");
                }
                else if (text.StartsWith("TAB")) 
                {
                    string tab_arg = text.Replace("TAB ", "");
                    TabTerminal.AppendText(tab_arg);
                }
                else if (text.StartsWith("ADD")) 
                {
                    string add_arg = text.Replace("ADD ", "");
                    TabTerminal.AppendText(add_arg);
                }
                else if (text.StartsWith("REMOVE")) 
                {
                    string remov_arg = text.Replace("REMOVE ", "");
                    TabTerminal.Text = TabTerminal.Text.Replace(remov_arg, "");
                }
                else if (text.StartsWith("DISCONNECT")) 
                {
                    Server_Disconnect();               
                }
                else
                {
                    TerminalWindow.AppendText($"{text}");
                }
            }
        }
        
        public void Button_Send(object sender, EventArgs e)
        {
            string message_send = MessageEntry.Text.Trim();
            MessageEntry.Clear();
            if (tcpClient.Connected)
            {
                if (message_send == "") 
                {
                    return;
                }
                else if (message_send.StartsWith("/help"))
                {
                    TerminalWindow.AppendText("\n---------------HELP---------------");
                    TerminalWindow.AppendText("\n/help --> Displays list of Commands");
                    TerminalWindow.AppendText("\n/clear --> Clears terminal Window");
                    sWriter.WriteLine($"/help");
                    sWriter.Flush();
                }
                else if (message_send.EndsWith("/clear"))
                {
                    TerminalWindow.Clear();
                    TerminalWindow.AppendText("\n>> Chat has been Cleared");
                }
                else
                {
                    sWriter.WriteLine($"{message_send}");
                    sWriter.Flush();
                }
            }
            else 
            {
                MessageBox.Show("Connect to a server to send messages");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void About_Press(object sender, EventArgs e)
        {
            MessageBox.Show($"Version: 1.0.0\nDeveloper: BlockWorks123", "TCP Client");
        }
    }
}