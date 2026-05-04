using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DbManager;
using System.IO.Pipes;
using System.Xml;

namespace DbManager.Network
{
    public class Server
    {
        private const string ExitCommand = "Exit";
        private const string ListenIp = "0.0.0.0";
        public void Listen(int port)
        {
      //DEADLINE 6: Implement the server as specified (eGela)
      //Have a look at the project ServerConsole to see how a TcpListener is used
      //Use XmlSerializer to create Xml commands

      try
      {
        DbManager.Database serverDatabase = new Database("admin", "adminPassword");
        //Listen on port 1200. Accept connections from any IP address
        TcpListener server = new TcpListener(IPAddress.Parse(ListenIp), 1200);

        server.Start();

        //("Server running and listening on port 1200");

        Socket socket = server.AcceptSocket();

        //("Connection accepted from " + socket.RemoteEndPoint);

        bool trueFalse = true;
        while (trueFalse == true)
        {
          byte[] buffer = new byte[100];
          int bytesRead = socket.Receive(buffer);
          buffer[bytesRead] = 0;
          ASCIIEncoding encoding = new ASCIIEncoding();
          string clientMessage = encoding.GetString(buffer).Substring(0, bytesRead);

          //("Message received from client: " + clientMessage);
          if (clientMessage == ExitCommand)
          {
            trueFalse = false;
          }
          else
          {
            string clientResult = serverDatabase.ExecuteMiniSQLQuery(clientMessage);
            socket.Send(encoding.GetBytes(clientResult));
          }
        }

        Task.Delay(2000).Wait();

        socket.Close();
        server.Stop();

        //("Server closed. Press any key to finish...");
        Console.ReadKey();
      }
      catch (Exception e)
      {
        //("Unhandled error: " + e);
      }
    }        
    }
}
