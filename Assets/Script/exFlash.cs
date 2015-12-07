using UnityEngine;
using System.Collections;

public class exFlash : MonoBehaviour 
{
	public GameObject body;
	private Color defColor;
	private Color defOutline;
	public Texture2D exTexture2D;
	public GameObject[] exparts;
	public Color exColor;
	// Use this for initialization
	void Start () 
	{
		defColor = body.GetComponent<Renderer>().material.color;
		defOutline = body.GetComponent<Renderer>().material.GetColor("_OutlineColor");
	}
	
	public void ExFlashBegin(int time)
	{
		StartCoroutine(EXFlash(time));
	}
	
	IEnumerator EXFlash(int num)
	{
	// in here we will flicker the material color and outline color.
		var i=0;
		
		for (i=0;i<=num;i++)
		{
			foreach (GameObject go in exparts)
			{
				foreach (Material mats in go.GetComponent<Renderer>().materials)
				{
					mats.color = exColor;
				}
			}
			
			body.GetComponent<Renderer>().material.SetColor ("_OutlineColor", exColor);
			yield return new WaitForSeconds(0.05f);
			
			foreach (GameObject go in exparts)
			{
				foreach (Material mats in go.GetComponent<Renderer>().materials)
				{
					mats.color = defColor;
				}
			}
			body.GetComponent<Renderer>().material.SetColor ("_OutlineColor", defOutline);
			yield return new WaitForSeconds(0.05f);
		}
		
		
	}
}
