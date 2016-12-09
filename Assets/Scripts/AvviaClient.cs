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
		myClient.Connect("192.168.0.31", 4444); // myClient.Connect("192.168.1.133", 4444);

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
		Debug.Log("Connessione stabilita con " + netMsg.conn.connectionId + " - " + netMsg.conn.hostId);
		//txt = status1.GetComponent<Text> (); 
		//txt.text = "Connessione stabilita con " + netMsg.conn.connectionId + " - " + netMsg.conn.hostId;
	}

	#if !UNITY_WEBGL
	public void OnCommand(NetworkMessage netMsg)
	{
		CommandMessage msg = netMsg.ReadMessage<CommandMessage>();
		Debug.Log("Ho ricevuto un messaggio sull'identificativo " + myID + " - " + myHost);
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

	/*if( GUI.Button(new Rect(50,50,100,100),"Load"))
	{
		scrMedia.Load("EasyMovieTexture.mp4");
		m_bFinish = false;
	}

	if( GUI.Button(new Rect(50,200,100,100),"Play"))
	{
		scrMedia.Play();
		m_bFinish = false;
	}

	if( GUI.Button(new Rect(50,350,100,100),"stop"))
	{
		scrMedia.Stop();
	}

	if( GUI.Button(new Rect(50,500,100,100),"pause"))
	{
		scrMedia.Pause();
	}

	if( GUI.Button(new Rect(50,650,100,100),"Unload"))
	{
		scrMedia.UnLoad();
	}

	if( GUI.Button(new Rect(50,800,100,100), " " + m_bFinish))
	{

	}

	if( GUI.Button(new Rect(200,50,100,100),"SeekTo"))
	{
		scrMedia.SeekTo(10000);
	}


	if( scrMedia.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
	{
		if( GUI.Button(new Rect(200,200,100,100),scrMedia.GetSeekPosition().ToString()))
		{
			scrMedia.SetSpeed(2.0f);
		}

		if( GUI.Button(new Rect(200,350,100,100),scrMedia.GetDuration().ToString()))
		{
			scrMedia.SetSpeed(1.0f);
		}

		if( GUI.Button(new Rect(200,450,100,100),scrMedia.GetVideoWidth().ToString()))
		{

		}

		if( GUI.Button(new Rect(200,550,100,100),scrMedia.GetVideoHeight().ToString()))
		{

		}
	}

	if( GUI.Button(new Rect(200,650,100,100),scrMedia.GetCurrentSeekPercent().ToString()))
	{

	}*/






}
