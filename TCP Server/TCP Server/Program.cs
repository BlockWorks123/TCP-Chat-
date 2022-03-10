using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace TCP_Server
{
    class Program
    {
        private static Dictionary<string, TcpClient> list_clients = new Dictionary<string, TcpClient>();
        private static Dictionary<TcpClient, string> list_nicknames = new Dictionary<TcpClient, string>();
        private static List<TcpClient> tcpClientsList = new List<TcpClient>();

        private static Dictionary<string, string> list_admin = new Dictionary<string, string>();

        private static TcpListener tcpListener = new TcpListener(IPAddress.Any, 8000);

        public static string chat_path = @"..\..\..\..\..\Server Logs\chat_logs.txt";
        public static string ban_path = @"..\..\..\..\..\Server Logs\ban_logs.txt";
        public static string bad_words = @"..\..\..\..\..\Server Logs\bad_words.txt";
        public static string tab_path = "Online Clients";

        static void Main(string[] args)
        {
            list_admin.Add("James", "Password");
            File.WriteAllText(chat_path, "");
            File.AppendAllText(chat_path, ">> Client Started");
            tcpListener.Start();
            Console.WriteLine(">> Server started...");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                StreamReader nickname_reader = new StreamReader(tcpClient.GetStream());
                StreamReader password_reader = new StreamReader(tcpClient.GetStream());
                string nickname = nickname_reader.ReadLine();

                tcpClientsList.Add(tcpClient);

                if (!list_nicknames.ContainsKey(tcpClient) && !list_clients.ContainsKey(nickname))
                {

                    foreach (string line in File.ReadLines(ban_path))
                    {

                        string line_final = line.Replace("\n", "");
                        if (nickname != line_final)
                        {
                            if (list_admin.ContainsKey(nickname))
                            {

                                Broadcast_Client("PASSWORD", tcpClient);
                                string password = password_reader.ReadLine();
                                if (Check_Password(password, list_admin[nickname], tcpClient))
                                {
                                    Start_Client(nickname, tcpClient);
                                }
                                else
                                {
                                    Broadcast_Client($"MESSAGE Incorrect Password", tcpClient);
                                    Broadcast_Client($"DISCONNECT", tcpClient);
                                    tcpClientsList.Remove(tcpClient);
                                    tcpClient.Close();
                                }
                            }
                            else
                            {
                                Start_Client(nickname, tcpClient);
                            }
                        }
                        else 
                        {
                            Broadcast_Client($"MESSAGE You are banned from the Server", tcpClient);
                            Broadcast_Client($"DISCONNECT", tcpClient);
                            tcpClientsList.Remove(tcpClient);
                            tcpClient.Close();
                        }
                    }
                }
                else 
                {
                    Broadcast_Client($"MESSAGE User already Exists", tcpClient);
                    Broadcast_Client($"DISCONNECT", tcpClient);
                    tcpClientsList.Remove(tcpClient);
                    tcpClient.Close();
                }
            }
        }

        private static void Start_Client(string nickname, TcpClient tcpClient)
        {
            list_nicknames.Add(tcpClient, nickname);
            list_clients.Add(nickname, tcpClient);

            Console.WriteLine($">> {nickname} joined the Chat");

            Thread thread = new Thread(Client_Listener);
            thread.Start(tcpClient);

            string message_log = File.ReadAllText(chat_path);
            Broadcast_Client(message_log, tcpClient);
            Broadcast_All($"\n>> {nickname} joined the Chat");
            File.AppendAllText(chat_path, $"\n>> {nickname} joined the Chat");

            Thread.Sleep(5000);

            Broadcast_Client($"ADD {tab_path}", tcpClient);

            if (list_admin.ContainsKey(nickname))
            {
                tab_path = tab_path + $"\n[ADMIN] {nickname} ";
                Broadcast_All($"ADD \n[ADMIN] {nickname} ");
            }
            else
            {
                tab_path = tab_path + $"\n{nickname} ";
                Broadcast_All($"ADD \n{nickname} ");
            }
        }

        private static bool Check_Password(string password, string correct_password, TcpClient tcpClient)
        {
            if (password == correct_password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Socket_Connected(TcpClient function_client)
        {
            Socket s = function_client.Client;
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        public static void Client_Listener(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            string nickname = list_nicknames[tcpClient];

            while (true)
            {
                try
                {
                    string message = reader.ReadLine();

                    if (Socket_Connected(tcpClient))
                    {
                        if (message != null)
                        {
                            message = message.ToLower();
                            Console.WriteLine($">> {nickname} : {message}");

                            if (message.StartsWith("/"))
                            {
                                if (message.EndsWith("/ping"))
                                {
                                    Broadcast_Client("\n>> Server : Hello World", tcpClient);
                                }
                                else if (message.StartsWith("/help"))
                                {
                                    Broadcast_Client("\n/help --> Gives list of avalible commands\n/ping --> Pings client\n/msg {client} {message} --> Sends private message to selected Client", tcpClient);
                                    if (list_admin.ContainsKey(nickname)) 
                                    {
                                        Broadcast_Client("\n/kick {argument} --> Kicks selected Client\n/ban {argument} Bans selected Client\n/censor {argument} --> Censors selected word\n/warn --> Warns Client", tcpClient);
                                    }
                                }
                                else if (message.StartsWith("/kick"))
                                {
                                    if (list_admin.ContainsKey(nickname)) 
                                    {
                                        string kick_argument = message.Replace("/kick ", "");
                                        foreach (TcpClient client in tcpClientsList) 
                                        {
                                            string kick_nickname = list_nicknames[client];
                                            if (kick_argument.StartsWith(kick_nickname)) 
                                            {
                                                kick_argument = kick_argument.Replace(kick_nickname, "");
                                                if (kick_argument == "") 
                                                {
                                                    kick_argument = "Unknown Reason";                                           
                                                }

                                                Broadcast_Client($"MESSAGE You were banned from the Server  REASON: {kick_argument}", client);
                                                Broadcast_Client($"DISCONNECT", client);

                                                if (list_admin.ContainsKey(kick_nickname))
                                                {
                                                    Broadcast_All($"REMOVE \n[ADMIN] {kick_nickname} ");
                                                    tab_path = tab_path.Replace($"\n[ADMIN] {kick_nickname} ", "");
                                                }
                                                else
                                                {
                                                    Broadcast_All($"REMOVE \n{kick_nickname} ");
                                                    tab_path = tab_path.Replace($"\n{kick_nickname} ", "");
                                                }

                                                File.AppendAllText(chat_path, $"\n>> {kick_nickname} left the Chat");
                                                File.AppendAllText(ban_path, $"\n{kick_nickname}");

                                                client.Close();

                                                list_clients.Remove(kick_nickname);
                                                list_nicknames.Remove(client);
                                                tcpClientsList.Remove(client);
                                            }
                                        }
                                    }
                                }
                                else if (message.StartsWith("/ban"))
                                {
                                    if (list_admin.ContainsKey(nickname))
                                    {
                                        string ban_argument = message.Replace("/ban ", "");
                                        foreach (TcpClient client in tcpClientsList)
                                        {
                                            string ban_nickname = list_nicknames[client];
                                            if (ban_argument.StartsWith(ban_nickname))
                                            {
                                                ban_argument = ban_argument.Replace(ban_nickname, "");
                                                if (ban_argument == "")
                                                {
                                                    ban_argument = "Unknown Reason";
                                                }

                                                Broadcast_Client($"MESSAGE You were banned from the Server  REASON: {ban_argument}", client);
                                                Broadcast_Client($"DISCONNECT", client);

                                                if (list_admin.ContainsKey(ban_nickname))
                                                {
                                                    Broadcast_All($"REMOVE \n[ADMIN] {ban_nickname} ");
                                                    tab_path = tab_path.Replace($"\n[ADMIN] {ban_nickname} ", "");
                                                }
                                                else
                                                {
                                                    Broadcast_All($"REMOVE \n{ban_nickname} ");
                                                    tab_path = tab_path.Replace($"\n{ban_nickname} ", "");
                                                }

                                                File.AppendAllText(chat_path, $"\n>> {ban_nickname} left the Chat");
                                                File.AppendAllText(ban_path, $"\n{ban_nickname}");

                                                client.Close();

                                                list_clients.Remove(ban_nickname);
                                                list_nicknames.Remove(client);
                                                tcpClientsList.Remove(client);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Broadcast_Client($"MESSAGE Invalid Permissions", tcpClient);
                                    }
                                }
                                else if (message.StartsWith("/warn")) 
                                {
                                    if (list_admin.ContainsKey(nickname))
                                    {
                                        string warn_argument = message.Replace("/warn ", "");
                                        foreach (TcpClient client in tcpClientsList)
                                        {
                                            string warn_nickname = list_nicknames[client];
                                            if (warn_argument.StartsWith(warn_nickname))
                                            {
                                                warn_argument = warn_argument.Replace(warn_nickname, "");
                                                if (warn_argument == "")
                                                {
                                                    warn_argument = "Unknown Reason";
                                                }

                                                Broadcast_Client($"MESSAGE You have been warned REASON: {warn_argument}", client);
                                            }
                                        }
                                    }
                                }
                                else if (message.StartsWith("/msg"))
                                {
                                    string private_message = message.Replace("/msg ", "");
                                    foreach (TcpClient client in tcpClientsList)
                                    {
                                        string msg_nickname = list_nicknames[client];
                                        if (private_message.StartsWith(msg_nickname))
                                        {
                                            private_message = private_message.Replace(msg_nickname, "");
                                            Broadcast_Client($"\n>> {nickname} --> {msg_nickname} : {private_message}", client);
                                            Broadcast_Client($"\n>> {nickname} --> {msg_nickname} : {private_message}", tcpClient);
                                        }
                                    }
                                }
                                else if (message.StartsWith("/censor"))
                                {
                                    if (list_admin.ContainsKey(nickname))
                                    {
                                        string banword_argument = message.Replace("/censor ", "");
                                        
                                        File.AppendAllText(bad_words, $"\n{banword_argument}");
                                        Console.WriteLine($"{banword_argument} was Censored");
                                        Broadcast_Client($"\n>> {banword_argument} was Censored", tcpClient);
                                    }
                                    else
                                    {
                                        Broadcast_Client($"MESSAGE Invalid Permissions", tcpClient);
                                    }
                                }


                            }
                            else
                            {
                                foreach (string line in File.ReadLines(bad_words))
                                {
                                    string final_line = line.Replace("\n", "");
                                    if (message.Contains(final_line))
                                    {
                                        message = message.Replace(final_line, "****");
                                    }
                                }

                                if (list_admin.ContainsKey(nickname)) 
                                {
                                    Broadcast_All($"\n>> [ADMIN] {nickname} : {message}");
                                    File.AppendAllText(chat_path, $"\n>> [ADMIN] {nickname} : {message}");
                                }
                                else 
                                {
                                    Broadcast_All($"\n>> {nickname} : {message}");
                                    File.AppendAllText(chat_path, $"\n>> {nickname} : {message}");
                                }
                            }
                        }
                    }
                    else 
                    {
                        if (tcpClientsList.Contains(tcpClient))
                        {
                            File.AppendAllText(chat_path, $"\n>> {nickname} left the Chat");
                            Broadcast_All($"\n>> {nickname} left the Chat");
                            Console.WriteLine($">> {nickname} left the Chat");

                            if (list_admin.ContainsKey(nickname)) 
                            {
                                Broadcast_All($"REMOVE \n[ADMIN] {nickname} ");
                                tab_path = tab_path.Replace($"\n[ADMIN] {nickname} ", "");
                            }
                            else 
                            {
                                Broadcast_All($"REMOVE \n{nickname} ");
                                tab_path = tab_path.Replace($"\n{nickname} ", "");
                            }

                            tcpClient.Close();

                            list_clients.Remove(nickname);
                            list_nicknames.Remove(tcpClient);
                            tcpClientsList.Remove(tcpClient);
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
            
            }
        }
        public static void Broadcast_Client(string msg, TcpClient excludeClient)
        {
            msg = msg.Replace("\n", "%EOL%");
            foreach (TcpClient client in tcpClientsList)
            {
                try 
                {
                    if (client == excludeClient)
                    {
                        StreamWriter sWriter = new StreamWriter(client.GetStream());
                        sWriter.WriteLine(msg);
                        sWriter.Flush();
                    }
                }
                catch 
                {
                    return;
                }
            }
        }

        public static void Broadcast_All(string msg)
        {
            msg = msg.Replace("\n", "%EOL%");
            foreach (TcpClient client in tcpClientsList)
            {
                try
                {
                    StreamWriter sWriter = new StreamWriter(client.GetStream());
                    sWriter.WriteLine(msg);
                    sWriter.Flush();
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
