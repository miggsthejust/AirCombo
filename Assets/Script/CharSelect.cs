using UnityEngine;
using System.Collections;

public class CharSelect : MonoBehaviour 
{
	public NetworkManager netManage;
	public ButtonSelect btnSelect;
	public Main gameMain;
	public AnnouncerSounds announce;
	public GameObject cam;
	public GameObject fEnd;
	public bool bDisable;
	public bool bFirstPick;
	
	public float timerInc;
	public int timerTime;
	public TextMesh timerMesh;
	
	public GameObject p1Sel;
	public GameObject p2Sel;
	
	public GameObject f1Port;
	public GameObject f2Port;
	public GameObject f3Port;
	
	public GameObject f1Vs;
	public GameObject f2Vs;
	public GameObject f3Vs;
	
	public TextMesh p1Name;
	public TextMesh p2Name;
	
	private bool bTimerStart;
	
	public GameObject blindSign;
	
	public Vector3 vsPort1Pos;
	public Vector3 vsPort2Pos;
	
	public Vector3 vsPort1Rot;
	public Vector3 vsPort2Rot;
	
	
	
	public void Start()
	{
		timerTime = 30;
	}
	
	void Update()
	{
		if (gameMain.bP1ChoseChar && gameMain.bP2ChoseChar && !gameMain.vsBegin)
		{
			gameMain.vsBegin = true;
			StartCoroutine(VsScreen());
		}
	}
	
