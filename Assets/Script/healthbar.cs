using UnityEngine;
using System.Collections;

public class healthbar : MonoBehaviour 
{
	
	public Main gameMain;
	public float barDisplay = 1.0f;
	public float barFlip = 1.0f;
	public Vector3 pos = new Vector3(0.02f,0.02f,0.0f);
	public GameObject barGraphic;
	//public Vector2 pos = new Vector2(Screen.width - 20.0f,Screen.height - 40.0f);
	public Vector2 size = new Vector2(60.0f,20.0f);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public bool bActive;
	private float reverseBarPOS;
	public bool bar1;
	private float barVel = 0.0f;
	//private float barHealth;
	/*
	void OnGUI()
	{
		if (bActive)
		{
		//print(Screen.width * size.x * barDisplay);
		
	    // draw the background:
		GUI.BeginGroup (new Rect (Screen.width * pos.x, Screen.height * pos.y, Screen.width * size.x, Screen.height * size.y));
	        GUI.Box (new Rect (0,0, Screen.width * size.x, Screen.height * size.y),progressBarEmpty);
	    GUI.EndGroup ();
		
		// draw the current health
	    GUI.BeginGroup (new Rect (Screen.width * pos.x + reverseBarPOS, Screen.height * pos.y, Screen.width * size.x * barDisplay, Screen.height * size.y));
			GUI.Box (new Rect (0,0, Screen.width * size.x , Screen.height * size.y),progressBarFull);
	    GUI.EndGroup ();
		}
	} 
	*/
	public void Activate()
	{
		//barFight = fighter;
		bActive = true;
	}
		
	void Update()
	{
		if (bActive)
		{
			float barHealth;
				
			if (bar1)
			{
				barHealth = gameMain.hp1 *0.001f;	
			}
			else
			{
				barHealth = gameMain.hp2 *0.001f;
			}
				//barFight.GetComponent<CoreStats>().health 
			
			barDisplay = Mathf.SmoothDamp(barDisplay,barHealth,ref barVel,0.06f);
			if (barDisplay < 0)
			{
				barDisplay = 0;
			}
			
			if (bar1)
			{
				barGraphic.transform.localScale = new Vector3(barDisplay * 1.02f, 1, 0.08f);
			}
			else
			{
				barGraphic.transform.localScale = new Vector3(barDisplay * -1.02f, 1, 0.08f);
			}
				barGraphic.transform.localPosition = new Vector3(barDisplay * pos.x,pos.y,pos.z);
			
		}
	}
	public void Reset()
	{
		barDisplay = 1.0f;
	}
}
