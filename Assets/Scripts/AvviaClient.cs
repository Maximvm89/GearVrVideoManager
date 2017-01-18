using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class AvviaClient : MonoBehaviour {

	public MediaPlayerCtrl scrMedia;

	NetworkClient myClient;

	Text txt;

	int myID;
	int myHost;

	//public GameObject status1;

	//public bool m_bFinish = false;

	public string video_path;

	public class MyMsgType {
		public static short Valore = MsgType.Highest + 1;
	}

	public class CommandMessage : MessageBase
	{
		public string comando;
		public int IdConnessione;
		public int IdHost;
	}

	// Use this for initialization
	void Start () {
		scrMedia.OnEnd += OnEnd;
		myClient = new NetworkClient ();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler(MyMsgType.Valore, OnCommand);
		myClient.Connect("192.168.0.12", 4444); // myClient.Connect("192.168.1.133", 4444);
		Debug.Log("STO CONNETTENDOMI");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Create a client and connect to the server port
	public void StartClient()
	{
		//myClient = new NetworkClient();
		//myClient.RegisterHandler(MsgType.Connect, OnConnected);
		//myClient.RegisterHandler(MyMsgType.Valore, OnCommand);
		//myClient.Connect("192.168.0.31", 4444); // myClient.Connect("192.168.1.133", 4444);
		//myClient.Connect("localhost", 4444);
	}


	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		myID = netMsg.conn.connectionId;
		myHost = netMsg.conn.hostId;
		Debug.Log("MI SONO CONNESSO");
		//txt = status1.GetComponent<Text> (); 
		//txt.text = "Connessione stabilita con " + netMsg.conn.connectionId + " - " + netMsg.conn.hostId;
	}

	#if !UNITY_WEBGL
	public void OnCommand(NetworkMessage netMsg)
	{
		CommandMessage msg = netMsg.ReadMessage<CommandMessage>();
		Debug.Log("HO RICEVUTO UN MESSAGGIO");
		if (netMsg.conn.hostId == myHost) {
			//txt = status1.GetComponent<Text> (); 
			//txt.text = "" + myID + " " + msg.comando;
			if (msg.comando == "Load") {
				scrMedia.Load (video_path);
				//m_bFinish = false;
			}

			if (msg.comando == "Play") {
				scrMedia.Play ();
				//m_bFinish = false;
			}

			if (msg.comando == "Stop") {
				scrMedia.Stop ();
			}
		}
	}
	#endif

	void OnEnd()
	{
		//m_bFinish = true;
	}
		
}
