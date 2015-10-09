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
	// This script also controols when the score is updated
	// and when the slot mini game is called.
	
	public GameObject circular_timer_ref;
	
	private GameObject move_script_ref;
	
	private MoveScript move_script;
	
	private GameObject score_display_ref;
	
	private GameObject wheel_ref;
	
	private WheelRotation wheel_rotation_script;
	
	private ScoreLogic score_logic;
	
	private GameObject match_fx_ref;
	
	private MatchFX match_fx;
	
	private GameObject slot_manager_ref;
	
	private slotManager slot_manager;
	
	public bool is_match = false;
	
	private CircularTimer circular_timer_Script;
	
	private GameObject match_sfx_ref;
	
	private AudioSource match_sfx;

	private GameObject mismatch_sfx_ref;
	
	private AudioSource mismatch_sfx;

    private SpawnTile spawnTile;
    private float doubleTapTime;

	void Start () 
	{
		circular_timer_Script = circular_timer_ref.GetComponent<CircularTimer>();
		
		move_script_ref = GameObject.Find("Move_Ref");
		move_script = move_script_ref.GetComponent<MoveScript>();
		
		wheel_ref = GameObject.Find("wheel_01");
		wheel_rotation_script = wheel_ref.GetComponent<WheelRotation>();
		
		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();
		
		match_fx_ref = GameObject.Find("match_fx");
		match_fx = match_fx_ref.GetComponent<MatchFX>();
		
		slot_manager_ref = GameObject.Find("slotManager");
		slot_manager = slot_manager_ref.GetComponent<slotManager>();
		
		match_sfx_ref = GameObject.Find("Match_SFX_01");
		match_sfx = match_sfx_ref.GetComponent<AudioSource>();
		
		mismatch_sfx_ref = GameObject.Find("MisMatch_SFX_01");
		mismatch_sfx = mismatch_sfx_ref.GetComponent<AudioSource>();

        GameObject temp = GameObject.Find("tileSpawn");
        if (temp != null) { spawnTile = temp.GetComponent<SpawnTile>(); }

	}

    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (spawnTile.clonedTiles[3].tag == "bomb")
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (Time.time < doubleTapTime + 0.3f)
                    {
                        wheel_rotation_script.match_count = wheel_rotation_script.match_count + 1;
                        score_logic.match_streak_counter = score_logic.match_streak_counter + 1;
                        UpdatScore(is_match);
                        circular_timer_Script.Reset();
                        move_script.is_touch_start = false;
                        is_match = true;
                    }

                    doubleTapTime = Time.time;
                }
            }
        }

        if (wheel_rotation_script.match_count == 30)
        {
            spawnTile.spawnRange = 5;
        }
        else if (wheel_rotation_script.match_count == 40)
        {
            spawnTile.spawnRange = 8;
        }
    }	

	// This fucntion is called when a trigger collider enters this objects collider.
	// When a Correct match is made the scroe is updated and a variable tracks
	// how many matches the player makes.

	void OnTriggerEnter2D(Collider2D col)
	{
		if(this.gameObject.tag == col.tag.Replace("Source", "Target"))
		{
			//Debug.Log(col.tag);
			
			match_fx_ref.transform.position = col.transform.position;
			
			match_fx.Run();
			
			match_sfx.Play();
			
			wheel_rotation_script.match_count = wheel_rotation_script.match_count + 1;

			circular_timer_Script.Reset();
			move_script.is_touch_start = false;
			is_match = true;
			
			score_logic.match_streak_counter = score_logic.match_streak_counter + 1;

			//Debug.Log(score_logic.match_streak_counter);
			
			// Update the score is called every correct match
			
			UpdatScore(is_match);
			
			// Check every correct match if the condition is
			// met to start the mini game.
			
			StartMiniGame();
		}
		
		else
		{
			mismatch_sfx.Play();
			wheel_rotation_script.mismatched_count = wheel_rotation_script.mismatched_count + 1;
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
	
	public void UpdatScore(bool match)
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
	
	// After the first 15 matches start a mini game.
	// After 25 matches if the player has a 
	// match streak that is a multiple of 20 then
	// call the mini game again.
	
	void StartMiniGame()
	{
		if(wheel_rotation_script.match_count == 15)
		{
			slot_manager.activateSlotGame(true);
			slot_manager.inMiniGame = true;
		}
		
		if(score_logic.match_streak_counter % 20 == 0 && wheel_rotation_script.match_count >= 25)
		{
			slot_manager.activateSlotGame(true);
			slot_manager.inMiniGame = true;
		}
	}
	
}






