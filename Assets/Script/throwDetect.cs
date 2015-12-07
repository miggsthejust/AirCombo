using UnityEngine;
using System.Collections;

public class throwDetect : MonoBehaviour 
{
	public bool bHit;
	public string owner;
	public FighterController controller;
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public float hitStun = 0;
	public bool knockDown = false;
	
	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		if (!bHit)
		{
			if (opponentCol.tag == "throwable")	
			{
				var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
				if (opponentOwner != owner)
				{
					Debug.Log(opponentCol + "thrown");
//					controller.ThrowingFSucceed();
					bHit = true;
				}
			}
		}
		
	}
	
}
