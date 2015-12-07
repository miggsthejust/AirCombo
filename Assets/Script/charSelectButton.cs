using UnityEngine;
using System.Collections;

public class charSelectButton : MonoBehaviour 
{
	public CharSelect cSel;
	public int butNum;
	
	void OnMouseOver()
	{
		 //renderer.material.color = Color.red;
	}
	
	void OnMouseExit()
	{
		//renderer.material.color = Color.white;
	}
	void OnMouseUp()
	{
		print ("activate");
		cSel.Activate(butNum);
	}
}
