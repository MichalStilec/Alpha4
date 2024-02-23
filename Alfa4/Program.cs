namespace Alpha4
{
    public class Program
    {
        static void Main(string[] args)
        {
            UDPdiscovery.Start();
            UDPdiscovery.UdpDiscovery();

            Console.ReadLine(); 
            UDPdiscovery.udpClient.Close();
            UDPdiscovery.receiveDone.Set();
        }
    }
}