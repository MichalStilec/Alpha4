using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alpha4
{
    public class UDPdiscovery
    {
        public static UdpClient udpClient;
        private static string myPeerId = Config.LoadPeer();
        private static string port = Config.LoadPort();
        private static int portInt = int.Parse(port);

        /// <summary>
        /// Starts the UDP listener on a separate thread
        /// </summary>
        public static void Start()
        {
            try
            {
                udpClient = new UdpClient(portInt);
                Thread udpThread = new Thread(UdpListener);
                udpThread.Start();
                Logs.LogSuccess("Succesfully started the UDP listener");
            }
            catch (Exception ex)
            {
                Logs.LogError(ex);
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Listens for incoming UDP messages and processes them
        /// </summary>
        public static void UdpListener()
        {
            while (true)
            {
                try
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = udpClient.Receive(ref remoteEndPoint);
                    string message = Encoding.UTF8.GetString(data);

                    Console.WriteLine("Q: " + message);

                    string response = ReceiveMessage(message);
                    if (!string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine("A: " + response);
                    }
                }
                catch (Exception ex)
                {
                    Logs.LogError(ex);
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Processes the received UDP message and generates a response
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ReceiveMessage(string msg)
        {
            try
            {
                if (msg.Contains("\"command\":\"hello\""))
                {
                    int start = msg.IndexOf("\"peer_id\":\"") + "\"peer_id\":\"".Length;
                    int end = msg.IndexOf("\"", start);
                    string peerId = msg.Substring(start, end - start);

                    if (!string.Equals(peerId, myPeerId))
                    {
                        Logs.LogSuccess("Succesfuly recieved message");
                        return $"{{\"status\":\"ok\",\"peer_id\":\"{myPeerId}\"}}";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                Logs.LogError(ex);
                Console.WriteLine($"Error: {ex.Message}");
                return "";
            }
        }

        /// <summary>
        /// Sends the message every 5 seconds
        /// </summary>
        public static void UdpDiscovery()
        {
            Timer udpDiscoveryTimer = new Timer((state) =>
            {
                SendMessage();
            }, null, 0, 5000);
        }

        /// <summary>
        /// Sends a message to all peers in the network
        /// </summary>
        public static void SendMessage()
        {
            string udpMessage = $"{{\"command\":\"hello\",\"peer_id\":\"{myPeerId}\"}}";
            byte[] data = Encoding.UTF8.GetBytes(udpMessage);
            udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, portInt));
        }
    }
}

