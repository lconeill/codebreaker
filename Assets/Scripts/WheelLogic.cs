﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private SpriteRenderer spawn_tile_renderer;
	private MoveScript active_tile_move_ref;
	
    private GameObject streak_fb_great_ref;
	private Image streak_fb_great;
	private GameObject streak_fb_awesome_ref;
	private Image streak_fb_awesome;
	private Image streak_fb_amazing;
	private GameObject streak_fb_amazing_ref;
	private Image streak_fb_unstoppable;
	private GameObject streak_fb_unstoppable_ref;
	private Image streak_fb_match_fiend;
	private GameObject streak_fb_match_fiend_ref;
	private Image streak_fb_mayhem;
	private GameObject streak_fb_mayhem_ref;

    private int score_multiplier = 1;
    private bool multiplier_flag = false;
    private float reward_time_increment = 0;
    private float reward_effect_time = 7;
    
    private NonSwipeActions non_swipe_script;
	private GameObject double_tap_ref;

    private Button double_points_button;         // button component of reduce tile shape power up game object

    private GameObject double_points_fx;
    private MatchFX double_points_match_fx;

    private GameObject freeze_timer_fx;
    private MatchFX freeze_timer_match_fx;

    private GameObject reduce_shape_fx;
    private MatchFX reduce_shape_match_fx;

	void Start () 
	{
		if(PlayerPrefs.GetInt("High Score") == 0)
		{
			PlayerPrefs.SetInt("High Score", 0);
		}
		
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
		
		streak_fb_great_ref = GameObject.Find("Great");
		
		streak_fb_great = streak_fb_great_ref.GetComponent<Image>();
		
		streak_fb_awesome_ref = GameObject.Find("Awesome");
		
		streak_fb_awesome = streak_fb_awesome_ref.GetComponent<Image>();
		
		streak_fb_amazing_ref = GameObject.Find("Amazing");
		
		streak_fb_amazing = streak_fb_amazing_ref.GetComponent<Image>();
		
		streak_fb_unstoppable_ref = GameObject.Find("Unstoppable");
		
		streak_fb_unstoppable = streak_fb_unstoppable_ref.GetComponent<Image>();
		
		streak_fb_match_fiend_ref = GameObject.Find("Match_Fiend");
		
		streak_fb_match_fiend = streak_fb_match_fiend_ref.GetComponent<Image>();
		
		streak_fb_mayhem_ref = GameObject.Find("Mayhem");
		
		streak_fb_mayhem = streak_fb_mayhem_ref.GetComponent<Image>();
		
        GameObject temp = GameObject.Find("tileSpawn");
        if (temp != null) { spawnTile = temp.GetComponent<SpawnTile>(); }
        
		double_tap_ref = GameObject.Find ("double_tap");
		
		non_swipe_script = double_tap_ref.GetComponent<NonSwipeActions>();

        GameObject temp_01 = GameObject.Find("double_point_power_up");
        if (temp_01 != null) { double_points_button = temp_01.GetComponent<Button>(); }

        double_points_fx = GameObject.Find("double_points_fx");
        if (double_points_fx != null) { double_points_match_fx = double_points_fx.GetComponent<MatchFX>(); }

        freeze_timer_fx = GameObject.Find("freeze_timer_fx");
        if (freeze_timer_fx != null) { freeze_timer_match_fx = freeze_timer_fx.GetComponent<MatchFX>(); }

        reduce_shape_fx = GameObject.Find("reduce_shape_fx");
        if (reduce_shape_fx != null) { reduce_shape_match_fx = reduce_shape_fx.GetComponent<MatchFX>(); }
	}

    void Update()
    {   
		if (slot_manager.inMiniGame == true)
		{
			spawn_tile_renderer = spawnTile.clonedTiles[3].GetComponent<SpriteRenderer>();
			spawn_tile_renderer.enabled = false;
			
			active_tile_move_ref = spawnTile.clonedTiles[3].GetComponent<MoveScript>();
			active_tile_move_ref.enabled = false;
		}
		
		else
		{
			spawn_tile_renderer = spawnTile.clonedTiles[3].GetComponent<SpriteRenderer>();
			spawn_tile_renderer.enabled = true;
			
			active_tile_move_ref = spawnTile.clonedTiles[3].GetComponent<MoveScript>();
			active_tile_move_ref.enabled = true;
		}
        
        if (wheel_rotation_script.match_count >= 10 && wheel_rotation_script.match_count < 15)
        {
            spawnTile.extendSpawnRange(4, 5);
        }

        else if (wheel_rotation_script.match_count >= 20 && wheel_rotation_script.match_count < 25)
        {
            spawnTile.extendSpawnRange(4, 9);
        }

        else if (wheel_rotation_script.match_count >= 30)
        {
            spawnTile.extendSpawnRange(4, 13);
        }

        // Show interactive tutorial to reduce shapes if never shown before
        if (wheel_rotation_script.match_count == 35)
        {
            Tutorial.tutorialSelector("reduce");
        }

        // Controls the score multiplier effect time
        if (multiplier_flag && !slot_manager.inMiniGame)
        {
            reward_time_increment += Time.deltaTime;
            double_points_button.enabled = false;

            if (reward_time_increment >= reward_effect_time)
            {
                multiplier_flag = false;
                score_multiplier = 1;
                reward_time_increment = 0;
                double_points_button.enabled = true;
            }
        }
    }


	// This function is called when a trigger collider enters this objects collider.
	// When a Correct match is made the scroe is updated and a variable tracks
	// how many matches the player makes.

	void OnTriggerEnter2D(Collider2D col)
	{
		if(this.gameObject.tag == col.tag.Replace("Source", "Target"))
		{
			//Debug.Log(col.tag);


            if (multiplier_flag)
            {
                double_points_fx.transform.position = col.transform.position;
                double_points_match_fx.Run();
            }

            //**
            // There are no longer any particle effects for the reduce shapes and freeze timer effects
            //**
            //if (circular_timer_Script.freezeTimerActivated())
            //{
            //    freeze_timer_fx.transform.position = col.transform.position;
            //    freeze_timer_match_fx.Run();
            //}

            //if (spawnTile.reduceShapeActivated())
            //{
            //    reduce_shape_fx.transform.position = col.transform.position;
            //    reduce_shape_match_fx.Run();
            //}

            // If any of the rewards are activated don't play default fx
            //if (multiplier_flag || spawnTile.reduceShapeActivated() || circular_timer_Script.freezeTimerActivated()) { }
            if (multiplier_flag) { }

            else
            {
                match_fx_ref.transform.position = col.transform.position;
                match_fx.Run();
            }

			match_sfx.Play();
			
			wheel_rotation_script.match_count = wheel_rotation_script.match_count + 1;
			
			circular_timer_Script.particle_duration = Time.time;
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
		
		else if(col.tag == "bomb")
		{
			non_swipe_script.bombReset();
		}
		
		else
		{
			mismatch_sfx.Play();
			wheel_rotation_script.mismatched_count = wheel_rotation_script.mismatched_count + 1;
			
			circular_timer_Script.particle_duration = Time.time;
			circular_timer_Script.Reset();
			move_script.is_touch_start = false;
			is_match = false;
			
			score_logic.match_streak_counter = 0;
			
			UpdatScore(is_match);
		}
	}

    public void increaseMultiplier()
    {
        multiplier_flag = true;
        score_multiplier = 2;
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
			if(score_logic.match_streak_counter == 10)
			{
				StartCoroutine(ShowStreakGreat());
                //score_logic.the_score = score_logic.the_score + (100 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (100 * score_multiplier);
			}
			
			if(score_logic.match_streak_counter == 20)
			{
				StartCoroutine(ShowStreakAwesome());
                //score_logic.the_score = score_logic.the_score + (200 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (200 * score_multiplier);
			}
						
			if(score_logic.match_streak_counter == 30)
			{
				StartCoroutine(ShowStreakAmazing());
                //score_logic.the_score = score_logic.the_score + (500 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (500 * score_multiplier);
			}
			
			if(score_logic.match_streak_counter == 40)
			{
				StartCoroutine(ShowStreakUnstoppable());
                //score_logic.the_score = score_logic.the_score + (1000 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (1000 * score_multiplier);
			}
			
			if(score_logic.match_streak_counter == 50)
			{
				StartCoroutine(ShowStreakMatchFiend());
                //score_logic.the_score = score_logic.the_score + (1500 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (1500 * score_multiplier);
			}
			
			if(score_logic.match_streak_counter == 60)
			{
				StartCoroutine(ShowStreakMayhem());
                //score_logic.the_score = score_logic.the_score + (3000 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (3000 * score_multiplier);
			}
			
			if(score_logic.match_streak_counter > 60)
			{
                //score_logic.the_score = score_logic.the_score + (200 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (200 * score_multiplier);
			}
			
			else
			{
                //score_logic.the_score = score_logic.the_score + (50 * score_multiplier);
                ScoreLogic.the_score = ScoreLogic.the_score + (50 * score_multiplier);
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
	
	IEnumerator ShowStreakGreat()
	{
		streak_fb_great.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_great.enabled = false;
	}
	
	IEnumerator ShowStreakAwesome()
	{
		streak_fb_awesome.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_awesome.enabled = false;
	}
	
	IEnumerator ShowStreakAmazing()
	{
		streak_fb_amazing.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_amazing.enabled = false;
	}
	
	IEnumerator ShowStreakUnstoppable()
	{
		streak_fb_unstoppable.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_unstoppable.enabled = false;
	}
	
	IEnumerator ShowStreakMatchFiend()
	{
		streak_fb_match_fiend.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_match_fiend.enabled = false;
	}
	
	IEnumerator ShowStreakMayhem()
	{
		streak_fb_mayhem.enabled = true;
		
		yield return new WaitForSeconds(1.0f);
		
		streak_fb_mayhem.enabled = false;
	}


    // Used to trigger double point particle effects in NonSwipeActions script
    public bool doublePointActivated()
    {
        return multiplier_flag;
    }
	
}