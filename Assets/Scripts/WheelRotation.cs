using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WheelRotation : MonoBehaviour 
{
	// This script is used for controlling the Wheels rotation logic.
	// The variables to control the wheel are made publisc to let 
	// us controll the speed and when to start or stop the spinning wheel.

	private float startRollLerp = 360;
	private Vector3 updateRotation = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	
	private int rotationSpeed = 110;
	public bool startRoll = true;
	public bool continueRotation = false;
	
	public int match_count = 0;
	public int mismatched_count = 0;

    public bool slowRotationFlag = false;
    private float rewardEffectTime = 5f;
    private float timeIncrement = 0;
    private int previousRotationSpeed;
    private int rewardRotationSpeed = 75;

    public GameObject fire_effect_game_object;
    private Image fire_effect_image;

    //private int rewardRotationSpeed = 0;

    //private Button orange_button;
	
	// get a reference to the slot game so that once
	// it starts we stop the roraion of the wheel.
	private GameObject slot_manager_ref;
	
	private slotManager slot_manager;
	
	// Use this for initialization
	void Start () 
	{
		rotation = transform.eulerAngles;
		
		slot_manager_ref = GameObject.Find("slotManager");
		slot_manager = slot_manager_ref.GetComponent<slotManager>();

        fire_effect_image = fire_effect_game_object.GetComponent<Image>();

        //GameObject temp = GameObject.Find("orange_power_up");
        //if (temp != null) { orange_button = temp.GetComponent<Button>(); }
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(slot_manager.inMiniGame == false)
		{
			if (startRoll)
			{
				startRollLerp += Time.deltaTime;
				
                if (startRollLerp > (360 + rotationSpeed) / (float)rotationSpeed)
				{
                    startRoll = false;
                    //startRollLerp = 360 / (float)rotationSpeed;
					continueRotation = true;
                    startRollLerp = 0;
				}
				//smoothStart();
			}
			
			else if (continueRotation)
			{
				speedLevel(match_count);
				rotation.z -= rotationSpeed * Time.deltaTime;
				transform.eulerAngles = rotation;
			}
		}

        if (slowRotationFlag && !slot_manager.inMiniGame)
        {
            timeIncrement += Time.deltaTime;
            fire_effect_image.enabled = true;

            if (timeIncrement > rewardEffectTime)
            {
                //rotationSpeed = previousRotationSpeed;
                slowRotationFlag = false;
                timeIncrement = 0;
                fire_effect_image.enabled = false;
                //startRoll = true;
                //orange_button.enabled = true;
            }
        }
	}
	
	public void smoothStart()
	{

        float stop = (360 + rotationSpeed) / (float)rotationSpeed;
        float perc = startRollLerp / stop;

		perc = 1 - Mathf.Cos(perc * Mathf.PI * 0.5f);
		
		updateRotation.z = Mathf.Lerp(rotation.z, rotation.z - 360, perc);
		
		transform.eulerAngles = updateRotation;
	}
	
	// This function based on the match count 
	// will increase the speed in which the wheel spins.

    public void slowRotationReward()
    {
        slowRotationFlag = true;
        //previousRotationSpeed = rotationSpeed;
        //rotationSpeed = rewardRotationSpeed;
        //orange_button.enabled = false;
    }

	void speedLevel(int count)
	{
        //if (!slowRotationFlag)
        //{
        if (count >= 3 && count < 9)
        {
            rotationSpeed = 130;
        }

		else if (count >= 9 && count < 16)
        {
            rotationSpeed = 150;
        }

        else if (count >= 17 && count < 20)
        {
            rotationSpeed = 170;
        }

        else if (count >= 20 && count < 25)
        {
            rotationSpeed = 190;
        }

        else if (count >= 25 && count < 30)
        {
            rotationSpeed = 210;
        }

        else if (count >= 35 && count < 40)
        {
            rotationSpeed = 200;
        }
            
		else if (count >= 40)
		{
			rotationSpeed = 220;
		}
        //}
	}
}