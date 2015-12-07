using UnityEngine;
using System.Collections;

public class FighterAnimation : MonoBehaviour 
{
	// Adjusts the speed at which the walk animation is played back
	public float walkForwardSpeedModifier = 2.5f;
	// Adjusts the speed at which the run animation is played back
	public float walkBackwardSpeedModifier = 1.5f;
	// Adjusts the speed at which the jump animation is played back
	public float jumpAnimationSpeedModifier = 2.0f;
	// Adjusts the speed at which the hang time animation is played back
	public float jumpLandAnimationSpeedModifier = 3.0f;
	public float standIdleSpeedModifier = 1.2f;
	public float crouchSpeedModifier = 3.0f;
		
	public float crossFadeSpeedModifier = 0.06f;
	
	//attack mods
	public float standPunchSpeedModifier = 3.0f;
	public float standHPunchSpeedModifier = 1.0f;
	public float CrouchHpunchSpeedModifier = 1.0f;
	public float crouchPunchSpeedModifier = 1.0f;
	public float standKickSpeedModifier = 3.0f;
	public float standHKickSpeedModifier = 1.0f;
	public float CrouchHkickSpeedModifier = 1.0f;
	public float crouchKickSpeedModifier = 1.0f;
	public float jumpPunchSpeedModifier = 1.0f;
	public float jumpHPunchSpeedModifier = 1.0f;
	public float jumpKickSpeedModifier = 1.0f;
	public float jumpHKickSpeedModifier = 1.0f;
	public float stompLSpeedModifier = 1.0f;
	public float standParrySpeedModifier = 1.0f;
	public float throwSpeedModifier = 1.0f;
	public float smackThrownSpeedModifier = 1.0f;
	public float special1SpeedModifier = 1.0f;
	public float special2SpeedModifier = 1.0f;
	public float special3SpeedModifier = 1.0f;
	public float special4SpeedModifier = 1.0f;
	public float special5SpeedModifier = 1.0f;
	public float special6SpeedModifier = 1.0f;
	
	// other mods
	public float standBlockSpeedModifier = 1.0f;
	public float crouchBlockSpeedModifier = 1.0f;

	public FighterController controller;
	public Animator animController;

	/*
	// for specialmoves
	public string special1Anim;
	public string special2Anim;
	public string special3Anim;
	public string special4Anim;
	public string special5Anim;
	public string special6Anim;
	
	//animations
	private AnimationState standHit;
	private AnimationState crouchHit;
	private AnimationState gotThrown;
	private AnimationState jump;
	private AnimationState jumpFalling;
	private AnimationState jumpLand;
	private AnimationState crouch;
	private AnimationState walkForward;
	private AnimationState walkBackward;
	private AnimationState standPunch;
	private AnimationState standHPunch;
	private AnimationState standKick;
	private AnimationState standHkick;
	private AnimationState crouchPunch;
	private AnimationState crouchHPunch;
	private AnimationState crouchKick;
	private AnimationState crouchHKick;
	private AnimationState jumpPunch;
	private AnimationState jumpHPunch;
	private AnimationState jumpKick;
	private AnimationState jumpHKick;
	private AnimationState standBlock;
	private AnimationState crouchBlock;
	private AnimationState standIdle;
	private AnimationState crouchIdle;
	private AnimationState knockdown;
	private AnimationState throwing;
	private AnimationState down;
	private AnimationState stompL;
	private AnimationState standParry;
	private AnimationState smackThrown;
	private AnimationState special1;
	private AnimationState special2;
	private AnimationState special3;
	private AnimationState special4;
	private AnimationState special5;
	private AnimationState special6;
	*/
	private bool bPaused;
	public string currentAnim;
	private float pauseTime;
	
	public string[] anims;
	public int animNum;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<FighterController>();
		animController = GetComponent<Animator>();
		//animation.Stop();
	
		// By default loop all animations
		//animation.wrapMode = WrapMode.Loop;
	
