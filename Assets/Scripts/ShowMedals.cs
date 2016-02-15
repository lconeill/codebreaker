﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMedals : MonoBehaviour 
{
	public GameObject default_medal_ref;
	public GameObject bronze_medal_ref;
	public GameObject silver_medal_ref;
	public GameObject gold_medal_ref;
    public GameObject medal_ribbon;
	
	private Image bronze_medal_img;
	private Image silver_medal_img;
	private Image gold_medal_img;

	private GameObject score_display_ref;
	private ScoreLogic score_logic;

    public GameObject highscore_text;
    public GameObject score_to_next_medal;
    public GameObject current_score_text;

    private bool stop_giftiz_mission_complete = true;      // Stop the Giftiz mission complete banner from showing
	
	// Use this for initialization
	void Start () 
	{
		//bronze_medal_img = bronze_medal_ref.GetComponent<Image>();
		//silver_medal_img = silver_medal_ref.GetComponent<Image>();
		//gold_medal_img = gold_medal_ref.GetComponent<Image>();
		
		default_medal_ref.SetActive(true);
		bronze_medal_ref.SetActive(false);
		silver_medal_ref.SetActive(false);
		gold_medal_ref.SetActive(false);
        //medal_ribbon.SetActive(false);
        highscore_text.SetActive(false);
		score_to_next_medal.SetActive(false);
        current_score_text.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShowEarnedMedal()
	{

        bronze_medal_ref.SetActive(false);
        silver_medal_ref.SetActive(false);
        gold_medal_ref.SetActive(false);
		medal_ribbon.SetActive(true);
		
        highscore_text.SetActive(true);
        highscore_text.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("High Score");

        current_score_text.SetActive(true);
        string score = ScoreLogic.the_score.ToString();
        current_score_text.GetComponent<Text>().text = "Score: " + score;

		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();


        // Show the Giftiz mission complete
        if (ScoreLogic.the_score >= 1500 && stop_giftiz_mission_complete)
        {
            #if UNITY_ANDROID

            GiftizBinding.missionComplete();
            stop_giftiz_mission_complete = false;

            #endif
        }

        //if (score_logic.the_score < 8000)
        if (ScoreLogic.the_score < 8000)
		{
			score_to_next_medal.SetActive(true);
			score_to_next_medal.GetComponent<Text>().text = "8000 points for Bronze";
		}

        //if (score_logic.the_score >= 8000 && score_logic.the_score < 15000)
        if (ScoreLogic.the_score >= 8000 && ScoreLogic.the_score < 15000)
		{
            //medal_ribbon.SetActive(true);
			default_medal_ref.SetActive(false);
			bronze_medal_ref.SetActive(true);
			score_to_next_medal.SetActive(true);
			score_to_next_medal.GetComponent<Text>().text = "15000 points for Silver";
		}

        //if (score_logic.the_score >= 15000 && score_logic.the_score < 25000)
        if (ScoreLogic.the_score >= 15000 && ScoreLogic.the_score < 25000)
		{
            //medal_ribbon.SetActive(true);
			default_medal_ref.SetActive(false);
			silver_medal_ref.SetActive(true);
			score_to_next_medal.SetActive(true);
			score_to_next_medal.GetComponent<Text>().text = "25000 points for Gold";
		}

        //if (score_logic.the_score >= 25000)
        if (ScoreLogic.the_score >= 25000)
		{
            //medal_ribbon.SetActive(true);
			default_medal_ref.SetActive(false);
			gold_medal_ref.SetActive(true);
		}
	}
}
