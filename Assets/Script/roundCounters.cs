using UnityEngine;
using System.Collections;

public class roundCounters : MonoBehaviour 
{

	public GameObject roundCounter;
	public Vector3[] counterPOS;
	
	public void GiveRoundCounter(int winNum,int gameCount,bool p1)
	{
		if (p1)
		{
			if (gameCount == 0)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[0],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[0];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[1],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[1];
				}
			}
			else if (gameCount == 1)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[2],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[2];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[3],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[3];
				}
			}
			else if (gameCount == 2)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[4],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[4];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[5],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[5];
				}
			}
		}
		
		else if (!p1)
		{
			if (gameCount == 0)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[6],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[6];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[7],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[7];
				}
			}
			else if (gameCount == 1)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[8],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[8];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[9],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[9];
				}
			}
			else if (gameCount == 2)
			{
				if (winNum == 0)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[10],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[10];
				}
				if (winNum == 1)
				{
					var currentCount = Instantiate(roundCounter,counterPOS[11],Quaternion.Euler (new Vector3(-90.0f,0,0))) as GameObject;
					currentCount.transform.parent = this.transform;
					currentCount.transform.localPosition = counterPOS[11];
				}
			}
		}
	}
	
	public void DeleteCounters()
	{
		foreach (Transform child in transform) 
		{            
			if (child.tag == "roundCounter")
			{
				Destroy (child.gameObject);
			}
		}
	}
}
