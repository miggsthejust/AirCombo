using UnityEngine;
using System.Collections;

public class EXProjectileScript : MonoBehaviour 
{
	
	public float projectileSpeed;
	public float dropoff; 
	private float currentDrop;
	private bool bHit;
	public string owner;
	public FighterController controller;
	public dangerDetect danger;
	
	public Vector3 hitDist;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public int hitType = 0;
	public int ex = 0;
	public int chip = 0;
	
	// Update is called once per frame
	void Update () 
	{
		var currentpos = transform.position;
		currentDrop += dropoff;
		if (controller.bFacingRight)
		{
			transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x + projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
		}
		else
		{
			transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x - projectileSpeed, Time.time),Mathf.Lerp(currentpos.y, currentpos.y - currentDrop, Time.time) ,currentpos.z);
		}
		
		if (currentpos.y <= 0)
		{
			danger.Destroying();
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		if (!bHit)
		{
			if ((opponentCol.tag == "hurtbox")||(opponentCol.tag == "hypebox"))	
			{
				var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
				if (opponentOwner != owner)
				{
					Debug.Log(opponentCol + "hit");
//					controller.stats.opponent.GetComponent<FighterController>().GotHit(hitDist,hitStun,hitDam,knockDown,hitType,ex,closestPoint,chip,true,false,true,false,false);
					bHit = true;
					danger.Destroying();

						Destroy (this.gameObject);

				}
			}
			else if (opponentCol.tag == "projectile")
			{
				bHit = true;
				danger.Destroying();
				//Instantiate(secondProj,this.transform.position,this.transform.rotation);

					Destroy (this.gameObject);

			}
		}
	}
}
