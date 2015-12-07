using UnityEngine;
using System.Collections;

public class CoreStats : MonoBehaviour 
{
	// this is where the major variables of the fighter are kept.
	public GameObject opponent;
	public Transform enemyPos;
	public Transform enemyBack;
	public Transform backPos;
	public int health = 1000;
	public bool knockedOut = false;
	
	public float pushAmount = 2;
	public float hitSpeed = 6; //speed of pushback when hit.
	public float downFrames = 90.0f;
	public float upFrames = 8.0f;
	
	public int throwTime;
	public int breakWindow = 7;
	public int softWindow = 12;
	public float throwDist = 5.5f;
	/*
	void Update()
	{
		health = 1000;
	}*/
}