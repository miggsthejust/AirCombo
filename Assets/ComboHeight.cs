using UnityEngine;
using System.Collections;

public class ComboHeight : MonoBehaviour 
{
	public float bestHeight = 0; // the best height reached so far.
	public Transform dummy;
	public bool comboActive = false;

	// Update is called once per frame
	void Update () 
	{
		if (comboActive) 
		{
			if (dummy.position.y > bestHeight) 
			{
				bestHeight = dummy.position.y;
			}
		}
	}

	void OnGUI()
	{
		if (comboActive)
		{
			GUI.Label(new Rect(20, Screen.height/4, 200, 20), "Best Height "+bestHeight.ToString());
		}
	}
}
