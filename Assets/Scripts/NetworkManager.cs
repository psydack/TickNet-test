using Netick;
using Netick.Unity;
using System.Runtime.InteropServices;
using UnityEngine;

public class NetworkManager : NetworkEventsListener
{
	[DllImport("user32.dll", EntryPoint = "SetWindowText")]
	public static extern bool SetWindowText(System.IntPtr hwnd, string lpString);
	[DllImport("user32.dll", EntryPoint = "FindWindow")]
	public static extern System.IntPtr FindWindow(string className, string windowName);


	[SerializeField] private NetworkTransportProvider transport;
	[SerializeField] private bool debugServerClientMode;
	[SerializeField] private GameObject ui;

	[Header("Network")]
	[Range(0, 65535)]
	[SerializeField] private int port = 36728;
	[SerializeField] private string serverIPAddress = "127.0.0.1";

	public void StartAsServer()
	{
		if (Network.Instance != null)
		{
			return;
		}

		_ = Network.StartAsServer(transport, port);

		ChangeWindowTitle("Server");
	}

	public void StartAsClient()
	{
		if (Network.Instance != null)
		{
			return;
		}

		Network.StartAsClient(transport, port).Connect(port, serverIPAddress);

		ChangeWindowTitle("Client");
	}

	public void StartAsHostClient()
	{
		if (Network.Instance != null)
		{
			return;
		}

		Network.LaunchResults sandboxes = Network.StartAsMultiplePeers(transport, port, null, true, true, 2);
		for (int i = 0; i < 2; i++)
		{
			sandboxes.Clients[i].Connect(port, serverIPAddress);
		}

		ChangeWindowTitle("HostClient");
	}

	public void StartMultipleClients()
	{
		if (Network.Instance != null)
		{
			return;
		}

		int clientAmount = Random.Range(2, 20);
		Network.LaunchResults sandboxes = Network.StartAsMultiplePeers(transport, port, null, false, false, clientAmount);

		for (int i = 0; i < clientAmount; i++)
		{
			sandboxes.Clients[i].Connect(port, serverIPAddress);
		}

		ChangeWindowTitle("MultipleClient");
	}

	public override void OnStartup(NetworkSandbox sandbox)
	{
		Destroy(ui);
		Debug.Log("OnStartup");
	}

	public override void OnClientConnected(NetworkSandbox sandbox, NetworkConnection client)
	{
		GetComponent<GameManager>().InstantiatePlayer();
	}

	private void ChangeWindowTitle(string title)
	{
		System.IntPtr windowPtr = FindWindow(null, "TestNetTick");
		_ = SetWindowText(windowPtr, "TestNetTick - " + title);
	}
}
