﻿using System;
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
            
            
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 8888);
            socket.Bind(ipEnd);
            socket.Listen(10);
            Console.WriteLine("wating for a client");
            Socket client = socket.Accept();
            IPEndPoint ipEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connect with {0} at port {1}", ipEndPoint.Address, ipEndPoint.Port);
            string welcome = "Welcome to my server";

            int recv;
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(welcome);

            client.Send(data, data.Length, SocketFlags.None);
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                    break;
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                //client.Send(data, recv, SocketFlags.None);
            }
            Console.Write("Disconnect form{0}", ipEndPoint.Address);
            client.Close();
            socket.Close();
        }


        // 获取一个文件下的每个视频地址
        /* 
         * DirectoryInfo 位于System.IO
         * @param path : 视频目录的地址  
         * example：
         *        string path = @"E:\File";
         * @return List : 存储视频地址的容器
        */
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
        }
    }
}