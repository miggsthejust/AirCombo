using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterController : MonoBehaviour 
{
	public Main gameMain;
	public CoreStats stats;
	public Movement movement;
	public Jump jump;
	public PlayerInput input;
	public PlayerInput playerInput;
	public Transform fighter;
	public FighterAnimation animations;
	public Parry parry;
	private exFlash ExEffects;
	public float enemyPos;
	public GameObject collisionBox;
	public GameObject hitBoxes;
	public GameObject idlehurtBoxes;
	public GameObject crouchHurtBoxes;
	public GameObject jumpHurtBoxes;
	public GameObject walkFHurtBoxes;
	public GameObject walkBHurtBoxes;
	public GameObject exChainHurtBox;


	//movement and collision bools
	public bool bFacingRight = false;
	public bool bCrouching = false;
	public bool bMoving = false;
	public bool bMoveForward = false;
	public bool bMoveBackward = false;
	public bool bJumping = false;
	public bool bJumpStart = false;
	public bool bJumpForward = false;
	public bool bJumpBackward = false;
	public bool bJumpFalling = false;
	public bool bLanded = false;
	public bool bGrounded = false;
	public bool bKnockdown = false;
	public bool bKnockedOut = false;
	public bool bPushed = false;
	public bool bNoPush = false;
	public bool bHit = false;
	public bool bHitSlide = false;
	public bool bAttackSlide = false;
	public bool bDanger = false;		
	public bool bBlocking = false;
	public bool bBlockStun = false;
	public bool bThrown = false;
	public bool bBouncing = false;
	public bool bParryWin = false;
	public bool bParryAttempt = false;
	public bool bParrying = false;
	public bool bPaused = false;



	public float currentDist = 0.0f;
	public float hitDist = 0.0f;
	public float slideDist = 0.0f;
	public float attackDist = 0.0f;
	public float hitStopTime = 0.015f;
	public float parryStopTime = 0.015f;
	public float counterStopTime = 0.015f;

	public float hitStopDelay = 0.16f;
	public float hitStopTimescale = 0.01f;

	// wall collision bools
	public bool bOnRightWall = false;
	public bool bOnLeftWall = false;
	public float smooth;
	public bool pushBack = false;
	public bool noPushBack = false;
    // attack bools
    public bool bLaunch = false;
    public float launchMulti = 2.0f;

    public bool bAttacking = false;
	//public bool bAttackActive = false; // used for counter hit
	public bool bThrowing = false;
	public bool bAirThrowL = false;
	public bool bAirThrowH = false;
	public bool bAirThrowEX = false;
	public bool bCThrowL = false;
	public bool	bCThrowH = false;
	public bool	bCThrowEX = false;
	public bool bCThrowingL = false;
	public bool bCThrowingH = false;
	public bool bCThrowingEX = false;
	public bool bAThrowingL = false;
	public bool bAThrowingH = false;
	public bool bAThrowingEX = false;
	public int tempThrowDam;
	public bool bProjThrown = false;
	public bool bUpperEX = false;
	public AttackHit currentHit;
	public float parentedPOS;
		
	public GameObject projectile;
	public int netMod = 0;
	public int comboCount = 1;
	
	// cancelling vars
	public bool bCanceling = false;
	public float cancelTime = 4.0f;	
	//private bool[] cancelArray = new bool[24]; 
	public List<bool> cancelArray = new List<bool>(24);
	
	// fx vars
	public GameObject hitSparkFx;
	public GameObject blockSparkFx;
	public GameObject parrySparkFx;
	public GameObject groundHitFx;
	public GameObject attackTrailFootL;
	public GameObject attackTrailFootR;
	public GameObject attackTrailHandL;
	public GameObject attackTrailHandR;
	
	
	// flags this as the frame we began throwing on.
	public bool bThrowingFrame = false;
	// attack storage vars
	Vector3 lastAttackDist;
	float lastAttackStun;
	int lastAttackDamg;
	bool lastAttackKDwn;
	int lastAttackType;
	int lastAttackExAm;
	Vector3 lastAttackSprk;
	int lastAttackChip;
	bool lastAttackPush;
	bool lastAttackLnch;
	bool lastAttackExAt;
	bool lastAttackPosB;
	bool lastAttackRigh;
	
	// sound vars
	fighterSounds fSounds;

	public bool DinfHealth =false;
	public int jumpCount;

	float forceY;
	float forceX;
	float gravity = 100.0f;
	public AttackClass[] attackList = new AttackClass[6];

	void Awake()
	{
		gameMain = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
		playerInput = GameObject.FindGameObjectWithTag("main").GetComponent<PlayerInput>();
		fSounds = GameObject.FindGameObjectWithTag("fighterSound").GetComponent<fighterSounds>();
		stats = GetComponent<CoreStats>();
		movement = GetComponent<Movement>();
		//input = main.GetComponent<PlayerInput>();
		jump = GetComponent<Jump>();
		animations = GetComponent<FighterAnimation>();
		fighter = this.transform;
//		fighterActivate = GetComponent<FighterActivate>();
//		attackList = GetComponents<AttackClass> ();
		bGrounded = true;
		cancelTime = cancelTime/60;
		hitStopTime = hitStopTime/60;
		parryStopTime = parryStopTime/6000;
		counterStopTime = counterStopTime/6000;
		smooth = 25.0f;
		//fSounds.PlayIntro();

	}

	
	// controls all functionality for a fighter. Called by main class.
	public void Activate () 
	{
		//Debug.Log("activate");
		// --- order of checks ---
		// defeat
		bMoving = false;
		bMoveForward = false;
		//bMoveBackward = false;
		//bCrouching = false;
		//bNoPush = false;
		//bDanger = false;
		//bBlocking = false;
		
		enemyPos = stats.enemyPos.position.x;
		//enemyPos = enemycol.transform.x;
		
		bool upButton;
		bool leftButton;
		bool rightButton;
		bool attack01ButtonDown;
		bool attack01Button;
		bool attack01ButtonUp;
		bool attack02ButtonDown;
		bool attack02Button;
		bool attack02ButtonUp;
		bool attack03ButtonDown;
		bool attack03Button;
		bool attack03ButtonUp;
		bool attack04ButtonDown;
		bool attack04Button;
		bool attack04ButtonUp;
	
		var inputState = playerInput.localBufState[gameMain.frameCount];
		

		upButton = inputState.upButton;
		leftButton = inputState.leftButton;
		rightButton = inputState.rightButton;
		attack01ButtonDown = inputState.attack01ButtonDown;
		attack01Button = inputState.attack01Button;
		attack02ButtonDown = inputState.attack02ButtonDown;
		attack03ButtonDown = inputState.attack03ButtonDown;
		attack03Button = inputState.attack03Button;
		attack03ButtonUp = inputState.attack03ButtonUp;
		attack04ButtonDown = inputState.attack04ButtonDown;

			//print (Network.time+" hp button down "+attack02ButtonDown);

		// if game is paused we do nothing
		if (!bPaused)
		{

			// if we are hit this frame then we resolve that hit. If our throw connected we ignore and continue throwing
			if (bHit)
			{
				jump.AirKnockback();
			}
			else 
			{
				if (this.tag == "fighter1")
				{
				if (!bKnockedOut)
				{
				if (bJumping)
				{
					if (!bGrounded)
					{
						if (transform.position.y > 4.0f)
						{
							if ((attack01ButtonDown)&&(!bAttacking))
							{
								StopAttacks();
							//	fighterActivate.StopAttacks();
								StartCoroutine("AttackRoutine",attackList[0]);
							}
							else if ((attack02ButtonDown)&&(!bAttacking))
							{
								StopAttacks();
							//	fighterActivate.StopAttacks();
								StartCoroutine("AttackRoutine",attackList[1]);
							}
							else if ((attack03ButtonDown)&&(!bAttacking))
							{
								StopAttacks();
							//	fighterActivate.StopAttacks();
								StartCoroutine("AttackRoutine",attackList[2]);
							}
							else if ((attack04ButtonDown)&&(!bAttacking))
							{
								if (attackList.Length > 3)
								{
									StopAttacks();
								//	fighterActivate.StopAttacks();
									StartCoroutine("AttackRoutine",attackList[3]);
								}
							}
						}

						
							if (bJumpFalling)
							{
								
								if (upButton)
								{
									if (bFacingRight)
									{
										if (rightButton)
										{
											bJumpForward = true;	
										}
										else if (leftButton)
										{
											bJumpBackward = true;
										}
									}
									else
									{
										if (rightButton)
										{
											bJumpBackward = true;	
										}
										else if (leftButton)
										{
											bJumpForward = true;
										}
									}
									if (!bJumpStart)
									{
										if (jumpCount > 0)
										{
											jumpCount -= 1;
											StartCoroutine("JumpStartup");
											bJumpStart = true;
										}
									}
								}
								else
								{
									if (rightButton)
									{		
										if (bFacingRight)
										{
											bMoving = true;
											bMoveForward = true;
											movement.MoveForward();
										}
										else
										{
											bMoving = true;
											bMoveBackward = true;
											movement.MoveBackward();
										}
									}
									else if (leftButton)
									{
										if (bFacingRight)
										{
											bMoving = true;
											bMoveBackward = true;
											movement.MoveBackward();
										}
										else
										{
											bMoving = true;
											bMoveForward = true;
											movement.MoveForward();	
										}
									}
								}
							
							}
							Jumping();
							//Facing(); // need to fix jump direction change when swapping facing first.
						
					}
					else if (!bLanded)
					{
						bLanded = true;
						//StartCoroutine("JumpLanding");
					}
				}
				else
				{
					bGrounded = true;
					
					//fighterActivate.Activate();
						
					if (!bAttacking)
					{
                        // launch if attacking on ground
                        if ((attack01ButtonDown) && (!bAttacking))
                        {
                            StopAttacks();
                            StartCoroutine("AttackRoutine", attackList[6]);
                        }
                        else if (upButton)
						{
							if (bFacingRight)
							{
								if (rightButton)
								{
									bJumpForward = true;	
								}
								else if (leftButton)
								{
									bJumpBackward = true;
								}
							}
							else
							{
								if (rightButton)
								{
									bJumpBackward = true;	
								}
								else if (leftButton)
								{
									bJumpForward = true;
								}
							}
							if (!bJumpStart)
							{
								StartCoroutine("JumpStartup");
								bJumpStart = true;
							}
						}
						// walk
						else if (rightButton)
						{		
							if (bFacingRight)
							{
								bMoving = true;
								bMoveForward = true;
								movement.MoveForward();
							}
							else
							{
								bMoving = true;
								bMoveBackward = true;
								movement.MoveBackward();
								SpawnIdleHitBox();
							}
						}
						else if (leftButton)
						{
							if (bFacingRight)
							{
								bMoving = true;
								bMoveBackward = true;
								movement.MoveBackward();
								SpawnIdleHitBox();
							}
							else
							{
								bMoving = true;
								bMoveForward = true;
								movement.MoveForward();
							}
						}
						else
						{
							//Debug.Log("resetting parry");
							bParryAttempt = false;
						}
							Facing();
							}
						}
					}
				
					animations.Animate();
				}
				Pushed();
				ApplyForces();
			}
		}
	}
		

	public void Facing()
	{
		// facing
		if (bFacingRight)
		{
			if (enemyPos <= fighter.transform.position.x)
			{
				Debug.Log("switch direction");
				bFacingRight = false;
				fighter.Rotate(0,180,0);
			}
		}
		else
		{
			if (enemyPos-2.0f > fighter.transform.position.x)
			{
				Debug.Log("switch direction");
				bFacingRight = true;
				fighter.Rotate(0,-180,0);
			}
		}
	}
	
	void Pushed()
	{
		if (bPushed)
		{
			if ((bOnLeftWall) || (bOnRightWall))
			{
				var opp = stats.opponent.GetComponent<DummyController>();
				if (opp.bOnLeftWall && bOnLeftWall)
				{
					
					if(!bGrounded)
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *6 * Time.deltaTime * netMod);
						}
						else
						{
							fighter.Translate(Vector3.back * stats.pushAmount *6 * Time.deltaTime * netMod);
						}
					}
					else
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *2 * Time.deltaTime * netMod);
						}
						else
						{
							fighter.Translate(Vector3.back * stats.pushAmount *2 * Time.deltaTime * netMod);
						}
					}
				}
				else if (opp.bOnRightWall && bOnRightWall)
				{
					print ("inside pushed");
					if(!bGrounded)
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.back * stats.pushAmount *6 * Time.deltaTime * netMod);
						}
						else
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *6 * Time.deltaTime * netMod);
						}
					}
					else
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.back * stats.pushAmount *2 * Time.deltaTime * netMod);
						}
						else
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *2 * Time.deltaTime * netMod);
						}
					}
				} 
			}
			else if ((!bOnLeftWall) && (!bOnRightWall))
			{
				//if (!bJumping) // build wall functionality next.
				//{
					if (bFacingRight)
					{
						//Debug.Log(controller.enemyPos+" "+controller.fighter.position.x);
						if (enemyPos < fighter.position.x)
						{
							fighter.Translate(Vector3.forward * movement.fSpeed * Time.deltaTime);
						}
						else if (enemyPos > fighter.position.x)
						{
							fighter.Translate(Vector3.back * movement.fSpeed * Time.deltaTime);
						}
					}
					else
					{
						//Debug.Log(controller.enemyPos+" "+controller.fighter.position.x);
						if (enemyPos < fighter.position.x)
						{
							fighter.Translate(Vector3.back * movement.fSpeed * Time.deltaTime);
						}
						else if (enemyPos > fighter.position.x)
						{
							fighter.Translate(Vector3.forward * movement.fSpeed * Time.deltaTime);
						}
					}
				//}
			}
		}
	}
	

	IEnumerator AttackRoutine(AttackClass attackClass)
	{
		// convert the frame counts to tenths of a second
		var startUp = attackClass.startTime/60; 
		var coolDown = attackClass.endTime/60; 

		// Attack Startup ------------------------------
		//Debug.Log(AttackClass.name+" start "+Time.time); 
		bAttacking = true; // we are now in attack state
		//bJumpAttack = true; // remove this variable. redundant
		animations.AttackAnimate(attackClass.name); // begin the proper animation
		yield return new WaitForSeconds(startUp); // pause actions until startUp completes
		// Attack Startup End --------------------------

		// Attack Active -------------------------------
		// this section will become a for loop to itterate through multiple hits
		for (int i = 0; i < attackClass.hitList.Count; i++) 
		{
			Debug.Log(attackClass.name+" hit #"+i+" activate "+Time.time);
			// need a function for moving the fighter
			if (attackClass.hitList [i].moves) 
			{
				AttackMoveBegin(attackClass.hitList[i].moveSpeed.y,attackClass.hitList[i].moveSpeed.x);
			}
			currentHit = attackClass.hitList[i];
			var pos = transform.TransformPoint (attackClass.hitList [i].spawnPosition); // this is where we will spawn hitboxes
			var attack01 = Instantiate (attackClass.hitList [i].hitBox, pos, this.transform.rotation) as GameObject; // spawn hitbox
			attack01.transform.parent = this.transform; // parent hitbox to fighter
			var attackOwner = attack01.GetComponent<hitDetect> (); // send attack info to hitbox
			attackOwner.controller = this;
			attackOwner.owner = this.tag; // define fighter as owner for collision check
			attackOwner.launch = attackClass.hitList [i].launch;
            if (attackClass.hitList[i].launch)
            {
                bLaunch = true;
            }
            attackOwner.wallBounce = attackClass.hitList [i].wallBounce;			
			attackOwner.hitDist = attackClass.hitList [i].push;  
			attackOwner.hitType = attackClass.hitList [i].exGain;
			attackOwner.grav = attackClass.hitList [i].gravInc;
			attackOwner.hitStun = attackClass.hitList [i].stun;
			var active = attackClass.hitList [i].hitTime / 60;
			yield return new WaitForSeconds (active); // wait until active time is done
			Destroy (attack01);
			if (attackClass.hitList [i].moves) 
			{
				AttackMoveEnd (); 
			}
		}
		// at this point we loop back if there are multiple hits
		// Attack Active End ------------------------------

		// Attack Cooldown --------------------------------
		//Debug.Log(AttackClass.name+" cooldown "+Time.time);
		yield return new WaitForSeconds(coolDown);// wait until cooldown time is done
        if (bLaunch)
        {
            StartCoroutine("LaunchStartup");
            bJumpStart = true;
            bLaunch = false;
        }
        //attackTrailHandL.renderer.enabled = false;
		//attackTrailHandR.renderer.enabled = false;
		//attackTrailFootL.renderer.enabled = false;
		//attackTrailFootR.renderer.enabled = false;
		bAttacking = false; // last thing we do is leave attack state
		Facing ();
		// Attack Cooldown End ---------------------------
	}

	public void AttackMoveBegin(float attackY,float attackX)
	{
		Debug.Log ("begining movement "+Time.time);
		forceY = attackY;
		forceX = attackX;
		//AttackMove ();
	}

	public void AttackMoveEnd()
	{
		forceX = 0.0f;
		forceY = 0.0f;
		bJumpFalling = true;
		jump.EndJump ();
	}

    // END OF ATTACKS

    public void ApplyForces()
	{
		//Debug.Log("forceX "+forceX+" forceY "+forceY);
		if (bPushed)
		{
			Debug.Log("pushed");
			//forceX = 0;
		}

		if ((bOnLeftWall) && (forceX < 0))
		{
			forceX = 0;	
		}
		else if ((bOnRightWall) && (forceX > 0))
		{
			forceX = 0;	
		}

		fighter.Translate(0, forceY * Time.deltaTime,forceX * Time.deltaTime);
		if (!bGrounded)
		{
			forceY -= gravity * Time.deltaTime;
		}
	}
	
	public void InterruptAttacks()
	{
		//print ("interrupting");
		//fighterActivate.StopAttacks();
		StopAttacks();
		bAttacking = false;
		var boxs = GameObject.FindGameObjectsWithTag("attackbox");
		if (boxs != null)
		{
			foreach (GameObject go in boxs)
			{
				Debug.Log(go);
				Destroy(go);
			}
		}
	}
	
	public void SpawnIdleHitBox()
	{
		if (hitBoxes != null)
		{
			Destroy(hitBoxes);
		}

		hitBoxes = Instantiate (idlehurtBoxes, transform.position, this.transform.rotation) as GameObject;

		hitBoxes.transform.parent = this.transform;
		//hitBoxes.transform.rotation = this.transform.rotation;
		var hitOwner = hitBoxes.GetComponent<hurtScript>();
		hitOwner.owner = this.tag;
	}

    // when we have successfuly landed a wallbounce attack on the dummy, we reset the gravity increase on each attack in our attack list.
	public void WallBounceReset ()
	{
		foreach (AttackClass classes in attackList)
		{
			classes.ResetHitsPush();
		}
	}

	public void CancelJump()
	{
//		bJumpAttack = false;
		bJumpBackward = false;
		bJumpForward = false;
		jump.length = 0;
		jump.height = 0;
	}

    // when successfully landing an air grab
	public void GotAGrab(float grabPOS, int damage)
	{
		Destroy (hitBoxes);
		//Debug.Log(grabPOS);
		fighter.position = new Vector3(stats.opponent.transform.position.x + grabPOS, stats.opponent.transform.position.y, 0);
		
		parentedPOS = grabPOS;
//		bCGrabbed = true;
		
		//fighter.parent = stats.opponent.transform;
		//bThrown = true;
		//StopAttacks();
		//fighterActivate.StopAttacks();
		//StartCoroutine(AGrabbed(damage));
	}
	
	IEnumerator JumpStartup()
	{
		yield return new WaitForSeconds(jump.startup);
		Debug.Log ("begining jump "+Time.time);
		bGrounded = false;
		bJumpStart = false;
		//jump.BeginJump();
		animations.JumpAnimate();
		bJumping = true;
		bJumpFalling = false;
		SpawnIdleHitBox();
		forceY = jump.jumpHeight;
		
		if (bJumpForward)
		{
			forceX = jump.jumpLength;
		}
		else if (bJumpBackward)
		{
			forceX = -jump.jumpLength;
		}
		else
		{
			forceX = 0;
		}
		fighter.Translate(0, forceY * Time.deltaTime,forceX * Time.deltaTime);
	}

    IEnumerator LaunchStartup()
    {
        yield return new WaitForSeconds(jump.startup);
        Debug.Log("begining Launch " + Time.time);
        bGrounded = false;
        bJumpStart = false;
        //jump.BeginJump();
        animations.JumpAnimate();
        bJumping = true;
        bJumpFalling = false;
        SpawnIdleHitBox();
        forceY = jump.jumpHeight * launchMulti;

        if (bJumpForward)
        {
            forceX = jump.jumpLength;
        }
        else if (bJumpBackward)
        {
            forceX = -jump.jumpLength;
        }
        else
        {
            forceX = 0;
        }
        fighter.Translate(0, forceY * Time.deltaTime, forceX * Time.deltaTime);
    }
    public void Jumping()
	{
		//Debug.Log ("jump "+fighter.position.y);
		if (fighter.position.y <= 0)
		{
			// need to find a way to correct fighter y pos to 0;
			JumpLanding();
		}
		else
		{
			if (forceY < 0)
			{
				if (!bJumpFalling)
				{
					jump.EndJump();
					bJumpFalling = true;
				}
			}
		}
	}

	void JumpLanding()
	{
		Debug.Log ("jumpGrounded");
		bGrounded = true;
		bJumpFalling = false;
		InterruptAttacks();
		print ("landing");
		forceX = 0.0f;
		forceY = 0.0f;
		//print (jump.landing);
		Facing();
		//yield return new WaitForSeconds(0);
		bJumping = false;
		bLanded = false;
		jump.bJumpEnded = false;
		SpawnIdleHitBox();
		var tempX = fighter.position.x;
		fighter.position = new Vector3(tempX,0,0);
	}

    // opens the cancel window to allow the player to cancel the current attack into another
	public void CancelWindow()
	{
		//StopAllCoroutines();
		//fighterActivate.StopAllCoroutines();
		StopCoroutine("Cancelling");
		StartCoroutine("Cancelling");
	}
	
    // opens and closes the ability to cancel current attack.
	IEnumerator Cancelling()
	{
		bCanceling = true;
		yield return new WaitForSeconds(cancelTime);
		bCanceling = false;
	}
	
	public void PositionSwap()
	{
		var oppControl = stats.opponent.GetComponent<FighterController>();
		var tempPos = this.transform.position;
		this.transform.position = oppControl.transform.position;
		oppControl.transform.position = tempPos;
		
		if (bFacingRight)
		{
			bFacingRight = false;
			//oppControl.bFacingRight = true;
			fighter.Rotate(0,180,0);
			
			//oppControl.bFacingRight = true;
		}
		else
		{
			bFacingRight = true;
			//oppControl.bFacingRight = false;
			fighter.Rotate(0,-180,0);
		}
	}
	
	public void StopAttacks()
	{
		// remove attackboxes
		foreach (Transform child in transform) 
		{            
			if (child.tag == "attackbox")
			{
				Destroy (child.gameObject);
				//stats.opponent.gameObject.GetComponent<FighterController>().bDanger = false;
			}
		}
		//Destroy (hitBoxes);
		//SpawnIdleHitBox();
		bAttacking = false;
	}

	public void KillProjectile()
	{
		Destroy (projectile);
//		fighterActivate.StopCoroutine("HspWeeThrow");
		animations.GetComponent<Animation>().Stop();
		Destroy(hitBoxes);
		SpawnIdleHitBox();
		bAttacking = false;
		bProjThrown = false;
	}
	public void ResetFighter()
	{
	//	fighterActivate.StopAllCoroutines();
		StopAllCoroutines();
		bCrouching = false;
		bMoving = false;
		bMoveForward = false;
		bMoveBackward = false;
		bJumping = false;
		bJumpForward = false;
		bJumpBackward = false;
		bJumpFalling = false;
	//	bJumpAttack = false;
		bGrounded = true;
		bKnockdown = false;
		bKnockedOut = false;
		bPushed = false;
		bNoPush = false;
		bHit = false;
		bHitSlide = false;
		bDanger = false;		
		bBlocking = false;
		bBlockStun = false;
		bParrying = false;
		bParryAttempt = false;
		bParryWin = false;
		bAttacking = false;
//		bCounterHit =false;
		comboCount = 0;
		animations.Animate();
		enemyPos = 0;
		
		SpawnIdleHitBox();
		
	}
}
