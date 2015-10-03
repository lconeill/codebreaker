using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class CircularTimer : MonoBehaviour
{

    public Image circularTimer;      // Image of the timer. Type = Filled, Method = Radial 360
    public float fillSpeed = 3f;     // How long the timer takes to fill in seconds
    public SpawnTile moveTile;       // Reference to SpawnTile class so we can move tiles when timer is full

    public slotManager slotManager;
    private float accumulate = 0;

    public bool increaseFillTime = false;
    private float originalFillSpeed;
    public float rewardFillSpeed = 20;
    private float rewardEffectTime = 5f;
    private float timeIncrement = 0;
    private bool endFillTimeIncrease = false;

    // Set timer to zero fill amount
    void Start()
    {
        Reset();
        originalFillSpeed = fillSpeed;
    }

    // Fill the timer based on the time variable
    // Move tiles and reset timer if timer is full
    void Update()
    {
        if (!slotManager.inMiniGame)
        {
            accumulate += Time.deltaTime;

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

  