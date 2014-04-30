using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;

public class UDPNetwork
{

	public static int Main(string[] args)
	{
		int port = 0;
		if (args.Length < 1 || !int.TryParse(args[0], out port))
			port = 14000;

		uint bufferSize = 0;
		if (args.Length < 2 || !uint.TryParse(args[1], out  bufferSize))
			bufferSize = 1024;


		UDPSocket udpSocket = new UDPSocket(port, bufferSize);
		udpSocket.OnReceived += (byte[] message, IPEndPoint receiver) => 
		{
			udpSocket.SendTo(message, receiver);
//			Console.WriteLine("received: {0}", BitConverter.ToInt64(arg2, 0));
		};
		udpSocket.Start();

		while (Console.ReadLine() != "quit")
		{
		}

		udpSocket.Stop();

		return 0;
	}
}

