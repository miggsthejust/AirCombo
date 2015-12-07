using UnityEngine;
using System.Collections;

public class SpawnFighters : MonoBehaviour 
{
	public NetworkManager netManage;
	
	public void Spawning (GameObject choiceFab,Transform spawnPos,bool bFighter1)
	{
		
		var fighter = Instantiate(choiceFab,spawnPos.position,Quaternion.identity) as GameObject;
		
		
		if (bFighter1 == true)
		{
			fighter.tag = "fighter1";
			fighter.GetComponent<FighterInit>().Spawn(spawnPos);
			fighter.GetComponent<FighterController>().input = this.gameObject.GetComponent<PlayerInput>();
		}
		else
		{
			fighter.tag = "fighter2";
			fighter.GetComponent<DummyInit>().Spawn(spawnPos);
			fighter.GetComponent<DummyController>().input = this.gameObject.GetComponent<PlayerInput>();
		}
		

	}
	
	public void NetSpawning (GameObject choiceFab,Transform spawnPos,bool bFighter1)
	{
		
		var fighter = Network.Instantiate(choiceFab,spawnPos.position,Quaternion.identity, 0) as GameObject;
		
		if (bFighter1 == true)
		{
			fighter.tag = "fighter1";
			print ("tag fight1");
		}
		else
		{
			fighter.tag = "fighter2";
			print ("tag fight2");
		}
		
		fighter.GetComponent<FighterInit>().Spawn(spawnPos);
		fighter.GetComponent<FighterController>().input = this.gameObject.GetComponent<PlayerInput>();
		netManage.SendFighterTag(fighter.GetComponent<NetworkView>().viewID, fighter.tag);
	}
}