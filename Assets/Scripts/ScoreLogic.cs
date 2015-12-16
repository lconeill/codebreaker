using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLogic : MonoBehaviour 
{
	// This script is where we create the reference to our score GUI.
	// the score is displayed on screen at all times and changes when
	// the score changes.
	
	public int the_score = 0;
	public int match_streak_counter = 0;
	
	private int previoius_score = 0;
	
	private Text gui_text;
	
	// Use this for initialization
	void Start () 
	{
		gui_text = GetComponent<Text>();
		the_score = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		gui_text.text = "Score : " + the_score;
		
		if(previoius_score != the_score)
		{
			//Debug.Log("The Score has changed!!!");
		}
		
		previoius_score = the_score;
	}
}
