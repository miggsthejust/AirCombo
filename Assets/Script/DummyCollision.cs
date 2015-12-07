using UnityEngine;
using System.Collections;

public class DummyCollision : MonoBehaviour 
{
	public int pushAmount = 2;
	private DummyController controller;
	private CoreStats stats;
	//public Collider pushCollider;
	//public Collider boundingBox; 
	
	void Start () 
	{
		controller = transform.parent.GetComponent<DummyController>();
		stats = transform.parent.GetComponent<CoreStats>();
	}
	
	void OnTriggerStay (Collider opponentCol) 
	{
		//Debug.Log(opponentCol.transform.parent);
		//Debug.Log(stats.opponent);
		if (opponentCol.tag == "collisionBox")
		{
			// if we arent unpushable
			if (opponentCol.transform.parent == stats.opponent.transform)
			{
				controller.bPushed = true;
			}
		}
	}
	
	void OnTriggerExit(Collider opponentCol) 
	{
		if (opponentCol.tag == "collisionBox")
		{
			if (opponentCol.transform.parent == stats.opponent.transform)
			{
				controller.bPushed = false;
			}
		}
	}
	
	public void OnWall(bool rWall)
	{
		if (rWall)
		{
			controller.bOnRightWall = true;
		}
		else
		{
			controller.bOnLeftWall = true;
		}
	}
	public void OffWall(bool rWall)
	{
		if (rWall)
		{
			controller.bOnRightWall = false;
		}
		else
		{
			controller.bOnLeftWall = false;
		}
	}
}

