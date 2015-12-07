using UnityEngine;
using System.Collections;

public class hypebar : MonoBehaviour 
{

	public Main gameMain;
	public float barDisplay = 1.0f;
	public float barFlip = 1.0f;
	public Vector3 pos = new Vector3(0.02f,0.02f,0.2f);
	//public Vector2 pos = new Vector2(Screen.width - 20.0f,Screen.height - 40.0f);
	public Vector2 size = new Vector2(60.0f,20.0f);
	public GameObject barGraphic;
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
		//print(Screen.width * size.x * barDisplay);
		if (bActive)
		{
	    // draw the background:
		GUI.BeginGroup (new Rect (Screen.width * pos.x, Screen.height * pos.y, Screen.width * size.x, Screen.height * size.y));
	        GUI.Box (new Rect (0,0, Screen.width * size.x, Screen.height * size.y),progressBarEmpty);
	    GUI.EndGroup ();
		
		// draw the current health
	    GUI.BeginGroup (new Rect (Screen.width * pos.x , Screen.height * pos.y, Screen.width * size.x * barDisplay, Screen.height * size.y));
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
				barHealth = gameMain.hy1 * 0.01f;
			}
			else
			{
				barHealth = gameMain.hy2 * 0.01f;
			}
			
			barDisplay = Mathf.SmoothDamp(barDisplay,barHealth,ref barVel,0.06f);
			
			if (bar1)
			{
				barGraphic.transform.localScale = new Vector3(barDisplay * 0.7505f, 1, 0.0525f);
			}
			else
			{
				barGraphic.transform.localScale = new Vector3(barDisplay * -0.7505f, 1, 0.0525f);
			}
			
			// if this is the first bar we shrink it from left to right.
			if (bar1)
			{
				barGraphic.transform.localPosition = new Vector3(-11.25f - (barDisplay * pos.x/2),pos.y,pos.z);
				//reverseBarPOS = Screen.width * size.x - (Screen.width * size.x * barDisplay);
				//print(reverseBarPOS);
			}
			else
			{
				barGraphic.transform.localPosition = new Vector3(11.25f - (barDisplay * pos.x/2),pos.y,pos.z);
			}
		}
	}
	public void Reset()
	{
		barDisplay = 1.0f;
	}
}
