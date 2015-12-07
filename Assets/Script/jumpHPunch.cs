using UnityEngine;
using System.Collections;

public class jumpHPunch : MonoBehaviour 
{
	public int type = 2; //2 is high hitting
	public float startTime = 4.0f;
	public float hitTime = 4.0f;
	public float endTime = 7.0f;
	public float push = 0.5f;
	public float stun = 11.0f;
	public int damage = 20;
	public int exGain = 10;
	public Vector3 spawnPosition = new Vector3 (2.5f, 10.0f, 0.0f);	
	//var punchHitPoints = 1;
	public GameObject hitBox;
	public GameObject dangerBox;
	public GameObject hurtBoxs;
}