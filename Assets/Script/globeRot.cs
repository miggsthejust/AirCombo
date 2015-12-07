using UnityEngine;
using System.Collections;

public class globeRot : MonoBehaviour {

	public int speed;
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate(Vector3.up, Time.deltaTime * speed, Space.World);
	}
}
