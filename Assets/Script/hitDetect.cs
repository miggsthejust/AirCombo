using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hitDetect : MonoBehaviour 
{
	public bool bHit;
	public string owner;
	public FighterController controller;
	public Vector3 hitDist;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public bool launch = false;
	public bool wallBounce = false;
	public bool noPush = false;
	public int hitType = 0;
	public int ex = 0;
	public float grav = 0;
	public bool exTrue;
	
	//public GameObject hitspark;
	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		if (!bHit)
		{
			if ((opponentCol.tag == "hurtbox")||(opponentCol.tag == "hypebox" && exTrue))	
			{
				var closestPoint = opponentCol.ClosestPointOnBounds(this.transform.position);
				closestPoint.z  = -3.2f;
				var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
				if (opponentOwner != owner)
				{
					Debug.Log(Time.time + " "+opponentCol + "hit");
					//Debug.DrawLine (closestPoint, this.transform.position, Color.white,5.0f);
					controller.CancelWindow();
					controller.stats.opponent.GetComponent<DummyController>().GotHit(hitDist,hitStun,hitDam,wallBounce,hitType,ex,closestPoint,grav,noPush,launch,exTrue,false,false);
					bHit = true;
				}
			}
		}
	}
}
