using UnityEngine;
using System.Collections;

public class throws : MonoBehaviour 
{
	public float startTime = 4.0f;
	public float hitTime = 4.0f;
	public float endTime = 7.0f;
	
	public float timeF = 90.0f;
	
	public float push = 0.5f;
	public float stun = 11.0f;
	public int damage = 20;
	public int exGain = 10;
	public Vector3 spawnPosition = new Vector3 (2.5f, 10.0f, 0.0f);	
	public Vector3 grabPosition = new Vector3 (2.5f, 10.0f, 0.0f);	
	
	public GameObject hitBox;
	public GameObject hurtBoxs;
}