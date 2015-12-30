using UnityEngine;
using System.Collections;

public class ButtonSelect : MonoBehaviour 
{
	public PlayerInput pInput;
	
	public string upButton;
	public string downButton;
	public string leftButton;
	public string rightButton;
	public string lPButton;
	public string lKButton;
	public string hPButton;
	public string hKButton;
	public bool bInvert;
	
	private bool bSetUp;
	private bool bSetDown;
	private bool bSetLeft;
	private bool bSetRight;
	private bool bSetLp;
	private bool bSetLk;
	private bool bSetHp;
	private bool bSetHk;
	
	public bool[] setArrayJ1;
	public bool[] setArrayJ2;
	
	public string[] buttonArrayJ1;
	public string[] buttonArrayJ2;
	public string[] axisArrayJ1;
	public string[] axisArrayJ2;
	
	public string[] axisArrayTemp;
	public string[] buttonArrayTemp;
	
	public bool bButtonSelect;
	public bool bPlayer2;
	
	// Use this for initialization
	void Start () 
	{
		//setArrayJ1 = new bool[bSetUp,bSetDown,bSetLeft,bSetRight,bSetLp,bSetLk,bSetHp,bSetHk];
		//setArrayJ2 = new bool[bSetUp,bSetDown,bSetLeft,bSetRight,bSetLp,bSetLk,bSetHp,bSetHk];
		bSetUp = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (bButtonSelect)
		{
			if (Input.GetKey("escape"))
			{
				bButtonSelect = false;	
				
			}
			else
			{
				if (bSetUp)
				{
					for(int i = 0; i < buttonArrayJ1.Length; i++)
					{
						if (Input.GetButtonDown(buttonArrayJ1[i])) // i need some code to wait until the button is back up before moving on.
						{
							upButton = buttonArrayJ1[i];
							bSetUp = false;
							bSetDown = true;
						}
					}
					
					for(int i = 0; i < axisArrayJ1.Length; i++)
					{
						if ((Input.GetAxis(axisArrayJ1[i]) < -0.9)||(Input.GetAxis(axisArrayJ1[i]) > 0.9))
						{
								// if we get a backwards input then we probably need to invert the axis.
							if (Input.GetAxis(axisArrayJ1[i]) > 0.9)
							{
								bInvert = true;
							}
							print (Input.GetAxis(axisArrayJ1[i]));
							upButton = axisArrayJ1[i];
							bSetUp = false;
							bSetDown = true;
							axisArrayTemp = axisArrayJ1;
							buttonArrayTemp = buttonArrayJ1;
						}
					}
					
					for(int i = 0; i < axisArrayJ2.Length; i++)
					{
						if ((Input.GetAxis(axisArrayJ2[i]) < -0.9)||(Input.GetAxis(axisArrayJ2[i]) > 0.9))
						{
								// if we get a backwards input then we probably need to invert the axis.
							if (Input.GetAxis(axisArrayJ2[i]) > 0.9)
							{
								bInvert = true;
							}
							print (Input.GetAxis(axisArrayJ2[i]));
							upButton = axisArrayJ2[i];
							bSetUp = false;
							bSetDown = true;
							axisArrayTemp = axisArrayJ2;
							buttonArrayTemp = buttonArrayJ2;
						}
					}
					// now keyboard keys
				}
				else if (bSetDown)
				{
					for(int i = 0; i < buttonArrayJ1.Length; i++)
					{
						if (Input.GetButton(buttonArrayJ1[i]))
						{
							downButton = buttonArrayJ1[i];
							bSetDown = false;
							bSetLeft = true;
						}
					}
					
					for(int i = 0; i < axisArrayTemp.Length; i++)
					{
						if ((Input.GetAxis(axisArrayTemp[i]) > 0.9)||(Input.GetAxis(axisArrayTemp[i]) < -0.9))
						{
							downButton = axisArrayTemp[i];
							bSetDown = false;
							bSetLeft = true;
						}
					}
					
					// now keyboard keys
				}
				else if (bSetLeft)
				{
					for(int i = 0; i < buttonArrayJ1.Length; i++)
					{
						if (Input.GetButton(buttonArrayJ1[i]))
						{
							leftButton = buttonArrayJ1[i];
							bSetLeft = false;
							bSetRight = true;
						}
					}
					
					for(int i = 0; i < axisArrayTemp.Length; i++)
					{
						if (((Input.GetAxis(axisArrayTemp[i]) > 0.9) && (axisArrayTemp[i] != upButton))|| ((Input.GetAxis(axisArrayTemp[i]) < -0.9) && (axisArrayTemp[i] != upButton)))
						{
							leftButton = axisArrayTemp[i];
							bSetLeft = false;
							bSetRight = true;
						}
					}
					// now keyboard keys
				}
				else if (bSetRight)
				{
					for(int i = 0; i < buttonArrayJ1.Length; i++)
					{
						if (Input.GetButton(buttonArrayJ1[i]))
						{
							rightButton = buttonArrayJ1[i];
							bSetRight = false;
							bSetLp = true;
						}
					}
					
					for(int i = 0; i < axisArrayTemp.Length; i++)
					{
						if ((Input.GetAxis(axisArrayTemp[i]) > 0.9) || (Input.GetAxis(axisArrayTemp[i]) < -0.9))
						{
							rightButton = axisArrayTemp[i];
							bSetRight = false;
							bSetLp = true;
						}
					}
					// now keyboard keys
				}
					
				else if (bSetLp)
				{
					//if (!Input.GetButton(rightButton))
					//{
					for(int i = 0; i < buttonArrayTemp.Length; i++)
					{
						if (Input.GetButtonDown(buttonArrayTemp[i]))
						{
							print (buttonArrayTemp[i]);
							lPButton = buttonArrayTemp[i];
							bSetLp = false;
							bSetLk = true;
						}
					}
					//}
					// now keyboard keys
				}
				else if (bSetLk)
				{
					if (!Input.GetButton(lPButton))
					{
						for(int i = 0; i < buttonArrayTemp.Length; i++)
						{
							if (Input.GetButtonDown(buttonArrayTemp[i]))
							{
								lKButton = buttonArrayTemp[i];
								bSetLk = false;
								bSetHp = true;
							}
						}
					}
					// now keyboard keys
				}
				else if (bSetHp)
				{
					if (!Input.GetButton(lKButton))
					{
						for(int i = 0; i < buttonArrayTemp.Length; i++)
						{
							if (Input.GetButtonDown(buttonArrayTemp[i]))
							{
								hPButton = buttonArrayTemp[i];
								bSetHp = false;
								bSetHk = true;
							}
						}
					}
					// now keyboard keys
				}
				else if (bSetHk)
				{
					if (!Input.GetButton(hPButton))
					{
						for(int i = 0; i < buttonArrayTemp.Length; i++)
						{
							if (Input.GetButtonDown(buttonArrayTemp[i]))
							{
								hKButton = buttonArrayTemp[i];
								bSetHk = false;
								SendButtons();
								bButtonSelect = false;
								
								bSetUp = true;
							}
						}
					}
					// now keyboard keys
				}
			}
			//fighter2
		}
	}
	
