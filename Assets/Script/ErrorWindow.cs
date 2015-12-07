using UnityEngine;
using System.Collections;

public class ErrorWindow : MonoBehaviour 
{
	public TextMesh errorMessage;
	public int returnDest;
	public FrontEnd fEnd;
	public Main gameMain;
	public GameObject returnBut;
	
	public void Error(string mes,int dest)
	{
		OnScreen();
		errorMessage.text = mes;
		returnDest = dest;
	}
	
	void ReturnButton()
	{
		if (returnDest == 1)
		{
			if (fEnd.bOffScreen)
			{
				fEnd.ReActivate();
				
				if (gameMain.begun)
				{
					gameMain.QuitGame();
				}
			}
			OffScreen();
		}
	}
	
	void OnScreen()
	{
		this.transform.localPosition = new Vector3(0,0,7.5f);
	}
	
	void OffScreen()
	{
		this.transform.Translate(1000,0,0);
	}
	
	void OnMouseUp()
	{
		ReturnButton ();
	}
}
