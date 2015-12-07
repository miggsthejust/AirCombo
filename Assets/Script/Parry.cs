using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour 
{
	public float parryWinTime = 4.0f; //in frames
	public float parryTime = 12.0f;
	// Use this for initialization
	void Start () 
	{
		parryWinTime = parryWinTime/60;
		parryTime = parryTime/60;
		//print ("parryWinTime "+parryWinTime);
	}
	
}
