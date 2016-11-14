using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AvviaClient : MonoBehaviour {

	NetworkClient myClient;

	public class MyMsgType {
		public static short Valore = MsgType.Highest + 1;
	}

	public class CommandMessage : MessageBase
	{
		public string comando;
		public int IdConnessione;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Create a client and connect to the server port
	public void StartClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler(MyMsgType.Valore, OnCommand);
		myClient.Connect("127.0.0.1", 4444);
	}

	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}

	public void OnCommand(NetworkMessage netMsg)
	{
		CommandMessage msg = netMsg.ReadMessage<CommandMessage>();
		Debug.Log("Sono " + msg.IdConnessione + " e ho ricevuto " + msg.comando);
	}
}
