using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AvviaServer : MonoBehaviour {

	NetworkClient myClient;

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
		
	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}
	void OnClientConnected (NetworkMessage msg)
	{
		Debug.Log ("Client Connected");
		SendScore (1000, 1);
	}

	public class MyMsgType {
		public static short Score = MsgType.Highest + 1;
	};

	public class ScoreMessage : MessageBase
	{
		public int score;
		public int lives;
	}

	public void SendScore(int score, int lives)
	{
		ScoreMessage msg = new ScoreMessage();
		msg.score = score;
		msg.lives = lives;

		NetworkServer.SendToAll(MyMsgType.Score, msg);
	}

	// Create a client and connect to the server port
	public void StartClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.RegisterHandler(MyMsgType.Score, OnScore);
		myClient.Connect("127.0.0.1", 4444);
	}

	public void OnScore(NetworkMessage netMsg)
	{
		ScoreMessage msg = netMsg.ReadMessage<ScoreMessage>();
		Debug.Log("OnScoreMessage " + msg.score);
	}
		
}
