using UnityEngine;
using System.Collections;

public class PsScript : MonoBehaviour 
{
	
	public float projectileSpeed;
	public float dropoff; 
	private float currentDrop;
	private bool bHit;
	public string owner;
	public FighterController controller;
	public dangerDetect danger;
	public float height;
	public float length;
	//public float drop = -0.2f;
	public bool exProj;
	
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public int chip = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public int hitType = 0;
	public int ex = 0;
	private bool bFired;
	public float pauseTime;
	public bool bPaused;
	
	public GameObject discdangerBox;
	public GameObject discprojectile;
	public GameObject discprojectileEx;
	public float discpush;
	public int discdamage;
	public float discstun;

	void Awake()
	{
		pauseTime = pauseTime/60;
	}
	// Update is called once per frame
	void Update () 
	{
		var currentpos = transform.position;
		
		if (currentpos.y >= height)
		{
			if (!bFired)
			{
				//FireDisc();
				StartCoroutine(Pausing());
				currentDrop = 0;
				projectileSpeed = 0;
				dropoff = dropoff * -0.2f;
				bFired = true;
			}
		}
		
		if (!bPaused)
		{
			currentDrop += dropoff;
			if (controller.bFacingRight)
			{
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x + projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
			}
			else
			{
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x - projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
			}
		}
		
		
		if (currentpos.y <= 0)
		{
			if (danger != null)
			{
				danger.Destroying();
			}


				Destroy (this.gameObject);

		}
	}
	
	IEnumerator Pausing()
	{
		bPaused = true;
		yield return new WaitForSeconds(pauseTime);
		bPaused = false;
		FireDisc();
		if (exProj)
		{
			yield return new WaitForSeconds(0.2f);
			FireDiscEx();
		}
	}
	
	public void FireDisc()
	{
		//launch disk from ps
		var danger01 = Instantiate(discdangerBox, this.transform.position, this.transform.rotation) as GameObject; 
		danger01.transform.parent = this.transform;
		var dangerOwner = danger01.GetComponent<dangerDetect>();
		dangerOwner.owner = this.tag;
		dangerOwner.controller = controller;
		dangerOwner.hitDist = discpush;
		dangerOwner.hitDam = discdamage;
		dangerOwner.hitStun = discstun;
		GameObject proj01; 

			proj01 = Instantiate (discprojectile, this.transform.position, this.transform.rotation) as GameObject;

		var attackOwner = proj01.GetComponent<ProjectileScript>();
		attackOwner.owner = this.tag;
		attackOwner.controller = controller;
		attackOwner.hitDist = discpush;
		attackOwner.hitDam = discdamage;
		attackOwner.hitStun = discstun;
		danger01.transform.parent = proj01.transform;
		attackOwner.danger = dangerOwner;
		//instantiate disc. needs angled code.
	}
	
	public void FireDiscEx()
	{
		//launch disk from ps
		var danger01 = Instantiate(discdangerBox, this.transform.position, this.transform.rotation) as GameObject; 
		danger01.transform.parent = this.transform;
		var dangerOwner = danger01.GetComponent<dangerDetect>();
		dangerOwner.owner = this.tag;
		dangerOwner.controller = controller;
		dangerOwner.hitDist = discpush;
		dangerOwner.hitDam = discdamage;
		dangerOwner.hitStun = discstun;
		GameObject proj01;

			proj01 = Instantiate (discprojectileEx, this.transform.position, this.transform.rotation) as GameObject;

		var attackOwner = proj01.GetComponent<ProjectileScript>();
		attackOwner.owner = this.tag;
		attackOwner.controller = controller;
		attackOwner.hitDist = discpush;
		attackOwner.hitDam = discdamage;
		attackOwner.hitStun = discstun;
		attackOwner.exTrue = exProj;
		danger01.transform.parent = proj01.transform;
		attackOwner.danger = dangerOwner;
		//instantiate disc. needs angled code.
	}
}
