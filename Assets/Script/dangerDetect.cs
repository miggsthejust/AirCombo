using UnityEngine;
using System.Collections;

public class dangerDetect : MonoBehaviour 
{
	public bool bHit;
	public string owner;
	public FighterController controller;
	public float hitDist = 0.0f;
	public int hitDam = 0;
	public float hitStun = 0;
	
	void OnTriggerEnter (Collider opponentCol) 
	{
		//Debug.Log("collide");
		
			if (opponentCol.tag == "hurtbox")	
			{
				var opponentOwner = opponentCol.transform.parent.GetComponent<hurtScript>().owner;
				if (opponentOwner != owner)
				{
					Debug.Log(opponentCol + "danger");
					controller.stats.opponent.GetComponent<FighterController>().bDanger = true;
					bHit = true;
				}
			}
		
	}
	public void Destroying()
	{
		controller.stats.opponent.GetComponent<FighterController>().bDanger = false;
		if (this.gameObject != null)
		{
		Destroy(this.gameObject);
		}
	}
	
}
