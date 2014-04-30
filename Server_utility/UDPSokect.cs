using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System;
using System.Net.NetworkInformation;

public class UDPSocket
{
	public enum ESocketState
	{
		Inactive,
		Active,
	}

	private EndPoint remoteSender = new IPEndPoint(IPAddress.Any, 0);
	byte[] receiveBuffer;
	Socket socket;
	public EndPoint localEndPoint;
	Thread  networkThread;
	ESocketState State =  ESocketState.Inactive;

	public System.Action<byte[], IPEndPoint> OnReceived;

	public UDPSocket(int port, uint bufferSize)
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		IPAddress localIP = IPAddress.Any;
		localEndPoint = new IPEndPoint(localIP, port);
		receiveBuffer = new byte[bufferSize];
	}

	public void Start()
	{
		socket.Bind(localEndPoint);
		State = ESocketState.Active;
		networkThread = new Thread(new ThreadStart(NetworkLoop));
		networkThread.IsBackground = true;
		networkThread.Start();
		Console.WriteLine("Started on {0}", localEndPoint);
	}

	public void Stop()
	{
		State = ESocketState.Inactive;
		socket.Close();
		Console.WriteLine("Stopped");
	}

	private void NetworkLoop()
	{
		while (State == ESocketState.Active)
		{
			if (socket.Poll(1000, SelectMode.SelectRead))
			{
				socket.ReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ref remoteSender);
				IPEndPoint ipsender = (IPEndPoint)remoteSender;
				if (null != OnReceived)
					OnReceived(receiveBuffer, ipsender);
				Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
			}
		}
	}

	public void SendTo(byte[] message, IPEndPoint receiver)
	{
		socket.SendTo(message, receiver);
	}
}
