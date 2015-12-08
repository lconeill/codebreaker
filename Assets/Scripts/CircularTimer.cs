using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class CircularTimer : MonoBehaviour
{

    public Image circularTimer;                 // Image of the timer. Type = Filled, Method = Radial 360
    public float fillSpeed = 2f;                // How long the timer takes to fill in seconds
    public SpawnTile moveTile;                  // Reference to SpawnTile class so we can move tiles when timer is full

    public slotManager slotManager;
    private float accumulate = 0;

    public bool increaseFillTime = false;
    private float fillSpeedBefore;
    public float rewardFillSpeed = 5;
    private float rewardEffectTime = 5f;
    private float timeIncrement = 0;
    private bool endFillTimeIncrease = false;

    private WheelRotation wheelRotation;            // The wheel rotation GameObject
    private int count = 0;
    private float fillSpeed_30;
    private float fillSpeed_40;
    private float fillSpeed_50;
    private float fillSpeed_60; 

	private GameObject wheel_logic_ref;
	
	private WheelLogic wheel_logic;

	private GameObject match_sfx_ref;
	
	private AudioSource match_sfx;
	

	private GameObject mismatch_sfx_ref;
	
	private AudioSource mismatch_sfx;

    private Button bell_button;
    
    private GameObject match_fx_ref;
    private ParticleSystem match_fx_particle;
    private MatchFX match_fx_script;
    
    public float particle_duration = 0.0f;
    
    private bool is_reset = false;

    // Set timer to zero fill amount
    void Start()
    {
        
        fillSpeed_30 = fillSpeed * 0.75f;
        fillSpeed_40 = fillSpeed * 0.625f;
        fillSpeed_50 = fillSpeed * 0.5f;
        fillSpeed_60 = fillSpeed * 0.4f; 

        GameObject temp = GameObject.Find("wheel_01");
        if (temp != null) { wheelRotation = temp.GetComponent<WheelRotation>(); }

        GameObject temp_1 = GameObject.Find("freeze_timer_power_up");
        if (temp_1 != null) { bell_button = temp_1.GetComponent<Button>(); }

		match_sfx_ref = GameObject.Find("Match_SFX_01");
		match_sfx = match_sfx_ref.GetComponent<AudioSource>();
        
		mismatch_sfx_ref = GameObject.Find("MisMatch_SFX_01");
		mismatch_sfx = mismatch_sfx_ref.GetComponent<AudioSource>();
		
		wheel_logic_ref = GameObject.Find("match_01");
		wheel_logic = wheel_logic_ref.GetComponent<WheelLogic>();
		
		match_fx_ref = GameObject.Find("match_fx");
		match_fx_particle = match_fx_ref.GetComponent<ParticleSystem>();
		match_fx_script = match_fx_ref.GetComponent<MatchFX>();
		
		Reset();
		
    }

    // Fill the timer based on the time variable
    // Move tiles and reset timer if timer is full
    void Update()
    {
		if(is_reset == true)
		{
			Reset();
		}
		
        if (!slotManager.inMiniGame)
        {
            accumulate += Time.deltaTime;
            
            if (wheelRotation.match_count == 30 && count == 0)
            {
                fillSpeed = fillSpeed_30;
                count++;
            }

            else if (wheelRotation.match_count == 40 && count == 1)
            {
                fillSpeed = fillSpeed_40;
                count++;
            }

            else if (wheelRotation.match_count == 50 && count == 2)
            {
                fillSpeed = fillSpeed_50;
                count++;
            }

            else if (wheelRotation.match_count == 60 && count == 3)
            {
                fillSpeed = fillSpeed_60;
                count++;
            }

            if (increaseFillTime)
            {
                fillSpeedBefore = fillSpeed;
                accumulate = accumulate * rewardFillSpeed/fillSpeed;
                fillSpeed = rewardFillSpeed;
                increaseFillTime = false;
                endFillTimeIncrease = true;
                bell_button.enabled = false;
            }

            circularTimer.fillAmount = accumulate/fillSpeed;

            if (endFillTimeIncrease)
            {
                timeIncrement += Time.deltaTime;

                if (timeIncrement / rewardEffectTime >= 1)
                {
                    timeIncrement = 0;
                    endFillTimeIncrease = false;
                    accumulate = accumulate * fillSpeedBefore / rewardFillSpeed;
                    fillSpeed = fillSpeedBefore;
                    bell_button.enabled = true;
                }
            }
           
			if (circularTimer.fillAmount == 1 && is_reset == false)
            {   
				//Debug.Log(moveTile.clonedTiles[3].tag);

				/*
				
				string[] unmatchable = {"Source_05", "Source_06", "Source_07"}; 
				string tile_value = moveTile.clonedTiles[3].tag;
				int pos = System.Array.IndexOf(unmatchable, tile_value);
				if(pos > -1)
				{
					wheelRotation.match_count = wheelRotation.match_count + 1;
					wheel_logic.UpdatScore(wheel_logic.is_match = true);
					match_sfx.Play();
					Reset();
				}
				
				*/

				wheelRotation.mismatched_count = wheelRotation.mismatched_count + 1;
				wheel_logic.UpdatScore(wheel_logic.is_match = false);
				match_fx_script.Run();
				particle_duration = Time.time;
				mismatch_sfx.Play();
            	Reset();


            }
        }
    }

    // Resets the timer
    public void Reset()
    {
		is_reset = true;
		moveTile.clonedTiles[3].SetActive(false);
		circularTimer.fillAmount = 0f;
		accumulate = 0;
		if(Time.time >= particle_duration + match_fx_particle.duration / 4)
		{
	        moveTile.moveTiles();
	        is_reset = false;
        }
    }
}