using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat_UPD
{
    public class Upd_client
    {
        public int localPort = 8001;
        public IPAddress brodcastAddress = IPAddress.Parse("224.0.0.0");
        public string? username = null;

        public Upd_client(string? username)
        {
            this.username = username;
        }
        public async Task SendMessageAsync()
        {
            using var sender = new UdpClient(); 
            while (true)
            {
                string? message = Console.ReadLine(); 
                if (string.IsNullOrWhiteSpace(message)) break;

                message = $"{username}: {message}";
                byte[] data = Encoding.Unicode.GetBytes(message);

                await sender.SendAsync(data, new IPEndPoint(brodcastAddress, localPort));
            }
        }
        public async Task ReceiveMessageAsync()
        {
            using var receiver = new UdpClient(localPort); 

            receiver.JoinMulticastGroup(brodcastAddress);
            while (true)
            {
                var result = await receiver.ReceiveAsync();
                string message = Encoding.Unicode.GetString(result.Buffer);
                Console.WriteLine(message);
            }
        }
    }
}
