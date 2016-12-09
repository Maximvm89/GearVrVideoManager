using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class AvviaServer : MonoBehaviour {

	NetworkClient myClient;

	public bool client1 = false;
	public bool client2 = false;
	public bool broadcast = false;

	int myID;
	Text txt;
	public GameObject status1;

	public GameObject connectionLight1;
	public GameObject connectionLight2;


	public Sprite verde;

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
		txt = status1.GetComponent<Text> (); 
		txt.text = "Server Avviato";

		//connectionLight1.GetComponent<SpriteRenderer> ().sprite = Resources.Load(verde, typeof(Sprite)) as Sprite;
	}

	void OnClientConnected (NetworkMessage msg)
	{
		CommandMessage msg1 = new CommandMessage(); 
		msg1.IdConnessione = msg.conn.connectionId;
		msg1.IdHost = msg.conn.hostId;
		Debug.Log ("Client Connected");
		Debug.Log (msg.conn.hostId);
		Debug.Log (msg.conn.connectionId);
		txt = status1.GetComponent<Text> (); 
		txt.text = "Connessione stabilita con " + msg.conn.connectionId + " - " + msg.conn.hostId;
		if(msg.conn.connectionId==1) {
			connectionLight1.GetComponent<SpriteRenderer>().sprite = verde;
		}
		if(msg.conn.connectionId==2) {
			connectionLight2.GetComponent<SpriteRenderer>().sprite = verde;
		}
		NetworkServer.SendToClient (msg.conn.connectionId, MyMsgType.Valore, msg1);

	}

	public class MyMsgType {
		public static short Valore = MsgType.Highest + 1;
	}

	public class CommandMessage : MessageBase
	{
		public string comando;
		public int IdConnessione;
		public int IdHost;
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
