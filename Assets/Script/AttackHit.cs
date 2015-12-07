using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AttackHit
{
	public GameObject hitBox;
	public int type = 0; // 0 is mid hitting
	public float hitTime = 4.0f;
	
	public Vector3 spawnPosition = new Vector3 (0.0f,5.0f, 2.0f);	
	
	public Vector3 push = new Vector3(0.5f,0.0f,0.0f);
	public float stun = 10.0f;
	public int damage = 20;
	public int exGain = 10;
	public bool launch = false;
	public bool wallBounce = false;

	// dummy gravity increasing vars
	public float gravInc; // amount to increase dummy gravity by.
	public float permInc = 0.1f; // sent to dummy gravity every hit. Slowly increases on each hit.
	public float pushYMax = 0.0f; // the amount that pushY will return to gradually.
	public float pushYOriginal = 0.0f; // the amount that pushY will return to gradually.
	public float tempInc = 0.0f; // increases on each hit and slowly decreases back to zero.

	public bool moves = false;
	public Vector3 moveSpeed = new Vector3(0.0f,0.0f,0.0f);
	
	public bool normalCancel = false;
	public bool specialCancel = false;
	public bool superCancel = false;
	public bool jumpCancel = false;

	public void Activate()
	{
		pushYMax = push.y;
		pushYOriginal = push.y;
		Debug.Log ("push Y = " + pushYOriginal);
	}

	public void IncreaseGrav()
	{
		push.y -= permInc; 
		pushYMax -= permInc;

		push.y -= tempInc;
		Debug.Log ("increasing grav by " + tempInc + permInc);
	}

	public void ReduceTemp(float redAmt)
	{
		//Debug.Log ("reduceTemp");
		if (pushYMax > push.y) 
		{
			push.y = push.y + redAmt;
		}
		if (pushYMax < push.y) 
		{
			push.y = pushYMax;
		}
	}

	public void ResetPush()
	{
		push.y = pushYOriginal;
		Debug.Log ("push Y = " + pushYOriginal);
	}
}