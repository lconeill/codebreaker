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
    private float originalFillSpeed;
    public float rewardFillSpeed = 20;
    private float rewardEffectTime = 5f;
    private float timeIncrement = 0;
    private bool endFillTimeIncrease = false;

    private WheelRotation wheelRotation;            // The wheel rotation GameObject
    private int count = 0;
    private float fillSpeed_30;
    private float fillSpeed_40;
    private float fillSpeed_50;
    private float fillSpeed_60; 

    // Set timer to zero fill amount
    void Start()
    {
        Reset();
        originalFillSpeed = fillSpeed;
        fillSpeed_30 = fillSpeed * 0.75f;
        fillSpeed_40 = fillSpeed * 0.625f;
        fillSpeed_50 = fillSpeed * 0.5f;
        fillSpeed_60 = fillSpeed * 0.4f; 

        GameObject temp = GameObject.Find("wheel_01");

        if (temp != null) 
        { 
            wheelRotation = temp.GetComponent<WheelRotation>(); 
        }
    }

    // Fill the timer based on the time variable
    // Move tiles and reset timer if timer is full
    void Update()
    {
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
                accumulate = accumulate * rewardFillSpeed/fillSpeed;
                fillSpeed = rewardFillSpeed;
                increaseFillTime = false;
                endFillTimeIncrease = true;
            }

            circularTimer.fillAmount = accumulate/fillSpeed;

            if (endFillTimeIncrease)
            {
                timeIncrement += Time.deltaTime;

                if (timeIncrement / rewardEffectTime >= 1)
                {
                    endFillTimeIncrease = false;
                    accumulate = accumulate * originalFillSpeed / rewardFillSpeed;
                    fillSpeed = originalFillSpeed;
                }
            }
           
            if (circularTimer.fillAmount == 1)
            {    
                Reset();
            }
        }
    }

    // Resets the timer
    public void Reset()
    {
        moveTile.moveTiles();
        circularTimer.fillAmount = 0f;
        accumulate = 0;
    }
}