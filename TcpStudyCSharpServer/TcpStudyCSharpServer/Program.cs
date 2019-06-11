using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace TcpStudyCSharpServer
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
using System.IO;
using System.Threading.Tasks;


//using Shell32;


namespace TcpStudyCSharpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"F:\Sofeware\UE4虚幻游戏引擎初学者指南视频教程\UE4 Fundamentals - Module 01";
            //DirectoryInfo folder = new DirectoryInfo(path);
            //foreach (FileInfo file in folder.GetFiles("*.class")) {
            //    Console.WriteLine(file.FullName);
            //}
            //Program p = new Program();
            //p.printVideoAddress(p.videoAddress(path));

            Console.WriteLine("[1]server start. "  );
            Socket sktServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 8888);
            Console.WriteLine("ipEnd.Address.ToString() = " + ipEnd.Address.ToString());
            sktServer.Bind(ipEnd);
            sktServer.Listen(10);
            Console.WriteLine("[1]server 启动成功. ");


            Console.WriteLine("[2]wating for a client: ");
            Socket client = sktServer.Accept();
            IPEndPoint ipEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("[2]Connect with ip {0} at port {1}", ipEndPoint.Address, ipEndPoint.Port);
            

            string welcome = "[3][server]Welcome to my server: ";
            int recv;
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(welcome);

            client.Send(  data, data.Length, SocketFlags.None);
            while (  true )
            {
                data = new byte[1024];
                recv = client.Receive(data);
                Console.WriteLine("[3]recv.ToString( ) = " + recv.ToString());
                //  exit 有特殊含义，将会退出程序。 有 recv == 0 
                if (  recv == 0  )
                {
                    break;
                }
                Console.WriteLine("[3]" + Encoding.UTF8.GetString(data, 0, recv));
                //client.Send(data, recv, SocketFlags.None);
            }
            Console.Write("[-1]Disconnect form {0}", ipEndPoint.Address);
            client.Close();
            sktServer.Close();
        }


        // 获取一个文件下的每个视频地址
        /* 
         * DirectoryInfo 位于System.IO
         * @param path : 视频目录的地址  
         * example：
         *        string path = @"E:\File";
         * @return List : 存储视频地址的容器
        */

        /*
        public List<string> videoAddress(string path)
        {
            List<string> list = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles("*.mp4"))
            {
                list.Add(file.FullName);
            }
            // Console.WriteLine(list.Count());
            return list;
        }


        // 打印查看 
        public void printVideoAddress(List<string> list)
        {
            foreach (var file in list)
            {
                Console.WriteLine(file);
            }
        }*/
    }
}
