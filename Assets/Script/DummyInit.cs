using UnityEngine;
using System.Collections;

public class DummyInit : MonoBehaviour 
{
	
	public bool bInit = false;
	public bool bSpawn = false;
	
	public void Spawn(Transform spawnPoint)
	{
		
		var controller = GetComponent<DummyController>();
		// here we reset our position and health.

		controller.bFacingRight = false;

		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;
		
		bSpawn = true;
	}
	
	public void Initialize(Main main)
	{
		var stats = GetComponent<CoreStats>();
		var controller = GetComponent<DummyController>();

		// set our opponent

			stats.opponent = GameObject.FindGameObjectWithTag("fighter1");
			main.p2Control = GetComponent<DummyController>();
			//controller.P2Tint();
			main.p2Control.DinfHealth = true;

		stats.enemyPos = stats.opponent.transform;
		stats.enemyBack = stats.opponent.GetComponent<CoreStats>().backPos;
		controller.SpawnIdleHitBox();
		//control.enemyPos = stats.opponent.transform.position.x;
		bInit = true;
	}
}

