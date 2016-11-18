using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class AvviaClient : MonoBehaviour {

	public MediaPlayerCtrl scrMedia;

	NetworkClient myClient;

	Text txt;

	public GameObject status1;

	public bool m_bFinish = false;

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
		scrMedia.OnEnd += OnEnd;
        scrMedia.OnReady += StartClient;

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
		myClient.Connect("192.168.0.9", 4444); // myClient.Connect("192.168.1.133", 4444);
	}

	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}

	#if !UNITY_WEBGL
	public void OnCommand(NetworkMessage netMsg)
	{
		CommandMessage msg = netMsg.ReadMessage<CommandMessage>();
		//txt = status1.GetComponent<Text>(); 
		//txt.text = msg.comando;

		if (msg.comando == "Load") {
			scrMedia.Load("J360.mp4");
			m_bFinish = false;
		}

		if (msg.comando == "Play") {
			scrMedia.Play();
			m_bFinish = false;
		}

		if (msg.comando == "Stop") {
			scrMedia.Stop();
		}

	
	}
	#endif

	void OnEnd()
	{
		m_bFinish = true;
	}







}
