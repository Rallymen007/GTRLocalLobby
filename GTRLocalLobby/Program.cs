using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GTRLocalLobby {
    class Program {
        static void Main(string[] args) {
            // port 802 for sim, 801 for semi pro
            TcpListener server = new TcpListener(IPAddress.Any, 802);

            server.Start();
            while (true) {
                TcpClient client = server.AcceptTcpClient();

                NetworkStream ns = client.GetStream();

                List<byte> connectedmsgbytelist = Encoding.ASCII.GetBytes("CONNECTED:").ToList();
                connectedmsgbytelist.Add(0xff);

                ns.Write(connectedmsgbytelist.ToArray(), 0, connectedmsgbytelist.Count);

                while (client.Connected) {
                    try {
                        byte[] msg = new byte[1024];
                        ns.Read(msg, 0, msg.Length);

                        //string msgstr = BitConverter.ToString(msg);
                        string msgstr = Encoding.ASCII.GetString(msg);
                        Console.WriteLine(msgstr);

                        if (msgstr.StartsWith("VERSION")) {
                            List<byte> versionmsgbytelist = Encoding.ASCII.GetBytes("VERSION:OK").ToList();
                            versionmsgbytelist.Add(0xff);

                            ns.Write(versionmsgbytelist.ToArray(), 0, versionmsgbytelist.Count);

                        }
                        if (msgstr.StartsWith("LOGIN")) {
                            List<byte> versionmsgbytelist = Encoding.ASCII.GetBytes("LOGIN:OK").ToList();
                            versionmsgbytelist.Add(0xff);

                            ns.Write(versionmsgbytelist.ToArray(), 0, versionmsgbytelist.Count);

                        }
                        if (msgstr.StartsWith("SETUP_LOGIN")) {
                            List<byte> versionmsgbytelist = Encoding.ASCII.GetBytes("LOGIN:OK").ToList();
                            versionmsgbytelist.Add(0xff);

                            ns.Write(versionmsgbytelist.ToArray(), 0, versionmsgbytelist.Count);

                        }
                        if (msgstr.StartsWith("PING")) {
                            List<byte> versionmsgbytelist = Encoding.ASCII.GetBytes("PING:").ToList();
                            versionmsgbytelist.Add(0xff);

                            ns.Write(versionmsgbytelist.ToArray(), 0, versionmsgbytelist.Count);

                        }
                        if (msgstr.StartsWith("LOGOUT")) {
                            List<byte> versionmsgbytelist = Encoding.ASCII.GetBytes("LOGOUT:OK").ToList();
                            versionmsgbytelist.Add(0xff);

                            ns.Write(versionmsgbytelist.ToArray(), 0, versionmsgbytelist.Count);
                            client.Close();
                        }
                    } catch(Exception e) {
                        Console.WriteLine(e);
                    }
                }
            }

        }
    }
}