using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class CircularTimer : MonoBehaviour
{

    public Image circularTimer;      // Image of the timer. Type = Filled, Method = Radial 360
    public float fillSpeed = 3f;     // How long the timer takes to fill in seconds
    public SpawnTile moveTile;       // Reference to SpawnTile class so we can move tiles when timer is full

    public slotManager softPause;
    public float accumulate = 0;

    // Set timer to zero fill amount
    void Start()
    {
        Reset();
    }

    // Resets the timer
    public void Reset()
    {
        circularTimer.fillAmount = 0f;
        accumulate = 0;
    }

    // Fill the timer based on the time variable
    // Move tiles and reset timer if timer is full
    void Update()
    {
        if (softPause.inMiniGame)
        {
            accumulate += Time.deltaTime;
            circularTimer.fillAmount = accumulate/fillSpeed;

            if (circularTimer.fillAmount == 1)
            {
                moveTile.moveTiles();
                Reset();
            }
        }
    }
}