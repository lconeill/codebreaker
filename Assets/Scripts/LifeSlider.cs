using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Soomla.Store;

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

    public GameObject explosion_animation;          // Game object that contains the bomb explosion animation
    public GameObject explosion_sfx;                // Game object that contains the sound effect for the bomb explosion
    public GameObject bomb_tick_sfx;                // Game object that contains the bomb ticking sound effect
    
    private bool is_gameover = true;
    
	private GameObject gameover_sfx_ref;
	private AudioSource gameover_sfx;

    private bool tutorial_exit = true; // Used to exit the check to display tutorial, so it's not constantly querying Tutorial script
	
	private GameObject restart_ref;
	private RestartLevel restart_script;

    // Find references to the GameObjects
    void Start()
    {
        GameObject temp = GameObject.Find("lifeSlider");
        GameObject temp_1 = GameObject.Find("wheel_01");
        GameObject temp_2 = GameObject.Find("slotManager");
        
        // Get reference to the Gameover UI and
        // the restart button.
        
        gameover_ref = GameObject.Find("UI");
        gameover_panel = gameover_ref.GetComponent<ShowPanels>();
		
        gameover_panel.HideGameOver();
		
		score_display_ref = GameObject.Find("score_display");
		score_logic = score_display_ref.GetComponent<ScoreLogic>();
		
		gameover_sfx_ref = GameObject.Find("GameOver_SFX_01");
		gameover_sfx = gameover_sfx_ref.GetComponent<AudioSource>();
		
		restart_ref = GameObject.Find("ADsManager");
		restart_script = restart_ref.GetComponent<RestartLevel>();
		
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
        
		restart_script.is_game_over = true;
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
                timeKeeper += matchSliderAmount * 5;
            }

            // Show interactive tutorial to increase life slider value
            if (timeKeeper > 10 && tutorial_exit)
            {
                Tutorial.tutorialSelector("life");
                tutorial_exit = false;
            }

            if (timeKeeper >= lifeTime)
            {
            	// When the life bar reaches the end then
            	// pause the game and show the Gameover 
            	// screen with a restart button. 

            	Time.timeScale = 0;

                //if (score_logic.the_score > PlayerPrefs.GetInt("High Score"))
                if (ScoreLogic.the_score > PlayerPrefs.GetInt("High Score"))
				{
                    //PlayerPrefs.SetInt("High Score", score_logic.the_score);
                    PlayerPrefs.SetInt("High Score", ScoreLogic.the_score);
					
					Debug.Log(PlayerPrefs.GetInt("High Score"));
            	}
            	
				gameover_panel.ShowGameOver();
				PlayGameOverSound();
				
				if(restart_script.is_game_over == true)
				{
					ADsManager();
				}
				
                // Stop ticking sfx. This is for the case when the bomb is still active but the life slider is zero
                bomb_tick_sfx.GetComponent<AudioSource>().Stop();
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

    // Used to inicrease the slider when a good reward from the slot game is acquired
    public void increaseSlider()
    {
        timeKeeper = Mathf.Clamp(timeKeeper - 10 - matchSliderAmount, 0, lifeTime);
    }
    
    // Plays the bomb explosion animation and triggers the game over panel
    public void bombOver()
    {
        explosion_animation.SetActive(true);
        explosion_sfx.GetComponent<AudioSource>().Play();
        StartCoroutine(playExplosion());
    }

    // Waits for explosion animation to finish and then pop up the game over screen
    IEnumerator playExplosion()
    {
        yield return new WaitForSeconds(0.7f);
        //Time.timeScale = 0;
        explosion_animation.SetActive(false);
        explosion_sfx.GetComponent<AudioSource>().Stop();

        // Stop ticking sfx
        // For case when the bomb has exploded but there is another bomb now active
        bomb_tick_sfx.GetComponent<AudioSource>().Stop();

        lifeTime = 0;
    }
    
    public void PlayGameOverSound()
    {
		if(is_gameover == true)
		{
			gameover_sfx.Play();
			
			is_gameover = false;
		}
    	
		else
		{
		
		}
		
    }
    
	// Show Unity ads
	public void ShowAd()
	{
		string remove_ad_id = MayhemStoreAssets.NO_ADS_LIFETIME_PRODUCT_ID;
		
		if (StoreInventory.GetItemBalance(remove_ad_id) == 0)
		{
			if (Advertisement.IsReady())
			{
				Advertisement.Show();
			}
		}
	}
	
	// Show AppBuddiz ads
	public void ShowAppBuddizAd()
	{
		string remove_ad_id = MayhemStoreAssets.NO_ADS_LIFETIME_PRODUCT_ID;
		
		if (StoreInventory.GetItemBalance(remove_ad_id) == 0)
		{
			AdBuddizBinding.ShowAd();
		}
	}
	
	public void SetIsGameOverTrue()
	{
		restart_script.is_game_over = true;
	}
	
	public void ADsManager()
	{
		if (restart_script.is_game_over == true)
		{
			restart_script.death_count = restart_script.death_count + 1;
			Debug.Log("This is the Death Count: " + restart_script.death_count);
			
			PlayerPrefs.SetInt("Player Deaths", restart_script.death_count);
			
			// Display appbuddiz ads
			if(PlayerPrefs.GetInt("Player Deaths") == 4)
			{
				Debug.Log("This is death number: " + restart_script.death_count);
				ShowAppBuddizAd();
			}
			
			// Display Unity ads
			if(PlayerPrefs.GetInt("Player Deaths") == 6)
			{
				Debug.Log("This is death number: " + restart_script.death_count);
				ShowAd();
				
				restart_script.death_count = 0;
				
				PlayerPrefs.SetInt("Player Deaths", restart_script.death_count);
			}
			
			restart_script.is_game_over = false;
		}
		
		else
		{
		
		}
	}

}  