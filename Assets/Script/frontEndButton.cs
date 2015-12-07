using UnityEngine;
using System.Collections;

public class frontEndButton : MonoBehaviour 
{
	public FrontEnd fEnd;
	public int butNum;
	public Color butColor;
	
	void OnMouseOver()
	{
		 GetComponent<Renderer>().material.SetColor("_TintColor", Color.white);
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.SetColor("_TintColor", Color.gray);
	}
	void OnMouseUp()
	{
		print ("activate");
		fEnd.Activate(butNum);
	}
}
