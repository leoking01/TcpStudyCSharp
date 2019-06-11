using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace TcpStudyCSharpClient
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
//    }
//}





using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpStudyCSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            Console.WriteLine("请输入服务器IP地址：");
            string stringIP = Console.ReadLine();
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPAddress ip = IPAddress.Parse(stringIP);
            IPEndPoint ipEnd = new IPEndPoint(ip, 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipEnd);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Fail to connect server");
                Console.WriteLine(e.ToString());
                return;
            }
            int rev = socket.Receive(data);
            //stringData = Encoding.ASCII.GetString(data, 0, rev);
            stringData = Encoding.UTF8.GetString(data, 0, rev);
            Console.WriteLine(stringData);
            input = Console.ReadLine();
            data = Encoding.UTF8.GetBytes(input);
            socket.Send(data, data.Length, SocketFlags.None);
            while (true)
            {
                if (input == "exit")
                    break;
                data = new byte[1024];
                //rev = socket.Receive(data);
                //stringData = Encoding.ASCII.GetString(data, 0, rev);
                //Console.WriteLine(stringData);
                input = Console.ReadLine();
                data = Encoding.UTF8.GetBytes(input);
                //socket.Send(data, rev, 0);
                socket.Send(data, data.Length, SocketFlags.None);
            }
            Console.WriteLine("Disconnect from server");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}


