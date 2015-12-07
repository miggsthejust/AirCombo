using UnityEngine;
using System.Collections;

public class MoveListScript : MonoBehaviour 
{
	public Vector3 screenPos;
	public Vector3 offPos;
	public GameObject moveSetk;
	public GameObject moveSetm;
	public GameObject moveSetr;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void ButtonPressed(int btnNum)
	{
		if (btnNum == 0)
		{
			// k599 moveset
			moveSetk.transform.localPosition = screenPos;
			moveSetm.transform.localPosition = offPos;
			moveSetr.transform.localPosition = offPos;
		}
		else if (btnNum == 1)
		{
			// Match moveset
			moveSetm.transform.localPosition = screenPos;
			moveSetk.transform.localPosition = offPos;
			moveSetr.transform.localPosition = offPos;
		}
		else if (btnNum == 2)
		{
			// reg moveset
			moveSetr.transform.localPosition = screenPos;
			moveSetm.transform.localPosition = offPos;
			moveSetk.transform.localPosition = offPos;
		}
	}
}