		// Jump animation are in a higher layer:
		// Thus when a jump animation is playing it will automatically override all other animations until it is faded out.
		// This simplifies the animation script because we can just keep playing the walk / run / idle cycle without having to spcial case jumping animations.
		/*		
	 	standHit = animation["hit"];
	 	standHit.layer = 3;
	 	standHit.speed = 1;
	 	standHit.wrapMode = WrapMode.ClampForever;
	 
		crouchHit = animation["crouchhit"];
		crouchHit.layer = 3;
		crouchHit.speed = 1;
		crouchHit.wrapMode = WrapMode.ClampForever;
		
	 	jump = animation["idle"];
	  	jump.layer = 2;
	  	jump.speed *= jumpAnimationSpeedModifier;
	  	jump.wrapMode = WrapMode.Once;
	 
		crouch = animation["crouch"];
		crouch.layer = 1;
		crouch.wrapMode = WrapMode.Loop;
		crouch.speed *= crouchSpeedModifier;
	/*	
	 	var jumpFall = animation["idle"];
	  	jumpFall.layer = jumpingLayer;
	  	jumpFall.wrapMode = WrapMode.ClampForever;
	  
	  	var jumpLand = animation["idle"];
	  	jumpLand.layer = jumpingLayer;
	  	jumpLand.speed *= jumpLandAnimationSpeedModifier;
	  	jumpLand.wrapMode = WrapMode.Once;

		standIdle = animation["stand"];
		//standIdle.layer = 1;
		standIdle.speed *= standIdleSpeedModifier;
		
	  	walkForward = animation["walkForward"];
	  	walkForward.speed = walkForwardSpeedModifier;
	  	//walkForward.layer = 1;
		
		walkBackward = animation["walkBackward"];
	  	walkBackward.speed = walkBackwardSpeedModifier;
	  //	walkBackward.layer = 1;
	  	
		crouch = animation["crouch"];
	//	crouch.layer = 1;
		crouch.wrapMode = WrapMode.Loop;
		crouch.speed *= crouchSpeedModifier;
		
		jump = animation["jump"];
	  //	jump.layer = 2;
	  	jump.speed *= jumpAnimationSpeedModifier;
	  	jump.wrapMode = WrapMode.Once;
		
		knockdown = animation["knockdown"];
	
	  	knockdown.speed *= jumpAnimationSpeedModifier;
	  	knockdown.wrapMode = WrapMode.Once;
	
	
		
	  	standPunch = animation["standLPunch"];
	 // 	standPunch.layer = 4;
	  	standPunch.speed = standPunchSpeedModifier;	
	  	standPunch.wrapMode = WrapMode.Once;
		
		standHPunch = animation["standHPunch"];
	 // 	standPunch.layer = 4;
	  	standHPunch.speed = standHPunchSpeedModifier;	
	  	standHPunch.wrapMode = WrapMode.Once;
		
		standKick = animation["standLKick"];
	 // 	standPunch.layer = 4;
	  	standKick.speed = standKickSpeedModifier;	
	  	standKick.wrapMode = WrapMode.Once;
		
		standHkick = animation["standHKick"];
	 // 	standPunch.layer = 4;
	  	standHkick.speed = standHKickSpeedModifier;	
	  	standHkick.wrapMode = WrapMode.Once;
		
		standBlock = animation["standBlock"];
		//standIdle.layer = 1;
		standBlock.speed *= standBlockSpeedModifier;
		standBlock.wrapMode = WrapMode.ClampForever;
		
		
		crouchPunch = animation["crouchLPunch"];
	  //	crouchPunch.layer = 4;
	  	crouchPunch.speed *= crouchPunchSpeedModifier;	
	  	crouchPunch.wrapMode = WrapMode.Once;
		
		crouchKick = animation["crouchLKick"];
	  //	crouchPunch.layer = 4;
	  	crouchKick.speed *= crouchKickSpeedModifier;	
	  	crouchKick.wrapMode = WrapMode.Once;
		
		crouchHKick = animation["crouchHKick"];
	  //	crouchPunch.layer = 4;
	  	crouchHKick.speed *= CrouchHkickSpeedModifier;	
	  	crouchHKick.wrapMode = WrapMode.Once;
		
		crouchHPunch = animation["crouchHPunch"];
	  //	crouchPunch.layer = 4;
	  	crouchHPunch.speed *= CrouchHpunchSpeedModifier;	
	  	crouchHPunch.wrapMode = WrapMode.Once;
		
		jumpPunch = animation["jumpLPunch"];
	 // 	jumpPunch.layer = 4;
	  	jumpPunch.speed *= jumpPunchSpeedModifier;	
	  	jumpPunch.wrapMode = WrapMode.ClampForever;
		
		jumpHPunch = animation["jumpHPunch"];
	 // 	jumpPunch.layer = 4;
	  	jumpHPunch.speed *= jumpHPunchSpeedModifier;	
	  	jumpHPunch.wrapMode = WrapMode.Once;
		
		jumpKick = animation["jumpLKick"];
	 // 	jumpPunch.layer = 4;
	  	jumpKick.speed *= jumpKickSpeedModifier;	
	  	jumpKick.wrapMode = WrapMode.ClampForever;
		
		jumpHKick = animation["jumpHKick"];
	 // 	jumpPunch.layer = 4;
	  	jumpHKick.speed *= jumpHKickSpeedModifier;	
	  	jumpHKick.wrapMode = WrapMode.Once;
		
		down = animation["down"];
	 // 	jumpPunch.layer = 4;
	  	//down.speed *= jumpPunchSpeedModifier;	
	  	down.wrapMode = WrapMode.ClampForever;
		
		crouchBlock = animation["crouchBlock"];
		//standIdle.layer = 1;
		crouchBlock.speed *= crouchBlockSpeedModifier;
		crouchBlock.wrapMode = WrapMode.ClampForever;
		
		standParry = animation["standParry"];
		//standIdle.layer = 1;
		standParry.speed *= standParrySpeedModifier;
		standParry.wrapMode = WrapMode.Once;
		
		throwing = animation["throwFinish"];
		throwing.speed *= throwSpeedModifier;
		throwing.wrapMode = WrapMode.Once;
		
		smackThrown = animation["smackGotThrown"];
		smackThrown.speed *= smackThrownSpeedModifier;
		smackThrown.wrapMode = WrapMode.Once;
	//	stompL = animation["eXStomp"];
		//standIdle.layer = 1;
	//	stompL.speed *= stompLSpeedModifier;
	//	stompL.wrapMode = WrapMode.Once;
		
		if (special6Anim != "")
		{
		special6 = animation[special6Anim];
		special6.speed *= special6SpeedModifier;
		special6.wrapMode = WrapMode.Once;
		}
		if (special5Anim != "")
		{
		special5 = animation[special5Anim];
		special5.speed *= special5SpeedModifier;
		special5.wrapMode = WrapMode.Once;
		}
		special4 = animation[special4Anim];
		special4.speed *= special4SpeedModifier;
		special4.wrapMode = WrapMode.Once;
		
		special3 = animation[special3Anim];
		special3.speed *= special3SpeedModifier;
		special3.wrapMode = WrapMode.Once;
		
		special2 = animation[special2Anim];
		special2.speed *= special2SpeedModifier;
		special2.wrapMode = WrapMode.Once;
		
		special1 = animation[special1Anim];
		special1.speed *= special1SpeedModifier;
		special1.wrapMode = WrapMode.Once;
		
		/*
		animation["SPJumpInUp"].speed *= special3SpeedModifier;
		animation["SPJumpInFront"].speed *= special3SpeedModifier;
		animation["SPJumpInDown"].speed *= special3SpeedModifier;
		animation["SPJumpInUp"].wrapMode = WrapMode.Once;
		animation["SPJumpInFront"].wrapMode = WrapMode.Once;
		animation["SPJumpInDown"].wrapMode = WrapMode.Once;
		*/
	}
	
	public void Animate () 
	{
		if (!bPaused)
		{
			if (!controller.bAttacking)
			{
				if (controller.bJumping)
				{
					animController.Play("jumping");
					currentAnim = "jumping";
					animNum = 12;
				}
				else if (controller.bGrounded) 
				{
					if (controller.bKnockdown)
					{
						animController.Play ("down");
						currentAnim = "down";
						animNum = 10;
					}
					// Are we moving the character?
					else if (controller.bMoving)
					{
						if (controller.bMoveForward)
						{
							animController.Play ("walkForward");
							currentAnim = "walkForward";
							animNum = 9;
						}
						else if (controller.bMoveBackward)
						{
							animController.Play ("walkBackward");
							currentAnim = "walkBackward";
							animNum = 8;
						}
					}
					// Go back to idle when not moving
					else
					{
						animController.Play ("stand");
						currentAnim = "stand";
						animNum = 7;
					}
				}
			}
		}
	}

	public void JumpAnimate()
	{
		//Debug.Log("jumping");
		//animation.Stop();
		animController.Play ("jumping");
		currentAnim = "jumping";
		animNum = 11;
	}

	public void AttackAnimate(string animName)
	{
		//Debug.Log("punching");
		//animController.Stop();
		animController.Play (animName);
		currentAnim = animName;
	}
	public void Pause()
	{
		bPaused = true;
		controller.bPaused = true;
		//Debug.Log ("anim paused "+animation[currentAnim].time);
		pauseTime = animController.speed;
		animController.speed = 0.0f;
	}
	public void UnPause()
	{
		animController.speed = pauseTime;
		//animation[currentAnim].time = pauseTime;
		//Debug.Log ("anim unpaused " +animation[currentAnim].time);
		bPaused = false;
		controller.bPaused = false;
	}
}
