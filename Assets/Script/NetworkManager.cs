using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class NetworkManager : MonoBehaviour 
{
	
	//DEBUG
	public bool tempMenu = false;
	//DEBUG
	public bool isOffline = false;
	private bool refreshing = false;
	private bool bConnecting = false;
	public bool bLogin = false;
	public bool netMenu = false;
	public bool bHostName = false;
	private HostData[] hostData; 
	private Ping[] serverPing;
	public string gameName = "PlayerName";
	private Main gameMain;
	public bool bCasual;
	private NetworkView view;
	public float btnX;
	public float btnY;
	public float btnW;
	public float btnH;
	
	public NetworkViewID p1ViewID;
	public NetworkViewID p2ViewID;
	
	private int netUpdateCount;
	public int netUpdateTime;
	public GameObject p1fighter;
	public GameObject p2fighter;
	public FighterController p1fighterCon;
	public FighterController p2fighterCon;
	public GameObject input;
	public ErrorWindow errorWin;
	private bool fightersSet;
	public RoundEnd rEnd;
	public FrontEnd fEnd;
	
	public NetworkPlayer player1;
	public NetworkPlayer player2;
	
	public GameObject cInputFab;
	
	// rollback code vars
	public bool netTagged;
	public bool bStarted;
	public bool bRolling; // are we rolling back.
	public bool bRollBackNeeded;
	public int lastGoodState;
	public int[] lastGoodGameState = new int[30000];
	public int currentRollThread;
	
	// states
	[Serializable]
	public struct State
	{
		public int timestamp;
		public int rollThread;
		public bool localApproval;
		public bool netApproval;
		public bool upButton;
		public bool downButton;
		public bool leftButton;
		public bool rightButton;
		public bool upButton2;
		public bool downButton2;
		public bool leftButton2;
		public bool rightButton2;
		public bool attack01ButtonDown;
		public bool attack01Button;
		public bool attack01ButtonUp;
		public bool attack02ButtonDown;
		public bool attack02Button;
		public bool attack02ButtonUp;
		public bool attack03ButtonDown;
		public bool attack03Button;
		public bool attack03ButtonUp;
		public bool attack04ButtonDown;
		public bool attack04Button;
		public bool attack04ButtonUp;
		public bool attack01ButtonDown2;
		public bool attack01Button2;
		public bool attack01ButtonUp2;
		public bool attack02ButtonDown2;
		public bool attack02Button2;
		public bool attack02ButtonUp2;
		public bool attack03ButtonDown2;
		public bool attack03Button2;
		public bool attack03ButtonUp2;
		public bool attack04ButtonDown2;
		public bool attack04Button2;
		public bool attack04ButtonUp2;
		public bool specialQCL;
		public bool specialQCR;
		public bool specialDPL;
		public bool specialDPR;
		public bool specialHCL;
		public bool specialHHK;
		public bool specialHLK;
		public bool specialQCL2;
		public bool specialQCR2;
		public bool specialDPL2;
		public bool specialDPR2;
		public bool specialHCL2;
		public bool specialHHK2;
		public bool specialHLK2;
		
		public bool b01dframe1;
		public bool	b01dframe2;
		public bool	b01dframe3;
		public bool b01uframe1;
		public bool	b01uframe2;
		public bool	b01uframe3;
		public bool b02dframe1;
		public bool	b02dframe2;
		public bool	b02dframe3;
		public bool b02uframe1;
		public bool	b02uframe2;
		public bool	b02uframe3;
		public bool b03dframe1;
		public bool	b03dframe2;
		public bool	b03dframe3;
		public bool b03uframe1;
		public bool	b03uframe2;
		public bool	b03uframe3;
		public bool b04dframe1;
		public bool	b04dframe2;
		public bool	b04dframe3;
		public bool b04uframe1;
		public bool	b04uframe2;
		public bool	b04uframe3;
		
		public bool b01d2frame1;
		public bool	b01d2frame2;
		public bool	b01d2frame3;
		public bool b01u2frame1;
		public bool	b01u2frame2;
		public bool	b01u2frame3;
		public bool b02d2frame1;
		public bool	b02d2frame2;
		public bool	b02d2frame3;
		public bool b02u2frame1;
		public bool	b02u2frame2;
		public bool	b02u2frame3;
		public bool b03d2frame1;
		public bool	b03d2frame2;
		public bool	b03d2frame3;
		public bool b03u2frame1;
		public bool	b03u2frame2;
		public bool	b03u2frame3;
		public bool b04d2frame1;
		public bool	b04d2frame2;
		public bool	b04d2frame3;
		public bool b04u2frame1;
		public bool	b04u2frame2;
		public bool	b04u2frame3;
	}
	
	[Serializable]
	class GameState
	{
		public int timestamp = 999999;
		public bool ranYet;
		public bool localApproval;
		public bool netApproval;
		public int failCount;
		public int rollThread;
		// controller
		public int p1Hp = 1000;
		public float p1XPos = -10.0f;
		public float p1YPos;
		public int p1Hype;
		public bool bFacingRight1 = true;
		public bool bCrouching1;
		public bool bMoving1;
		public bool bMoveForward1;
		public bool bMoveBackward1;
		public bool bJumping1;
		public bool bJumpStart1;
		public bool bJumpForward1;
		public bool bJumpBackward1;
		public bool bJumpFalling1;
		public bool bJumpAttack1;
		public bool bLanded1;
		public bool bGrounded1 = true;
		public bool bKnockdown1;
		public bool bKnockedOut1;
		public bool bPushed1;
		public bool bNoPush1;
		public bool bHit1;
		public bool bHitSlide1;
		public bool bDanger1;		
		public bool bBlocking1;
		public bool bBlockStun1;
		public bool bThrown1;
		public bool bBouncing1;
		public bool bParryWin1;
		public bool bParryAttempt1;
		public bool bParrying1;
		public bool bPaused1;
		public float currentDist1;
		public float hitDist1;
		public bool bOnRightWall1;
		public bool bOnLeftWall1;
		public bool pushBack1;
		public bool noPushBack1;
		public bool bAttacking1;
		public bool bThrowing1;
		public bool bAirThrowL1;
		public bool bAirThrowH1;
		public bool bAirThrowEX1;
		public bool bCThrowL1;
		public bool	bCThrowH1;
		public bool	bCThrowEX1;
		public bool bCThrowingL1;
		public bool bCThrowingH1;
		public bool bAThrowingL1;
		public bool bAThrowingH1;
		public bool bAThrowingEX1;
		public int tempThrowDam1;
		public bool bProjThrown1;
		public bool bCanceling1;
		public String currentAnim1;
		public float animTime1;
		// jump
		public bool bJumpEnded1;
		public float height1;
		public float length1;
		// activate
		public bool specialCheckR011;
		public bool specialCheckL011;
		public bool specialCheckR0b1;
		public bool specialCheckL0b1;
		public bool specialCheck031;
		public bool specialCheck041;
		// fighter 2
		// controller
		public int p2Hp = 1000;
		public float p2XPos = 10.0f;
		public float p2YPos;
		public int p2Hype = 1000;
		public bool bFacingRight2 = false;
		public bool bCrouching2;
		public bool bMoving2;
		public bool bMoveForward2;
		public bool bMoveBackward2;
		public bool bJumping2;
		public bool bJumpStart2;
		public bool bJumpForward2;
		public bool bJumpBackward2;
		public bool bJumpFalling2;
		public bool bJumpAttack2;
		public bool bLanded2;
		public bool bGrounded2 = true;
		public bool bKnockdown2;
		public bool bKnockedOut2;
		public bool bPushed2;
		public bool bNoPush2;
		public bool bHit2;
		public bool bHitSlide2;
		public bool bDanger2;		
		public bool bBlocking2;
		public bool bBlockStun2;
		public bool bThrown2;
		public bool bBouncing2;
		public bool bParryWin2;
		public bool bParryAttempt2;
		public bool bParrying2;
		public bool bPaused2;
		public float currentDist2;
		public float hitDist2;
		public bool bOnRightWall2;
		public bool bOnLeftWall2;
		public bool pushBack2;
		public bool noPushBack2;
		public bool bAttacking2;
		public bool bThrowing2;
		public bool bAirThrowL2;
		public bool bAirThrowH2;
		public bool bAirThrowEX2;
		public bool bCThrowL2;
		public bool	bCThrowH2;
		public bool	bCThrowEX2;
		public bool bCThrowingL2;
		public bool bCThrowingH2;
		public bool bAThrowingL2;
		public bool bAThrowingH2;
		public bool bAThrowingEX2;
		public int tempThrowDam2;
		public bool bProjThrown2;
		public bool bCanceling2;
		public String currentAnim2;
		public float animTime2;
		// jump
		public bool bJumpEnded2;
		public float height2;
		public float length2;
		// activate
		public bool specialCheckR012;
		public bool specialCheckL012;
		public bool specialCheckR0b2;
		public bool specialCheckL0b2;
		public bool specialCheck032;
		public bool specialCheck042;
	}
	
	public State[] localBufState = new State[30000];
	public State[] roundtwoState = new State[30000];
	public State[] networkBufState = new State[3];
	public State playingState;

	/*
	[SerializeField]
	private GameState[] savedState = new GameState[30000];
	private GameState[] roundtwosaved = new GameState[30000];
	*/
	public int gameStateSaveCount;
	private int gameStateSaveTime = 0;
	private GameState netGameState;
	private GameState lastSavedGameState;
	private GameState roundtwolast;
	private int lastSent; // last input sent to opponent
	
	void Start()
	{
		btnX = Screen.width *0.01f;
		btnY = Screen.width *0.01f;
		btnW = Screen.width *0.2f;
		btnH = Screen.width *0.06f;
		gameMain = GetComponent<Main>();
		view = GetComponent<NetworkView>();
		netUpdateTime = 1;
		//input = GetComponent<PlayerInput>();
		rEnd = GetComponent<RoundEnd>();
	
	}
	
	public void StartServer()
	{
		Network.InitializeServer(32,25002,!Network.HavePublicAddress());
		
		if (!bCasual)
		{
			MasterServer.RegisterHost("EXPO_CWAR","Console War","version 1");
		}
		else
		{
			MasterServer.RegisterHost("EXPO_CASUAL",gameMain.playerName,"version 1");
		}
	}
	
	public void EndServer()
	{
		Network.Disconnect();
		MasterServer.UnregisterHost();
	}
	public void RefreshHostList()
	{
		if (!bCasual)
		{
			MasterServer.RequestHostList("EXPO_CWAR");
		}
		else
		{
			MasterServer.RequestHostList("EXPO_CASUAL");
		}
		refreshing = true;
		MasterServer.PollHostList();
		
	}
	
	// messages
	void OnServerInitialized()
	{
		Debug.Log ("server initialized");
	}
	void OnMasterServerEvent(MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log ("Registration Succeeded");
		}
	}
	void OnConnectedToServer()
	{
		
		Debug.Log ("connected to server");
		//networkView.RPC("StartGame",RPCMode.All);
		bConnecting = false;
		netMenu = false;
		gameMain.fEnd.netBar.transform.Translate(1000,0,0);
		
		if (!gameMain.isSpectator)
		{
			gameMain.netPlay = true; // needs to be broadcast to server too
			GetComponent<NetworkView>().RPC("SendName",RPCMode.Server,gameMain.playerName);
		}
	}
	void OnPlayerConnected (NetworkPlayer player)
	{
		netMenu = false;
		fEnd.MessageOff();
		
		string playString = player.ToString();
		int playNum = Convert.ToInt32(playString);
		print("player "+playNum+" connected");
		
		if (playNum == 1 || playNum == 0)
		{
			GetComponent<NetworkView>().RPC("SendName",RPCMode.Others,gameMain.playerName);
		}
		if (playNum > 1)
		{
		//	networkView.RPC("SpectatorJoin",player,gameMain.timerTime,gameMain.p1Control.stats.health,gameMain.p2Control.stats.health,gameMain.p1Control.hype.hypeAmount,gameMain.p2Control.hype.hypeAmount,gameMain.cam.transform.position.x,gameMain.cam.transform.position.y);
		}
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		string playString = player.ToString();
		int playNum = Convert.ToInt32(playString);
		print("player "+playNum+" disconnected");
		
		if (playNum == 1 || playNum == 0)
		{
			print ("opponent has disconnected");
			errorWin.Error("Opponent has disconnected",1);
			gameMain.EndRoutines();
			
			if (Network.isServer)
			{
				Network.Disconnect();
				MasterServer.UnregisterHost();
			}
			
			//Network.RemoveRPCs(player);        
			//Network.DestroyPlayerObjects(player);
			if (gameMain.bPauseMenu)
			{
				gameMain.ClosePauseMenu();
			}
			else if (gameMain.gameEndMenu)
			{
				gameMain.CloseGameOverMenu();
			}
			else if (gameMain.bChooseChar)
			{
				gameMain.CharsDone();
				//gameMain.fEnd.ReActivate();
			}
		}
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
        if (Network.isServer)
            Debug.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                Debug.Log("Lost connection to the server");
            else
			{
                Debug.Log("Successfully disconnected from the server");
			  //	Network.RemoveRPCs(player);        
				//Network.DestroyPlayerObjects(player);
			
				errorWin.Error("You have been disconnected",1);
				gameMain.EndRoutines();
			
				if (gameMain.bPauseMenu)
				{
					gameMain.ClosePauseMenu();
				}
				else if (gameMain.gameEndMenu)
				{
					gameMain.CloseGameOverMenu();
				}
				else if (gameMain.bChooseChar)
				{
					gameMain.CharsDone();
					//gameMain.fEnd.ReActivate();
				}
			}
    }
	
	[RPC]
	public void SendName(string oppName)
	{
		gameMain.player2Name = oppName;
		if (gameMain.begun)
		{
			gameMain.QuitGame();
		}
		gameMain.netPlay = true;
		gameMain.ChooseChars();
		gameMain.DinfHealth = false;
		gameMain.bInfTime = false;
	}
	// called when both players have chosen characters.
	public void CharactersChosen()
	{
		if (!bStarted)
		{
			bStarted = true;
			//gameMain.CharsDone();
			GetComponent<NetworkView>().RPC("StartUp",RPCMode.All);
		}
	}
	
	
	// TagNetwork is used to assign tags to each of the input gameObjects so they can be referenced. Called by StartUp.
	[RPC]
	public void TagNetwork(NetworkViewID objectID)
	{
		var netInput = NetworkView.Find(objectID);
		Debug.Log("netInput = "+netInput);
		
		if (Network.isServer)
		{
			netInput.tag = "input2";
		}
		else
		{
			netInput.tag = "input1";
		}
		netTagged = true;
	}
	
	public void SendFighterTag(NetworkViewID fighterID,string tagName)
	{
		GetComponent<NetworkView>().RPC("TagFightNetwork",RPCMode.Others,fighterID,tagName);
	}
	
	[RPC]
	public void TagFightNetwork(NetworkViewID objectID,string netTag)
	{
		var netFighter = NetworkView.Find(objectID);
		Debug.Log("netFighter = "+netFighter);
		netFighter.tag = netTag;
		
	}
	// startUp instantiates the input prefabs and assign tags on the local version. Called by CharactersChosen.
	[RPC]
	void StartUp()
	{
		gameMain.CharsDone();
		input = Network.Instantiate(cInputFab,Vector3.zero,Quaternion.identity,0) as GameObject; //create playerInputs on both players
		Debug.Log("instantiating input");
		
		if (Network.isServer)
		{
			input.tag = "input1";
		}
		else
		{
			input.tag = "input2";
		}
			
		GetComponent<NetworkView>().RPC("TagNetwork",RPCMode.Others,input.GetComponent<NetworkView>().viewID);
		Debug.Log (Network.player + " loading fighters");
	}
	
	public void startSetFighters()
	{
		StartCoroutine(SetFighters());
	}
	
	[RPC]
	void SpectatorJoin(int time,int p1health,int p2health,int p1hype,int p2hype,float camX,float camY)
	{
		gameMain.SpectatorSetup(time,camX,camY);
		// update hud and timer for spectator.
		// move fighters to 0 z for spectator.
		
	}
	
	// wait for a fraction and assign fighters and controllers to variables.
	IEnumerator SetFighters()
	{
		yield return new WaitForSeconds(1);
		p1fighter = GameObject.FindGameObjectWithTag("fighter1");
		p1fighterCon = p1fighter.GetComponent<FighterController>();
		p2fighter = GameObject.FindGameObjectWithTag("fighter2");
		p2fighterCon = p2fighter.GetComponent<FighterController>();
		Debug.Log ("player" +Network.player + " p1 fighter = " +p1fighter);
		Debug.Log ("player" +Network.player + " p2 fighter = " +p2fighter);
		fightersSet = true;
	}
	
	
	// tell the opponent a fighter has died
	public IEnumerator SendKo(bool f1)
	{
		yield return new WaitForSeconds(0.2f);	
		GetComponent<NetworkView>().RPC("RecieveKo",RPCMode.Others,f1);
	}
	
	[RPC]
	void RecieveKo (bool f1)
	{
		
		if (gameMain.bRoundStarted)
		{
			gameMain.bRoundStarted = false;
			gameMain.bRunGame = false;
			if (f1)
			{
				gameMain.EndRound("fighter1");
			}
			else
			{
				gameMain.EndRound("fighter2");	
			}
		}	
	}
	
	
	public void SendRematch()
	{
		// when the client wants to rematch
		if (Network.isClient)
		{
			GetComponent<NetworkView>().RPC("RecieveRematch",RPCMode.Others);
		}
		else if (Network.isServer)
		{
			GetComponent<NetworkView>().RPC("RecieveRematch",RPCMode.Others);
		}
	}
	
	[RPC]
	void RecieveRematch()
	{
		print ("recieved rematch");
		if (Network.isServer)
		{
			gameMain.clientRematch = true;
		}
		
		if (Network.isClient)
		{
			gameMain.servRematch = true;
		}
	}
	
	
	public void SendSpectatorRematch()
	{
		GetComponent<NetworkView>().RPC("SpectatorRematch",RPCMode.Others);
	}
	
	[RPC]
	void SpectatorRematch()
	{
		if (gameMain.isSpectator)
		{
			gameMain.clientRematch = true;
			gameMain.servRematch = true;
		}
	}
	
	public void SendReset()
	{
		// when the client wants to rematch
		if (Network.isClient)
		{
			gameMain.bRestartClient = true;
			GetComponent<NetworkView>().RPC("RecieveReset",RPCMode.Server);
		}
		else if (Network.isServer)
		{
			gameMain.bRestartServer = true;
			GetComponent<NetworkView>().RPC("RecieveReset",RPCMode.Others);
		}
	}
	
	[RPC]
	void RecieveReset()
	{
		if (Network.isServer)
		{
			gameMain.bRestartClient = true;
		}
		else if (Network.isClient)
		{
			gameMain.bRestartServer = true;
		}
	}
	
	public void SendFighterChoice(int choice)
	{
		if (Network.isServer)
		{
			GetComponent<NetworkView>().RPC("RecieveFighterChoice",RPCMode.Others,choice);
		}
		else
		{
			GetComponent<NetworkView>().RPC("RecieveFighterChoice",RPCMode.Server,choice);
		}
	}
	
	[RPC]
	public void RecieveFighterChoice(int choice)
	{
		if (Network.isServer)
		{
			if (choice == 0)
			{
				gameMain.bP2ChoseChar = true;
				gameMain.fighterChoice02 = gameMain.regChar;
				gameMain.netChose = gameMain.regChar;
			}
			else if (choice == 1)
			{
				gameMain.bP2ChoseChar = true;
				gameMain.fighterChoice02 = gameMain.kazChar;
				gameMain.netChose = gameMain.kazChar;
			}
			else if (choice == 2)
			{
				gameMain.bP2ChoseChar = true;
				gameMain.fighterChoice02 = gameMain.matChar;
				gameMain.netChose = gameMain.matChar;
			}
			else
			{
				Debug.Log ("no choice recieved");
			}
			
		}
		else
		{
			if (choice == 0)
			{
				gameMain.bP1ChoseChar = true;
				gameMain.fighterChoice01 = gameMain.regChar;
				gameMain.netChose = gameMain.regChar;
			}
			else if (choice == 1)
			{
				gameMain.bP1ChoseChar = true;
				gameMain.fighterChoice01 = gameMain.kazChar;
				gameMain.netChose = gameMain.kazChar;
			}
			else if (choice == 2)
			{
				gameMain.bP1ChoseChar = true;
				gameMain.fighterChoice01 = gameMain.matChar;
				gameMain.netChose = gameMain.matChar;
			}
			else
			{
				Debug.Log ("no choice recieved");
			}
			
		}
	}
	
	public void SendNetCharChange()
	{
		//send opponent to character change screen
		
		GetComponent<NetworkView>().RPC("RecieveNetCharChange",RPCMode.Others);		
	}
	
	[RPC]
	public void RecieveNetCharChange()
	{
		if (!gameMain.isSpectator)
		{
			gameMain.bChooseChar = true;
			gameMain.cSel.OnScreen();
			gameMain.CloseGameOverMenu();
			gameMain.CharChangeReset();
		}
	}
	
	public void Disconnect()
	{
		Network.Disconnect();
		Destroy(p1fighter);
		Destroy (p2fighter);
		Destroy (gameMain.player);
	}
	
	public void PreNetUpdate()
	{
		//Debug.Log ("running preNetUpdate");

	}
	
	[RPC]
	public void NetUpdate(bool p2,float p1netx,float p1nety,float p2netx,float p2nety)
	{
		if (Network.isServer)
		{

		}
	}
	
	
	public void ResetBuffer()
	{
		//print (Network.time+ "new round resetting buffers");
		//savedBuffer = localBufState;
		//localBufState = new State[30000];
		//savedState = new GameState[30000];
		
	//	WipeOldStates(0,lastGoodState);
		currentRollThread = 0;
		for (int i = 0; i < lastGoodGameState.Length; i++)
		{
			// clear each state from here to old state.
			lastGoodGameState[i] = 0;
			
		}
		lastGoodState = 0;
		
		//gameStateSaveCount = 0;
		//playingState = localBufState[0];
		//lastSavedGameState = roundtwolast;
		
		
		////print ("new round "+savedState[gameMain.frameCount].timestamp);
	}
	/*
	public void SendState()
	{
		//print (Network.time +" sendInputs" + localBufState[gameMain.frameCount].timestamp);
		//once we recieve info from other client we store all inputs into a state and send that state back to other client to compare.
		State[] tempStates = new State[3];
		tempStates[0] = localBufState[gameMain.frameCount];
		tempStates[0].rollThread = currentRollThread;
		if (gameMain.frameCount > 3)
		{
			tempStates[1] = localBufState[gameMain.frameCount-1];
			tempStates[2] = localBufState[gameMain.frameCount-2];
			tempStates[1].rollThread = currentRollThread;
			tempStates[2].rollThread = currentRollThread;
		}
		
		
			
		
		
		var b = new BinaryFormatter();    
		//Create an in memory stream    
		var m = new MemoryStream();    
		//Save the scores    
		b.Serialize(m, tempStates);
		
		networkView.RPC("RecieveState",RPCMode.Others,gameMain.frameCount, m.GetBuffer());
	}
	
	[RPC]
	public void RecieveState(int timestamp, byte[] netByte)
	{
		var b = new BinaryFormatter();            
		var m = new MemoryStream(netByte);    
		State[] netStates = new State[3];

		netStates = b.Deserialize(m) as State[];
		
		if (netStates[0].rollThread == currentRollThread)
		{
			////print ("recieved attack "+a11);
			networkBufState[0] = netStates[0];
			CompareInputs(timestamp,0); // compare local state to net state.
			
			if (gameMain.frameCount > 3)
			{
				networkBufState[1] = netStates[1];
				networkBufState[2] = netStates[2];
				////print (gameMain.lagTimer +"recieveState "+timestamp);
				CompareInputs(timestamp-1,1);
				CompareInputs(timestamp-2,2);
			}
			//print (Network.time +" recieveInputs " + timestamp);
		}
		else
		{
			//print("ignoring old recieved inputs "+netStates[0].rollThread+" "+currentRollThread);
		}
	}
	
	public void CompareInputs(int timestamp,int num)
	{
		//go through the inputs of this state with that of the recieved state and see if they sync. If not we rollback.
		//print (Network.time+" comparing Inputs at "+timestamp+" "+networkBufState[num].timestamp);
		bool[] attackButtons = new bool[20];
		
		if (timestamp == networkBufState[num].timestamp)
		{
			if (localBufState[timestamp].attack01ButtonDown != networkBufState[num].attack01ButtonDown)
			{
				// states do not match rollback
				//print (Time.time+" att11 do not match");
				attackButtons[0] = true;
				bRollBackNeeded = true;
				if (networkBufState[num].b01dframe3 || localBufState[timestamp].b01dframe3)
				{
					localBufState[timestamp-2].b01dframe1 = true;
					localBufState[timestamp-1].b01dframe2 = true;
					localBufState[timestamp-2].attack01ButtonDown = true;
					localBufState[timestamp-1].attack01ButtonDown = true;
				}
				else if (networkBufState[num].b01dframe2|| localBufState[timestamp].b01dframe2)
				{
					localBufState[timestamp-1].b01dframe1 = true;
					localBufState[timestamp+1].b01dframe3 = true;
					localBufState[timestamp-1].attack01ButtonDown = true;
					localBufState[timestamp+1].attack01ButtonDown = true;
				}
				else if (networkBufState[num].b01dframe1|| localBufState[timestamp].b01dframe1)
				{
					localBufState[timestamp+1].b01dframe2 = true;
					localBufState[timestamp+2].b01dframe3 = true;
					localBufState[timestamp+1].attack01ButtonDown = true;
					localBufState[timestamp+2].attack01ButtonDown = true;
				}
			} 
			if (localBufState[timestamp].attack01ButtonDown2 != networkBufState[num].attack01ButtonDown2)
			{
				// states do not match rollback
				//print (Time.time+" att12 do not match");
				attackButtons[4] = true;
				bRollBackNeeded = true;
				if (networkBufState[num].b01d2frame3 || localBufState[timestamp].b01d2frame3)
				{
					localBufState[timestamp-2].b01d2frame1 = true;
					localBufState[timestamp-1].b01d2frame2 = true;
					localBufState[timestamp-2].attack01ButtonDown2 = true;
					localBufState[timestamp-1].attack01ButtonDown2 = true;
				}
				else if (networkBufState[num].b01dframe2 || localBufState[timestamp].b01d2frame2)
				{
					localBufState[timestamp-1].b01d2frame1 = true;
					localBufState[timestamp+1].b01d2frame3 = true;
					localBufState[timestamp-1].attack01ButtonDown2 = true;
					localBufState[timestamp+1].attack01ButtonDown2 = true;
				}
				else if (networkBufState[num].b01dframe1 || localBufState[timestamp].b01d2frame1)
				{
					localBufState[timestamp+1].b01d2frame2 = true;
					localBufState[timestamp+2].b01d2frame3 = true;
					localBufState[timestamp+1].attack01ButtonDown2 = true;
					localBufState[timestamp+2].attack01ButtonDown2 = true;
				}
			}
			if (localBufState[timestamp].attack02ButtonDown != networkBufState[num].attack02ButtonDown)
			{
				// states do not match rollback
				//print (Time.time+" att21 do not match");
				attackButtons[1] = true;
				bRollBackNeeded = true;
				if (networkBufState[num].b02dframe3 || localBufState[timestamp].b02dframe3)
				{
					localBufState[timestamp-2].b02dframe1 = true;
					localBufState[timestamp-1].b02dframe2 = true;
					localBufState[timestamp-2].attack02ButtonDown = true;
					localBufState[timestamp-1].attack02ButtonDown = true;
				}
				else if (networkBufState[num].b02dframe2 || localBufState[timestamp].b02dframe2)
				{
					localBufState[timestamp-1].b02dframe1 = true;
					localBufState[timestamp+1].b02dframe3 = true;
					localBufState[timestamp-1].attack02ButtonDown = true;
					localBufState[timestamp+1].attack02ButtonDown = true;
				}
				else if (networkBufState[num].b02dframe1 || localBufState[timestamp].b02dframe1)
				{
					localBufState[timestamp+1].b02dframe2 = true;
					localBufState[timestamp+2].b02dframe3 = true;
					localBufState[timestamp+1].attack02ButtonDown = true;
					localBufState[timestamp+2].attack02ButtonDown = true;
				}
			} 
			if (localBufState[timestamp].attack02ButtonDown2 != networkBufState[num].attack02ButtonDown2)
			{
				// states do not match rollback
				//print (Time.time+" att22 do not match");
				attackButtons[5] = true;
				bRollBackNeeded = true;
				if (networkBufState[num].b02d2frame3 || localBufState[timestamp].b02d2frame3)
				{
					localBufState[timestamp-2].b02d2frame1 = true;
					localBufState[timestamp-1].b02d2frame2 = true;
					localBufState[timestamp-2].attack02ButtonDown2 = true;
					localBufState[timestamp-1].attack02ButtonDown2 = true;
				}
				else if (networkBufState[num].b02dframe2 || localBufState[timestamp].b02d2frame2)
				{
					localBufState[timestamp-1].b02d2frame1 = true;
					localBufState[timestamp+1].b02d2frame3 = true;
					localBufState[timestamp-1].attack02ButtonDown2 = true;
					localBufState[timestamp+1].attack02ButtonDown2 = true;
				}
				else if (networkBufState[num].b02dframe1 || localBufState[timestamp].b02d2frame1)
				{
					localBufState[timestamp+1].b02d2frame2 = true;
					localBufState[timestamp+2].b02d2frame3 = true;
					localBufState[timestamp+1].attack02ButtonDown2 = true;
					localBufState[timestamp+2].attack02ButtonDown2 = true;
				}
			}
			if (localBufState[timestamp].attack03ButtonDown != networkBufState[num].attack03ButtonDown)
			{
				// states do not match rollback
				//print (Network.time+" att31 do not match");
				attackButtons[2] = true;
				bRollBackNeeded = true;
			} 
			if (localBufState[timestamp].attack03ButtonDown2 != networkBufState[num].attack03ButtonDown2)
			{
				// states do not match rollback
				//print (Network.time+" att32 do not match");
				attackButtons[6] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].attack04ButtonDown != networkBufState[num].attack04ButtonDown)
			{
				// states do not match rollback
				//print (Network.time+" att41 do not match");
				attackButtons[3] = true;
				bRollBackNeeded = true;
			} 
			if (localBufState[timestamp].attack04ButtonDown2 != networkBufState[num].attack04ButtonDown2)
			{
				// states do not match rollback
				//print (Network.time+" att42 do not match");
				attackButtons[7] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialQCR != networkBufState[num].specialQCR)
			{
				//print (Network.time+" QCR1 does not match");
				attackButtons[8] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialQCL != networkBufState[num].specialQCL)
			{
				//print (Network.time+" QCL1 does not match");
				attackButtons[9] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialDPR != networkBufState[num].specialDPR)
			{
				//print (Network.time+" DPR1 does not match");
				attackButtons[10] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialDPL != networkBufState[num].specialDPL)
			{
				//print (Network.time+" DPL1 does not match");
				attackButtons[11] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialHCL != networkBufState[num].specialHCL)
			{
				//print (Network.time+" HCL1 does not match");
				attackButtons[12] = true;
				bRollBackNeeded = true;
			}
			
			if (localBufState[timestamp].specialQCR2 != networkBufState[num].specialQCR2)
			{
				//print (Network.time+" QCR2 does not match");
				attackButtons[13] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialQCL2 != networkBufState[num].specialQCL2)
			{
				//print (Network.time+" QCL2 does not match");
				attackButtons[14] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialDPR2 != networkBufState[num].specialDPR2)
			{
				//print (Network.time+" DPR2 does not match");
				attackButtons[15] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialDPL2 != networkBufState[num].specialDPL2)
			{
				//print (Network.time+" DPL2 does not match");
				attackButtons[16] = true;
				bRollBackNeeded = true;
			}
			if (localBufState[timestamp].specialHCL2 != networkBufState[num].specialHCL2)
			{
				//print (Network.time+" HCL2 does not match");
				attackButtons[17] = true;
				bRollBackNeeded = true;
			}
			if (bRollBackNeeded)
			{
				if (timestamp != lastSent)
				{
					RollbackAttack(timestamp,attackButtons);
					bRollBackNeeded = false;
					lastSent = timestamp;
				}
			}
			else
			{
				// states match let the opponent know.
				
				//print (Network.time+ " Inputs match " +timestamp);
				localBufState[timestamp].localApproval = true;
				networkView.RPC("RecieveApprove",RPCMode.Others,timestamp);
				////print ("playingstate = "+playingState.timestamp);
				//}
			}
		}
	}
	
	[RPC]
	public void RecieveApprove (int timestamp)
	{
		// our opponent has checked a state and found it matches its own. when we rollback it will be to this state.
		localBufState[timestamp].netApproval = true;
		////print (gameMain.lagTimer +"State Net approved "+timestamp+" " + localBufState[timestamp].netApproval +" "+ localBufState[timestamp].localApproval);
		if (localBufState[timestamp].localApproval)
		{
			
			lastGoodState = timestamp;
			//WipeOldStates(lastGoodState,gameMain.frameCount);
			//playingState = localBufState[lastGoodState]; // these are needed to work
			networkView.RPC("DoubleCheck",RPCMode.Others,timestamp);
		}
	}
	
	[RPC]
	void DoubleCheck (int timestamp)
	{
		lastGoodState = timestamp;
		//WipeOldStates(timestamp,gameMain.frameCount);
		//playingState = localBufState[lastGoodState]; // these are needed to work
	}
	
	void RollbackAttack(int timestamp,bool[] attackButtons)
	{
		//if (timestamp > lastGoodState)
		//{
		//WipeOldStates(timestamp,gameMain.frameCount);
		//gameMain.frameCount = timestamp;
		////print ("attack rollback to "+timestamp);
		//check for each attack for each fighter.
		
		
		if (attackButtons[0])
		{
			localBufState[timestamp].attack01ButtonDown = true;
		}
		if (attackButtons[1])
		{
			localBufState[timestamp].attack02ButtonDown = true;
		}
		if (attackButtons[2])
		{
			localBufState[timestamp].attack03ButtonDown = true;
		}
		if (attackButtons[3])
		{
			localBufState[timestamp].attack04ButtonDown = true;
		}
		if (attackButtons[4])
		{
			localBufState[timestamp].attack01ButtonDown2 = true;
		}
		if (attackButtons[5])
		{
			localBufState[timestamp].attack02ButtonDown2 = true;
		}
		if (attackButtons[6])
		{
			localBufState[timestamp].attack03ButtonDown2 = true;
		}
		if (attackButtons[7])
		{
			localBufState[timestamp].attack04ButtonDown2 = true;
		}
		if (attackButtons[8])
		{
			localBufState[timestamp].specialQCR = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[9])
		{
			localBufState[timestamp].specialQCL = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[10])
		{
			localBufState[timestamp].specialDPR = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[11])
		{
			localBufState[timestamp].specialDPL = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[12])
		{
			localBufState[timestamp].specialHCL = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		
		if (attackButtons[13])
		{
			localBufState[timestamp].specialQCR2 = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[14])
		{
			localBufState[timestamp].specialQCL2 = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[15])
		{
			localBufState[timestamp].specialDPR2 = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[16])
		{
			localBufState[timestamp].specialDPL2 = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		if (attackButtons[17])
		{
			localBufState[timestamp].specialHCL2 = true;
			////print (gameMain.frameCount+" special repaired"+localBufState[timestamp].timestamp);
		}
		// problem is if this happens too late then it gets missed and overwritten by our next frame.
		// we should put a bool in gamemain that checks to see if we are rollingback or not.
		//playingState = localBufState[timestamp];
		//bRolling = true; // turned off in gameMain
		
		// need to send array over network
		//Get a binary formatter    
		var b = new BinaryFormatter();    
		//Create an in memory stream    
		var m = new MemoryStream();    
		//Save the scores    
		b.Serialize(m, attackButtons);
		//networkView.RPC("RewindStateAttack",RPCMode.Others,timestamp,m.GetBuffer());
		
		//print (Network.time+ "corrected inputs");
		// need to create a catch up function to delay restarting play until caught up.
		
		// maybe send actions from fighter rather than inputs...
		//}
		//else
		//{
		//	//print ("too old");
		//}
	}
		
	void Rollback(int timestamp)
	{
		// on each client we have a function comparing states. each time a comparison is good the client sends an approval message to the other client. 
		// When we determine a rollback is necessary we call this function to run through previous states until it finds one that has approval from both clients.
		// Once it determines which state we are rolling back to then it goes through the process of resetting us back to the approved frame.
		// //print("rolling back");
		
		//WipeOldStates(lastGoodState,gameMain.frameCount);
		gameMain.frameCount = lastGoodState;
		//playingState = localBufState[lastGoodState];
		networkView.RPC("RewindState",RPCMode.Others,lastGoodState);
	}

	[RPC]
	void RewindState (int timestamp)
	{
		if (timestamp == lastGoodState)
		{
			//WipeOldStates(lastGoodState,gameMain.frameCount);
			////print (gameMain.lagTimer +" rewinding to "+timestamp);
			gameMain.frameCount = timestamp;
			//playingState = localBufState[lastGoodState];
			
		}
		else
		{
			////print (gameMain.lagTimer +" last good states do not match! "+timestamp+" "+lastGoodState + "---------------------------------------------------------");
			// TEMPORARY UNTIL I CAN FIND A BETTER FIX
			////print ("rewinding to "+timestamp);
			//gameMain.frameCount = timestamp;
			//playingState = localBufState[timestamp];
		}
	}
	
	[RPC]
	void RewindStateAttack (int timestamp,byte[] netByte)
	{
		////print ("attack rewinding to "+timestamp);
		var b = new BinaryFormatter();        
		//Create a memory stream with the data    
		var m = new MemoryStream(netByte);    
		//Load back the scores    
		//(string[])soapFormat.Deserialize(fstream);
		//var netGameState = b.Deserialize(m) as Array;
		bool[] attackButtons = new bool[20];

		attackButtons = b.Deserialize(m) as bool[];
		
		
		//check for each attack for each fighter.
		if (attackButtons[0])
		{
			localBufState[timestamp].attack01ButtonDown = true;
		}
		if (attackButtons[1])
		{
			localBufState[timestamp].attack02ButtonDown = true;
		}
		if (attackButtons[2])
		{
			localBufState[timestamp].attack03ButtonDown = true;
		}
		if (attackButtons[3])
		{
			localBufState[timestamp].attack04ButtonDown = true;
		}
		if (attackButtons[4])
		{
			localBufState[timestamp].attack01ButtonDown2 = true;
		}
		if (attackButtons[5])
		{
			localBufState[timestamp].attack02ButtonDown2 = true;
		}
		if (attackButtons[6])
		{
			localBufState[timestamp].attack03ButtonDown2 = true;
		}
		if (attackButtons[7])
		{
			localBufState[timestamp].attack04ButtonDown2 = true;
		}
		if (attackButtons[8])
		{
			localBufState[timestamp].specialQCR = true;
		}
		if (attackButtons[9])
		{
			localBufState[timestamp].specialQCL = true;
		}
		if (attackButtons[10])
		{
			localBufState[timestamp].specialDPR = true;
		}
		if (attackButtons[11])
		{
			localBufState[timestamp].specialDPL = true;
		}
		if (attackButtons[12])
		{
			localBufState[timestamp].specialHCL = true;
		}
		//playingState = localBufState[timestamp];
		//bRolling = true; // turned off in gameMain
		
	}
	
	
	void FullStateSave()
	{
		
		// every interval we record the entire game state and send to opponent.
	//	//print ("saving gameState" +gameMain.frameCount);
		// controller
		var time = gameMain.frameCount;
	
		savedState[time].timestamp = gameMain.frameCount;
		savedState[time].ranYet = true;
		savedState[time].rollThread = currentRollThread;
		
		savedState[time].p1Hp = p1fighterCon.stats.health;
		savedState[time].p1XPos = p1fighterCon.transform.position.x;
		savedState[time].p1YPos = p1fighterCon.transform.position.y;
		savedState[time].p1Hype = p1fighterCon.hype.hypeAmount;
		savedState[time].bProjThrown1 = p1fighterCon.bProjThrown;
		savedState[time].bJumping1 = p1fighterCon.bJumping;
		savedState[time].bJumpStart1 = p1fighterCon.bJumpStart;
		savedState[time].bJumpForward1 = p1fighterCon.bJumpForward;
		savedState[time].bJumpBackward1 = p1fighterCon.bJumpBackward;
		savedState[time].bJumpFalling1 = p1fighterCon.bJumpFalling;
		savedState[time].bJumpAttack1 = p1fighterCon.bJumpAttack;
		savedState[time].bGrounded1 = p1fighterCon.bGrounded;
		savedState[time].currentAnim1 = p1fighterCon.animations.currentAnim;
		savedState[time].animTime1 = p1fighterCon.animations.animation[p1fighterCon.animations.currentAnim].time;
		savedState[time].bJumpEnded1 =  p1fighterCon.jump.bJumpEnded;
		savedState[time].height1 = p1fighterCon.jump.height;
		savedState[time].length1 = p1fighterCon.jump.length;
		/*
		savedState[time].bFacingRight1 = p1fighterCon.bFacingRight;
		savedState[time].bCrouching1 = p1fighterCon.bCrouching;
		savedState[time].bMoving1 = p1fighterCon.bMoving;
		savedState[time].bMoveForward1 = p1fighterCon.bMoveForward;
		savedState[time].bMoveBackward1 = p1fighterCon.bMoveBackward;
		
		savedState[time].bLanded1 = p1fighterCon.bLanded;
		
		savedState[time].bKnockdown1 = p1fighterCon.bKnockdown;
		savedState[time].bKnockedOut1 = p1fighterCon.bKnockedOut;
		savedState[time].bPushed1 = p1fighterCon.bPushed;
		savedState[time].bNoPush1 = p1fighterCon.bNoPush;
		savedState[time].bHit1 = p1fighterCon.bHit;
		savedState[time].bHitSlide1 = p1fighterCon.bHitSlide;
		savedState[time].bDanger1 = p1fighterCon.bDanger;		
		savedState[time].bBlocking1 = p1fighterCon.bBlocking;
		savedState[time].bBlockStun1 = p1fighterCon.bBlockStun;
		savedState[time].bThrown1 = p1fighterCon.bThrown;
		savedState[time].bBouncing1 = p1fighterCon.bBouncing;
		savedState[time].bParryWin1 = p1fighterCon.bParryWin;
		savedState[time].bParryAttempt1 = p1fighterCon.bParryAttempt;
		savedState[time].bParrying1 = p1fighterCon.bParrying;
		savedState[time].bPaused1 = p1fighterCon.bPaused;
		savedState[time].currentDist1 = p1fighterCon.currentDist;
		savedState[time].hitDist1 = p1fighterCon.hitDist;
		savedState[time].bOnRightWall1 = p1fighterCon.bOnRightWall;
		savedState[time].bOnLeftWall1 = p1fighterCon.bOnLeftWall;
		savedState[time].pushBack1 = p1fighterCon.pushBack;
		savedState[time].noPushBack1 = p1fighterCon.noPushBack;
		savedState[time].bAttacking1 = p1fighterCon.bAttacking;
		savedState[time].bThrowing1 = p1fighterCon.bThrowing;
		savedState[time].bAirThrowL1 = p1fighterCon.bAirThrowL;
		savedState[time].bAirThrowH1 = p1fighterCon.bAirThrowH;
		savedState[time].bAirThrowEX1 = p1fighterCon.bAirThrowEX;
		savedState[time].bCThrowL1 = p1fighterCon.bCThrowL;
		savedState[time].bCThrowH1 = p1fighterCon.bCThrowH;
		savedState[time].bCThrowEX1 = p1fighterCon.bCThrowEX;
		savedState[time].bCThrowingL1 = p1fighterCon.bCThrowingL;
		savedState[time].bCThrowingH1 = p1fighterCon.bCThrowingH;
		savedState[time].bAThrowingL1 = p1fighterCon.bAThrowingL;
		savedState[time].bAThrowingH1 = p1fighterCon.bAThrowingH;
		savedState[time].bAThrowingEX1 = p1fighterCon.bAThrowingEX;
		savedState[time].tempThrowDam1 = p1fighterCon.tempThrowDam;
		
		savedState[time].bCanceling1 = p1fighterCon.bCanceling;
		
		// activate
		savedState[time].specialCheckR011 = p1fighterCon.fighterActivate.specialCheckR01;
		savedState[time].specialCheckL011 = p1fighterCon.fighterActivate.specialCheckL01;
		savedState[time].specialCheckR0b1 = p1fighterCon.fighterActivate.specialCheckR0b;
		savedState[time].specialCheckL0b1 = p1fighterCon.fighterActivate.specialCheckL0b;
		savedState[time].specialCheck031 = p1fighterCon.fighterActivate.specialCheck03;
		savedState[time].specialCheck041 = p1fighterCon.fighterActivate.specialCheck04;
		
		// fighter2
		// controller
		
		savedState[time].p2Hp = p2fighterCon.stats.health;
		savedState[time].p2XPos = p2fighterCon.transform.position.x;
		savedState[time].p2YPos = p2fighterCon.transform.position.y;
		savedState[time].p2Hype = p2fighterCon.hype.hypeAmount;
		savedState[time].bProjThrown2 = p2fighterCon.bProjThrown;
		savedState[time].bJumping2 = p2fighterCon.bJumping;
		savedState[time].bJumpStart2 = p2fighterCon.bJumpStart;
		savedState[time].bJumpForward2 = p2fighterCon.bJumpForward;
		savedState[time].bJumpBackward2 = p2fighterCon.bJumpBackward;
		savedState[time].bJumpFalling2 = p2fighterCon.bJumpFalling;
		savedState[time].bGrounded2 = p2fighterCon.bGrounded;
		savedState[time].currentAnim2 = p2fighterCon.animations.currentAnim;
		savedState[time].animTime2 = p2fighterCon.animations.animation[p2fighterCon.animations.currentAnim].time;
		savedState[time].bJumpEnded2 =  p2fighterCon.jump.bJumpEnded;
		savedState[time].height2 = p2fighterCon.jump.height;
		savedState[time].length2 = p2fighterCon.jump.length;
		
		savedState[time].bFacingRight2 = p2fighterCon.bFacingRight;
		savedState[time].bCrouching2 = p2fighterCon.bCrouching;
		savedState[time].bMoving2 = p2fighterCon.bMoving;
		savedState[time].bMoveForward2 = p2fighterCon.bMoveForward;
		savedState[time].bMoveBackward2 = p2fighterCon.bMoveBackward;
	
		savedState[time].bJumpAttack2 = p2fighterCon.bJumpAttack;
		savedState[time].bLanded2 = p2fighterCon.bLanded;
		
		savedState[time].bKnockdown2 = p2fighterCon.bKnockdown;
		savedState[time].bKnockedOut2 = p2fighterCon.bKnockedOut;
		savedState[time].bPushed2 = p2fighterCon.bPushed;
		savedState[time].bNoPush2 = p2fighterCon.bNoPush;
		savedState[time].bHit2 = p2fighterCon.bHit;
		savedState[time].bHitSlide2 = p2fighterCon.bHitSlide;
		savedState[time].bDanger2 = p2fighterCon.bDanger;		
		savedState[time].bBlocking2 = p2fighterCon.bBlocking;
		savedState[time].bBlockStun2 = p2fighterCon.bBlockStun;
		savedState[time].bThrown2 = p2fighterCon.bThrown;
		savedState[time].bBouncing2 = p2fighterCon.bBouncing;
		savedState[time].bParryWin2 = p2fighterCon.bParryWin;
		savedState[time].bParryAttempt2 = p2fighterCon.bParryAttempt;
		savedState[time].bParrying2 = p2fighterCon.bParrying;
		savedState[time].bPaused2 = p2fighterCon.bPaused;
		savedState[time].currentDist2 = p2fighterCon.currentDist;
		savedState[time].hitDist2 = p2fighterCon.hitDist;
		savedState[time].bOnRightWall2 = p2fighterCon.bOnRightWall;
		savedState[time].bOnLeftWall2 = p2fighterCon.bOnLeftWall;
		savedState[time].pushBack2 = p2fighterCon.pushBack;
		savedState[time].noPushBack2 = p2fighterCon.noPushBack;
		savedState[time].bAttacking2 = p2fighterCon.bAttacking;
		savedState[time].bThrowing2 = p2fighterCon.bThrowing;
		savedState[time].bAirThrowL2 = p2fighterCon.bAirThrowL;
		savedState[time].bAirThrowH2 = p2fighterCon.bAirThrowH;
		savedState[time].bAirThrowEX2 = p2fighterCon.bAirThrowEX;
		savedState[time].bCThrowL2 = p2fighterCon.bCThrowL;
		savedState[time].bCThrowH2 = p2fighterCon.bCThrowH;
		savedState[time].bCThrowEX2 = p2fighterCon.bCThrowEX;
		savedState[time].bCThrowingL2 = p2fighterCon.bCThrowingL;
		savedState[time].bCThrowingH2 = p2fighterCon.bCThrowingH;
		savedState[time].bAThrowingL2 = p2fighterCon.bAThrowingL;
		savedState[time].bAThrowingH2 = p2fighterCon.bAThrowingH;
		savedState[time].bAThrowingEX2 = p2fighterCon.bAThrowingEX;
		savedState[time].tempThrowDam2 = p2fighterCon.tempThrowDam;
		
		savedState[time].bCanceling2 = p2fighterCon.bCanceling;
		
		// activate
		savedState[time].specialCheckR012 = p2fighterCon.fighterActivate.specialCheckR01;
		savedState[time].specialCheckL012 = p2fighterCon.fighterActivate.specialCheckL01;
		savedState[time].specialCheckR0b2 = p2fighterCon.fighterActivate.specialCheckR0b;
		savedState[time].specialCheckL0b2 = p2fighterCon.fighterActivate.specialCheckL0b;
		savedState[time].specialCheck032 = p2fighterCon.fighterActivate.specialCheck03;
		savedState[time].specialCheck042 = p2fighterCon.fighterActivate.specialCheck04;
		
		lastSavedGameState = savedState[time];
		
	}
	
	public void SendGameState()
	{
		var time = lastSavedGameState.timestamp;
	//	//print (time);
		//Get a binary formatter    
		var b = new BinaryFormatter();    
		//Create an in memory stream    
		var m = new MemoryStream();    
		//Save the scores    
		
		b.Serialize(m, savedState[time]);
		networkView.RPC("GameStateRecieve",RPCMode.Others,m.GetBuffer());
	
	}
	
	[RPC]
	void GameStateRecieve(byte[] netByte)
	{
		
		var b = new BinaryFormatter();        
		//Create a memory stream with the data    
		var m = new MemoryStream(netByte);    
		//Load back the scores    
		//(string[])soapFormat.Deserialize(fstream);
		//var netGameState = b.Deserialize(m) as Array;

		netGameState = b.Deserialize(m) as GameState;
		
		gameMain.stateRecieved = true;
	}
	
	
	public void GameStateCompare()
	{
		
		if (gameMain.stateRecieved)
		{

			var timestamp = netGameState.timestamp;
			
			// begin comparison
			if (timestamp < gameMain.frameCount)
			{
				//print (Network.time+" comparing states at frame " +timestamp);
				gameMain.stateRecieved = false;
				if ((netGameState.p1Hp != savedState[timestamp].p1Hp)||(netGameState.p2Hp != savedState[timestamp].p2Hp))
				{
					//print (Network.time+" ------- States Mismatch ------------- "+timestamp);
					//print ("net p1 = "+netGameState.p1Hp);
					//print ("loc p1 = "+savedState[timestamp].p1Hp);
					//print ("net p2 = "+netGameState.p2Hp);
					//print ("loc p2 = "+savedState[timestamp].p2Hp);
					
					// check states before and after
					if (netGameState.p2Hp != savedState[timestamp-1].p2Hp || netGameState.p2Hp != savedState[timestamp+1].p2Hp)
					{
						GameStateRollback();
					}
				}
				/*
				else if ((netGameState.bProjThrown1 != savedState[timestamp].bProjThrown1)||(netGameState.bProjThrown2 != savedState[timestamp].bProjThrown2))
				{
					//print (Network.time+" ------- States Mismatch ------------- "+timestamp);
					//print ("net p1 projectile = "+netGameState.bProjThrown1);
					//print ("loc p1 projectile = "+savedState[timestamp].bProjThrown1);
					GameStateRollback();
				}
				
				else
				{
					// if everything matches
					savedState[timestamp].localApproval = true;
					//print (Network.time+" state meets local approval" +timestamp);
					networkView.RPC("RecieveGameStateApprove",RPCMode.Others,timestamp);
				}
			}
			else
			{
				if (netGameState.rollThread < currentRollThread)
				{
					//print(Network.time+" state from old thread. Ignored "+netGameState.rollThread+" "+currentRollThread);
					//networkView.RPC("fixThread",RPCMode.Others,currentRollThread);
				}
				else if (netGameState.rollThread > currentRollThread)
				{
					//print(Network.time+" state from newer thread. updating our thread "+netGameState.rollThread+" "+currentRollThread);
					currentRollThread = netGameState.rollThread;
					//networkView.RPC("fixThread",RPCMode.Others,currentRollThread);
				}
				else
				{
					//print(Network.time+" game state is too far ahead "+timestamp +" "+gameMain.frameCount);
					//networkView.RPC("NetRevert",RPCMode.All,lastGoodGameState[0]);
				}
			}
		}
	}
	
	[RPC]
	void fixThread(int threadNum)
	{
		currentRollThread = threadNum;
	}
	// FULL ROLLBACK
	// at a lower interval we rollback the entire game state if necessary.
	void GameStateRollback()
	{
		bRolling = true;
		//print (Network.time+" ------- Rewinding State to "+lastGoodGameState[0]);
		currentRollThread++;
		WipeOldStates(lastGoodGameState[0],gameMain.frameCount);
		gameMain.frameCount = lastGoodGameState[0];
		
		savedState[lastGoodGameState[0]].failCount++;
		//print (Network.time+" state fail count "+savedState[lastGoodGameState[0]].failCount);
		if (savedState[lastGoodGameState[0]].failCount > 3)
		{
			//print (Network.time+" catastrophic damage, repairing");
			GameStateRepair();
			// a new function to find us a new lastGoodState
		}
		//print ("loc p1 = "+savedState[gameMain.frameCount].p1Hp);
		//print ("loc p2 = "+savedState[gameMain.frameCount].p2Hp);
		
		if (p1fighterCon.stats.health != 0)
		{
			p1fighterCon.stats.health = savedState[lastGoodGameState[0]].p1Hp;
		}
		if (p2fighterCon.stats.health != 0)
		{
			p2fighterCon.stats.health = savedState[lastGoodGameState[0]].p2Hp;
		}
		//p1fighter.transform.position = new Vector3(savedState[lastGoodGameState[0]].p1XPos,savedState[lastGoodGameState[0]].p1YPos,0);
		//p2fighter.transform.position = new Vector3(savedState[lastGoodGameState[0]].p2XPos,savedState[lastGoodGameState[0]].p2YPos,0);
		p1fighterCon.animations.animation.Stop(); // need to replace with exact animation frame.
		p2fighterCon.animations.animation.Stop(); // need to replace with exact animation frame.
		p1fighterCon.animations.animation.Play (savedState[lastGoodGameState[0]].currentAnim1);
		p1fighterCon.animations.animation[savedState[lastGoodGameState[0]].currentAnim1].time = savedState[lastGoodGameState[0]].animTime1;
		p2fighterCon.animations.animation.Play (savedState[lastGoodGameState[0]].currentAnim2);
		p2fighterCon.animations.animation[savedState[lastGoodGameState[0]].currentAnim2].time = savedState[lastGoodGameState[0]].animTime2;
		
		/*
		p1fighterCon.bGrounded = savedState[lastGoodGameState[0]].bGrounded1;
		p1fighterCon.bJumping = savedState[lastGoodGameState[0]].bJumping1;
		p1fighterCon.bJumpForward = savedState[lastGoodGameState[0]].bJumpForward1;
		p1fighterCon.bJumpBackward = savedState[lastGoodGameState[0]].bJumpBackward1;
		p1fighterCon.bJumpStart = savedState[lastGoodGameState[0]].bJumpStart1;
		p1fighterCon.bJumpFalling = savedState[lastGoodGameState[0]].bJumpFalling1;
		p1fighterCon.jump.bJumpEnded = savedState[lastGoodGameState[0]].bJumpFalling1;
		p1fighterCon.jump.height = savedState[lastGoodGameState[0]].height1;
		p1fighterCon.jump.length = savedState[lastGoodGameState[0]].length1;
		
		//print ("bGrounded "+p1fighterCon.bGrounded);
		//print ("bJumping "+p1fighterCon.bJumping);
		//print ("bJumpFor "+p1fighterCon.bJumpForward);
		//print ("bJumpBak "+p1fighterCon.bJumpBackward);
		//print ("bJumpStart "+p1fighterCon.bJumpStart);
		//print ("bJumpfall "+p1fighterCon.bJumpFalling);
		//print ("bJumpEnd "+p1fighterCon.jump.bJumpEnded);
		
		
		p2fighterCon.bGrounded = savedState[lastGoodGameState[0]].bGrounded2;
		p2fighterCon.bJumping = savedState[lastGoodGameState[0]].bJumping2;
		p2fighterCon.bJumpForward = savedState[lastGoodGameState[0]].bJumpForward2;
		p2fighterCon.bJumpBackward = savedState[lastGoodGameState[0]].bJumpBackward2;
		p2fighterCon.bJumpStart = savedState[lastGoodGameState[0]].bJumpStart2;
		p2fighterCon.bJumpFalling = savedState[lastGoodGameState[0]].bJumpFalling2;
		p2fighterCon.jump.height = savedState[lastGoodGameState[0]].height2;
		p2fighterCon.jump.length = savedState[lastGoodGameState[0]].length2;
		*/
		
		/*
		//undo KO
		if (!savedState[lastGoodGameState[0]].bKnockedOut1 && p1fighterCon.bKnockedOut)
		{
			gameMain.CancelEndRound();
			p1fighterCon.CancelKo();
			//print("KO is false!! Rollback "+lastGoodGameState[0]);
		}
		if (!savedState[lastGoodGameState[0]].bKnockedOut2 && p2fighterCon.bKnockedOut)
		{
			gameMain.CancelEndRound();
			p2fighterCon.CancelKo();
			//print("KO is false!! Rollback "+lastGoodGameState[0]);
		}
		
		//undo knockdown
		if (!savedState[lastGoodGameState[0]].bKnockdown2 && p2fighterCon.bKnockdown)
		{
			p2fighterCon.CancelKnockdown();
		}
		// stop attack
		if (!savedState[lastGoodGameState[0]].bAttacking1 && p1fighterCon.bAttacking)
		{
			p1fighterCon.StopAttacks();
		}
		//erase projectiles
		if (!savedState[lastGoodGameState[0]].bProjThrown1 && p1fighterCon.projectile != null)
		{
			p1fighterCon.KillProjectile();
		}
		if (!savedState[lastGoodGameState[0]].bProjThrown2 && p2fighterCon.projectile != null)
		{
			p2fighterCon.KillProjectile();
		}
		
		// send message to revert to opponent.
		networkView.RPC("NetRevert",RPCMode.Others,lastGoodGameState[0]);
	}
	
	[RPC]
	void NetRevert(int timestamp)
	{
		bRolling = true;
		//print (Network.time+" ------- Rewinding State to "+lastGoodGameState[0]);
		WipeOldStates(lastGoodGameState[0],gameMain.frameCount);
		currentRollThread++;
		gameMain.frameCount = lastGoodGameState[0];
		if (p1fighterCon.stats.health != 0)
		{
			p1fighterCon.stats.health = savedState[lastGoodGameState[0]].p1Hp;
		}
		if (p2fighterCon.stats.health != 0)
		{
			p2fighterCon.stats.health = savedState[lastGoodGameState[0]].p2Hp;
		}
		//p1fighter.transform.position = new Vector3(savedState[lastGoodGameState[0]].p1XPos,savedState[lastGoodGameState[0]].p1YPos,0);
		//p2fighter.transform.position = new Vector3(savedState[lastGoodGameState[0]].p2XPos,savedState[lastGoodGameState[0]].p2YPos,0);
		p1fighterCon.animations.animation.Stop(); // need to replace with exact animation frame.
		p2fighterCon.animations.animation.Stop(); // need to replace with exact animation frame.
		p1fighterCon.animations.animation[savedState[lastGoodGameState[0]].currentAnim1].time = savedState[lastGoodGameState[0]].animTime1;
		p2fighterCon.animations.animation.Play (savedState[lastGoodGameState[0]].currentAnim2);
		p2fighterCon.animations.animation[savedState[lastGoodGameState[0]].currentAnim2].time = savedState[lastGoodGameState[0]].animTime2;
		/*
		p1fighterCon.bGrounded = savedState[lastGoodGameState[0]].bGrounded1;
		p1fighterCon.bJumping = savedState[lastGoodGameState[0]].bJumping1;
		p1fighterCon.bJumpForward = savedState[lastGoodGameState[0]].bJumpForward1;
		p1fighterCon.bJumpBackward = savedState[lastGoodGameState[0]].bJumpBackward1;
		p1fighterCon.bJumpStart = savedState[lastGoodGameState[0]].bJumpStart1;
		p1fighterCon.bJumpFalling = savedState[lastGoodGameState[0]].bJumpFalling1;
		p1fighterCon.jump.bJumpEnded = savedState[lastGoodGameState[0]].bJumpFalling1;
		p1fighterCon.jump.height = savedState[lastGoodGameState[0]].height1;
		p1fighterCon.jump.length = savedState[lastGoodGameState[0]].length1;
		
		p2fighterCon.bGrounded = savedState[lastGoodGameState[0]].bGrounded2;
		p2fighterCon.bJumping = savedState[lastGoodGameState[0]].bJumping2;
		p2fighterCon.bJumpForward = savedState[lastGoodGameState[0]].bJumpForward2;
		p2fighterCon.bJumpBackward = savedState[lastGoodGameState[0]].bJumpBackward2;
		p2fighterCon.bJumpStart = savedState[lastGoodGameState[0]].bJumpStart2;
		p2fighterCon.bJumpFalling = savedState[lastGoodGameState[0]].bJumpFalling2;
		p2fighterCon.jump.height = savedState[lastGoodGameState[0]].height2;
		p2fighterCon.jump.length = savedState[lastGoodGameState[0]].length2;
		*/
		
		/*
		//undo KO
		if (!savedState[lastGoodGameState[0]].bKnockedOut1 && p1fighterCon.bKnockedOut)
		{
			gameMain.CancelEndRound();
			p1fighterCon.CancelKo();
			//print("KO is false!! Rollback "+lastGoodGameState[0]);
		}
		if (!savedState[lastGoodGameState[0]].bKnockedOut2 && p2fighterCon.bKnockedOut)
		{
			gameMain.CancelEndRound();
			p2fighterCon.CancelKo();
			//print("KO is false!! Rollback "+lastGoodGameState[0]);
		}
		
		//undo knockdown
		if (!savedState[lastGoodGameState[0]].bKnockdown2 && p2fighterCon.bKnockdown)
		{
			p2fighterCon.CancelKnockdown();
		}
		
		// stop attack
		if (!savedState[lastGoodGameState[0]].bAttacking1 && p1fighterCon.bAttacking)
		{
			p1fighterCon.StopAttacks();
		}
		//erase projectiles
		if (!savedState[lastGoodGameState[0]].bProjThrown1 && p1fighterCon.projectile != null)
		{
			p1fighterCon.KillProjectile();
		}
		if (!savedState[lastGoodGameState[0]].bProjThrown2 && p2fighterCon.projectile != null)
		{
			p2fighterCon.KillProjectile();
		}
		//p1fighterCon.bJumping = savedState[lastGoodGameState].bJumping1;
		//p2fighterCon.bJumping = savedState[lastGoodGameState].bJumping2;
		// send message to revert to opponent.
	}
	
	
	void ShiftArray()
	{
		int[] tempArray = new int[300];
		for(int i = 0; i < 30; i++)
		{
			tempArray[i] = lastGoodGameState[i];
		}
		
		for (int i = 1; i < 30; i++)
		{
			lastGoodGameState[i] = tempArray[i-1];
		}
	}
	
	void GameStateRepair()
	{
		// go back to an older goodgamestate.
		int[] tempArray = new int[300];
		for(int i = 0; i < 30; i++)
		{
			tempArray[i] = lastGoodGameState[i];
			////print (i+" is "+tempArray[i]);
		}
		
		for (int i = 0; i < 30; i++)
		{
			lastGoodGameState[i] = tempArray[i+1];
			////print (i+" is "+lastGoodGameState[i]);
		}
		//print (Network.time+" repaired to "+lastGoodGameState[0]);
		savedState[lastGoodGameState[0]].rollThread = currentRollThread;
	}
	
	[RPC]
	void RecieveGameStateApprove(int timestamp)
	{
		if (gameMain.frameCount > 0)
		{
			// our opponent has checked a state and found it matches its own. when we rollback it will be to this state.
			savedState[timestamp].netApproval = true;
			////print ("State Net approved "+timestamp+" " + savedState[timestamp].netApproval +" "+ savedState[timestamp].localApproval);
			//if (timestamp >= lastGoodGameState[0])
			//{
			if (timestamp > lastGoodGameState[0])
			{
				ShiftArray();
				lastGoodGameState[0] = timestamp;
				networkView.RPC("DoubleCheckGameState",RPCMode.Others,timestamp);
			}
			//}
			//else
			//{
			//	//print (gameMain.frameCount+ " timestamp is old ignoring " + timestamp);
			//}
		}
	}
	
	[RPC]
	void DoubleCheckGameState (int timestamp)
	{
		if (timestamp > lastGoodGameState[0])
		{
			ShiftArray();
			lastGoodGameState[0] = timestamp;
		}
	}
	
	void WipeOldStates(int timestamp,int oldstamp)
	{
		for (int i = timestamp +1; i < oldstamp; i++)
		{
			// clear each state from here to old state.
			localBufState[i] = new State();
			savedState[i] = new GameState();
		}
	}

*/
	public void PollMasterServer()
	{
		if (!bCasual)
		{
			MasterServer.RequestHostList("EXPO_CWAR");
		}
		else
		{
			MasterServer.RequestHostList("EXPO_CASUAL");
		}
		//print(MasterServer.PollHostList().Length);
		if(MasterServer.PollHostList().Length > 0)
		{
			Debug.Log ("list generated");
			hostData = MasterServer.PollHostList();
			serverPing = new Ping[hostData.Length];
			Debug.Log (hostData.Length);
			for (int i = 0; i < hostData.Length; i++)
        	{
				serverPing[i] = new Ping(hostData[i].ip.ToString());
				//print ("serverping "+i +" complete");
			}
		}
		else
		{
			Debug.Log("No Games");
		}
	}
	
	void Update()
	{
		if (!isOffline)
		{
			if (fightersSet)
			{
				if (gameMain.bRoundStarted)
				{
					netUpdateCount++;
					if (netUpdateCount >= netUpdateTime)
					{
						netUpdateCount = 0;
						//PreNetUpdate();
					}
				}
			}
			
			if (refreshing)
			{
				if(MasterServer.PollHostList().Length > 0)
				{
					Debug.Log ("list generated");
					refreshing = false;
					
					hostData = MasterServer.PollHostList();
					serverPing = new Ping[hostData.Length];
					Debug.Log (hostData.Length);
					for (int i = 0; i < hostData.Length; i++)
		        	{
						serverPing[i] = new Ping(hostData[i].ip[0].ToString());
						//print (hostData[i].ip[0]);
					}
					MasterServer.ClearHostList();
				}
			}
			if (gameMain.bFadeInComplete)
			{
				gameStateSaveCount++;
				if (gameStateSaveCount >= gameStateSaveTime)
				{
					gameStateSaveCount = 0;
				//	FullStateSave();
				}
			}
		}
	}

	void OnFailedToConnect(NetworkConnectionError error ) 
	{    
		Debug.Log("Could not connect to server: "+ error);
		gameMain.Ainfo.Insert(0,"Could not connect to server");
		bConnecting = false;
	}
	// Gui
	void OnGUI()
	{
		if (netMenu)
		{
			GUI.Label(new Rect(Screen.width/2 - 20,0, 200, 28), "AVAILABLE GAMES");
			GUI.Box(new Rect(Screen.width/2 - 20, 30, btnW * 2.0f,Screen.height - Screen.height/8),"");
			if (hostData != null )
			{
				var butOff = 0.0f;
				
				for (int i = 0; i < hostData.Length; i++)
		        {
					////print(hostData[i].connectedPlayers);
					//if (hostData[i].connectedPlayers == 1)
					//{
						////print (serverPing[0].isDone);
						if (serverPing != null)
						{
							////print (serverPing[i].isDone);
							//if(serverPing[i].isDone)
							//{
								if(GUI.Button(new Rect(Screen.width/2 - 20,30+butOff,btnW * 2.0f,btnH),hostData[i].gameName+" - " +serverPing[i].time.ToString()))
								{
									if (hostData[i].connectedPlayers == 1)
									{
										Network.Connect(hostData[i]);
										bConnecting = true;
									}
									else
									{
										//GUI.Label(new Rect(20, Screen.height-50, 200, 20), "Server is full. pick another.");
										Network.Connect(hostData[i]);
										bConnecting = true;
										gameMain.isSpectator = true;
										gameMain.introDone = true;
										gameMain.netPlay = true;
									}
								}
							//}
						}
					//}
					butOff += 58.0f;
				}
			}
			else
			{
				GUI.Label(new Rect(Screen.width - Screen.width/2.5f,Screen.height/2, 200, 28), "No games currently available.");
			}
		}
		
		else if (bHostName)
		{
			gameName = GUI.TextField(new Rect(Screen.width/2-100, Screen.height/2, 200, 20), gameName, 25);
		}
		
		if (!Network.isClient && !Network.isServer && !isOffline && !bLogin && tempMenu)
		{
			gameName = GUI.TextField(new Rect(Screen.width - 210, 10, 200, 20), gameName, 25);
			
			if (GUI.Button(new Rect(btnX,btnY,btnW,btnH),"Start Server"))
			{
				StartServer();
			}
			
			if (GUI.Button(new Rect(btnX,btnY * 12.0f,btnW,btnH),"Refresh"))
			{
				RefreshHostList();
			}
			if (GUI.Button(new Rect(btnX,btnY * 24.0f,btnW,btnH),"Offline"))
			{
				//gameMain.StartRound();
				isOffline = true;
			}
			
			if (hostData != null)
			{
				var butOff = 0.0f;
				for (int i = 0; i < hostData.Length; i++)
		        {
					//print(hostData[i].connectedPlayers);
					//if (hostData[i].connectedPlayers == 1)
					//{
						if(GUI.Button(new Rect(btnX * 2.0f + btnW,btnY * butOff,btnW * 2.0f,btnH),hostData[i].gameName +" - " +serverPing[i].time.ToString()))
						{
							if (hostData[i].connectedPlayers == 1)
							{
								Network.Connect(hostData[i]);
								bConnecting = true;
							}
							else
							{
								//GUI.Label(new Rect(20, Screen.height-50, 200, 20), "Server is full. pick another.");
								Network.Connect(hostData[i]);
								bConnecting = true;
								gameMain.isSpectator = true;
								gameMain.introDone = true;
								gameMain.netPlay = true;
							}
						}
					//}
					butOff += 12.0f;
				}
			}
		}
		if (bConnecting)
		{
			GUI.Label(new Rect(20, Screen.height-50, 200, 20), "Connecting to server");
		}
		if (Network.isServer)
		{
			GUI.Label(new Rect(20, Screen.height-50, 200, 20), "Server");
		}
		
		//GUI.Label(new Rect(20, Screen.height-70, 200, 20), );
		
		if (bRolling)
		{
			GUI.Label(new Rect(20, Screen.height-90, 200, 20), "ROLLBACK");
		}
		//if(GUI.Button(new Rect(20,Screen.height-50,80,20),"//print Inputs"))
		//{
		//	//printInputs();
		//}
	}
}
