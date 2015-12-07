using UnityEngine;
using System.Collections;

public class RoundEnd : MonoBehaviour 
{
	private ScreenFade sFade;
	private Main gameMain;
	public float koDelay = 5.0f;
	public float lastHitTime = 2.0f;
	public float lastHitTimescale = 0.2f;
	public int pOneRounds = 0;
	public int pTwoRounds = 0;
	public int gameCount = 0;
	private bool bKo = false;
	private GameObject winText;
	private GameObject koText;
	public GameObject readyTextFab;
	public GameObject koTextFab;
	public int roundsToWin;
	public string winString;
	public GameObject cam;
	public bool bDouble;
	
	public GameObject hud;
	public GameObject roundCounter;
	public Vector3[] counterPOS;
	
	void Awake()
	{
		sFade = gameObject.GetComponent<ScreenFade>();
		gameMain = gameObject.GetComponent<Main>();
		
	}
	
	void Update()
	{
		if (bKo)
		{
			Time.timeScale = lastHitTimescale;
		}
		
	}
	public void KoCalled(bool f1)
	{
		StartCoroutine("playerKo",f1);
	}
	// when a player health drops to zero, do this stuff.
	IEnumerator playerKo(bool f1)
	{
		yield return new WaitForSeconds(koDelay);
		
		koText = Instantiate(koTextFab,new Vector3(cam.transform.position.x,8.0f,-10.0f),Quaternion.identity) as GameObject;
		koText.transform.parent = cam.transform;
		
		if (f1)
		{
			Debug.Log("Player 1 Defeated");
			pTwoRounds++;
			GiveRoundCounter(pTwoRounds-1,gameCount, false);
			if (gameMain.fighterChoice01 == gameMain.matChar)
			{
			//gameMain.fSounds.PlayKo
			}
		}
		else
		{
			Debug.Log("Player 2 Defeated");
			pOneRounds++;
			GiveRoundCounter(pOneRounds-1,gameCount, true);
		}
		bKo = true;
		Time.timeScale = lastHitTimescale;
		yield return new WaitForSeconds(lastHitTime);
		bKo = false;
		Time.timeScale = gameMain.gameSpeed;
		//Time.timeScale = 1.0f;
		
		if (pOneRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(false));
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkGameEnd",RPCMode.Others,false);
			}
		}
		else if(pTwoRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(true));
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkGameEnd",RPCMode.Others,true);
			}
		}
		else
		{
			StartCoroutine(roundEndRoutine());
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkRoundEnd",RPCMode.Others);
			}
		}
		Destroy(koText.gameObject);
	}
	
	public void Cancel()
	{
		StopAllCoroutines();
		bKo = false;
	}
	public void TimeOverCalc(int hp1, int hp2)
	{
		if (hp1 > hp2)
		{
			Debug.Log("fighter1 wins");
			pOneRounds++;
			GiveRoundCounter(pOneRounds-1,gameCount, true);
			if (Network.isServer)
			{
				GetComponent<NetworkView>().RPC("NetworkTimeOver",RPCMode.Others,true,false);
			}
		
		}
		else if (hp1 < hp2)
		{
			Debug.Log("fighter2 wins");
			pTwoRounds++;
			GiveRoundCounter(pTwoRounds-1,gameCount, false);
			if (Network.isServer)
			{
				GetComponent<NetworkView>().RPC("NetworkTimeOver",RPCMode.Others,false,false);
			}
		}
		else if (hp1 == hp2)
		{
			Debug.Log ("draw");
			if (Network.isServer)
			{
				GetComponent<NetworkView>().RPC("NetworkTimeOver",RPCMode.Others,false,true);
			}
		}
		
		
		if (pOneRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(false));
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkGameEnd",RPCMode.Others,false);
			}
		}
		else if(pTwoRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(true));
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkGameEnd",RPCMode.Others,true);
			}
		}
		else
		{
			StartCoroutine(roundEndRoutine());
			
			if (Network.isServer && gameMain.netPlay)
			{
				GetComponent<NetworkView>().RPC("NetworkRoundEnd",RPCMode.Others);
			}
		}
		
		//sFade.fadeOut = true;
		//gameMain.bRunGame = false;
		//gameMain.resetTimer();
	}
	
	[RPC]
	public void NetworkTimeOver(bool p1Win,bool bDraw)
	{
		if (bDraw)
		{
			Debug.Log("draw");
		}
		else if (p1Win)
		{
			Debug.Log("fighter1 wins");
			pOneRounds++;
			GiveRoundCounter(pOneRounds-1,gameCount, true);
		}
		else
		{
			Debug.Log("fighter2 wins");
			pTwoRounds++;
			GiveRoundCounter(pTwoRounds-1,gameCount, false);
		}
		
		if (pOneRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(false));
		}
		else if(pTwoRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(true));
		}
		else
		{
			StartCoroutine(roundEndRoutine());
		}
	}
	
	IEnumerator GameWin(bool p2win)
	{
		
		//gameMain.bRunGame = false;
		gameMain.EndRoutines();
		yield return new WaitForSeconds(0.5f);
		// give round counter to winner.
		// highlight game win counter for correct player
		
		yield return new WaitForSeconds(0.2f);
		winText = Instantiate(readyTextFab,new Vector3(cam.transform.position.x,8.0f,-10.0f),Quaternion.identity) as GameObject;
		winText.transform.parent = cam.transform;
		
		if (p2win == false)
		{
			winString = "Player One Wins";
			if (Network.isServer || !gameMain.netPlay)
			{
				//gameMain.p1Control.fSounds.PlayEnding();
			}
		}
		else
		{
			winString = "Player Two Wins";
			if (Network.isServer || !gameMain.netPlay)
			{
				//gameMain.p2Control.fSounds.PlayEnding();
			}
		}
		
		winText.GetComponent<TextMesh>().text = winString;
		
		// cam move in and fighter anim
		yield return new WaitForSeconds(1.5f);
		Destroy(winText.gameObject);
		gameMain.gameOver = true;
		
		if (!gameMain.isSpectator)
		{
			gameMain.GameEndMenu();
			gameMain.gameEndMenu = true;
		}
		//gameCount++;
		// if ranked match give points to winner and update faction score.
		if (Network.isServer && gameMain.netPlay && !gameMain.netManage.bCasual)
		{
//			gameMain.webLog.UpScores(1);
			
		}
		
	}
	
	IEnumerator roundEndRoutine()
	{
		// stop gameplay
		//gameMain.bRunGame = false;
		gameMain.EndRoutines();
		
		yield return new WaitForSeconds(0.5f);
		// give round counter to winner.
		
		sFade.fadeOut = true;
		yield return new WaitForSeconds(1.2f);
		gameMain.resetTimer();
		gameMain.ReadyForReset();
		if (!gameMain.netPlay)
		{
			gameMain.RestartRound();
		}
	}
	
	[RPC]
	void NetworkRoundEnd()
	{
		gameMain.bRoundStarted = false;
		StartCoroutine(roundEndRoutine());
	}
	
	[RPC]
	void NetworkGameEnd(bool p2win)
	{
		gameMain.bRoundStarted = false;
		StartCoroutine(GameWin(p2win));
	}
	
	void GiveRoundCounter(int winNum,int gameCount,bool p1)
	{
		hud.GetComponent<roundCounters>().GiveRoundCounter(winNum,gameCount,p1);
		if (Network.isServer)
		{
			GetComponent<NetworkView>().RPC("NetworkRoundCounter",RPCMode.Others,winNum,gameCount,p1);
		}
	}
	
	[RPC]
	public void NetworkRoundCounter(int winNum,int gameCount,bool p1)
	{
		hud.GetComponent<roundCounters>().GiveRoundCounter(winNum,gameCount,p1);
	}
	
	public void DoubleCalled()
	{
		if (!bDouble)
		{
			bDouble = true;
			StartCoroutine("doubleKo");
		}
	}
	
	// when a player health drops to zero, do this stuff.
	public IEnumerator doubleKo()
	{
		yield return new WaitForSeconds(koDelay);
		
		
			Debug.Log("Player 1 Defeated");
			//pTwoRounds++;
			//GiveRoundCounter(pTwoRounds-1,gameCount, false);
		
			Debug.Log("Player 2 Defeated");
			//pOneRounds++;
			//GiveRoundCounter(pOneRounds-1,gameCount, true);
		
		bKo = true;
		Time.timeScale = lastHitTimescale;
		yield return new WaitForSeconds(lastHitTime);
		bKo = false;
		Time.timeScale = gameMain.gameSpeed;
		//Time.timeScale = 1.0f;
		
		if (pOneRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(false));
		}
		else if(pTwoRounds >= roundsToWin)
		{
			StartCoroutine(GameWin(true));
		}
		else
		{
			StartCoroutine(roundEndRoutine());
		}
		bDouble = false;
	}
	
}
