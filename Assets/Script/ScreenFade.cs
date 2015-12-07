using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour 
{

	public float fadeInTime = 5.0f;
	public float fadeOutTime = 5.0f;
	public float alphaFadeValue = 1.0f;
	public Texture fadeTexture;
	public bool fadeOut;
	public bool fadeIn;
	public bool bRoundStarted;
	public Main gameMain;
	
	void Awake()
	{
		gameMain = GetComponent<Main>();
	}
	
	void OnGUI()
	{
		if (fadeOut)
		{
			FadeToBlack();
		}
		else if (fadeIn)
		{
			FadeFromBlack();
		}
	}

	void FadeToBlack()
	{
		alphaFadeValue += Mathf.Clamp01(Time.deltaTime / fadeOutTime);
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height ), fadeTexture,ScaleMode.StretchToFill,false,10.0f );
		if ( alphaFadeValue >= 1.0)
		{
			alphaFadeValue = 1.0f;
			
			if (gameMain.gameOver)
			{
				
			}
			else
			{
				fadeOut = false;
				
			}
		}
		
	}
	
	void FadeFromBlack()
	{
		alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / fadeInTime);
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height ), fadeTexture,ScaleMode.StretchToFill,false,10.0f );
		if ( alphaFadeValue <= 0)
		{
			alphaFadeValue = 0;
			fadeIn = false;
			//gameMain.bFadeInComplete = true;
		}
	}
}
