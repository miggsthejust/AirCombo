using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class standLKick : MonoBehaviour 
{
	public int type = 0; // 0 is mid hitting
	public float startTime = 4.0f;
	public float hitTime = 4.0f;
	public float endTime = 7.0f;
	public float push = 0.5f;
	public float stun = 11.0f;
	public int damage = 20;
	public int exGain = 10;
	public bool bSlide = false;
	public float slideDist = 0.0f;
	public Vector3 spawnPosition = new Vector3 (2.5f, 10.0f, 0.0f);	
	//var punchHitPoints = 1;
	public GameObject hitBox;
	public GameObject dangerBox;
	public GameObject hurtBoxs;
	public List<bool> cancelArray = new List<bool>(24);
}