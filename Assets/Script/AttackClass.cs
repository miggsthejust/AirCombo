using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AttackClass 
{
	public string name;
	public float startTime = 4.0f;
	public float endTime = 7.0f;

	public float reduceGravAmt = 0.1f;
	public int reduceGravInc = 60;
	public int reduceGravCount = 0;

	public List<AttackHit> hitList = new List<AttackHit>(1);

	void Awake()
	{
		reduceGravCount = 0;
	}

	public void ActivateHits()
	{
		foreach (AttackHit hits in hitList)
		{
			hits.Activate();
		}
	}

	void Update()
	{
		if (reduceGravCount >= reduceGravInc) 
		{
			reduceGravCount = 0;
			foreach (AttackHit hits in hitList)
			{
				hits.ReduceTemp(reduceGravAmt);
			}
		} 
		else 
		{
			reduceGravCount++;
		}
	}

	public void ResetHitsPush()
	{
		foreach (AttackHit hits in hitList)
		{
			hits.ResetPush();
		}
	}


}