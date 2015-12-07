using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour 
{
		public NetworkPlayer player1;
		public NetworkPlayer player2;
	
		public bool control2 = false;
		public bool netplay = false;
		
		public Main gameMain;
		public NetworkManager netManage;
		public bool upButton;
		public bool downButton;
		public bool leftButton;
		public bool rightButton;
	
		public int cliFrame;
		public int servFrame;
	
		public bool upButton2;
		public bool downButton2;
		public bool leftButton2;
		public bool rightButton2;
		// tracks held
		public bool attack01Button;
		public bool attack02Button;
		public bool attack03Button;
		public bool attack04Button;
		public bool attack01Button2;
		public bool attack02Button2;
		public bool attack03Button2;
		public bool attack04Button2;
		
		// tracks first press
		public bool attack01ButtonDown;
		public bool b01dframe1;
		public bool b01dframe2;
		public bool b01dframe3;
	
		public bool attack02ButtonDown;
		public bool b02dframe1;
		public bool b02dframe2;
		public bool b02dframe3;
	
		public bool attack03ButtonDown;
		public bool b03dframe1;
		public bool b03dframe2;
		public bool b03dframe3;
	
		public bool attack04ButtonDown;
		public bool b04dframe1;
		public bool b04dframe2;
		public bool b04dframe3;
	
		public bool attack01ButtonDown2;
		public bool b01d2frame1;
		public bool b01d2frame2;
		public bool b01d2frame3;
	
		public bool attack02ButtonDown2;
		public bool b02d2frame1;
		public bool b02d2frame2;
		public bool b02d2frame3;
	
		public bool attack03ButtonDown2;
		public bool b03d2frame1;
		public bool b03d2frame2;
		public bool b03d2frame3;
	
		public bool attack04ButtonDown2;
		public bool b04d2frame1;
		public bool b04d2frame2;
		public bool b04d2frame3;
		
		// tracks release
		public bool attack01ButtonUp;
		public bool	b01uframe1;
		public bool	b01uframe2;
		public bool	b01uframe3;
	
		public bool attack02ButtonUp;
		public bool	b02uframe1;
		public bool	b02uframe2;
		public bool	b02uframe3;
	
		public bool attack03ButtonUp;
		public bool	b03uframe1;
		public bool	b03uframe2;
		public bool	b03uframe3;
	
		public bool attack04ButtonUp;
		public bool	b04uframe1;
		public bool	b04uframe2;
		public bool	b04uframe3;
	
		public bool attack01ButtonUp2;
		public bool	b01u2frame1;
		public bool	b01u2frame2;
		public bool	b01u2frame3;
		public bool attack02ButtonUp2;
		public bool	b02u2frame1;
		public bool	b02u2frame2;
		public bool	b02u2frame3;
	
		public bool attack03ButtonUp2;
		public bool	b03u2frame1;
		public bool	b03u2frame2;
		public bool	b03u2frame3;
	
		public bool attack04ButtonUp2;
		public bool	b04u2frame1;
		public bool	b04u2frame2;
		public bool	b04u2frame3;
	
		public bool netUpButton;
		public bool netDownButton;
		public bool netLeftButton;
		public bool netRightButton;
		public bool netFire1;
		public bool netFire2;
		public bool netFire3;
		public bool netFire4;
	
		public bool netUpButton2;
		public bool netDownButton2;
		public bool netLeftButton2;
		public bool netRightButton2;
		public bool netFire12;
		public bool netFire22;
		public bool netFire32;
		public bool netFire42;
	
		private int pressCount;
		private int	sendCount;
		private int recCount;
		private bool btnDown1;
		private bool btnDown2;
		private bool btnDown3;
		private bool btnDown4;
		private bool btnUp1;
		private bool btnUp2;
		private bool btnUp3;
		private bool btnUp4;
	
		public bool bSpecialQCR;
		public bool bSpecialQCL;
		public bool bSpecialDPR;
		public bool bSpecialDPL;
		public bool bSpecialHCL;
		public bool bSpecial03;
		public bool bSpecial04;
	
		public bool bSpecialQCR2;
		public bool bSpecialQCL2;
		public bool bSpecialDPR2;
		public bool bSpecialDPL2;
		public bool bSpecialHCL2;
		public bool bSpecial032;
		public bool bSpecial042;
	
		public struct playerInputs
		{
			public string upButton;
			public string downButton;
			public string leftButton;
			public string rightButton;
			public bool invert;
			public string lpButton;
			public string lkButton;
			public string hpButton;
			public string hkButton;
			
			public string upButton2;
			public string downButton2;
			public string leftButton2;
			public string rightButton2;
			public bool invert2;
			public string lpButton2;
			public string lkButton2;
			public string hpButton2;
			public string hkButton2;
			
		}
		//public playerInputs localPlayerInput = new playerInputs("joy01hori","joy01hori","joy01vert","joy01vert",false,"joy01btn01","joy01btn02","joy01btn03","joy01btn04");
		public playerInputs localPlayerInput;
	
		// this is the input state we will store each frame
		public class State
		{
			public bool upButton;
			public bool downButton;
			public bool leftButton;
			public bool rightButton;
			public bool attack01ButtonDown;
			public bool attack01Button;
			public bool attack01ButtonUp;
			public bool attack02ButtonDown;
			public bool attack02Button;
			public bool attack02ButtonUp;
			public bool attack03ButtonDown;
			public bool attack03Button;
			public bool attack03ButtonUp;
			public bool attack04ButtonDown;
			public bool attack04Button;
			public bool attack04ButtonUp;
		}

		//bool[] State = new bool[16];
		 
		// an array of states we will store and call
		public List<State> localBufState = new List<State>();
		//State[] networkBufState = new State[];
//		State[] savedBuffer = new State[3000];
		public int localStateCount;
		public float timeThreshold = 0.05F;
		public State playingState;
		State statenet;
	
		// debug
		public int qcrLog;
		public bool qcrDown;
		public bool qcrRight;
	// Use this for initialization
	void Start()
	{
		//gameMain = GetComponent<Main>();
		gameMain = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
		netManage = GameObject.FindGameObjectWithTag("main").GetComponent<NetworkManager>();
		
		
		localPlayerInput.upButton = "joy01vert";
		localPlayerInput.downButton = "joy01vert";
		localPlayerInput.leftButton = "joy01hori";
		localPlayerInput.rightButton = "joy01hori";
		localPlayerInput.invert = false;
		localPlayerInput.lpButton = "joy01btn01";
		localPlayerInput.lkButton = "joy01btn02";
		localPlayerInput.hpButton = "joy01btn04";
		localPlayerInput.hkButton = "joy01btn03";
		
		bool bButtonSaved = PlayerPrefs.HasKey("1h");
		if (bButtonSaved)
		{
			LoadSave();
		}
		else
		{
			print("no save to load");
		}
	}
	

	
	public void CheckInput () 
	{
			
		if (((Input.GetAxis(localPlayerInput.upButton) < -0.5)&&(!localPlayerInput.invert))||(Input.GetButton("upButton")))
		{
			upButton = true;
		}
		else if ((Input.GetAxis(localPlayerInput.upButton) > 0.5)&&(localPlayerInput.invert))
		{
			upButton = true;
		}
		else
		{
			upButton =false;
		}
		
		if (((Input.GetAxis(localPlayerInput.downButton) > 0.5)&&(!localPlayerInput.invert))||(Input.GetButton("downButton")))
		{
			downButton = true;
		}
		else if ((Input.GetAxis(localPlayerInput.downButton) < -0.5)&&(localPlayerInput.invert))
		{
			downButton = true;
		}
		else
		{
			downButton =false;
		}
		
		if ((Input.GetAxis(localPlayerInput.leftButton) < -0.5)||(Input.GetButton("leftButton")))
		{
			leftButton = true;
		}
		else
		{
			leftButton =false;
		}
		
		if ((Input.GetAxis(localPlayerInput.rightButton) > 0.5)||(Input.GetButton("rightButton")))
		{
			rightButton = true;
		}
		else
		{
			rightButton =false;
		}
		//upButton = Input.GetButton("upButton");
		//downButton = Input.GetButton("downButton");
		//leftButton = Input.GetButton("leftButton");
		//rightButton = Input.GetButton("rightButton");

		//upButton2 = Input.GetButton("upButton2");
		//upButton2 = true;
		//downButton2 = Input.GetButton("downButton2");
		
		//leftButton2 = Input.GetButton("leftButton2");
		//rightButton2 = Input.GetButton("rightButton2");
		//rightButton2 = true;
		if (b01dframe3)
		{
			b01dframe1 = false;
			b01dframe2 = false;
			b01dframe3 = false;
			attack01ButtonDown = false;
		}
		else if (b01dframe2)
		{
			b01dframe3 = true;
			attack01ButtonDown = true;
		}
		else if (b01dframe1)
		{
			b01dframe2 = true;
			attack01ButtonDown = true;
		}
		else if(Input.GetButtonDown(localPlayerInput.lpButton)||(Input.GetButtonDown("Fire1")))
		{
			if (!attack01ButtonDown)
			{
				b01dframe1 = true;
				attack01ButtonDown = true;
			}
		}
		else
		{
			attack01ButtonDown = false;
		}
		
		if (Input.GetButton(localPlayerInput.lpButton)||(Input.GetButton("Fire1")))
		{
			attack01Button = true;
		}
		else
		{
			attack01Button = false;
		}
		
		if (b01uframe3)
		{
			b01uframe1 = false;
			b01uframe2 = false;
			b01uframe3 = false;
			attack01ButtonUp = false;
		}
		else if (b01uframe2)
		{
			b01uframe3 = true;
			attack01ButtonUp = true;
		}
		else if (b01uframe1)
		{
			b01uframe2 = true;
			attack01ButtonUp = true;
		}
		else if(Input.GetButtonUp(localPlayerInput.lpButton)||(Input.GetButtonUp("Fire1")))
		{
			if (!attack01ButtonUp)
			{
				b01uframe1 = true;
				attack01ButtonUp = true;
			}
		}
		else
		{
			attack01ButtonUp = false;
		}
		
		if (b02dframe3)
		{
			b02dframe1 = false;
			b02dframe2 = false;
			b02dframe3 = false;
			attack02ButtonDown = false;
		}
		else if (b02dframe2)
		{
			b02dframe3 = true;
			attack02ButtonDown = true;
		}
		else if (b02dframe1)
		{
			b02dframe2 = true;
			attack02ButtonDown = true;
		}
		else if(Input.GetButtonDown(localPlayerInput.hpButton)||(Input.GetButtonDown("Fire2")))
		{
			if (!attack02ButtonDown)
			{
				b02dframe1 = true;
				attack02ButtonDown = true;
			}
		}
		else
		{
			attack02ButtonDown = false;
		}
		if(Input.GetButton(localPlayerInput.hpButton)||(Input.GetButton("Fire2")))
		{
			attack02Button = true;
		}
		else
		{
			attack02Button = false;
		}
		
		if (b02uframe3)
		{
			b02uframe1 = false;
			b02uframe2 = false;
			b02uframe3 = false;
			attack02ButtonUp = false;
		}
		else if (b02uframe2)
		{
			b02uframe3 = true;
			attack02ButtonUp = true;
		}
		else if (b02uframe1)
		{
			b02uframe2 = true;
			attack02ButtonUp = true;
		}
		else if(Input.GetButtonUp(localPlayerInput.hpButton)||(Input.GetButtonUp("Fire2")))
		{
			if (!attack02ButtonUp)
			{
				b02uframe1 = true;
				attack02ButtonUp = true;
			}
		}
		else
		{
			attack02ButtonUp = false;
		}
		
		if (b03dframe3)
		{
			b03dframe1 = false;
			b03dframe2 = false;
			b03dframe3 = false;
			attack03ButtonDown = false;
		}
		else if (b03dframe2)
		{
			b03dframe3 = true;
			attack03ButtonDown = true;
		}
		else if (b03dframe1)
		{
			b03dframe2 = true;
			attack03ButtonDown = true;
		}
		else if(Input.GetButtonDown(localPlayerInput.lkButton)||(Input.GetButtonDown("Fire3")))
		{
			if (!attack03ButtonDown)
			{
				b03dframe1 = true;
				attack03ButtonDown = true;
			}
		}
		else
		{
			attack03ButtonDown = false;
		}
		
		if (Input.GetButton(localPlayerInput.lkButton)||(Input.GetButton("Fire3")))
		{
			attack03Button = true;
		}
		else
		{
			attack03Button = false;
		}
		
		if (b03uframe3)
		{
			b03uframe1 = false;
			b03uframe2 = false;
			b03uframe3 = false;
			attack03ButtonUp = false;
		}
		else if (b03uframe2)
		{
			b03uframe3 = true;
			attack03ButtonUp = true;
		}
		else if (b03uframe1)
		{
			b03uframe2 = true;
			attack03ButtonUp = true;
		}
		else if (Input.GetButtonUp(localPlayerInput.lkButton)||(Input.GetButtonUp("Fire3")))
		{
			if (!attack03ButtonUp)
			{
				b03uframe1 = true;
				attack03ButtonUp = true;
			}
		}
		else
		{
			attack03ButtonUp = false;
		}
		
		if (b04dframe3)
		{
			b04dframe1 = false;
			b04dframe2 = false;
			b04dframe3 = false;
			attack04ButtonDown = false;
		}
		else if (b04dframe2)
		{
			b04dframe3 = true;
			attack04ButtonDown = true;
		}
		else if (b04dframe1)
		{
			b04dframe2 = true;
			attack04ButtonDown = true;
		}
		else if(Input.GetButtonDown(localPlayerInput.hkButton)||(Input.GetButtonDown("Fire4")))
		{
			if (!attack04ButtonDown)
			{
				b04dframe1 = true;
				attack04ButtonDown = true;
			}
		}
		else
		{
			attack04ButtonDown = false;
		}
		
		if (Input.GetButton(localPlayerInput.hkButton)||(Input.GetButton("Fire4")))
		{
			attack04Button = true;
		}
		else
		{
			attack04Button = false;
		}
		
		if (b04uframe3)
		{
			b04uframe1 = false;
			b04uframe2 = false;
			b04uframe3 = false;
			attack04ButtonUp = false;
		}
		else if (b04uframe2)
		{
			b04uframe3 = true;
			attack04ButtonUp = true;
		}
		else if (b04uframe1)
		{
			b04uframe2 = true;
			attack04ButtonUp = true;
		}
		else if (Input.GetButtonUp(localPlayerInput.hkButton)||(Input.GetButtonUp("Fire4")))
		{
			if (!attack04ButtonUp)
			{
				b04uframe1 = true;
				attack04ButtonUp = true;
			}
		}
		else
		{
			attack04ButtonUp = false;
		}
		
		

	}

	public void InputBuffer()
	{
		// Save currect received state as 0 in the buffer, safe to overwrite after shifting
		State currentState = new State();

		//print (currentState.attack01ButtonDown);

		currentState.attack01ButtonDown = attack01ButtonDown;
		currentState.attack01Button = attack01Button;
		currentState.attack01ButtonUp = attack01ButtonUp;
		currentState.attack02ButtonDown = attack02ButtonDown;
		currentState.attack02Button = attack02Button;
		currentState.attack02ButtonUp = attack02ButtonUp;
		currentState.attack03ButtonDown = attack03ButtonDown;
		currentState.attack03Button = attack03Button;
		currentState.attack03ButtonUp = attack03ButtonUp;
		currentState.attack04ButtonDown = attack04ButtonDown;
		currentState.attack04Button = attack04Button;
		currentState.attack04ButtonUp = attack04ButtonUp;
		currentState.upButton = upButton;
		currentState.downButton = downButton;
		currentState.leftButton = leftButton;
		currentState.rightButton = rightButton;
		localBufState.Add (currentState);



		//localBufState[gameMain.frameCount].upButton2 = upButton2;
	}
	
	public void PlaybackBuffer()
	{
		playingState = localBufState[gameMain.frameCount];
	}
	
	public void ResetBuffer()
	{
		//savedBuffer = localBufState;
		//localBufState = new State[60000];
	}
		
	public bool HoldLKCheck01()
	{
		if (attack03Button)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool HoldLKCheck02()
	{
		if (attack03Button2)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool HoldHKCheck01()
	{
		if (attack04Button)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool HoldHKCheck02()
	{
		if (attack04Button2)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool QCRCheck(bool fighter1)
	{
		// check through last several frames to look for down and right inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var rightCheck = false;
		var downFrame = 9999;
		var rightFrame = 9999;
		//Debug.Log("qcr check " + fighter1);
		//Debug.Log (netManage.localBufState[i]);
		if (gameMain.frameCount > 8)
		{
			var sBuf = gameMain.frameCount - 8;
			//var downFrame = 9;
			//var rightFrame = 9;
			//Debug.Log("checking from "+sBuf);
			if (fighter1)
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && !netManage.localBufState[i].rightButton && !netManage.localBufState[i].leftButton && !downCheck)
					{
						//Debug.Log("down check "+i);
						//downFrame = i;
						downCheck = true;
						qcrDown = true;
						downFrame = i;
						
						for (int j = downFrame; j < gameMain.frameCount; j++)
			    		{
							//Debug.Log("right check "+ j);
							if (netManage.localBufState[j].rightButton && !netManage.localBufState[j].upButton && !netManage.localBufState[j].downButton && !netManage.localBufState[j].leftButton && !rightCheck)
							{
								//Debug.Log("right check "+ j);
								//rightFrame = i;
								rightCheck = true;
								qcrRight = true;
								rightFrame = j;
							}
						}
					}
					
					
					
					//print("down "+netManage.localBufState[i].downButton+ " "+i);
					//print("right "+netManage.localBufState[i].rightButton+ " "+i);
					/*
					if (downFrame > rightFrame)
					{
						//print ("wrong order");
						downCheck = false;
						rightCheck = false;
					}
					*/
				}
			}
			else
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && !netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].leftButton2 && !downCheck)
					{
						//Debug.Log("down check");
						downFrame = i;
						downCheck = true;
						
						for (int j = downFrame; j < gameMain.frameCount; j++)
		    			{
							if (netManage.localBufState[j].rightButton2 && !netManage.localBufState[j].upButton2 && !netManage.localBufState[j].downButton2 && !netManage.localBufState[j].leftButton2 && !rightCheck)
							{
								//Debug.Log("right check "+ i);
								//rightFrame = i;
								rightCheck = true;
								qcrRight = true;
								rightFrame = j;
							}
						}
					}
					
					
					/*
					if (downFrame > rightFrame)
					{
						//print ("wrong order");
						downCheck = false;
						rightCheck = false;
					}
					*/
					//print("down "+netManage.localBufState[i].downButton2+ " "+i);
					//print("right "+netManage.localBufState[i].rightButton2+ " "+i);
				}
			}
			
			if (downCheck && rightCheck)
			{
				//Debug.Log("qcf check" + gameMain.frameCount);
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
		
	public bool QCLCheck(bool fighter1)
	{
		// check through last several frames to look for down and left inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var leftCheck = false;
		var downFrame = 9999;
		var leftFrame = 9999;
		//Debug.Log("qfc check");
		//localBufState[currentState.timestamp]
		//Debug.Log(gameMain.frameCount );
		if (gameMain.frameCount > 8)
		{
			var sBuf = gameMain.frameCount-8;
			//Debug.Log(sBuf);
			if (fighter1)
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && !netManage.localBufState[i].rightButton && !netManage.localBufState[i].leftButton && !downCheck)
					{
						//Debug.Log("down check");
						downCheck = true;
						downFrame = i;
						
						for (int j = downFrame; j < gameMain.frameCount; j++)
		    			{
							if (!netManage.localBufState[j].rightButton && !netManage.localBufState[j].upButton && !netManage.localBufState[j].downButton && netManage.localBufState[j].leftButton && !leftCheck)
							{
								//Debug.Log("right check "+ i);
								//rightFrame = i;
								leftCheck = true;
								leftFrame = j;
							}
						}
					}
					
					
					
				}
			}
			else
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && !netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].leftButton2 && !downCheck)
					{
						//Debug.Log("down check");
						downFrame = i;
						downCheck = true;
						
						for (int j = downFrame; j < gameMain.frameCount; j++)
		    			{
							if (!netManage.localBufState[j].rightButton2 && !netManage.localBufState[j].upButton2 && !netManage.localBufState[j].downButton2 && netManage.localBufState[j].leftButton2 && !leftCheck)
							{
								//Debug.Log("right check "+ i);
								//rightFrame = i;
								leftCheck = true;
								leftFrame = j;
							}
						}
					}
					
					
					/*
					if (downFrame > rightFrame)
					{
						//print ("wrong order");
						downCheck = false;
						rightCheck = false;
					}
					*/
				}
			}
			if (downCheck && leftCheck)
			{
				//Debug.Log("qcf check");
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
	
	public bool HCLCheck(bool fighter1)
	{
		// check through last several frames to look for right, down and left inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var rightCheck = false;
		var leftCheck = false;
		//Debug.Log("qfc check");
		//localBufState[currentState.timestamp]
		//Debug.Log(gameMain.frameCount );
		if (gameMain.frameCount > 16)
		{
			var sBuf = gameMain.frameCount-16;
			
			if (fighter1)
			{
				//Debug.Log(sBuf);
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && netManage.localBufState[i].rightButton && !netManage.localBufState[i].leftButton)
					{
						//Debug.Log("down check");
						rightCheck = true;
					}
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && !netManage.localBufState[i].rightButton && !netManage.localBufState[i].leftButton)
					{
						//Debug.Log("down check");
						downCheck = true;
					}
					
					if (!netManage.localBufState[i].rightButton && !netManage.localBufState[i].upButton && !netManage.localBufState[i].downButton && netManage.localBufState[i].leftButton)
					{
						//Debug.Log("right check");
						leftCheck = true;
					}
					
				}
			}
			else
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
				//Debug.Log(i);
					if (!netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].leftButton2)
					{
						//Debug.Log("down check");
						rightCheck = true;
					}
					//Debug.Log(i);
					if (netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && !netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].leftButton2)
					{
						//Debug.Log("down check");
						downCheck = true;
					}
					
					if (!netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].upButton2 && !netManage.localBufState[i].downButton2 && netManage.localBufState[i].leftButton2)
					{
						//Debug.Log("right check");
						leftCheck = true;
					}
				}
			}
			
			if (downCheck && leftCheck && rightCheck)
			{
				Debug.Log("hcl check");
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
	
	public bool HCRCheck(bool fighter1)
	{
		// check through last several frames to look for right, down and left inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var rightCheck = false;
		var leftCheck = false;
		//Debug.Log("qfc check");
		//localBufState[currentState.timestamp]
		//Debug.Log(gameMain.frameCount );
		if (gameMain.frameCount > 16)
		{
			var sBuf = gameMain.frameCount-16;
			
			if (fighter1)
			{
				//Debug.Log(sBuf);
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!localBufState[i].downButton && !localBufState[i].upButton && !localBufState[i].rightButton && localBufState[i].leftButton)
					{
						//Debug.Log("down check");
						leftCheck = true;
					}
					//Debug.Log(i);
					if (localBufState[i].downButton && !localBufState[i].upButton && !localBufState[i].rightButton && !localBufState[i].leftButton)
					{
						//Debug.Log("down check");
						downCheck = true;
					}
					
					if (localBufState[i].rightButton && !localBufState[i].upButton && !localBufState[i].downButton && localBufState[i].leftButton)
					{
						//Debug.Log("right check");
						rightCheck = true;
					}
					
				}
			}
			else
			{

			}
			
			if (downCheck && leftCheck && rightCheck)
			{
				Debug.Log("hcr check");
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
	
	public bool DPLCheck(bool fighter1)
	{
		// check through last several frames to look for down and left inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var leftCheck = false;
		var dlCheck = false;
		var downFrame = 9999;
		var leftFrame = 9999;
		//Debug.Log("qfc check");
		//localBufState[currentState.timestamp]
		//Debug.Log(gameMain.frameCount );
		if (gameMain.frameCount > 16)
		{
			var sBuf = gameMain.frameCount-16;
			//Debug.Log(sBuf);
			if (fighter1)
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && !netManage.localBufState[i].rightButton && netManage.localBufState[i].leftButton)
					{
						//Debug.Log("left check");
						leftFrame = i;
						leftCheck = true;
						for (int j = leftFrame; j < gameMain.frameCount; j++)
			    		{
							if (!netManage.localBufState[j].rightButton && !netManage.localBufState[j].upButton && netManage.localBufState[j].downButton && !netManage.localBufState[j].leftButton)
							{
								//Debug.Log("down check");
								downFrame = j;
								downCheck = true;
								for (int k = downFrame; k < gameMain.frameCount; k++)
			    				{
									if (!netManage.localBufState[k].rightButton && !netManage.localBufState[k].upButton && netManage.localBufState[k].downButton && netManage.localBufState[k].leftButton)
									//if (!netManage.localBufState[k].rightButton && !netManage.localBufState[k].upButton && netManage.localBufState[k].leftButton && netManage.localBufState[k].leftButton)
									{
										//Debug.Log("dl check");
										dlCheck = true;
									}
								}
							}
						}		
					}	
				}
			}
			else
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && !netManage.localBufState[i].rightButton2 && netManage.localBufState[i].leftButton2 && !leftCheck)
					{
						//Debug.Log("down check");
						leftFrame = i;
						leftCheck = true;
						for (int j = leftFrame; j < gameMain.frameCount; j++)
			    		{
							if (!netManage.localBufState[j].rightButton2 && !netManage.localBufState[j].upButton2 && netManage.localBufState[j].downButton2 && !netManage.localBufState[j].leftButton2 && !downCheck)
							{
								//Debug.Log("right check");
								downFrame = j;
								downCheck = true;
								for (int k = downFrame; k < gameMain.frameCount; k++)
			    				{
									if (!netManage.localBufState[k].rightButton2 && !netManage.localBufState[k].upButton2 && netManage.localBufState[k].downButton2 && netManage.localBufState[k].leftButton2 && !dlCheck)
									{
										//Debug.Log("right check");
										//rightFrame = i;
										dlCheck = true;
									}
								}
							}
						}
					}
				}
			}
			/*
			if (downFrame < leftFrame)
			{
				//print ("wrong order");
				//print ("down"+downFrame+" left"+leftFrame);
				downCheck = false;
				leftCheck = false;
				dlCheck = false;
			}
			*/
			if (downCheck && leftCheck && dlCheck)
			{
				//Debug.Log("dp check");
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
	
	public bool DPRCheck(bool fighter1)
	{
		// check through last several frames to look for down and left inputs
		// if both have been entered and in order then we flag this true
		// if not then flag it false
		var downCheck = false;
		var rightCheck = false;
		var drCheck = false;
		var downFrame = 9999;
		var rightFrame = 9999;
		//Debug.Log("qfc check");
		//localBufState[currentState.timestamp]
		//Debug.Log(gameMain.frameCount );
		if (gameMain.frameCount > 16)
		{
			var sBuf = gameMain.frameCount-16;
			//Debug.Log(sBuf);
			if (fighter1)
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!netManage.localBufState[i].downButton && !netManage.localBufState[i].upButton && netManage.localBufState[i].rightButton && !netManage.localBufState[i].leftButton && !rightCheck)
					{
						//Debug.Log("right check "+ i);
						rightFrame = i;
						rightCheck = true;
						
						for (int j = rightFrame; j < gameMain.frameCount; j++)
			    		{
							if (!netManage.localBufState[j].rightButton && !netManage.localBufState[j].upButton && netManage.localBufState[j].downButton && !netManage.localBufState[j].leftButton && !downCheck)
							{
								//Debug.Log("down check "+ j);
								downFrame = j;
								downCheck = true;
								for (int k = downFrame; k < gameMain.frameCount; k++)
			    				{
									if (netManage.localBufState[k].rightButton && !netManage.localBufState[k].upButton && netManage.localBufState[k].downButton && !netManage.localBufState[k].leftButton && !drCheck)
									//if (netManage.localBufState[k].rightButton && !netManage.localBufState[k].upButton && !netManage.localBufState[k].leftButton && !drCheck)
									{
										//Debug.Log("rightDown check "+ k);
										//Debug.Log("right check");
										drCheck = true;
									}
								}
							}
							
						}
					}
					
					
				}
			}
			else
			{
				for (int i = sBuf; i < gameMain.frameCount; i++)
			    {
					//Debug.Log(i);
					if (!netManage.localBufState[i].downButton2 && !netManage.localBufState[i].upButton2 && netManage.localBufState[i].rightButton2 && !netManage.localBufState[i].leftButton2 && !rightCheck)
					{
						//Debug.Log("down check");
						rightFrame = i;
						rightCheck = true;
						for (int j = rightFrame; j < gameMain.frameCount; j++)
			    		{
							if (!netManage.localBufState[j].rightButton2 && !netManage.localBufState[j].upButton2 && netManage.localBufState[j].downButton2 && !netManage.localBufState[j].leftButton2 && !downCheck)
							{
								//Debug.Log("right check");
								downFrame = j;
								downCheck = true;
								for (int k = downFrame; k < gameMain.frameCount; k++)
			    				{
									if (netManage.localBufState[k].rightButton2 && !netManage.localBufState[k].upButton2 && netManage.localBufState[k].downButton2 && !netManage.localBufState[k].leftButton2 && !drCheck)
									{
										//Debug.Log("right check");
										//rightFrame = i;
										drCheck = true;
									}
								}
							}
						}
						
					}
				}
			}
			/*
			if (downFrame < rightFrame)
			{
				//print ("wrong order");
				//print ("down"+downFrame+" left"+leftFrame);
				downCheck = false;
				rightCheck = false;
				drCheck = false;
			}
			*/
			if (downCheck && rightCheck && drCheck)
			{
				Debug.Log("dp check");
				return true;
			}
			else 
				return false;
		}
		else
			return false;
	}
	
	
	void LoadSave()
	{
		
		if (PlayerPrefs.GetInt("1vf") == 1)
		{
			localPlayerInput.invert = true;
		}
		else
		{ 
			localPlayerInput.invert = false;
		}
		localPlayerInput.upButton = PlayerPrefs.GetString("1v");
		localPlayerInput.downButton = PlayerPrefs.GetString("1v");
		localPlayerInput.leftButton = PlayerPrefs.GetString("1h");
		localPlayerInput.rightButton = PlayerPrefs.GetString("1h");
		localPlayerInput.lpButton = PlayerPrefs.GetString("1lp");
		localPlayerInput.lkButton = PlayerPrefs.GetString("1lk");
		localPlayerInput.hpButton = PlayerPrefs.GetString("1hp");
		localPlayerInput.hkButton = PlayerPrefs.GetString("1hk");
		print (localPlayerInput.upButton);
		print (localPlayerInput.downButton);
		print (localPlayerInput.leftButton);
		print (localPlayerInput.rightButton);
		print (localPlayerInput.invert);
		print (localPlayerInput.lpButton);
		print (localPlayerInput.lkButton);
		print (localPlayerInput.hpButton);
		print (localPlayerInput.hkButton);
	}
	
	void OnGUI()
	{
		
	
		//GUI.Label(new Rect(Screen.width-40, Screen.height-200, 200, 20), qcrLog.ToString());
		//GUI.Label(new Rect(Screen.width-100, 200, 200, 20), qcrDown.ToString());
		//GUI.Label(new Rect(Screen.width-100, 220, 200, 20), qcrRight.ToString());
		
	}
	
}
