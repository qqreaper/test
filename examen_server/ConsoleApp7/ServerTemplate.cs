using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp7
{
    abstract class TemplateAbstract
    {
        public abstract IPAddress ParseIpAdress(string Ip);

        public abstract TcpListener CreateNewTcpListener(IPAddress localAddr, int port);

        public abstract void WriteLog(string log);
    }
    class TemplateServer : TemplateAbstract
    {
        public override IPAddress ParseIpAdress(string Ip)
        {
            IPAddress localAddr = IPAddress.Parse(Ip);

            return localAddr;
        }
        public override TcpListener CreateNewTcpListener(IPAddress localAddr, int port)
        {
            return new TcpListener(localAddr, port);
        }
        public override void WriteLog(string log)
        {
            Console.WriteLine(log);
        }
    }
}