	void SendButtons()
	{
		pInput = gameObject.GetComponent<PlayerInput>();
		if (bPlayer2)
		{
			pInput.localPlayerInput.upButton2 = upButton;
			pInput.localPlayerInput.downButton2 = downButton;
			pInput.localPlayerInput.leftButton2 = leftButton;
			pInput.localPlayerInput.rightButton2 = rightButton;
			pInput.localPlayerInput.lpButton2 = lPButton;
			pInput.localPlayerInput.lkButton2 = lKButton;
			pInput.localPlayerInput.hpButton2 = hPButton;
			pInput.localPlayerInput.hkButton2 = hKButton;
			pInput.localPlayerInput.invert2 = bInvert;
			print ("player 2 buttons set");
		}
		else
		{
			pInput.localPlayerInput.upButton = upButton;
			pInput.localPlayerInput.downButton = downButton;
			pInput.localPlayerInput.leftButton = leftButton;
			pInput.localPlayerInput.rightButton = rightButton;
			pInput.localPlayerInput.lpButton = lPButton;
			pInput.localPlayerInput.lkButton = lKButton;
			pInput.localPlayerInput.hpButton = hPButton;
			pInput.localPlayerInput.hkButton = hKButton;
			pInput.localPlayerInput.invert = bInvert;
			print ("buttons sent to playerInput");
			
			PlayerPrefs.SetString("1h",leftButton);
			PlayerPrefs.SetString("1v",upButton);
			if (bInvert)
			{
				PlayerPrefs.SetInt("1vf",1);
			}
			else
			{
				PlayerPrefs.SetInt("1vf",0);
			}
			PlayerPrefs.SetString("1lp",lPButton);
			PlayerPrefs.SetString("1lk",lKButton);
			PlayerPrefs.SetString("1hp",hPButton);
			PlayerPrefs.SetString("1hk",hKButton);
			PlayerPrefs.Save();
			// also add a isButtons bool for move buttons
		}
		bPlayer2 = false;
	}
	
	void OnGUI()
	{
		if (bButtonSelect)
		{
			if (bSetUp)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press Up");
			}
			else if (bSetDown)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press Down");
			}
			else if (bSetLeft)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press Left");
			}
			else if (bSetRight)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press Right");
			}
			else if (bSetLp)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press LP");
			}
			else if (bSetLk)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press LK");
			}
			else if (bSetHp)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press HP");
			}
			else if (bSetHk)
			{
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2,200,30),"Press HK");
			}
		}
	}
}
