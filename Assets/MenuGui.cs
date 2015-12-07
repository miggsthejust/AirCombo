using UnityEngine;
using System.Collections;


public class MenuGui : MonoBehaviour 
{
	public bool attackMenuOn = false;
	private int attackSelect = 0;
	private int attackChoice01Int = 0;
	private int attackChoice02Int = 1;
	private int attackChoice03Int = 2;
	private int attackChoice04Int = 3;
	private string[] attackNumStrings = {"A1", "A2", "A3","A4"};
	private string[] attackChoice01Strings = new string[8];

	public FighterController fighter;
	public GameObject attacks;
	public AttackClass[] attackList; 

	void Start()
	{
		attackList = attacks.GetComponent<AttackList> ().attackList;
		attackChoice01Strings = new string[attackList.Length];

		for (int i = 0; i < attackList.Length; i++) 
		{
			attackChoice01Strings[i] = attackList[i].name;
		}
	}

	void OnGUI () 
	{
		if (attackMenuOn) 
		{
			GUI.Window (0, new Rect (0, 0, 336, 170), WindowFunction, "");
		} 
		else 
		{
			if (GUI.Button (new Rect (0, 10, 60, 20), "Moves"))
			{
				attackMenuOn = true;
			}
		}
	}

	void WindowFunction (int windowID) 
	{
		attackSelect = GUI.Toolbar (new Rect (10, 8, 180, 16), attackSelect,attackNumStrings);

		if (attackSelect == 0)
		{
			attackChoice01Int = GUI.SelectionGrid  (new Rect (10, 25, 320, 80), attackChoice01Int, attackChoice01Strings, 4);
		}
		else if (attackSelect == 1)
		{
			attackChoice02Int = GUI.SelectionGrid  (new Rect (10, 25, 320, 80), attackChoice02Int, attackChoice01Strings, 4);
		}
		else if (attackSelect == 2)
		{
			attackChoice03Int = GUI.SelectionGrid  (new Rect (10, 25, 320, 80), attackChoice03Int, attackChoice01Strings, 4);
		}
		else if (attackSelect == 3)
		{
			attackChoice04Int = GUI.SelectionGrid  (new Rect (10, 25, 320, 80), attackChoice04Int, attackChoice01Strings, 4);
		}

		if (GUI.Button (new Rect (306, 4, 24, 20), "X")) 
		{
			attackMenuOn = false;	
		}

		if (GUI.Button (new Rect (250, 146, 60, 20), "SET")) 
		{
			// function to set attacks to fighter
			fighter.attackList[0] = attackList[attackChoice01Int]; // send attack data from master attack list to fighter list.
			fighter.attackList[1] = attackList[attackChoice02Int]; // send attack data from master attack list to fighter list.
			fighter.attackList[2] = attackList[attackChoice03Int]; // send attack data from master attack list to fighter list.
			fighter.attackList[3] = attackList[attackChoice04Int]; // send attack data from master attack list to fighter list.
			fighter.attackList[0].ActivateHits();
			fighter.attackList[1].ActivateHits();
			fighter.attackList[2].ActivateHits();
			fighter.attackList[3].ActivateHits();
		}
	}
}