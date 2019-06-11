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
            Console.WriteLine("client start. ");

            Console.WriteLine("请输入服务器IP地址：");
            string stringIP;
            if(false )
            {
              stringIP = Console.ReadLine();
            }
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            stringIP = "127.0.0.1";

            IPAddress ip = IPAddress.Parse(stringIP);
            IPEndPoint ipEnd = new IPEndPoint(ip, 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipEnd);
                Console.WriteLine("[1]  connect server 成功. ");
            }
            catch (SocketException e)
            {
                Console.WriteLine("[1]Fail to connect server");
                Console.WriteLine(e.ToString());
                return;
            }

            byte[] data = new byte[1024];
            int rev = socket.Receive(data);
            //stringData = Encoding.ASCII.GetString(data, 0, rev);
            string input, stringData;
            stringData = Encoding.UTF8.GetString(data, 0, rev);
            Console.WriteLine("[2]" + stringData);
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
            Console.WriteLine("[-1]Disconnect from server");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}


