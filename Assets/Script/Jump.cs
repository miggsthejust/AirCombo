using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour 
{
	public float jumpHeight = 2.0f; // controls y dist on each frame.
	public float gravity = 0.1f; // controls the amount of y decay each frame.
	public float jumpLength = 0.5f; // controls x dist each frame.
	public int jumpMulti = 6; // can be used to increase or decrease jump without altering height and gravity values.
	public float knockHeight = 2.0f;
	public float startup = 0.06f;
	public float landing = 0.06f;
	
	private Transform fighter;
	private FighterController controller;
	private CameraScroll cam;
	
	public float height = 0.0f;
	public float length = 0.0f;
	
	// bounce vars
	public float bounceHeight = 2.0f;
	public float bounceLength = 0.5f;
	
	public int netMod = 4;
	public bool bJumpEnded;
	
	void Awake()
	{
		controller = GetComponent<FighterController>();
		fighter = transform;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>();
		startup = startup/60;
		landing = landing/60;
	}
	
	public void Jumping()
	{
		//Debug.Log ("jump "+fighter.position.y);
		if (fighter.position.y <= 0)
		{
			// need to find a way to correct fighter y pos to 0;
			JumpGround();
		}
		else
		{
			height -= gravity * Time.deltaTime * netMod;
			//Debug.Log(height);
			if ((controller.bPushed)||(cam.camBlock))
			{
				Debug.Log("pushed");
				//length = 0;
				fighter.Translate(0, height * Time.deltaTime * netMod,0);
			}
			else 
			{
				if (controller.bOnLeftWall)
				{
					if ((controller.bFacingRight)&&(controller.bJumpBackward) || (!controller.bFacingRight)&&(controller.bJumpForward))
					{
						length = 0;
					}
				}
				else if (controller.bOnRightWall)
				{
					if ((!controller.bFacingRight)&&(controller.bJumpBackward) || (controller.bFacingRight)&&(controller.bJumpForward))
					{
						length = 0;
					}
						
				}
				fighter.Translate(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
			}
			//Vector3 curpos = fighter.position;
			//Vector3 targetpos = new Vector3(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
			//fighter.position = Vector3.Lerp(curpos, targetpos, Time.time);
			
			if (height < 0)
			{

				if (!controller.bJumpFalling)
				{
					EndJump();
					controller.bJumpFalling = true;
				}
			}
		}
	}
	
	public void BeginJump()
	{
		Debug.Log ("begining jump "+Time.time);
		controller.bJumping = true;
		controller.bJumpFalling = false;
		controller.SpawnIdleHitBox();
		height = jumpHeight;
		
		if (controller.bJumpForward)
		{
			length = jumpLength;
		}
		else if (controller.bJumpBackward)
		{
			length = -jumpLength;
		}
		else
		{
			length = 0;
		}
		
		if (cam.camBlock)
		{
			if (controller.bJumpBackward)
			{
				length = 0;
			}
		}
		
		if (controller.bOnLeftWall)
		{
			if ((controller.bFacingRight)&&(controller.bJumpBackward) || (!controller.bFacingRight)&&(controller.bJumpForward))
			{
				length = 0;
			}
		}
		else if (controller.bOnRightWall)
		{
			if ((!controller.bFacingRight)&&(controller.bJumpBackward) || (controller.bFacingRight)&&(controller.bJumpForward))
			{
				length = 0;
			}
				
		}
		
		fighter.Translate(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
	}
	
	public void EndJump()
	{
		Debug.Log ("ending jump "+Time.time);

		controller.bJumpForward = false;
		controller.bJumpBackward = false;
		controller.bAttacking = false;
		//controller.bJumping = false;

	}

	public void JumpGround()
	{

	}

	// when fighter is hit out of the air
	public void BeginKnock()
	{
		Debug.Log ("begining knock");
		//controller.bJumping = true;
		height = knockHeight;
		//length = -jumpLength;
		
		if (cam.camBlock)
		{
			length = 0;
		}
		
		if (controller.bOnLeftWall||controller.bOnRightWall)
		{
			length = 0;	
		}
		
		fighter.Translate(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
	}
	
	public void AirKnockback()
	{
		Debug.Log ("knock "+fighter.position.y);
		if (fighter.position.y <= 0)
		{
			// need to find a way to correct fighter y pos to 0;
			EndKnock();
		}
		else
		{
			height -= gravity * Time.deltaTime * netMod/2;
			//Debug.Log(height);
			if (controller.bPushed)
			{
				Debug.Log("pushed");
				length = 0;
			}
			
			if (cam.camBlock)
			{
				length = 0;
			}
			
			if (controller.bOnLeftWall||controller.bOnRightWall)
			{
				length = 0;
			}
			fighter.Translate(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
			//Vector3 curpos = fighter.position;
			//Vector3 targetpos = new Vector3(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
			//fighter.position = Vector3.Lerp(curpos, targetpos, Time.time);
			
			if (height < 0)
			{
				controller.bJumpFalling = true;
			}
		}
	}
	
	public void EndKnock()
	{
	//	Debug.Log ("ending knockback");
		controller.bHit = false;
		controller.bJumpForward = false;
		controller.bJumpBackward = false;
		controller.bJumpFalling = false;
		controller.bAttacking = false;
		controller.bGrounded = true;
		controller.InterruptAttacks();
		var tempX = fighter.position.x;
		fighter.position = new Vector3(tempX,0,0);
		
	}


    public void BeginLaunch()
    {
        Debug.Log("begining launch " + Time.time);
        controller.bJumping = true;
        controller.bJumpFalling = false;
        controller.SpawnIdleHitBox();
        height = jumpHeight * 3.0f;

        if (controller.bJumpForward)
        {
            length = jumpLength;
        }
        else if (controller.bJumpBackward)
        {
            length = -jumpLength;
        }
        else
        {
            length = 0;
        }

        if (cam.camBlock)
        {
            if (controller.bJumpBackward)
            {
                length = 0;
            }
        }

        if (controller.bOnLeftWall)
        {
            if ((controller.bFacingRight) && (controller.bJumpBackward) || (!controller.bFacingRight) && (controller.bJumpForward))
            {
                length = 0;
            }
        }
        else if (controller.bOnRightWall)
        {
            if ((!controller.bFacingRight) && (controller.bJumpBackward) || (controller.bFacingRight) && (controller.bJumpForward))
            {
                length = 0;
            }

        }

        fighter.Translate(0, height * Time.deltaTime * netMod, length * Time.deltaTime * netMod);
    }
    // when fighter is hit with an attack that causes a bounce
    public void BeginBounce()
	{
		Debug.Log ("begining bounce");
		//controller.bJumping = true;
		height = bounceHeight;
		length = -bounceLength;
		
		if (cam.camBlock)
		{
			length = 0;
		}
		
		if (controller.bOnLeftWall || controller.bOnRightWall)
		{
			length = 0;
		}
		
		fighter.Translate(0, height * Time.deltaTime,length * Time.deltaTime);
	}
	
	public void CGrabBounce()
	{
		//Debug.Log ("jump "+fighter.position.y);
		if (fighter.position.y <= 0)
		{
			// need to find a way to correct fighter y pos to 0;
			EndBounce();
		}
		else
		{
			height -= gravity * Time.deltaTime;
			//Debug.Log(height);
			
			if (cam.camBlock)
			{	
				length = 0;
			}
			
			if (controller.bOnLeftWall || controller.bOnRightWall)
			{
					length = 0;	
			}
			
			fighter.Translate(0, height * Time.deltaTime,length * Time.deltaTime);
			//Vector3 curpos = fighter.position;
			//Vector3 targetpos = new Vector3(0, height * Time.deltaTime,length * Time.deltaTime);
			//fighter.position = Vector3.Lerp(curpos, targetpos, Time.time);
			
			if (height < 0)
			{
				//controller.bJumpFalling = true;
			}
		}
	}
	
	public void EndBounce()
	{
		Debug.Log ("ending bounce");
		controller.bBouncing = false;
		//controller.bGrounded = true;
		var tempX = fighter.position.x;
		fighter.position = new Vector3(tempX,0,0);
		if (controller.bKnockdown)
		{
			//controller.StartCoroutine(controller.OnFloor());
			Debug.Log("calling to floor");
//			controller.ToFloor();
		}
	}
}
