using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
	
	public GameObject frontEnd;
	public CharSelect charSelect;
	public Main gameMain;
	public ButtonSelect btnSelect;
	public MoveListScript moveList;
	
	public bool bDisable;
	public bool bOffScreen;
	
	
	public void Activate(int Num)
	{
		//if (!loginWait)
		//{
		if (!bDisable)
		{
			if (Num == 0)
			{
				print("Move list");
				OffScreen();
				moveList.transform.Translate(-1000,0,0);
			}
			if (Num == 1)
			{
				print("change character");
				if (gameMain.bPauseMenu)
				{
					gameMain.ClosePauseMenu();
				}
				else if (gameMain.gameEndMenu)
				{
					gameMain.CloseGameOverMenu();
					gameMain.SendNetCharChange();
				}
			
				charSelect.OnScreen();
				gameMain.CharChangeReset();
				
			}
			if (Num == 2)
			{
				print("Main Menu");
				gameMain.ClosePauseMenu();
				//charSelect.transform.Translate(-1000,0,0);
				//charSelect.OnScreen();
				gameMain.QuitGame();
				frontEnd.GetComponent<FrontEnd>().ReActivate();
				//bOffScreen = true;
			}
			if (Num == 3)
			{
				print("Joy 1");
				btnSelect.bButtonSelect = true;
			}
			if (Num == 4)
			{
				print("Joy 2");
				btnSelect.bButtonSelect = true;
				btnSelect.bPlayer2 = true;
			}
			if (Num == 5)
			{
				
				print("close menu");
				gameMain.ClosePauseMenu();
				//this.transform.Translate(1000,0,0);
			}
			if (Num == 6)
			{
				//switch to Host Game
				print("Rematch");
				gameMain.RematchPress();
				gameMain.CloseGameOverMenu();
				//this.transform.Translate(1000,0,0);
			}
			
			if (Num == 7)
			{
				//switch to Host Game
				print("disconnect");
				gameMain.CloseGameOverMenu();
				gameMain.Disconnect();
				gameMain.QuitGame();
				frontEnd.GetComponent<FrontEnd>().ReActivate();
				//this.transform.Translate(1000,0,0);
			}
			if (Num == 8)
			{
				//switch to Host Game
				print("close move list");
				moveList.transform.Translate(1000,0,0);
				Reactivate();
				
			}
		}	
	}
	
	public void Reactivate()
	{
		this.transform.Translate(-1000,0,0);
		bOffScreen = false;
		//netManage.isOffline = false;
	}
	
	public void OffScreen()
	{
		this.transform.Translate(1000,0,0);
		bOffScreen = true;
	}
	
}

