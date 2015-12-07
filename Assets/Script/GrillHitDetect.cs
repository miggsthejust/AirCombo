using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrillHitDetect : MonoBehaviour 
{
	public bool bHit;
	public string owner;
	public FighterController controller;
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public bool launch = false;
	public bool noPush = false;
	public int hitType = 0;
	public int ex = 0;
	public int chip = 0;
	public bool exTrue;
	public bool posBased;
	public bool right;
	
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
					// send left or right based on position of grill
					if (closestPoint.x > transform.position.x)
					{
						posBased = true;
						right = true;
					}
					else
					{
						posBased = true;
						right = false;
					}
					
					Debug.Log(Time.time + " "+opponentCol + "hit");
					//Debug.DrawLine (closestPoint, this.transform.position, Color.white,5.0f);
					controller.CancelWindow();
//					controller.stats.opponent.GetComponent<FighterController>().GotHit(hitDist,hitStun,hitDam,knockDown,hitType,ex,closestPoint,chip,true,launch,exTrue,posBased,right);
					bHit = true;
				}
			}
		}
	}
}
