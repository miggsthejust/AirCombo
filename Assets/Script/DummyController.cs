using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyController : MonoBehaviour 
{
	public Main gameMain;
	public CoreStats stats;
	public Movement movement;
	public PlayerInput input;
	public PlayerInput playerInput;
	public Transform fighter;
	public DummyAnimation animations;
	public CameraScroll cam;

	public float enemyPos;
	public GameObject collisionBox;
	public GameObject hitBoxes;
	public GameObject idlehurtBoxes;

	//movement and collision bools
	public bool bFacingRight = false;
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
	public bool bThrown = false;
	public bool bBouncing = false;
	public bool bPaused = false;
	
	public float currentDist = 0.0f;
	public float hitDist = 0.0f;
	public float slideDist = 0.0f;
	public float attackDist = 0.0f;
	public float hitStopTime = 0.015f;
	public float hitStopDelay = 0.16f;
	public float hitStopTimescale = 0.01f;
	public float wallBounceSpeed = 30.0f;
	public bool bWallBouncing = false;

	// wall collision bools
	public bool bOnRightWall = false;
	public bool bOnLeftWall = false;
	public float smooth;
	public bool pushBack = false;
	public bool noPushBack = false;
	public int wallBounceCountL = 1;
	public int wallBounceCountR = 1;
	public int wallBounceCountU = 1;
	public int wallBounceCountD = 1;

	// attack bools

	public AttackHit currentHit;
	public float parentedPOS;
		
	public int comboCount = 1;

	// fx vars
	public GameObject hitSparkFx;
	public GameObject groundHitFx;
	
	// used by got hit
	bool bHitFrame = false;
	// flags this as the frame we began throwing on.
	public bool bThrowingFrame = false;
	// attack storage vars
	Vector3 lastAttackDist;
	float lastAttackStun;
	int lastAttackDamg;
	bool lastAttackWBnc;
	int lastAttackType;
	int lastAttackExAm;
	Vector3 lastAttackSprk;
	float lastAttackGrav;
	bool lastAttackPush;
	bool lastAttackLnch;
	bool lastAttackExAt;
	bool lastAttackPosB;
	bool lastAttackRigh;
	
	// sound vars
	public fighterSounds fSounds;

	public bool DinfHealth =false;

	public float forceY;
	public float forceX;
	public float gravity = 100.0f;
	public AttackClass[] attackList = new AttackClass[6];

	void Awake()
	{
		gameMain = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
		playerInput = GameObject.FindGameObjectWithTag("main").GetComponent<PlayerInput>();
		fSounds = GameObject.FindGameObjectWithTag("fighterSound").GetComponent<fighterSounds>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>();
		stats = GetComponent<CoreStats>();
		movement = GetComponent<Movement>();
		//input = main.GetComponent<PlayerInput>();
		animations = GetComponent<DummyAnimation>();
		fighter = this.transform;
//		attackList = GetComponents<AttackClass> ();
		bGrounded = true;
		hitStopTime = hitStopTime/60;
		smooth = 25.0f;
		//fSounds.PlayIntro();

	}

	
	// controls all functionality for a fighter. Called by main class.
	public void Activate () 
	{
		//Debug.Log("activate");
		// --- order of checks ---
		
		enemyPos = stats.enemyPos.position.x;

		// if game is paused we do nothing
		if (!bPaused)
		{

			// if we are hit this frame then we resolve that hit. If our throw connected we ignore and continue throwing.
			if (bHitFrame)
			{
				ResolveHit(lastAttackDist,lastAttackStun,lastAttackDamg,lastAttackWBnc,lastAttackType,lastAttackExAm,lastAttackSprk,lastAttackGrav,lastAttackPush,lastAttackLnch,lastAttackExAt,lastAttackPosB,lastAttackRigh);	
				return;
			}

			//if (bBouncing)
			//{
				//CGrabBounce();
			//}

			else if (bHit)
			{
				AirKnockback();
			}


				animations.Animate();
				Pushed();
				ApplyForces();

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
				var opp = stats.opponent.GetComponent<FighterController>();
				if (opp.bOnLeftWall && bOnLeftWall)
				{
					
					if(!bGrounded)
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *6 * Time.deltaTime );
						}
						else
						{
							fighter.Translate(Vector3.back * stats.pushAmount *6 * Time.deltaTime );
						}
					}
					else
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *2 * Time.deltaTime );
						}
						else
						{
							fighter.Translate(Vector3.back * stats.pushAmount *2 * Time.deltaTime );
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
							fighter.Translate(Vector3.back * stats.pushAmount *6 * Time.deltaTime );
						}
						else
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *6 * Time.deltaTime );
						}
					}
					else
					{
						if (bFacingRight)
						{
							fighter.Translate(Vector3.back * stats.pushAmount *2 * Time.deltaTime );
						}
						else
						{
							fighter.Translate(Vector3.forward * stats.pushAmount *2 * Time.deltaTime );
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
	

    // once we are airborne apply gravity and velocity
	public void ApplyForces()
	{
		//Debug.Log("forceX "+forceX+" forceY "+forceY);

		if (bPushed)
		{
			Debug.Log("pushed");
			forceX = 0;
		}
		if ((bOnLeftWall) && (forceX > 0))
		{
			forceX = 0;	
			if (bWallBouncing)
			{
				Debug.Log("applyforce wallbounce");
				WallBounce(false);
			}
		}
		else if ((bOnRightWall) && (forceX < 0))
		{
			forceX = 0;	
			if (bWallBouncing)
			{
				Debug.Log("applyforce wallbounce");
				WallBounce(true);
			}
		}

		fighter.Translate(0, forceY * Time.deltaTime,forceX * Time.deltaTime);

		if (!bGrounded)
		{
			forceY -= gravity * Time.deltaTime;
		}
	}
	
    // spawns a default hitbox when idle
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
	
    // called by attackboxes to trigger hit effects and send attack attributes
	public void GotHit(Vector3 distance,float stun, int damage,bool wBounce,int type,int ex,Vector3 sparkPOS,float grav,bool noPush, bool launch,bool exAttack,bool posBased,bool right)
	{
		// store attack vars locally for use at end of frame.
		lastAttackDist = distance;
		lastAttackStun = stun;
		lastAttackDamg = damage;
		lastAttackWBnc = wBounce;
		lastAttackType = type;
		lastAttackExAm = ex;
		lastAttackSprk = sparkPOS;
		lastAttackGrav = grav;
		lastAttackPush = noPush;
		lastAttackLnch = launch;
		lastAttackExAt = exAttack;
		lastAttackPosB = posBased;
		lastAttackRigh = right;
		bHitFrame = true;
	}
	
    // once we have declared we are hit, resolve that hit on next frame
    // this includes playing sounds based on hit strength, adjusting combo count, wallbounces, and launching
	public void ResolveHit(Vector3 distance,float stun, int damage,bool wBounce,int type,int ex,Vector3 sparkPOS,float grav,bool noPush, bool launch,bool exAttack,bool posBased,bool right)
	{
		//if (bParryWin && hype.hypeAmount >= 50)
		//print (exAttack);
//		bCounterHit = false;
		bHitFrame = false;
		

			Debug.Log ("Im hit! "+gameMain.frameCount);
			
			if (stun < 12)
			{
				fSounds.PlayGotHitL();
			}
			else if (stun < 18)
			{
				fSounds.PlayGotHitM();
			}
			else
			{
				fSounds.PlayGotHitH();
			}
			// counterhit code
			
			if (bHit)
			{
				// if we were already in hitstun the combo counter increases.
				comboCount++;
				// scaling for hype chains
				
			}
			else
			{
				comboCount = 1;
			}
			bHit = true;
			
//			TakeDamage(damage);
			// remove throw boxes.
			//var boxs = GameObject.FindGameObjectsWithTag("throwable");
			stats.opponent.GetComponent<FighterController>().currentHit.IncreaseGrav(); // add more grav increase to the used attack
			//gravity = gravity + grav; // increase the dummy gravity by grav amount.
			//Destroy ();
			
			Instantiate(hitSparkFx,sparkPOS,Quaternion.identity);
			
			if (wBounce) 
			{
				bWallBouncing = true;
				Debug.Log("wallbounce = "+bWallBouncing);
			}

			if (bGrounded)
			{
				//knock opponent into air and knockdown on landing
				BeginKnock(distance.y,distance.x);
				//bBouncing = true;
				bGrounded = false;
				//bKnockdown = true;
				//bHit = false;
				//StopAttacks();
				//fighterActivate.StopAttacks();
				//Destroy(hitBoxes);
				//animations.KOAnimate();
				animations.AirHitAnimate();
				StopCoroutine("HitStop");
				StopCoroutine("HitStun");
				stun = 0;
					
			}
			else
			{
				animations.AirHitAnimate();
				BeginKnock(distance.y,distance.x);
				StopCoroutine("HitStop");
				StartCoroutine("HitStop",stun);
				//Destroy(hitBoxes);
			}
	}

    // creates the animation freeze when we are hit.
	IEnumerator HitStop(float stun) 
	{
		//Debug.Log ("hitstop begun " +hitStopTime);
		//bHitSlide = true;
		//yield return new WaitForSeconds(hitStopDelay);
		//Time.timeScale = hitStopTimescale;
		animations.Pause();
		stats.opponent.GetComponent<FighterAnimation>().Pause();

		yield return new WaitForSeconds(hitStopTime);

		animations.UnPause();
		stats.opponent.GetComponent<FighterAnimation>().UnPause();
		//Debug.Log ("hitstop finished " +gameMain.frameCount);
	//	Time.timeScale = 1.0f;
		//Time.timeScale = gameMain.gameSpeed;
		//StopCoroutine("HitStun");
		if (stun != 0)
		{
			//StartCoroutine("HitStun",stun);
		}
		
		//bHitSlide = true;
	}
	/*
	IEnumerator AttackerHitStop(float stun) // disabled waiting for fix
	{
		yield return new WaitForSeconds(0.16f);
		Time.timeScale = 0.01f;
		animations.Pause();
		stats.opponent.GetComponent<FighterAnimation>().Pause();
		yield return new WaitForSeconds(hitStopTime);
		animations.UnPause();
		stats.opponent.GetComponent<FighterAnimation>().UnPause();
		Debug.Log ("attacker hitstop finished " +gameMain.frameCount);
		//Time.timeScale = 1.0f;
		Time.timeScale = gameMain.gameSpeed;
		
	}
	*/

	public void CancelJump()
	{
//		bJumpAttack = false;
		bJumpBackward = false;
		bJumpForward = false;
//		jump.length = 0;
//		jump.height = 0;
	}

    // called by grabbox to declare we have been air grabbed
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
	
    // when we have been air grabbed, apply grab effects
	public void AGrabbed(int damage,bool exThrow)
	{
		// grabbed animation
		//yield return new WaitForSeconds(1.0f);
		Debug.Log ("take damage and begin bounce");
		var groundFx = Instantiate(groundHitFx,this.transform.position,Quaternion.identity) as GameObject;
		//bKnockdown = true;
		Destroy(hitBoxes);

		fighter.parent = null;
		
		//BeginBounce();
//		bCGrabbed = false;
		bBouncing = true;
		bKnockdown = true;
		animations.KOAnimate();
		//yield return new WaitForSeconds(1.0f); // bouncing
		//print ("bounce should be done");
		//bBouncing = false;
		//animations.KnockedDown();
		//StopAttacks();
		//fighterActivate.StopAttacks();
//		StartCoroutine(OnFloor());
		//bThrown = false;
	}
	
    // REMOVE AFTER TESTING
    // temp code to allow multiple combos until proper reset code is created.
	void JumpLanding()
	{
		Debug.Log ("jumpGrounded");
		bGrounded = true;
		bJumpFalling = false;
		print ("landing");
		forceX = 0.0f;
		forceY = 0.0f;
		//print (jump.landing);
		Facing();
		//yield return new WaitForSeconds(0);
		bJumping = false;
		bLanded = false;
		SpawnIdleHitBox();
		var tempX = fighter.position.x;
		fighter.position = new Vector3(tempX,0,0);
	}

	// wall bounce
	public void WallBounce(bool bRightWall)
	{
		// check which side this is and send the dummy back the other direction
		// later we add an amount of times wall bounce is allowed and decrease on each hit
		Debug.Log ("wallbouncing");
		//Instantiate(hitSparkFx,this.transform.position,Quaternion.identity);
		fSounds.PlayGotGroundHit ();
		bWallBouncing = false;
		forceY = 60.0f;

		if (bRightWall) 
		{
			forceX = wallBounceSpeed;
		} 
		else 
		{
			forceX = -wallBounceSpeed;
		}

		stats.opponent.GetComponent<FighterController> ().WallBounceReset ();
	}


	// when fighter is hit out of the air
	public void BeginKnock(float knockHeight,float knockLength)
	{
		Debug.Log ("begining knock");
		//controller.bJumping = true;
		forceY = knockHeight;

		if (enemyPos <= fighter.transform.position.x) 
		{
			forceX = -knockLength;
		} 
		else if (enemyPos > fighter.transform.position.x) 
		{
			forceX = knockLength;
		}
		
		if ((bOnLeftWall) && (forceX > 0))
		{
			forceX = 0;	
			if (bWallBouncing)
			{
				if (wallBounceCountL > 0)
				{
					wallBounceCountL -= 1;
					Debug.Log ("activating wallbounce");
					WallBounce(false);
				}
			}
		}
		else if ((bOnRightWall) && (forceX < 0))
		{
			forceX = 0;	
			if (wallBounceCountR > 0)
			{
				wallBounceCountR -= 1;
				Debug.Log ("activating wallbounce");
				WallBounce(true);
			}
		}

		fighter.Translate(0, forceY * Time.deltaTime ,forceX * Time.deltaTime);
	}

    // not sure why this is still needed, try to remove.
	public void AirKnockback()
	{
		//Debug.Log ("knock "+fighter.position.y);
		if (fighter.position.y <= 0)
		{
			// need to find a way to correct fighter y pos to 0;
			EndKnock();
		}
		else
		{
			//forceY -= gravity * Time.deltaTime;
			//Debug.Log(height);
			if (bPushed)
			{
				//Debug.Log("pushed");
				//forceX = 0;
			}
			
			//if (cam.camBlock)
			//{
		//		length = 0;
		//	}
			
			if (bOnLeftWall||bOnRightWall)
			{
				//forceX = 0;
			}
			//fighter.Translate(0, forceY * Time.deltaTime ,forceX * Time.deltaTime);
			//Vector3 curpos = fighter.position;
			//Vector3 targetpos = new Vector3(0, height * Time.deltaTime * netMod,length * Time.deltaTime * netMod);
			//fighter.position = Vector3.Lerp(curpos, targetpos, Time.time);
			
			if (forceY < 0)
			{
				bJumpFalling = true;
			}
		}
	}
	
	public void EndKnock()
	{
		//Debug.Log ("ending knockback");
		bHit = false;
		bJumpForward = false;
		bJumpBackward = false;
		bJumpFalling = false;
		bGrounded = true;
		bLanded = true;
		forceX = 0;
		forceY = 0;
		var tempX = fighter.position.x;
		fighter.position = new Vector3(tempX,0,0);

		
	}
	/*
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
*/

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

	public void ResetFighter()
	{
	//	fighterActivate.StopAllCoroutines();
		StopAllCoroutines();

		bJumping = false;
		bJumpForward = false;
		bJumpBackward = false;
		bJumpFalling = false;
		bGrounded = true;
		bKnockdown = false;
		bKnockedOut = false;
		bPushed = false;
		bNoPush = false;
		bHit = false;
		bHitSlide = false;
		comboCount = 0;
		animations.Animate();
		enemyPos = 0;
		SpawnIdleHitBox();
		
	}
// this should move to it's own proper gui class eventually.
	void OnGUI()
	{
		if (comboCount > 1)
		{
			if (this.tag == "fighter1")
			{
				GUI.Label(new Rect(Screen.width - 80, Screen.height/3, 200, 20), "COMBO "+comboCount.ToString());
			}
			else
				GUI.Label(new Rect(20, Screen.height/3, 200, 20), "COMBO "+comboCount.ToString());
		}
	}
}
