using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Currency : MonoBehaviour {

    private int previousCorrectMatches;
    private int previousMatchStreak;

    private ScoreLogic scoreLogic;
    private WheelRotation wheelRotation; 

    private int currency;
    private string stringCurrency = "x0";
    private Text currencyText;

	// Use this for initialization
	void Start () 
    {
        GameObject temp = GameObject.Find("score_display");
        GameObject temp_1 = GameObject.Find("wheel_01");

        if (temp != null) { scoreLogic = temp.GetComponent<ScoreLogic>(); }

        if (scoreLogic != null)
        {
            previousMatchStreak = scoreLogic.match_streak_counter;
        }

        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }

        if (wheelRotation != null)
        {
            previousCorrectMatches = wheelRotation.match_count;
        }

        currencyText = gameObject.GetComponent<Text>();
        currencyText.text = stringCurrency;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (wheelRotation.match_count > previousCorrectMatches)
        {
            if (wheelRotation.match_count % 5 == 0)
            {
                currency += 50;
                stringCurrency = "x" + currency;
                currencyText.text = stringCurrency;
            }
        }

        if (scoreLogic.match_streak_counter > previousMatchStreak)
        {
            for (int i = 5; i < 200; i += 5)
            {
                if (scoreLogic.match_streak_counter % i == 0)
                {
                    currency += 5 + i - 5;
                    stringCurrency = "x" + currency;
                    currencyText.text = stringCurrency;
                    //break;
                }
            }
        }

        previousCorrectMatches = wheelRotation.match_count;
        previousMatchStreak = scoreLogic.match_streak_counter;
	}
}
