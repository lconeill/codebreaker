using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeSlider : MonoBehaviour
{

    public float lifeTime = 20;                     // How much seconds it takes the life slider to reach zero
    public int matchSliderAmount = 2;               // The amount to increase/decrease the life slider for correct/incorrect matches

    private Slider lifeSlider;                      // The life slider GameObject
    private WheelRotation wheelRotation;            // The wheel rotation GameObject
    private slotManager slotManager;

    private float timeKeeper;                       // Counter used to increase/decrease the life slider value
    private int previousCorrectMatches;             // The number of correct matches of the last rendered frame
    private int previousIncorrectMatches;           // The number of incorrect matches of the last rendered frame
    
    // Reference to the UI
    
    private GameObject gameover_ref;
    private ShowPanels gameover_panel;

	private GameObject score_display_ref;
	private ScoreLogic score_logic;
	
	private GameObject currency_ref;
	private Currency currency_script;
	
    // Find references to the GameObjects
    void Start()
    {
        GameObject temp = GameObject.Find("lifeSlider");
        GameObject temp_1 = GameObject.Find("wheel_01");
        GameObject temp_2 = GameObject.Find("slotManager");
        
        // Get reference to the Gameover UI and
        // the restart button.
        
        //gameover_ref = GameObject.Find("UI");
        //gameover_panel = gameover_ref.GetComponent<ShowPanels>();
		
        //gameover_panel.HideGameOver();
		
		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();
		
		currency_ref = GameObject.Find("currency_text");
		currency_script = currency_ref.GetComponent<Currency>();
		
		// Make sure that the game on start is not paused
		
		Time.timeScale = 1;
		
        if (temp != null) { lifeSlider = temp.GetComponent<Slider>();}
        if (lifeSlider != null) { lifeSlider.value = 0.5f; }

        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }
        if (wheelRotation != null) 
        {
            previousCorrectMatches = wheelRotation.match_count;
            previousIncorrectMatches = wheelRotation.mismatched_count;
        }

        if (temp_2 != null) { slotManager = temp_2.GetComponent<slotManager>(); }
    }

    // Constantly decrease the value of life slider and end game when value is 1
    // Increase/decrease the value based on whether a correct/incorrect match was made
    void Update()
    {
        if (!slotManager.inMiniGame)
        {
            timeKeeper += Time.deltaTime;
            lifeSlider.value = (lifeTime - timeKeeper) / lifeTime;

            if (wheelRotation.match_count > previousCorrectMatches)
            {
                timeKeeper = Mathf.Clamp(timeKeeper - matchSliderAmount, 0, lifeTime);
            }

            if (wheelRotation.mismatched_count > previousIncorrectMatches)
            {
                timeKeeper += matchSliderAmount;
            }

            if (timeKeeper >= lifeTime)
            {
            	// When the life bar reaches the end then
            	// pause the game and show the Gameover 
            	// screen with a restart button. 
            	
            	Time.timeScale = 0;
            	
				gameover_panel.ShowGameOver();
				
				if(score_logic.the_score > PlayerPrefs.GetInt("High Score"))
				{
					PlayerPrefs.SetInt("High Score", score_logic.the_score);
					
					Debug.Log(PlayerPrefs.GetInt("High Score"));
            	}
            	
				/*
				if(currency_script.currency > PlayerPrefs.GetInt("Coins"))
				{
					PlayerPrefs.SetInt("Coins", currency_script.currency);
					
					Debug.Log (PlayerPrefs.GetInt("Coins"));
				}
				*/	//Debug.Log("Game Over Sucka");
            }

            previousCorrectMatches = wheelRotation.match_count;
            previousIncorrectMatches = wheelRotation.mismatched_count;
        }
    }

    // Used to decrease the slider when a bad reward from the slot game is acquired
    public void decreaseSlider()
    {
        timeKeeper = Mathf.Clamp(timeKeeper + 5 - matchSliderAmount, 0, lifeTime);
    }

    // Used to injcrease the slider when a good reward from the slot game is acquired
    public void increaseSlider()
    {
        timeKeeper = Mathf.Clamp(timeKeeper - 5 - matchSliderAmount, 0, lifeTime);
    }
}  