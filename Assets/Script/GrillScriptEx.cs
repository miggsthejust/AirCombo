using UnityEngine;
using System.Collections;

public class GrillScriptEx: MonoBehaviour 
{
	
	private bool bHit;
	public string owner;
	public FighterController controller;
	public dangerDetect danger;
	public GameObject dangerBox;
	public GameObject hitBox;
	public float length;
	//public float drop = -0.2f;
	public bool exProj;
	public GameObject expPart;
	
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
	public float expTime;
	public float hitTime;
	public bool exTrue;
	public float projectileSpeed;
	public float slowDown;

	void Awake()
	{
		pauseTime = pauseTime/60;
		expTime = expTime/60;
		StartCoroutine(Pausing ());
	}
	// Update is called once per frame
	void Update () 
	{
		if (bPaused)
		{
			// flashing code
			
		}
		//sliding code
		var currentpos = transform.position;
		projectileSpeed -= slowDown;
		slowDown = slowDown * 2;
		
		if (projectileSpeed > 0)
		{
			if (controller.bFacingRight)
			{
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x + projectileSpeed, Time.time),currentpos.y ,currentpos.z);
			}
			else
			{
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x - projectileSpeed, Time.time),currentpos.y ,currentpos.z);
			}
		}
	}
	
	IEnumerator Pausing()
	{
		bPaused = true;
		yield return new WaitForSeconds(pauseTime);
		bPaused = false;
		StartCoroutine(Explode());
	}
	
	IEnumerator Explode()
	{
		
		//instance explosion fx
		var exp01 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		var danger01 = Instantiate(dangerBox, this.transform.position, this.transform.rotation) as GameObject; 
		danger01.transform.parent = this.transform;
		var dangerOwner = danger01.GetComponent<dangerDetect>();
		dangerOwner.owner = this.tag;
		dangerOwner.controller = controller;
		
		var attack01 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack01.transform.parent = this.transform;
		var attackOwner = attack01.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack01);
		
		yield return new WaitForSeconds(expTime);
		
		
		//instance explosion fx
		var exp02 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		
		var attack02 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack02.transform.parent = this.transform;
		attackOwner = attack02.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack02);
		
		yield return new WaitForSeconds(expTime);
		
		var exp03 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		
		var attack03 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack03.transform.parent = this.transform;
		attackOwner = attack03.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack03);
		
		yield return new WaitForSeconds(expTime);
		
		var exp04 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		
		var attack04 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack04.transform.parent = this.transform;
		attackOwner = attack04.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack04);
		
		yield return new WaitForSeconds(expTime);
		
		var exp05 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		
		var attack05 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack05.transform.parent = this.transform;
		attackOwner = attack05.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack05);
		
		yield return new WaitForSeconds(expTime);
		
		var exp06 = Instantiate(expPart, this.transform.position, Quaternion.identity) as GameObject; 
		
		var attack06 = Instantiate(hitBox, this.transform.position, this.transform.rotation) as GameObject;
		attack06.transform.parent = this.transform;
		attackOwner = attack06.GetComponent<GrillHitDetect>();
		attackOwner.owner = owner;
		attackOwner.hitDist = hitDist;
		attackOwner.hitDam = hitDam;
		attackOwner.hitStun = hitStun;
		attackOwner.chip = chip;
		attackOwner.knockDown = knockDown;
		attackOwner.exTrue = exTrue;
		//attackOwner.hitType = spAirThrow[0].type;
		attackOwner.controller = controller;
		
		yield return new WaitForSeconds(hitTime);
		
		Destroy(attack06);
		
		yield return new WaitForSeconds(expTime);
		
		controller.bProjThrown = false;
		dangerOwner.Destroying();
	
		Destroy (this.gameObject);

	}
	
}

