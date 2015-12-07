using UnityEngine;
using System.Collections;

public class MovelistButton : MonoBehaviour 
{
	public MoveListScript move;
	public int butNum;
	public GameObject otherBtn1;
	public GameObject otherBtn2;
	
	void Start()
	{
		if (butNum == 0)
		{
			GetComponent<Renderer>().material.SetColor("_TintColor", Color.white);
		}
	}
	
	void OnMouseUp()
	{
		print ("activate");
		move.ButtonPressed(butNum);
		GetComponent<Renderer>().material.SetColor("_TintColor", Color.white);
		otherBtn1.GetComponent<Renderer>().material.SetColor("_TintColor", Color.grey);
		otherBtn2.GetComponent<Renderer>().material.SetColor("_TintColor", Color.grey);
	}
}
