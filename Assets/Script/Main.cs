using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public string versionNum = "version 0.03";
	
	public bool begun = false;
	public bool bRoundStarted;
	public int roundTime = 30000;
	public bool bFadeInComplete;

	public GameObject fighterChoice01;
	public GameObject fighterChoice02;
	
	public GameObject readyText;
	public GameObject readyTextFab;
	
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
	public RoundEnd rEnd;
	public ButtonSelect btnSelect;
	
	public FrontEnd fEnd;
	
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
		
		Debug.Log("spawn char 1");
		GetComponent<SpawnFighters>().Spawning(fighterChoice01,spawn01.transform,true);
		Debug.Log("spawn char 2");
		GetComponent<SpawnFighters>().Spawning(fighterChoice02,spawn02.transform,false);
		
		//begun = true;
		StartRound ();
		// spawning of HUD
		//Debug.Log("spawn HUD");
	}

	
	// Update is called once per frame
	void Update () 
	{	
		if (!gameOver)
		{
			if (bFadeInComplete == true)
			{				
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
				
					player.CheckInput();// check which inputs are down
					player.InputBuffer();// store those inputs
					player.PlaybackBuffer();// playback inputs
					p1Control.Activate();// activate fighter 1
					p2Control.Activate();// activate fighter 2
					frameCount++;
						
				}
			}
			else
			{
				if(begun)
				{
					//Debug.Log ("getComponent");
					// check to see if both characters have confirmed they are initialized.
					
					f1Init = GameObject.FindWithTag("fighter1").GetComponent<FighterInit>();
					f2Init = GameObject.FindWithTag("fighter2").GetComponent<DummyInit>();
						
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
				else if (!bChooseChar && !begun)
				{
					LoadFighters();
				}
			}
		}
	}

	
	public void EndRound(string fighter)
	{
		StartCoroutine("EndRoundRoute",fighter);
	}
	
	IEnumerator EndRoundRoute (string fighter)
	{
		yield return new WaitForSeconds(0.016f);
		bRoundStarted = false;
		RestartRound();
	}
	
	public void EndRoutines()
	{
		StopCoroutine("RunGame");
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
			
			
			sFade.fadeIn = true;
			begun = true;
			StartCoroutine(ReadyText());
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
		sFade.fadeOut = true;
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
			GetComponent<ScreenFade>().fadeIn = true;
			StartCoroutine(ReadyText());
			//Debug.Log(bRoundStarted);
			//GetComponent(RoundBegin).BeginRound();
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
		introDone = false;
		gameEndMenu = false;
		rEnd.pOneRounds = 0;
		rEnd.pTwoRounds = 0;
		servRematch = false;
		clientRematch = false;
		sFade.fadeOut = true;
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
		bChooseChar = true;
		bP2ChoseChar = false;
		bP1ChoseChar = false;
		fighterChoice01 = null;
		fighterChoice02 = null;
		vsBegin = false;
		gameOver = false;
		DinfHealth = false;
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
		introDone = false;
		gameEndMenu = false;
		rEnd.pOneRounds = 0;
		rEnd.pTwoRounds = 0;
		servRematch = false;
		clientRematch = false;
		bRunGame = false;
		//sFade.fadeOut = true;
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
	
	public void RematchPress()
	{
		servRematch = true;
		clientRematch = true;
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
		
		
	}
}