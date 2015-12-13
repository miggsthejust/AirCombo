using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public string versionNum = "version 0.03";
	
	public string playerName;
	public string player2Name;
	
	public bool netPlay = false;
	public bool begun = false;
	public bool bRoundStarted;
	public int roundTime = 30000;
	public bool bFadeInComplete;
	public GameObject fighterChoice01;
	public GameObject fighterChoice02;
	
	public int timerInc;
	public bool bTimerStart;
	public int timerTime;
	public TextMesh timerMesh;
	public GameObject readyText;
	public GameObject readyTextFab;
	public Vector3 timerPOS = new Vector3(0,0,0);
	TextMesh timerInst;
	
	public GameObject regChar;
	public GameObject kazChar;
	public GameObject matChar;
	
	public GameObject spawn01;
	public GameObject spawn02;
	public GameObject cam;
	public Vector3 camStartPos;
	public bool bChooseChar;
	public bool bP1ChoseChar;
	public bool bP2ChoseChar;
	public GameObject netChose;
	public bool vsBegin;
	public bool introDone;
	public fighterSounds fSounds;
	public AnnouncerSounds aSounds;
	
	public bool bRestartClient;
	public bool bRestartServer;
	public FighterInit f1Init;
	public DummyInit f2Init;
	public MenuGui attackMenu;
		
	public bool fightersInit = false;
	
	public FighterController p1Control;
	public DummyController p2Control;
	public PlayerInput player;
	public NetworkManager netManage;
	public RoundEnd rEnd;
	public ButtonSelect btnSelect;
	private NetworkViewID p1ViewID;
	private NetworkViewID p2ViewID;
	public FrontEnd fEnd;
	public CharSelect cSel;
	public GameObject hud;
	public ScreenFade sFade;
	public ComboHeight cHeight;
	
	public PlayerInput p1Input;
	public PlayerInput p2Input;
	
	public int frameCount; // tracks which frame of gameplay we are on.
	public bool bRunGame; // have we started the rungame coroutine.
	public bool bStartingDelay;
	public int cliFrame;
	public int servFrame;
	
	public bool stateSent;
	public bool stateRecieved;
	
	public bool bTimeOver;
	
	public healthbar hb1;
	public healthbar hb2;
	public hypebar hyb1;
	public hypebar hyb2;
	public float hp1;
	public float hp2;
	public float hy1;
	public float hy2;
	
	public float time;
	
	public float lagTime;
	public float lagTimer;
	public bool inputStored;
	
	// testing resending inputs
	public bool bStoredFrame;
	public float storedFrame;
	public float resendCount;
	
	public bool gameOver;
	public bool gameEndMenu;
	
	public bool servRematch;
	public bool clientRematch;
	public bool isSpectator;
	
	public ArrayList Ainfo = new ArrayList();
	
	public int sendGame;
	
	public PauseMenu pMenu;
	public bool bPauseMenu;
	
	public PauseMenu gameOverBar;
	//DEBUG REMOVE
	public bool bInfTime;
	public bool DinfHealth;
	
	// bug email
	public string Eemail;
	public string Esubject;
	public string Ebody;
	public string betaString;
	
	public float gameSpeed;

	// Use this for initialization
	void Awake()
	{
		player = GetComponent<PlayerInput>();
		netManage = GetComponent<NetworkManager>();
		rEnd = GetComponent<RoundEnd>();
		btnSelect = GetComponent<ButtonSelect>();
		cHeight = GetComponent<ComboHeight> ();
		attackMenu = GetComponent<MenuGui> ();
		//bChooseChar = true;
		Application.targetFrameRate = 60;
		Time.timeScale = gameSpeed;
		bool bButtonSaved = PlayerPrefs.HasKey("1h");
		if (bButtonSaved)
		{
			LoadSave();
		}
	}

	// called by __ when ready to spawn the fighter objects. once complete start round
	public void LoadFighters () 
	{
		// spawning of characters
		if (netPlay && Network.isServer)
		{
			Debug.Log("spawn char 1");
			GetComponent<SpawnFighters>().NetSpawning(fighterChoice01,spawn01.transform,true);
			Debug.Log("spawn char 2");
			GetComponent<SpawnFighters>().NetSpawning(fighterChoice02,spawn02.transform,false);
		}
		else if (!netPlay)
		{
			Debug.Log("spawn char 1");
			GetComponent<SpawnFighters>().Spawning(fighterChoice01,spawn01.transform,true);
			Debug.Log("spawn char 2");
			GetComponent<SpawnFighters>().Spawning(fighterChoice02,spawn02.transform,false);
		}
		//begun = true;
		StartRound ();
		// spawning of HUD
		//Debug.Log("spawn HUD");
	}

	// called by ___ find the correct input objects for a net game. needed for spawning fighter objects.
	public void SetNetInput()
	{
		p1Input = GameObject.FindGameObjectWithTag("input1").GetComponent<PlayerInput>();
		p2Input = GameObject.FindGameObjectWithTag("input2").GetComponent<PlayerInput>();
		Debug.Log ("player 1 = "+p1Input.tag);
		Debug.Log ("player 2 = "+p2Input.tag);
		LoadFighters();
	}
	// Update is called once per frame
	void Update () 
	{
		if (servRematch && clientRematch) // if both players select rematch, start next game.
		{
			StartCoroutine(StartNextGame());
			//gameOver = false;
		}
		
		if (!gameOver)
		{
			if (netPlay && bRestartClient && bRestartServer)
			{
				RestartRound();
				bRestartClient = false;
				bRestartServer = false;
			}
			
			if (bFadeInComplete == true)
			{
				if (!bTimerStart)
				{
					bTimerStart = true;
					StartCoroutine("Timer");
				}
				if (netPlay == true)
				{
					if (!bRunGame)
					{
						StartCoroutine("RunGame");
						bRunGame = true;
					}
				}
				else
				{
					if (DinfHealth)
					{
						p1Control.hype.hypeAmount = 100;

					}
					if (Input.GetKeyDown("escape") && !bPauseMenu)
					{
						PauseMenu();
					}
					else if (Input.GetKeyDown("escape") && bPauseMenu)
					{
						ClosePauseMenu();
					}
					// run the main stack. Make sure it's all necessary to be called every frame.
					// character 1 code
					// character 2 code
					// networking if applicable
					// most others can be run in guiUpdate
					if (!bPauseMenu)
					{
						hp1 = p1Control.stats.health;
						hp2 = p2Control.stats.health;
						hy1 = p1Control.hype.hypeAmount;
						player.CheckInput();// check which inputs are down
						player.InputBuffer();// store those inputs
						player.PlaybackBuffer();// playback inputs
						p1Control.Activate();// activate fighter 1
						p2Control.Activate();// activate fighter 2
						frameCount++;
						//Timer();
					}
				}
			}
			else
			{
				if(begun)
				{
					//Debug.Log ("getComponent");
					// check to see if both characters have confirmed they are initialized.
					//cSel.transform.Translate(1000,0,0);
					
					hb1.bar1 = true;
					hb1.Activate();
					hb2.Activate();
					hyb1.bar1 = true;
					hyb1.Activate();
					hyb2.Activate();
					
					if (Network.isServer || !netPlay)
					{
						f1Init = GameObject.FindWithTag("fighter1").GetComponent<FighterInit>();
						f2Init = GameObject.FindWithTag("fighter2").GetComponent<DummyInit>();
						
						var f1Object = f1Init.gameObject;
						var f2Object = f2Init.gameObject;
						
						if (f1Init.bInit && f2Init.bInit)
						{
							// check to see if both characters have confirmed they are initialized.
							fightersInit = true;
							//Debug.Log ("fightersInit");
						}
						else if (f1Init.bSpawn && f2Init.bSpawn)
						{
							// check to see if both fighters have been spawned.
							f1Init.Initialize(this);
							f2Init.Initialize(this);
						}
					}
				}
				else if (!bChooseChar && !begun)
				{
					/*if(netPlay)
					{
						if (netManage.netTagged)
						{
							SetNetInput();
						}
						if (netChose != null)
						{
							if (Network.isServer)
							{
								//netChose = fighterChoice02;
								netManage.CharactersChosen();
							}
							else 
							{
								//netChose = fighterChoice01;
							}
						}
					}
					else
					{*/
						LoadFighters();
					//}
				}
			}
		}
	}


	public void ReadyForReset()
	{
		//netManage.SendReset();
	}
	
	public void resetTimer()
	{
		Destroy(timerInst.gameObject);
	}
	
	public void EndRound(string fighter)
	{
		StartCoroutine("EndRoundRoute",fighter);
	}
	
	IEnumerator EndRoundRoute (string fighter)
	{
		yield return new WaitForSeconds(0.016f);
		bRoundStarted = false;
		
		if (fighter == "fighter1")
		{
			if (Network.isServer && netPlay||!netPlay)
			{
				if (p2Control.stats.health <= 0)
				{
					Debug.Log ("double ko");
					rEnd.DoubleCalled();
				}
				else
				{
					Debug.Log ("end round");
					rEnd.KoCalled(true);
				
					if (netPlay && Network.isServer)
					{
						//StartCoroutine(netManage.SendKo(true));
					}
				}
			}
			else
			{
				RestartRound();
			}
		}
		else
		{
			if (Network.isServer && netPlay||!netPlay)
			{
				if (p1Control.stats.health <= 0)
				{
					Debug.Log ("double ko");
					rEnd.DoubleCalled();
				}
				else
				{
					rEnd.KoCalled(false);
					if (netPlay && Network.isServer)
					{
						//StartCoroutine(netManage.SendKo(false));
					}
				}
			}
		}
	}
	
	public void EndRoutines()
	{
		StopCoroutine("RunGame");
		StopCoroutine("Timer");
	}
	
	public void CancelEndRound()
	{
		// if we need to rollback a round end
		rEnd.Cancel();
	}
	public void StartRound()
	{
		//yield return new WaitForSeconds(fadeOutTime);
		if (!bRoundStarted)
		{
			
			Debug.Log("begin round");
			gameOver = false;
			bRoundStarted = true;
			//Destroy(p1Control);
			//Destroy(p2Control);
			begun = false;
			fightersInit = false;
			bFadeInComplete = false;
			frameCount = 0;
			//LoadFighters();
			attackMenu.fighter = GameObject.FindWithTag("fighter1").GetComponent<FighterController>();
			cHeight.dummy = GameObject.FindWithTag("fighter2").transform;
			cHeight.comboActive = true;
			if (netPlay)
			{
				//netManage.startSetFighters();
			}
			else 
			{
				//LoadFighters();
			}
			
			sFade.fadeIn = true;
			begun = true;
			StartCoroutine(ReadyText());
			//var readyText = Instantiate(readyMesh,timerPOS,Quaternion.identity) as GameObject;
			//Debug.Log(bRoundStarted);
			//GetComponent(RoundBegin).BeginRound();
		}
	
	}
	
	IEnumerator StartNextGame()
	{
		gameEndMenu = false;
		rEnd.pOneRounds = 0;
		rEnd.pTwoRounds = 0;
		servRematch = false;
		clientRematch = false;
		resetTimer();
		sFade.fadeOut = true;
		hud.GetComponent<roundCounters>().DeleteCounters();
		if (Network.isServer)
		{
			p1Control.hype.hypeAmount = 0;
			//netManage.SendSpectatorRematch();
		}
		yield return new WaitForSeconds(2.0f);
		RestartRound();
	}
	
	IEnumerator ReadyText()
	{
		if (!introDone)
		{
			aSounds.PlayMusic();
			introDone = true;
			cam.GetComponent<Animation>().Play("p1IntroSwoop");
			//play intro anim and speech
			if (fighterChoice01 == matChar)
			{
				fSounds.PlayMatIntro();
			}
			else if (fighterChoice01 == regChar)
			{
				fSounds.PlayRegIntro();
			}
			else if (fighterChoice01 == kazChar)
			{
				fSounds.PlayKazIntro();
			}
			
			yield return new WaitForSeconds(4);
			cam.GetComponent<Animation>().Play("p2IntroSwoop");
			
			if (fighterChoice02 == matChar)
			{
				fSounds.PlayMatIntro();
			}
			else if (fighterChoice02 == regChar)
			{
				fSounds.PlayRegIntro();
			}
			else if (fighterChoice02 == kazChar)
			{
				fSounds.PlayKazIntro();
			}
			yield return new WaitForSeconds(4);

			
			
			timerInst = Instantiate(timerMesh,timerPOS,Quaternion.identity) as TextMesh;
			timerInst.transform.parent = cam.transform;
			timerInst.transform.localPosition = new Vector3(0,3.5f,13.0f);
			timerTime = 50;
			bTimerStart = false;

		}
		hud.transform.localPosition = new Vector3(0,-7.6f,28.8f);
		cam.transform.position = camStartPos;
		cam.transform.rotation = new Quaternion(0,0,0,0);
		gameOver = false;
		yield return new WaitForSeconds(0.6f);
		readyText = Instantiate(readyTextFab,new Vector3(0.0f,8.0f,-10.0f),Quaternion.identity) as GameObject;
		readyText.GetComponent<Animation>().Play("readyText");
		yield return new WaitForSeconds(0.5f);
		//Destroy (readyText);
		readyText.GetComponent<Animation>().Stop();
		readyText.GetComponent<TextMesh>().text = "your";
		readyText.GetComponent<Animation>().Play("readyText");
		yield return new WaitForSeconds(0.5f);
		readyText.GetComponent<Animation>().Stop();
		readyText.GetComponent<TextMesh>().text = "body";
		readyText.GetComponent<Animation>().Play("readyText");
		yield return new WaitForSeconds(0.5f);
		readyText.GetComponent<Animation>().Stop();
		readyText.GetComponent<TextMesh>().text = "";
		bFadeInComplete = true;
		//yield return new WaitForSeconds(0.5f);
		cam.GetComponent<CameraScroll>().active = true;
		//readyText.GetComponent<TextMesh>().text = "fight!";
		//readyText.animation.Play("readyText");
		//yield return new WaitForSeconds(0.5f);
		Destroy (readyText);
	}
	
	public void RestartRound()
	{
		//yield return new WaitForSeconds(fadeOutTime);
		if (!bRoundStarted)
		{
			Debug.Log("begin round");
			bRoundStarted = true;

			cam.transform.position = camStartPos;
			cam.transform.rotation = new Quaternion(0,0,0,0);
			cam.GetComponent<CameraScroll>().camBoundsU = false;
			cam.GetComponent<CameraScroll>().camBoundsL = false;
			cam.GetComponent<CameraScroll>().camBoundsR = false;
			hb1.Reset();
			hb2.Reset();
			//begun = false;
			//fightersInit = false;
			bFadeInComplete = false;
			bTimeOver = false;
			stateSent = false;
			stateRecieved = false;
			inputStored = false;
			frameCount = 0;
			bRunGame = false;
			cliFrame = 0;
			servFrame = 0;
			gameOver = false;
			//Destroy (timerInst.gameObject);
			timerInst = Instantiate(timerMesh,timerPOS,Quaternion.identity) as TextMesh;
			timerInst.transform.parent = cam.transform;
			timerTime = 50;
			bTimerStart = false;
			//LoadFighters();
			
			GetComponent<ScreenFade>().fadeIn = true;
			StartCoroutine(ReadyText());
			//Debug.Log(bRoundStarted);
			//GetComponent(RoundBegin).BeginRound();
		}
	}
	
	//void Timer()
	//{
	//	var frameConvert = frameCount/60;
	//	time = Mathf.Round(50.0f - frameConvert);
	//}
	
	IEnumerator Timer()
	{
		//print ("timer is running");
		if (!bInfTime)
		{
			yield return new WaitForSeconds(timerInc);
			timerTime -= 1;
			timerInst.text = timerTime.ToString();
			
			if (timerTime == 0)
			{
				bTimeOver = true;
				bRoundStarted = false;
				if (Network.isServer)
				{
					rEnd.TimeOverCalc(p1Control.stats.health,p2Control.stats.health);
				}
				StopCoroutine("Timer");
			}
			else
			{
				StartCoroutine("Timer");
			}
		}
	}
	
	
	void PauseMenu()
	{
		bPauseMenu = true;
		pMenu.Reactivate();
	}
	public void ClosePauseMenu()
	{
		pMenu.OffScreen();
		bPauseMenu = false;
	}
	public void QuitGame()
	{
		aSounds.StopMusic();
		// stop gameplay. reset all buffers.
		gameOver = true;
		begun = false;
		bRoundStarted = false;
		fightersInit = false;
		bFadeInComplete = false;
		hb1.bActive = false;
		hb2.bActive = false;
		hyb1.bActive = false;
		hyb2.bActive = false;
		introDone = false;
		
		gameEndMenu = false;
		rEnd.pOneRounds = 0;
		rEnd.pTwoRounds = 0;
		servRematch = false;
		clientRematch = false;
		resetTimer();
		sFade.fadeOut = true;
		hud.GetComponent<roundCounters>().DeleteCounters();
		
		//if (netPlay && Network.isServer || !netPlay)
		//{	
			//Destroy(p1Control.gameObject);
			//Destroy(p2Control.gameObject);
			//Destroy(p1Input.gameObject);
			//Destroy(p2Input.gameObject);
			var tempCon1 = GameObject.FindGameObjectWithTag("fighter1");
			var tempCon2 = GameObject.FindGameObjectWithTag("fighter2");
			var tempinp1 = GameObject.FindGameObjectWithTag("input1");
			var tempinp2 = GameObject.FindGameObjectWithTag("input2");
			Destroy(tempCon1);
			Destroy(tempCon2);
			Destroy(tempinp1);
			Destroy(tempinp2);
			//print (tempinp1);
			//print (tempinp2);
		//}
		Destroy (timerInst);
		netPlay = false;
		bChooseChar = true;
		bP2ChoseChar = false;
		bP1ChoseChar = false;
		fighterChoice01 = null;
		fighterChoice02 = null;
		vsBegin = false;
		bTimerStart = false;
		gameOver = false;
		DinfHealth = false;
		bInfTime = false;
		hud.transform.localPosition = new Vector3(1000,-7.6f,28.8f);
		GetComponent<ScreenFade>().alphaFadeValue = 1;
		cam.GetComponent<CameraScroll>().active = false;
		
	}
	
	public void CharChangeReset()
	{
		gameOver = true;
		begun = false;
		bRoundStarted = false;
		fightersInit = false;
		
		bFadeInComplete = false;
		hb1.bActive = false;
		hb2.bActive = false;
		hyb1.bActive = false;
		hyb2.bActive = false;
		introDone = false;
		//netManage.bStarted = false;
		//netManage.netTagged = false;
		gameEndMenu = false;
		rEnd.pOneRounds = 0;
		rEnd.pTwoRounds = 0;
		servRematch = false;
		clientRematch = false;
		bRunGame = false;
		resetTimer();
		//sFade.fadeOut = true;
		hud.GetComponent<roundCounters>().DeleteCounters();
		var tempCon1 = GameObject.FindGameObjectWithTag("fighter1");
		var tempCon2 = GameObject.FindGameObjectWithTag("fighter2");
		var tempinp1 = GameObject.FindGameObjectWithTag("input1");
		var tempinp2 = GameObject.FindGameObjectWithTag("input2");

		Destroy(tempinp1);
		Destroy(tempinp2);
		Destroy(tempCon1);
		Destroy(tempCon2);
		hud.transform.localPosition = new Vector3(1000,-7.6f,28.8f);
		GetComponent<ScreenFade>().alphaFadeValue = 1;
		var tempcam = cam.GetComponent<CameraScroll>();
		tempcam.active = false;
		tempcam.camBoundsU = false;
		tempcam.camBoundsL = false;
		tempcam.camBoundsR = false;
		
	}
	
	public void Disconnect()
	{
		//netManage.Disconnect();
	}
	
	public void ChooseChars()
	{
		bChooseChar = true;
		cSel.OnScreen();
		fEnd.OffScreen();
	}
	public void CharsDone()
	{
		bChooseChar = false;
		cSel.OffScreen();
	}
	
	public void SendNetCharChange()
	{
		//send opponent to character change screen
		//netManage.SendNetCharChange();	
	}
	
	public void RematchPress()
	{
		if (netPlay)
		{
			if (Network.isServer)
			{
				servRematch = true;
				//netManage.SendRematch();
			}
			else
			{
				//netManage.SendRematch();
				clientRematch = true;
			}
		}
		else
		{
			servRematch = true;
			clientRematch = true;
		}
	}	
	
	public void GameEndMenu()
	{
		// move menu into position
		gameOverBar.transform.localPosition = new Vector3(0,-0.09f,21.5f);
		cam.GetComponent<CameraScroll>().active = false;
	}
	public void CloseGameOverMenu()
	{
		gameOverBar.OffScreen();
		gameEndMenu = false;
	}
	
	public void SpectatorSetup(int netTime,float camX,float camY)
	{
		hud.transform.localPosition = new Vector3(0,-7.6f,28.8f);
		cam.transform.position = (new Vector3(camX,camY,-28.54f));
		cam.transform.rotation = new Quaternion(0,0,0,0);
		cam.GetComponent<CameraScroll>().active = true;
			
		timerInst = Instantiate(timerMesh,timerPOS,Quaternion.identity) as TextMesh;
		timerInst.transform.parent = cam.transform;
		timerInst.transform.localPosition = new Vector3(0,3.5f,13.0f);
		timerTime = netTime;
		bTimerStart = true;
		
		float p1x = p1Control.transform.position.x;
		float p1y = p1Control.transform.position.y;
		float p2x = p2Control.transform.position.x;
		float p2y = p2Control.transform.position.y;
		p1Control.transform.position = new Vector3(p1x,p1y,0);
		p2Control.transform.position = new Vector3(p2x,p2y,0);
	}
	
	void LoadSave()
	{
		var pInput = gameObject.GetComponent<PlayerInput>();
		if (PlayerPrefs.GetInt("1vf") == 1)
		{
			pInput.localPlayerInput.invert = true;
		}
		else
		{
			pInput.localPlayerInput.invert = false;
		}
		pInput.localPlayerInput.upButton = PlayerPrefs.GetString("1v");
		pInput.localPlayerInput.downButton = PlayerPrefs.GetString("1v");
		pInput.localPlayerInput.leftButton = PlayerPrefs.GetString("1h");
		pInput.localPlayerInput.rightButton = PlayerPrefs.GetString("1h");
		
		pInput.localPlayerInput.lpButton = PlayerPrefs.GetString("1lp");
		pInput.localPlayerInput.lkButton = PlayerPrefs.GetString("1lk");
		pInput.localPlayerInput.hpButton = PlayerPrefs.GetString("1hp");
		pInput.localPlayerInput.hkButton = PlayerPrefs.GetString("1hk");
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 20), versionNum);
		
		/*
		//bug email stuff
		GUI.Label(new Rect(10 , 18, 2000, 28), betaString);
		
		Ebody = GUI.TextField(new Rect(10 , 40, 320, 72), Ebody );
		
		if (GUI.Button(new Rect(332, 40,80,30), "Send"))
		{
			if (Ebody != "")
			{
   				Application.OpenURL("mailto:" + Eemail + "?subject=" + Esubject + "&body=" + Ebody);
			}
		}
		*/
		
		/*
		if (gameEndMenu)
		{
			GUI.Box(new Rect(Screen.width/2 - 40, Screen.height/2-40, 100, 60),"");
			//if (GUI.Button(new Rect(Screen.width/2 - 40, Screen.height/2, 80, 30), "Disconnect"))
		//	{
				//netManage.Disconnect();
		//	}
			if ((GUI.Button(new Rect(Screen.width/2 - 40, Screen.height/2-30,80,30), "Rematch"))||(Input.GetButton(player.localPlayerInput.lkButton)))
			{
				if (netPlay)
				{
					if (Network.isServer)
					{
						servRematch = true;
						netManage.SendRematch();
					}
					else
					{
						netManage.SendRematch();
						clientRematch = true;
					}
				}
				else
				{
					servRematch = true;
					clientRematch = true;
				}
			}
		}
		*/
	}
}