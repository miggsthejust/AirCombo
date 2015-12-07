using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour 
{
	public PauseMenu menu;
	public int butNum;
	
	void OnMouseOver()
	{
		 GetComponent<Renderer>().material.SetColor("_TintColor", Color.white);
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.SetColor("_TintColor", Color.grey);
	}
	void OnMouseUp()
	{
		print ("activate");
		menu.Activate(butNum);
	}
}
