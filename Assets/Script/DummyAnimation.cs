using UnityEngine;
using System.Collections;

public class DummyAnimation : MonoBehaviour 
{
	public DummyController controller;

	public float jumpAnimationSpeedModifier = 2.0f;
	// Adjusts the speed at which the hang time animation is played back
	public float jumpLandAnimationSpeedModifier = 3.0f;
	public float standIdleSpeedModifier = 1.2f;
		
	public float crossFadeSpeedModifier = 0.06f;

	//animations
	private AnimationState standHit;
	private AnimationState crouchHit;
	private AnimationState gotThrown;
	private AnimationState jump;
	private AnimationState jumpFalling;
	private AnimationState jumpLand;
	private AnimationState standIdle;
	private AnimationState knockdown;
	private AnimationState down;
	private AnimationState smackThrown;
	
	private bool bPaused;
	public string currentAnim;
	private float pauseTime;
	
	public string[] anims;
	public int animNum;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<DummyController>();
		//animation.Stop();
	
		// By default loop all animations
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
	
		// Jump animation are in a higher layer:
		// Thus when a jump animation is playing it will automatically override all other animations until it is faded out.
		// This simplifies the animation script because we can just keep playing the walk / run / idle cycle without having to spcial case jumping animations.

		standIdle = GetComponent<Animation>()["stand"];
		//standIdle.layer = 1;
		standIdle.speed *= standIdleSpeedModifier;
		
		knockdown = GetComponent<Animation>()["knockdown"];
	
	  	knockdown.speed *= jumpAnimationSpeedModifier;
	  	knockdown.wrapMode = WrapMode.Once;
	
		down = GetComponent<Animation>()["down"];
	 // 	jumpPunch.layer = 4;
	  	//down.speed *= jumpPunchSpeedModifier;	
	  	down.wrapMode = WrapMode.ClampForever;
	
	}
	
	public void Animate () 
	{
		if (!bPaused)
		{
			
			if (!controller.bKnockedOut)
			{
				if (!controller.bHit)
				{
					if (controller.bJumping)
					{
						GetComponent<Animation>().CrossFade ("jumping", crossFadeSpeedModifier);
						currentAnim = "jumping";
						animNum = 12;
					}
					else if (controller.bGrounded) 
					{
						if (controller.bKnockdown)
						{
							GetComponent<Animation>().Play ("down");
							currentAnim = "down";
							animNum = 10;
						}
				
						// Go back to idle when not moving
						else
						{
							GetComponent<Animation>().CrossFade ("stand", 0, PlayMode.StopSameLayer);
							currentAnim = "stand";
							animNum = 7;
						}
					}
				}
			}
		}
	}
	

	public void JumpAnimate()
	{
		//Debug.Log("jumping");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("jump");
		currentAnim = "jump";
		animNum = 11;
	}
	public void StandHitAnimate()
	{
		//Debug.Log("hit");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("standHitLow");
		currentAnim = "standHitLow";
		animNum = 3;
	}
	public void KnockedDown()
	{
		//Debug.Log("knockdown");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("knockdown");
		GetComponent<Animation>().PlayQueued("down");
		//animation.Play("down");
		currentAnim = "down";
		animNum = 14;
	}
	public void GetUp()
	{
		//Debug.Log("getUp");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("getUp");
		currentAnim = "getUp";
		animNum = 7;
	}
	public void GotThrown()
	{
	//	Debug.Log("thrown");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("gotThrown");
		currentAnim = "gotThrown";
		animNum = 30;
	}
	public void AirHitAnimate()
	{
		//Debug.Log("knockdown");
		GetComponent<Animation>().Stop();
		//animation.Play ("down");
		GetComponent<Animation>().Play ("knockdown");
		currentAnim = "knockdown";
		animNum = 5;
	}
	public void KOAnimate()
	{
		//Debug.Log("knockdown");
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play ("knockdown");
		currentAnim = "down";
		GetComponent<Animation>().PlayQueued("down");
		//animation.Play("down");
		animNum = 5;
	}

	public void Pause()
	{
		bPaused = true;
		controller.bPaused = true;
		Debug.Log ("anim paused "+GetComponent<Animation>()[currentAnim].time);
		pauseTime = GetComponent<Animation>()[currentAnim].time;
		GetComponent<Animation>().Stop();
	}
	public void UnPause()
	{
		GetComponent<Animation>().Play(currentAnim);
		GetComponent<Animation>()[currentAnim].time = pauseTime;
		Debug.Log ("anim unpaused " +GetComponent<Animation>()[currentAnim].time);
		bPaused = false;
		controller.bPaused = false;
	}
}
