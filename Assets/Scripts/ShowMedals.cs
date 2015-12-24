using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMedals : MonoBehaviour 
{
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

	// Use this for initialization
	void Start () 
	{
		//bronze_medal_img = bronze_medal_ref.GetComponent<Image>();
		//silver_medal_img = silver_medal_ref.GetComponent<Image>();
		//gold_medal_img = gold_medal_ref.GetComponent<Image>();
		
		bronze_medal_ref.SetActive(false);
		silver_medal_ref.SetActive(false);
		gold_medal_ref.SetActive(false);
        medal_ribbon.SetActive(false);
        highscore_text.SetActive(false);
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

        highscore_text.SetActive(true);
        highscore_text.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("High Score");

		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();

        if (score_logic.the_score >= 10000 && score_logic.the_score < 20000)
		{
            medal_ribbon.SetActive(true);
			bronze_medal_ref.SetActive(true);
		}

        if (score_logic.the_score >= 20000 && score_logic.the_score < 30000)
		{
            medal_ribbon.SetActive(true);
			silver_medal_ref.SetActive(true);
		}

        if (score_logic.the_score >= 30000)
		{
            medal_ribbon.SetActive(true);
			gold_medal_ref.SetActive(true);
		}
	}
}
