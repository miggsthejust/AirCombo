using UnityEngine;
using System.Collections;

public class CamDetect : MonoBehaviour 
{
	public bool bRightWall = false;
	public bool bLeftWall = false;
	public bool bUpWall = false;
	public bool bDownWall = false;
	public CameraScroll cScroll;
	
	void Start()
	{
		cScroll = this.transform.parent.GetComponent<CameraScroll>();
	}
	
	void OnTriggerStay (Collider fighterCol) 
	{
		if (fighterCol.tag == "dummyBox")
		{
			if (bUpWall)
			{
				cScroll.camBoundsU = true;
			}
			else if (bDownWall)
			{
				cScroll.camBoundsD = true;
			}
			else if (bRightWall)
			{
				cScroll.camBoundsR = true;
			}
			else if (bLeftWall)
			{
				cScroll.camBoundsL = true;
			}
		}
	}
	
	void OnTriggerExit (Collider fighterCol) 
	{
		if (fighterCol.tag == "dummyBox")
		{
			if (bUpWall)
			{
				cScroll.camBoundsU = false;
			}
			else if (bDownWall)
			{
				cScroll.camBoundsD = false;
			}
			else if (bRightWall)
			{
				cScroll.camBoundsR = false;
			}
			else if (bLeftWall)
			{
				cScroll.camBoundsL = false;
			}
		}
	}
	
}
