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
        private static string myPeerId = "stilec2"; 
        public static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public static void Start()
        {
            try
            {
                udpClient = new UdpClient(9876);
                Thread udpListenerThread = new Thread(UdpListener);
                udpListenerThread.Start();
            }
            catch (Exception ex) { }
        }

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

                    // Process the received message and send a response
                    string response = ProcessReceivedMessage(message);
                    if (!string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine("A: " + response);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing message: {ex.Message}");
                }
            }
        }

        public static string ProcessReceivedMessage(string receivedMessage)
        {
            try
            {
                if (receivedMessage.Contains("\"command\":\"hello\""))
                {
                    int startIndex = receivedMessage.IndexOf("\"peer_id\":\"") + "\"peer_id\":\"".Length;
                    int endIndex = receivedMessage.IndexOf("\"", startIndex);
                    string peerId = receivedMessage.Substring(startIndex, endIndex - startIndex);

                    if (peerId != "stilec2")
                    {
                        return $"{{\"status\":\"ok\",\"peer_id\":\"{myPeerId}\"}}";
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
                return string.Empty;
            }
        }

        public static void UdpDiscovery()
        {
            Timer udpDiscoveryTimer = new Timer((state) =>
            {
                SendMessage();
            }, null, 0, 5000);
        }

        public static void SendMessage()
        {
            string udpMessage = $"{{\"command\":\"hello\",\"peer_id\":\"{myPeerId}\"}}";
            byte[] data = Encoding.UTF8.GetBytes(udpMessage);
            udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, 9876));
        }
    }
}

