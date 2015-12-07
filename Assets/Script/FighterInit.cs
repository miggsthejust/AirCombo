using UnityEngine;
using System.Collections;

public class FighterInit : MonoBehaviour 
{
	
	public bool bInit = false;
	public bool bSpawn = false;
	
	public void Spawn(Transform spawnPoint)
	{
		
		var controller = GetComponent<FighterController>();
		// here we reset our position and health.

		controller.bFacingRight = true;
		
		
		
		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;
		
		bSpawn = true;
	}
	
	public void Initialize(Main main)
	{
		var stats = GetComponent<CoreStats>();
		var controller = GetComponent<FighterController>();

		// set our opponent

		stats.opponent = GameObject.FindGameObjectWithTag("fighter2");
		main.p1Control = GetComponent<FighterController>();
		if (main.DinfHealth)
		{
			main.p1Control.DinfHealth = true;
			
		}


		stats.enemyPos = stats.opponent.transform;
		stats.enemyBack = stats.opponent.GetComponent<CoreStats>().backPos;
		controller.SpawnIdleHitBox();
		//control.enemyPos = stats.opponent.transform.position.x;
		bInit = true;
	}
}

