using UnityEngine;
using System.Collections;

public class WheelLogic : MonoBehaviour 
{
	// This script is attached to Each match area
	// The function of the script in its current state is to
	// check for collision when a tile enters its collider.
	// When a correct match is made the circular timer script is
	// invoked and calls the functions to move the tiels
	// and reset the timer.
	//
	// This script also controols when the score is updated.
	
	public GameObject circular_timer_ref;
	
	private GameObject move_script_ref;
	
	private MoveScript move_script;
	
	private GameObject score_display_ref;
	
	private GameObject wheel_ref;
	
	private WheelRotation wheel_rotation_script;
	
	private ScoreLogic score_logic;
	
	public bool is_match = false;
	
	private CircularTimer circular_timer_Script;

	void Start () 
	{
		circular_timer_Script = circular_timer_ref.GetComponent<CircularTimer>();
		
		move_script_ref = GameObject.Find("Move_Ref");
		move_script = move_script_ref.GetComponent<MoveScript>();
		
		wheel_ref = GameObject.Find("wheel_01");
		wheel_rotation_script = wheel_ref.GetComponent<WheelRotation>();
		
		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();

	}
	
	// This fucntion is called when a trigger collider enters this objects collider.
	// When a Correct match is made the scroe is updated and a variable tracks
	// how many matches the player makes.

	void OnTriggerEnter2D(Collider2D col)
	{
		if(this.gameObject.tag == col.tag.Replace("Source", "Target"))
		{
			Debug.Log(col.tag);
			
			wheel_rotation_script.match_count = wheel_rotation_script.match_count + 1;

			circular_timer_Script.Reset();
			move_script.is_touch_start = false;
			is_match = true;
			
			score_logic.match_streak_counter = score_logic.match_streak_counter + 1;

			Debug.Log(score_logic.match_streak_counter);
			
			UpdatScore(is_match);
		}
		
		else
		{
			wheel_rotation_script.mismatched_count = wheel_rotation_script.mismatched_count + 1;
			Debug.Log("This is not a match!");
			circular_timer_Script.Reset();
			move_script.is_touch_start = false;
			is_match = false;
			
			score_logic.match_streak_counter = 0;
			
			UpdatScore(is_match);
		}
	}
	
	// This function checks to see if the player has made a corect or incorrect
	// match. If the play makes a correct match then his score is updated, if 
	// the player does 5 consecutive matches they get a streak bonus. Streak
	// Bonuses are done in increments of 5 to a total of a 30 streak where the 
	// total points awarded is 3000.
	
	void UpdatScore(bool match)
	{
		if (match == true)
		{
			if(score_logic.match_streak_counter == 5)
			{
				score_logic.the_score = score_logic.the_score + 100;
			}
			
			if(score_logic.match_streak_counter == 10)
			{
				score_logic.the_score = score_logic.the_score + 200;
			}
						
			if(score_logic.match_streak_counter == 15)
			{
				score_logic.the_score = score_logic.the_score + 500;
			}
			
			if(score_logic.match_streak_counter == 20)
			{
				score_logic.the_score = score_logic.the_score + 1000;
			}
			
			if(score_logic.match_streak_counter == 25)
			{
				score_logic.the_score = score_logic.the_score + 1500;
			}
			
			if(score_logic.match_streak_counter == 30)
			{
				score_logic.the_score = score_logic.the_score + 3000;
			}
			
			else
			{
				score_logic.the_score = score_logic.the_score + 50;
			}
		}
		
		else
		{
			score_logic.match_streak_counter = 0;
		}
	}
	
}






