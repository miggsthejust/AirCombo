using UnityEngine;
using System.Collections;

public class FrontEnd : MonoBehaviour 
{
	public GameObject butBar;
	public GameObject profBar;
	public GameObject netBar;
	public GameObject hostBar;
	public GameObject logBar;
	public GameObject facBar;
	public CharSelect charSelect;
	public GameObject waitMes;
	public NetworkManager netManage;
	public Main gameMain;
	public GameObject globe;
	public GameObject cam;
	public PieChartMeshController chart;
	public GameObject controlWin;
	
	public bool bLoginPress;
	public bool bProfilePress;
	public bool loginWait;
	public bool bDisable;
	public bool bWaiting;
	public bool bOffScreen;
	public float panTime;
		
	// Use this for initialization
	void Start () 
	{
		loginWait = true;
		this.transform.localPosition = new Vector3(0,-0.33f,21.96f);
		//loggedIn();
		logBar.transform.localPosition = new Vector3(1000,6.76f,0.32f);
		butBar.transform.localPosition = new Vector3(-0.6f,5.9f,0.8f);
		profBar.transform.localPosition = new Vector3(0.58f,5.8f,0.76f);
		cam.GetComponent<Animation>().Play("camLoginSwoop");
		StartCoroutine(ActivateChart());
	}	
	
