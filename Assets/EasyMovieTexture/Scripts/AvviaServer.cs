using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class AvviaServer : MonoBehaviour {

	NetworkClient myClient;

	public bool client1 = false;
	public bool client2 = false;
	public bool broadcast = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartServer() {
		NetworkServer.Listen(4444);
		Debug.Log ("Server avviato");
		NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
	}

	void OnClientConnected (NetworkMessage msg)
	{
		Debug.Log ("Client Connected");
		Debug.Log (msg.conn.connectionId);
	}

	public class MyMsgType {
		public static short Valore = MsgType.Highest + 1;
	}

	public class CommandMessage : MessageBase
	{
		public string comando;
		public int IdConnessione;
	}
		
	public void SendCommand(string com)
	{
		CommandMessage msg = new CommandMessage();
		msg.comando = com; 

		if ((client1) || (broadcast)) {
			msg.IdConnessione = 1;
			NetworkServer.SendToClient (1, MyMsgType.Valore, msg);
		}
		if ((client2) || (broadcast)) {
			msg.IdConnessione = 2;
			NetworkServer.SendToClient (2, MyMsgType.Valore, msg);
		}
	}
		
	public void checkboxes(int which) {
		if (which == 1) {
			client1 = !client1;
		}
		if (which == 2) {
			client2 = !client2;
		}
	}

	public void bc() {
		broadcast = !broadcast;
	}

}
