using UnityEngine;
using System.Collections;

public class wallDetect : MonoBehaviour 
{
	public bool bRightWall = false;
	public FighterCollision fCol;
	public DummyCollision dCol;

	void OnTriggerStay (Collider fighterCol) 
	{
		if (fighterCol.tag == "collisionBox")
		{
			fCol = fighterCol.gameObject.GetComponent<FighterCollision>();
			//var cont = controller.controller.GetComponent<FighterController>();
			fCol.OnWall(bRightWall);
		}
		else if (fighterCol.tag == "dummyBox")
		{
			dCol = fighterCol.gameObject.GetComponent<DummyCollision>();
			//var cont = controller.controller.GetComponent<FighterController>();
			dCol.OnWall(bRightWall);
		}
	}
	
	void OnTriggerExit (Collider fighterCol) 
	{
		if (fighterCol.tag == "collisionBox")
		{
			fCol = fighterCol.gameObject.GetComponent<FighterCollision>();
			//var cont = controller.GetComponent<FighterController>();
			fCol.OffWall(bRightWall);
		}
		else if (fighterCol.tag == "dummyBox")
		{
			dCol = fighterCol.gameObject.GetComponent<DummyCollision>();
			//var cont = controller.controller.GetComponent<FighterController>();
			dCol.OffWall(bRightWall);
		}
	}
}