	public void Activate(int Num)
	{
		//if (!loginWait)
		//{
		if (!bDisable)
		{
			if (Num == 0)
			{
				
				print("online selected");
				butBar.transform.localPosition = new Vector3(1000,5.9f,0.8f);
				profBar.transform.localPosition = new Vector3(1000.58f,5.8f,0.76f);
				globe.transform.position = new Vector3(1000,14.86f,18.43f);
				chart.transform.localPosition = new Vector3(1000,16.94f,12.5f);
				//netBar.transform.Translate(-1000,0,0);
				gameMain.netManage.bCasual = false;
				
				if (!loginWait)
				{
					netBar.transform.localPosition = new Vector3(-0.6f,6.77f,0.79f);
					netManage.netMenu = true;
					netManage.RefreshHostList();
				}
				else
				{
					logBar.transform.localPosition = new Vector3(0,6.76f,0.32f);
//					webLog.bLogin = false;
				}
			}
			if (Num == 16)
			{
				
				print("online selected");
				butBar.transform.localPosition = new Vector3(1000,5.9f,0.8f);
				profBar.transform.localPosition = new Vector3(1000.58f,5.8f,0.76f);
				globe.transform.position = new Vector3(1000,14.86f,18.43f);
				chart.transform.localPosition = new Vector3(1000,16.94f,12.5f);
				//netBar.transform.Translate(-1000,0,0);
				gameMain.netManage.bCasual = true;
				if (!loginWait)
				{
					netBar.transform.localPosition = new Vector3(-0.6f,6.77f,0.79f);
					netManage.netMenu = true;
					netManage.RefreshHostList();
				}
				else
				{
					logBar.transform.localPosition = new Vector3(0,6.76f,0.32f);
//					webLog.bLogin = false;
				}
			}
			if (Num == 1)
			{
				netManage.isOffline = true;
				//butBar.transform.Translate(1000,0,0);
				//switch to character select
				this.transform.localPosition = new Vector3(1000,-0.33f,21.96f);
				globe.transform.position = new Vector3(1000,14.86f,18.43f);
				chart.transform.localPosition = new Vector3(1000,16.94f,12.5f);
				//charSelect.transform.Translate(-1000,0,0);
				charSelect.OnScreen();
				bOffScreen = true;
			}
			if (Num == 2)
			{
				//switch to Options
				butBar.transform.localPosition = new Vector3(1000,5.9f,0.8f);
				chart.transform.localPosition = new Vector3(1000,16.94f,12.5f);
				//this.transform.Translate(1000,0,0);
			}
			if (Num == 3)
			{
				//switch to Host Game
				if (!bWaiting)
				{
					
					print("host game menu");
					netBar.transform.localPosition = new Vector3(1000,6.77f,0.79f);
					//hostBar.transform.Translate(-1000,0,0);
					hostBar.transform.localPosition = new Vector3(0,1.16f,-0.34f);
					netManage.netMenu = false;
					netManage.bHostName = true;
					
					
				}
				//this.transform.Translate(1000,0,0);
			}
			if (Num == 4)
			{
				// refresh server list
				netManage.RefreshHostList();
			}
			if (Num == 5)
			{
				// return to main from net
				print("back to main");
				butBar.transform.localPosition = new Vector3(-0.6f,5.9f,0.8f);
				netBar.transform.localPosition = new Vector3(1000,6.77f,0.79f);
				globe.transform.position = new Vector3(-0.26f,14.86f,18.43f);
				chart.transform.localPosition = new Vector3(-0.18f,16.94f,12.5f);
				profBar.transform.localPosition = new Vector3(0.58f,5.8f,0.76f);
				netManage.netMenu = false;
			}
			if (Num == 6)
			{
				// start server from host menu
				print("Begin Host Game");
				hostBar.transform.localPosition = new Vector3(1000,6.77f,0.79f);
				netManage.bHostName = false;
				netManage.StartServer();
				waitMes.transform.localPosition = new Vector3(0,-5.24f,16.84f);
				bWaiting = true;
				
				/*
				butBar.transform.Translate(-1000,0,0);
				globe.transform.position = new Vector3(-0.26f,14.86f,18.43f);
				chart.transform.Translate(-1000,0,0);
				profBar.transform.localPosition = new Vector3(0.58f,5.8f,0.76f);
				netManage.netMenu = false;
				*/
			}
			if (Num == 7)
			{
				// cancel hosting server
				print("cancel hosting");
				if (netManage.netMenu)
				{
				netBar.transform.localPosition = new Vector3(-0.6f,6.77f,0.79f);
				hostBar.transform.localPosition = new Vector3(1000,6.77f,0.79f);
				netManage.netMenu = true;
				netManage.bHostName = false;
				netManage.PollMasterServer();
				}
				netManage.EndServer();
				if (bWaiting)
				{
					waitMes.transform.localPosition = new Vector3(1000,-5.24f,16.84f);
					bWaiting = false;
				}
			}
		//}
			if (Num == 8)
			{
				// login
				bLoginPress = true;
//				webLog.StartLogin();
			}
			if (Num == 9)
			{
				// create new account
				//bProfilePress = true;
			
				// switch to faction choice menu.
			}
			if (Num == 10)
			{
				// added separate entry for training mode
				netManage.isOffline = true;
				//butBar.transform.Translate(1000,0,0);
				//switch to character select
				this.transform.localPosition = new Vector3(1000,-0.33f,21.96f);
				//globe.transform.Translate(1000,0,0);
				chart.transform.localPosition = new Vector3(1000,16.94f,12.5f);
				charSelect.OnScreen();
				bOffScreen = true;
				gameMain.DinfHealth = true;
				gameMain.bInfTime = true;
			}
			if (Num == 11)
			{
//				webLog.StartCreate(1);
				bProfilePress = true;
				loggedIn();
			}
			if (Num == 12)
			{
		//		webLog.StartCreate(3);
				bProfilePress = true;
				loggedIn();
			}
			if (Num == 13)
			{
		//		webLog.StartCreate(2);
				bProfilePress = true;
				loggedIn();
			}
			if (Num == 14)
			{
				// control screen
				controlWin.transform.localPosition = new Vector3(0,6.55f,0);
			}
			if (Num == 15)
			{
				// control screen off
				controlWin.transform.localPosition = new Vector3(1000,6.55f,0);
			}
		}
		
	}
	public void loggedIn()
	{
		
		bLoginPress = false;
		logBar.transform.localPosition = new Vector3(1000,6.76f,0.32f);
		facBar.transform.localPosition = new Vector3(1000,0,0);
		loginWait = false;
		netBar.transform.localPosition = new Vector3(-0.6f,6.77f,0.79f);
		netManage.netMenu = true;
		netManage.RefreshHostList();
	}
	public void ReActivate()
	{
		//cam.animation.Play("camLoginSwoop");
		cam.transform.position = new Vector3(0.0f,23.02f,-1.7f);
		cam.transform.rotation = new Quaternion(0.158f,0.0f,0.0f,1.0f);
		netManage.isOffline = false;
		ResetMenu();
	}
	public void OffScreen()
	{
		this.transform.localPosition = new Vector3(1000,-0.33f,21.96f);
		globe.transform.position = new Vector3(-0.26f,14.86f,18.43f);
		bOffScreen = true;
	}
	
	public void ResetMenu()
	{
		this.transform.localPosition = new Vector3(0,-0.33f,21.96f);
		butBar.transform.localPosition = new Vector3(-0.6f,5.9f,0.8f);
		globe.transform.position = new Vector3(-0.26f,14.86f,18.43f);
		profBar.transform.localPosition = new Vector3(0.58f,5.8f,0.76f);
		chart.transform.localPosition = new Vector3(-0.18f,16.94f,12.5f);
		netBar.transform.localPosition = new Vector3(1000,6.77f,0.79f);
		logBar.transform.localPosition = new Vector3(1000,6.76f,0.32f);
		facBar.transform.localPosition = new Vector3(1000,0,0);
		waitMes.transform.localPosition = new Vector3(1000,-5.24f,16.84f);
		controlWin.transform.localPosition = new Vector3(1000,6.55f,0);
		bOffScreen = false;
	}
	
	IEnumerator ActivateChart()
	{
//		webLog.GetChartData();
		yield return new WaitForSeconds(panTime);
		
		chart.Activate();
	}
	public void MessageOff()
	{
		waitMes.transform.localPosition = new Vector3(1000,-5.24f,16.84f);
		bWaiting = false;
	}
}