	public void Activate(int Num)
	{
		if (!bDisable)
		{
			if (Num == 0)
			{
				print("kaz selected");
				if(netManage.isOffline)
				{
					if (!bFirstPick)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.kazChar;
						p1Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z -0.1f);
						bFirstPick = true;
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.kazChar;
						p2Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z -0.1f);
						StartCoroutine(VsScreen());
						//OffScreen();
					}
				}
				else
				{
					if (Network.isServer)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.kazChar;
						p1Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z-0.1f);
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.kazChar;
						p2Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z-0.1f);
					}
					netManage.SendFighterChoice(1);
					//StartCoroutine(VsScreen());
				}
				//announce.PlaySelected();
			}
			if (Num == 1)
			{
				print ("mat selected");
				if(netManage.isOffline)
				{
					if (!bFirstPick)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.matChar;
						p1Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z -0.1f);
						bFirstPick = true;
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.matChar;
						p2Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z -0.1f);
						StartCoroutine(VsScreen());
					}
				}
				else
				{
					
					if (Network.isServer)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.matChar;
						p1Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z -0.1f);
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.matChar;
						p2Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z -0.1f);
					}
					netManage.SendFighterChoice(2);
					//StartCoroutine(VsScreen());
				}
				//announce.PlaySelected();
			}
			if (Num == 2)
			{
				print("reg selected");
				
				if(netManage.isOffline)
				{
					if (!bFirstPick)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.regChar;
						p1Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z -0.1f);
						bFirstPick = true;
						
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.regChar;
						
						p2Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z -0.1f);
						StartCoroutine(VsScreen());
						
					}
				}
				else
				{
					
					if (Network.isServer)
					{
						gameMain.bP1ChoseChar = true;
						gameMain.fighterChoice01 = gameMain.regChar;
						p1Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z -0.1f);
					}
					else
					{
						gameMain.bP2ChoseChar = true;
						gameMain.fighterChoice02 = gameMain.regChar;
						p2Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z -0.1f);
					}
					netManage.SendFighterChoice(0);
					//StartCoroutine(VsScreen());
				}
				//announce.PlaySelected();
			}
			if (Num == 3)
			{
				//joystick config
				print("joystick config");
				btnSelect.bButtonSelect = true;
				bDisable = true;
			}
			if (Num == 4)
			{
				// blind
				print("blind select");
//				p1Sel.active = false;			
//				p2Sel.active = false;
				blindSign.transform.position = new Vector3(0,1,-2);
				
				// tell opponent if networked
			}
			if (Num == 5)
			{
				// random select
				RandSelect();
			}
			if (Num == 6)
			{
				if (!btnSelect.bButtonSelect)
				{
					// back to main
					print("back to main");
					var fScript = fEnd.GetComponent<FrontEnd>();
					OffScreen();
					fScript.ReActivate();	
					if (gameMain.netPlay)
					{
						if (Network.isServer)
						{
							netManage.EndServer();
						}
							netManage.Disconnect();
					}
				}
			}
		}
	}
	public void OnScreen()
	{
		this.transform.localPosition = new Vector3(0,0,21.96f);
		timerTime = 30;
		timerMesh.text = timerTime.ToString();
		StartCoroutine("Timer");
		blindSign.transform.position = new Vector3(1000,1,-2);
		fEnd.GetComponent<FrontEnd>().bOffScreen = true;
		
		cam.transform.position = new Vector3(0.0f,23.02f,-1.7f);
		cam.transform.rotation = new Quaternion(0.158f,0.0f,0.0f,1.0f);
		
		if (gameMain.netPlay)
		{
			if (Network.isServer)
			{
				p1Name.text = gameMain.playerName;
				p2Name.text = gameMain.player2Name;
			}
			else
			{
				p1Name.text = gameMain.player2Name;
				p2Name.text = gameMain.playerName;
			}
		}
	}
	
	public void OffScreen()
	{
		this.transform.localPosition = new Vector3(1000,0,21.96f);
		StopCoroutine("Timer");
		bFirstPick = false;
		p1Sel.transform.position = new Vector3 (1000,0,0);
		p2Sel.transform.position = new Vector3 (1000,0,0);
	}
	
	void RandSelect()
	{
		print("random select");
		var choice = Random.Range(0,3);
		
		if (netManage.isOffline)
		{
			if (!bFirstPick)
			{
				if (choice == 0)
				{
					gameMain.fighterChoice01 = gameMain.regChar;
					p1Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.24f,f3Port.transform.position.z);
				}
				else if (choice == 1)
				{
					gameMain.fighterChoice01 = gameMain.kazChar;
					p1Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.24f,f1Port.transform.position.z);
				}
				else if (choice == 2)
				{
					gameMain.fighterChoice01 = gameMain.matChar;
					p1Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.24f,f2Port.transform.position.z);
				}
				
				gameMain.bP1ChoseChar = true;
				bFirstPick = true;
			}
			else
			{
				if (choice == 0)
				{
					gameMain.fighterChoice02 = gameMain.regChar;
					p2Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z);
				}
				else if (choice == 1)
				{
					gameMain.fighterChoice02 = gameMain.kazChar;
					p2Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z);
				}
				else if (choice == 2)
				{
					gameMain.fighterChoice02 = gameMain.matChar;
					p2Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z);
				}
				gameMain.bP2ChoseChar = true;
				
				
				StartCoroutine(VsScreen());
			}
		}
		else
		{
			if (Network.isServer)
			{
				if (choice == 0)
				{
					gameMain.fighterChoice01 = gameMain.regChar;
					p1Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z);
				}
				else if (choice == 1)
				{
					gameMain.fighterChoice01 = gameMain.kazChar;
					p1Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z);
				}
				else if (choice == 2)
				{
					gameMain.fighterChoice01 = gameMain.matChar;
					p1Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z);
				}
			}
			else
			{
				if (choice == 0)
				{
					gameMain.fighterChoice02 = gameMain.regChar;
					p2Sel.transform.position = new Vector3 (f3Port.transform.position.x,f3Port.transform.position.y + 0.2f,f3Port.transform.position.z);
				}
				else if (choice == 1)
				{
					gameMain.fighterChoice02 = gameMain.kazChar;
					p2Sel.transform.position = new Vector3 (f1Port.transform.position.x,f1Port.transform.position.y + 0.2f,f1Port.transform.position.z);
				}
				else if (choice == 2)
				{
					gameMain.fighterChoice02 = gameMain.matChar;
					p2Sel.transform.position = new Vector3 (f2Port.transform.position.x,f2Port.transform.position.y + 0.2f,f2Port.transform.position.z);
				}
			}
			
			netManage.SendFighterChoice(choice);
			StartCoroutine(VsScreen());
		}
	}
	
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(timerInc);
		timerTime -= 1;
		timerMesh.text = timerTime.ToString();
		if (timerTime == 0)
		{
			if (netManage.isOffline && !bFirstPick)
			{
				RandSelect();
			}
				RandSelect ();
		}
		else
		{
			StartCoroutine("Timer");
		}
	}
	IEnumerator VsScreen()
	{
		yield return new WaitForSeconds(0.5f); // pause before leaving
		// fade out
		
		yield return new WaitForSeconds(0.5f);
		OffScreen();
		GameObject port1 = new GameObject();
		GameObject port2 = new GameObject();
		if (gameMain.fighterChoice01 == gameMain.kazChar)
		{
			port1 = Instantiate(f1Vs, vsPort1Pos,Quaternion.Euler(vsPort1Rot)) as GameObject;
		}
		else if (gameMain.fighterChoice01 == gameMain.regChar)
		{
			port1 = Instantiate(f3Vs, vsPort1Pos,Quaternion.Euler(vsPort1Rot)) as GameObject;
		}
		else if (gameMain.fighterChoice01 == gameMain.matChar)
		{
			port1 = Instantiate(f2Vs, vsPort1Pos,Quaternion.Euler(vsPort1Rot)) as GameObject;
		}
		
		if (gameMain.fighterChoice02 == gameMain.kazChar)
		{
			port2 = Instantiate(f1Vs,vsPort2Pos,Quaternion.Euler(vsPort2Rot)) as GameObject;
		}
		else if (gameMain.fighterChoice02 == gameMain.regChar)
		{
			port2 = Instantiate(f3Vs,vsPort2Pos,Quaternion.Euler(vsPort2Rot)) as GameObject;
		}
		else if (gameMain.fighterChoice02 == gameMain.matChar)
		{
			port2 = Instantiate(f2Vs,vsPort2Pos,Quaternion.Euler(vsPort2Rot)) as GameObject;
		}
		
		port1.GetComponent<Animation>().Play("leftPortIntro");
		port2.GetComponent<Animation>().Play("rightPortIntro");
		
		announce.PlayVs();
		yield return new WaitForSeconds(2.5f);
		gameMain.bChooseChar = false;
		yield return new WaitForSeconds(0.2f);
		Destroy (port1);
		Destroy (port2);
		gameMain.bP1ChoseChar = false;
		gameMain.bP2ChoseChar = false;
		gameMain.vsBegin = false;
		gameMain.gameOver = false;
	}
}
