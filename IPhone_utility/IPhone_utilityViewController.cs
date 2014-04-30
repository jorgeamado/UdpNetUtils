using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace IPhone_utility
{
	public partial class IPhone_utilityViewController : UIViewController
	{
		UDPSocket udpSocket;
		Queue<long> latencyQueue;

		public IPhone_utilityViewController() : base("IPhone_utilityViewController", null)
		{
			latencyQueue = new Queue<long>();
			udpSocket = new UDPSocket(0, 1024);
			udpSocket.OnReceived = (receiveBuffer, endPoint) =>
			{
				long received = BitConverter.ToInt64(receiveBuffer, 0);
				var latency = DateTime.Now.Ticks - received;
//				Console.WriteLine(DateTime.FromBinary(latency).Millisecond);

				lock (latencyQueue)
				{
					latencyQueue.Enqueue(latency);
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			UITextFieldCondition shouldReturnLambda = (UITextField textField) =>
			{
				textField.ResignFirstResponder();
				return true; 
			};

			ServerIPTextField.ShouldReturn += shouldReturnLambda;
			ServerPortTextField.ShouldReturn += shouldReturnLambda;
			PackageCount.ShouldReturn += shouldReturnLambda;
			base.ViewDidLoad();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void UpdateUI()
		{
			if (udpSocket != null)
				LocalIPLabel.Text = udpSocket.localEndPoint.ToString();
		}

		partial void OnStartButtonClick(NSObject sender)
		{
			udpSocket.Start();
			LocalIPLabel.Text = udpSocket.localEndPoint.ToString();
			StartButton.Enabled = false;
			ServerView.Hidden = false;
		}

		partial void OnSendPackages(NSObject sender)
		{
			IPAddress serverIp = IPAddress.Any;
			int serverPort = 0;
			int packageCount = 0;

			if (IPAddress.TryParse(ServerIPTextField.Text, out serverIp)
				&& int.TryParse(ServerPortTextField.Text, out serverPort)
				&& int.TryParse(PackageCount.Text, out packageCount))
			{
				IPEndPoint serverEndPoint = new IPEndPoint(serverIp, serverPort);
				for (int a = 0; a < packageCount; ++a)
				{
					var ticks = DateTime.Now.Ticks;
					udpSocket.SendTo(BitConverter.GetBytes(ticks), serverEndPoint);
//					Console.WriteLine("send : " + ticks.ToString());
				}
			}
		}

		partial void OnUpdateLatecyClick(NSObject sender)
		{
			lock (latencyQueue)
			{
				if (latencyQueue.Count > 0)
				{
					long sum = 0; 
					foreach (var latency in latencyQueue)
					{
						sum += latency;
					}
					long average = sum / latencyQueue.Count;
					ReceivedTextView.Text += string.Format("average latency = {0} ms \n", DateTime.FromBinary(average).Millisecond);
				}
			}
		}

		partial void OnClearDataClick(NSObject sender)
		{
			lock (latencyQueue)
			{
				latencyQueue.Clear();
				ReceivedTextView.Text = "";
			}
		}
	}
}
