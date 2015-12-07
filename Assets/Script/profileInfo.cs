using UnityEngine;
using System.Collections;

public class profileInfo : MonoBehaviour 
{
	//public FrontEnd fEnd;
	public string playerName;
	public string gamesPlayed;
	public string gameWins;
	public string regWins;
	public string kazWins;
	public string matWins;
	public int faction;
	
	// text objects
	public TextMesh pName;
	public TextMesh pFac;
	public TextMesh tGames;
	public TextMesh wGames;
	public TextMesh rWins;
	public TextMesh kWins;
	public TextMesh mWins;
	
	public void Populate()
	{
		pName.text = playerName;
		tGames.text = gamesPlayed.ToString();
		wGames.text = gameWins.ToString();
		rWins.text = regWins.ToString();
		kWins.text = kazWins.ToString();
		mWins.text = matWins.ToString();
		
		if (faction == 1)
		{
			pFac.text = "M $yndicate";
			pFac.GetComponent<Renderer>().material.color = Color.green;
		}
		else if (faction == 2)
		{
			pFac.text = "S International";
			pFac.GetComponent<Renderer>().material.color = Color.blue;
		}
		else if (faction == 3)
		{
			pFac.text = "N Corp";
			pFac.GetComponent<Renderer>().material.color = Color.red;
		}
		
		print ("prof populated");
	}
	/*
	void OnGUI()
	{
		if (!fEnd.bOffScreen && !fEnd.loginWait)
		{
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f, Screen.height/8, 200, 28), playerName);
		
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f, Screen.height/6, 200, 28), "Games Played");
			GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/6, 200, 28), gamesPlayed);
		
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f, Screen.height/5, 200, 28), "Total Wins");
			GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/5, 200, 28), gameWins);
		
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f , Screen.height/4.3f, 200, 28), "Regicide Wins");
			GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/4.3f, 200, 28), regWins);
		
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f , Screen.height/3.7f, 200, 28), "K-599 Wins");
			GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/3.7f, 200, 28), kazWins);
		
			GUI.Label(new Rect(Screen.width - Screen.width/5.5f , Screen.height/3.3f, 200, 28), "Matchstick Wins");
			GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/3.3f, 200, 28), matWins);
			
			
			
			//GUI.Label(new Rect(Screen.width - Screen.width/16, Screen.height/6, 200, 28), "25");
		/*
			GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 28), gamesPlayed.ToString());
			GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 28), gameWins.ToString());
			GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 28), regWins.ToString());
			GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 28), kazWins.ToString());
			GUI.Label(new Rect(Screen.width/2-40 , Screen.height-60, 200, 28), matWins.ToString());
		
		}
	}
	*/
}
