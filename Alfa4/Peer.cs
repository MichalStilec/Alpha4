using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alfa4
{
    public class Peer
    {
        public static UdpClient udpClient;
        private static string myPeerId = "molic-peer1"; // Změňte podle potřeby
        public static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public static void StartUdpListener()
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
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);

                // Zde můžete implementovat logiku pro zpracování přijaté UDP zprávy (dotazu nebo odpovědi)
                Console.WriteLine("Received UDP: " + message);
            }
        }

        public static void PeriodicUdpDiscovery()
        {
            Timer udpDiscoveryTimer = new Timer((state) =>
            {
                SendUdpDiscovery();
            }, null, 0, 5000);
        }

        public static void SendUdpDiscovery()
        {
            string udpMessage = $"{{\"command\":\"hello\",\"peer_id\":\"{myPeerId}\"}}";
            byte[] data = Encoding.UTF8.GetBytes(udpMessage);
            udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, 9876));
        }

        public static void SendMessages()
        {
            // Zde můžete implementovat logiku pro odesílání TCP zpráv ostatním peerům
        }

        public static void ReceiveMessages()
        {
            // Zde můžete implementovat logiku pro příjem TCP zpráv od ostatních peerů
        }
    }
}
