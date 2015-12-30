using UnityEngine;
using System.Collections;

public class exBlueScript : MonoBehaviour 
{
	
	public float projectileSpeed;
	public bool linearDrop;
	public float dropoff; 
	private float currentDrop;
	private bool bHit;
	public string owner;
	public FighterController controller;
//	public FighterActivate activate;
	public dangerDetect danger;
	
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public int hitType = 0;
	public int ex = 0;
	public int chip = 0;
	public bool exTrue;
	
	// Update is called once per frame
	void Update () 
	{
		var currentpos = transform.position;
		if (!linearDrop)
		{
			currentDrop += dropoff;
		}
		else
		{
			currentDrop = dropoff;
		}
		
		if (controller.bFacingRight)
		{
			transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x + projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
		}
		else
		{
			transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x - projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
		}
		
		if (currentpos.y <= 0|| currentpos.x >= 60 || currentpos.x <= -60)
		{
			if (danger != null)
			{
				danger.Destroying();
			}
			controller.bProjThrown = false;
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		if (!bHit)
		{
			if (opponentCol.tag == "hurtbox")	
			{
				var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
				if (opponentOwner != owner)
				{
					Debug.Log(opponentCol + "hit");
					//controller.CancelWindow();
//					controller.fighterActivate.BlueEXHit();
//					controller.stats.opponent.GetComponent<FighterController>().GotHit(hitDist,hitStun,hitDam,knockDown,hitType,ex,closestPoint,chip,true,false,exTrue,false,false);
					controller.bProjThrown = false;
					bHit = true;
					if (danger != null)
					{
						danger.Destroying();
					}

						Destroy (this.gameObject);

				}
			}
			else if (opponentCol.tag == "projectile")
			{
				bHit = true;
				controller.bProjThrown = false;
				if (danger != null)
				{
					danger.Destroying();
				}
				//Instantiate(secondProj,this.transform.position,this.transform.rotation);

					Destroy (this.gameObject);

			}
		}
	}
}
