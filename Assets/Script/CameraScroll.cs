using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour 
{
	public GameObject boundsL;
	public GameObject boundsR;
	
	public bool camBoundsL;
	public bool camBoundsR;
	public bool camBoundsU;
	public bool camBoundsD;
	
	public float scrollSpeed;
	public float lookUpSpeed;
	public bool camBlock;
	public bool active;
	
	// Update is called once per frame
	void Update () 
	{
		if (active)
		{
			if (camBoundsU)
			{
				//print ("left");
				var currentpos = transform.position;
				transform.position = new Vector3(currentpos.x,Mathf.Lerp(currentpos.y, currentpos.y + lookUpSpeed, Time.deltaTime),currentpos.z);
				camBlock = false;
			}

			else if (camBoundsD && transform.position.y > 8.6f)
			{
				//print ("left");
				var currentpos = transform.position;
				transform.position = new Vector3(currentpos.x,Mathf.Lerp(currentpos.y, currentpos.y - lookUpSpeed*6.0f, Time.deltaTime),currentpos.z);
				camBlock = false;
				//transform.position = new Vector3(Mathf.Lerp(currentRot.x, currentRot.x - scrollSpeed, Time.deltaTime),currentRot.y,currentRot.z);
			}

			if ((camBoundsL == true)&&(camBoundsR != true))
			{
				//print ("left");
				var currentpos = transform.position;
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x - scrollSpeed, Time.deltaTime),currentpos.y,currentpos.z);
				camBlock = false;
			}
			else if ((camBoundsR == true)&&(camBoundsL != true))
			{
				//print ("right");
				var currentpos = transform.position;
				transform.position = new Vector3(Mathf.Lerp(currentpos.x, currentpos.x + scrollSpeed, Time.deltaTime), currentpos.y, currentpos.z);
				camBlock = false;
			}
			else if ((camBoundsL == true)&&(camBoundsR == true))
			{
				camBlock = true;
			}
		}
	}
}
