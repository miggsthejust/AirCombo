using UnityEngine;
using System.Collections;

public class hypeGuage : MonoBehaviour 
{
	public int hypeAmount; // measures amount of hype available
	public bool hypeLvl1; // 25 hype
	public bool hypeLvl2; // 50 hype
	public bool hypeLvl3; // 75 hype
	public bool hypeLvl4; // 100 hype
	
	
	//for GUI
	//public GameObject barFight;  //the character that this bar is displaying the health of.
	public float barDisplay = 1.0f;
	public float barFlip = 1.0f;
	public Vector2 pos = new Vector2(0.02f,0.02f);
	//public Vector2 pos = new Vector2(Screen.width - 20.0f,Screen.height - 40.0f);
	public Vector2 size = new Vector2(60.0f,20.0f);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	private bool bActive;
	private float reverseBarPOS;
	public bool bar1;
	private float barVel = 0.0f;
	public int hypePOS;
	// Use this for initialization
	void Start () 
	{
		hypeAmount = 0;
		hypeLvl1 = false;
		hypeLvl2 = false;
		hypeLvl3 = false;
		hypeLvl4 = false;
		if (this.tag == "fighter1")
		{
			hypePOS = 10;
		}
		else
		{
			hypePOS = Screen.width - 50;
		}
	}
	void Update()
	{
		if (hypeAmount > 100)
		{
			hypeAmount = 100;
		}
	}
	public void ResetHype()
	{
		hypeAmount = 0;
		hypeLvl1 = false;
		hypeLvl2 = false;
		hypeLvl3 = false;
		hypeLvl4 = false;
	}
	/*
	void OnGUI()
	{
		GUI.Label(new Rect(hypePOS, Screen.height-30, 200, 20), "hype "+hypeAmount.ToString());
	}
	*/
	
	/*
	// display
	void OnGUI()
	{
		//print(Screen.width * size.x * barDisplay);
		
	    // draw the background:
		GUI.BeginGroup (new Rect (Screen.width * pos.x, Screen.height * pos.y, Screen.width * size.x, Screen.height * size.y));
	        GUI.Box (new Rect (0,0, Screen.width * size.x, Screen.height * size.y),progressBarEmpty);
	    GUI.EndGroup ();
		
		// draw the current hype
	    GUI.BeginGroup (new Rect (Screen.width * pos.x , Screen.height * pos.y, Screen.width * size.x * barDisplay, Screen.height * size.y));
			GUI.Box (new Rect (0,0, Screen.width * size.x , Screen.height * size.y),progressBarFull);
	    GUI.EndGroup ();
	} 
	
	public void Activate()
	{
		//barFight = fighter;
		bActive = true;
	}
	
	void Update()
	{
		if (bActive)
		{
			var barHype = hypeAmount *0.001f;
			
			barDisplay = Mathf.SmoothDamp(barDisplay,barHype,ref barVel,0.06f);
			
			//Debug.Log("bar "+barDisplay);
			// if this is the first bar we shrink it from left to right.
			if (bar1)
			{
				//reverseBarPOS = Screen.width * size.x - (Screen.width * size.x * barDisplay);
				//print(reverseBarPOS);
			}
		}
	}
	public void Reset()
	{
		barDisplay = 1.0f;
	}
	*/
}
