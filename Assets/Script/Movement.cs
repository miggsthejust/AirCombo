using UnityEngine;
using System.Collections;

// this component handles all functionality and variables related to fighter movement.
public class Movement : MonoBehaviour 
{
	
	public int fSpeed = 10;
	public int bSpeed = 8;
	public int netMod = 0;
	private FighterController controller;
	private CameraScroll cam;
		
	void Awake()
	{
		controller = GetComponent<FighterController>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>();
	}
	
	public void MoveForward()
	{	
		if ((controller.bOnLeftWall) && (!controller.bFacingRight) || (controller.bOnRightWall) && (controller.bFacingRight))
		{
			//Debug.Log("on wall");
		}
		else
		{
			//transform.Translate(Vector3.forward * fSpeed * Time.deltaTime * netMod);
			//Vector3 currentpos = transform.position;
			//var newx = currentpos.x + fSpeed;
			//Vector3 newpos = new Vector3(newx,0,0);
			//Debug.Log (newpos.x);
			//Debug.Log (currentpos.x);
			transform.Translate(Vector3.forward * fSpeed * Time.deltaTime * netMod);
			//var curSmooth = speedSmoothing * Time.deltaTime;
			//moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
			//var movement = moveDirection * moveSpeed + Vector3 (0, verticalSpeed, 0);
			//movement *= Time.deltaTime;
			//transform.position = Vector3.Lerp(currentpos, newpos, 0.1f);
		}
	}
	
	public void MoveBackward()
	{
		if ((controller.bOnLeftWall) && (controller.bFacingRight) || (controller.bOnRightWall) && (!controller.bFacingRight)||(cam.camBlock))
		{
			//Debug.Log("on wall");
		}
		else
		{
			transform.Translate(Vector3.back * bSpeed * Time.deltaTime * netMod);
		}
	}
}
