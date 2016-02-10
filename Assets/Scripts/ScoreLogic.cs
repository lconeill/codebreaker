using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLogic : MonoBehaviour 
{
	// This script is where we create the reference to our score GUI.
	// the score is displayed on screen at all times and changes when
	// the score changes.
	
	public static int the_score = 0;
	public int match_streak_counter = 0;
	
	private int previoius_score = 0;
	
	private Text gui_text;
    private bool tutorial_exit = true; // Used to exit the check to display tutorial, so it's not constantly querying Tutorial script

    private bool first_exit = true;
    private bool second_exit = true;
    private bool third_exit = true;
    private bool fourth_exit = true;
    private bool fifth_exit = true;

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

        if (the_score >= 4000 && tutorial_exit)
        {
            Tutorial.tutorialSelector("double");
            tutorial_exit = false;
        }

        unlockAchievement(the_score);
	}


    // Unlocks the achievement once the score has been reached
    private void unlockAchievement(int score)
    {
        if (score >= 4000 && first_exit)
        {
            Achievements.unlockFirstAchievement();
            first_exit = false;
        }

        else if (score >= 8000 && second_exit)
        {
            Achievements.bronzeAchievement();
            second_exit = false;
        }

        else if (score >= 15000 && third_exit)
        {
            Achievements.silverAchievement();
            third_exit = false;
        }

        else if (score >= 25000 && fourth_exit)
        {
            Achievements.goldAchievement();
            fourth_exit = false;
        }

        else if (score >= 50000 && fifth_exit)
        {
            Achievements.finalAchievement();
            fifth_exit = false;
        }
    }
}
