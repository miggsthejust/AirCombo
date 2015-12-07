using UnityEngine;
using System.Collections;

public class aGrabDetect : MonoBehaviour 
{
	public bool bHit;
	public string owner;
	public FighterController controller;
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	public bool exTrue;

	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		if (!bHit)
		{
			if ((opponentCol.tag == "hurtbox")||(opponentCol.tag == "hypebox" && exTrue))	
			{
				if (!controller.stats.opponent.GetComponent<FighterController>().bHit)
				{
					var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
					if (opponentOwner != owner)
					{
						Debug.Log(opponentCol + "thrown");
						controller.InterruptAttacks();
//						controller.AGrab();
						bHit = true;
					}
				}
			}
		}
	}
}

