namespace Alfa4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Peer.StartUdpListener();
            Peer.PeriodicUdpDiscovery();

            // Pro testování můžete spustit samostatné vlákno pro odesílání zpráv nebo přijímání zpráv pomocí TCP
            Thread sendThread = new Thread(Peer.SendMessages);
            sendThread.Start();

            Thread receiveThread = new Thread(Peer.ReceiveMessages);
            receiveThread.Start();

            Console.ReadLine(); // Program bude běžet, dokud uživatel nezadá Enter
            Peer.udpClient.Close();
            Peer.receiveDone.Set();
        }
    }
}