using UnityEngine;
using System.Collections;
using System;


public class NonSwipeActions : MonoBehaviour {

    public CircularTimer circularTimer;

    private SpawnTile spawnTile;
    private WheelRotation wheelRotation;
    private ScoreLogic scoreLogic;
    private AudioSource matchSFX;
    private AudioSource mismatchSFX;
    private WheelLogic wheelLogic;
    private MoveScript moveScript;

    private float doubleTapTime = 0;
    private string[] tagArray = { "Source_01", "Source_02", "Source_03", "Source_04" };
    private float diffuseTimer = 0;
    private bool isBombTouch = false;
    Touch touch;


	// Use this for initialization
	void Start () 
    {
        GameObject temp = GameObject.Find("tileSpawn");
        if (temp != null) { spawnTile = temp.GetComponent<SpawnTile>(); }

        GameObject temp_2 = GameObject.Find("wheel_01");
        if (temp_2 != null) { wheelRotation = temp_2.GetComponent<WheelRotation>(); }

        GameObject temp_3 = GameObject.Find("score_display");
        if (temp_3 != null) { scoreLogic = temp_3.GetComponent<ScoreLogic>(); }

        GameObject temp_4 = GameObject.Find("Match_SFX_01");
        if (temp_4 != null) { matchSFX = temp_4.GetComponent<AudioSource>(); }

        GameObject temp_5 = GameObject.Find("match_01");
        if (temp_5 != null) { wheelLogic = temp_5.GetComponent<WheelLogic>(); }

        GameObject temp_6 = GameObject.Find("Move_Ref");
        if (temp_6 != null) { moveScript = temp_6.GetComponent<MoveScript>(); }

        GameObject temp_7 = GameObject.Find("MisMatch_SFX_01");
        if (temp_7 != null) { mismatchSFX = temp_7.GetComponent<AudioSource>(); }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount > 0)
        {
            doubleTap();
            diffuseBomb();
        }

        if (spawnTile.clonedTiles[3].tag == "bomb" && circularTimer.circularTimer.fillAmount >= 0.99)
        {
            isBombTouch = false;
            diffuseTimer = 0;
            badReset();
            Debug.Log("The bomb exploded!");
        }
	}

    public void diffuseBomb()
    {

        if (spawnTile.clonedTiles[3].tag == "bomb")
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                diffuseTimer = Time.time;
                isBombTouch = true;
            }

            if (isBombTouch && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isBombTouch = false;
                diffuseTimer = 0;
                badReset();
                Debug.Log("The bomb exploded!");
            }

            if (isBombTouch && (Time.time - diffuseTimer) >= 0.5)
            {
                isBombTouch = false;
                diffuseTimer = 0;
                goodReset();
                Debug.Log("Diffused the bomb!");
            }
        }

        else
        {
            diffuseTimer = 0;
            isBombTouch = false;
        }
    }

    // Double tap logic - fails because reset is being called 4 times per actual double tap
    public void doubleTap()
    {

        // Increase score and reset if double tap on new tile shapes
        if (spawnTile.clonedTiles[3].tag == "Source_05")
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Time.time < doubleTapTime + 0.3f)
                {
                    goodReset();
                }

                doubleTapTime = Time.time;
            }
        }

        // Reset streak and reset timer if double tap on original tile shapes
        else if (Array.IndexOf(tagArray, spawnTile.clonedTiles[3].tag) > -1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Time.time < doubleTapTime + 0.3f)
                {
                    badReset();
                }

                doubleTapTime = Time.time;
            }
        }
    }

    public void goodReset()
    {
        wheelRotation.match_count = wheelRotation.match_count + 1;
        scoreLogic.match_streak_counter = scoreLogic.match_streak_counter + 1;
        wheelLogic.is_match = true;
        wheelLogic.UpdatScore(wheelLogic.is_match);
        circularTimer.Reset();
        moveScript.is_touch_start = false;
        matchSFX.Play();
    }

    public void badReset()
    {
        wheelLogic.is_match = false;
        wheelLogic.UpdatScore(wheelLogic.is_match);
        circularTimer.Reset();
        moveScript.is_touch_start = false;
        mismatchSFX.Play();
        wheelRotation.mismatched_count = wheelRotation.mismatched_count + 1;
        scoreLogic.match_streak_counter = 0;
    }
}
