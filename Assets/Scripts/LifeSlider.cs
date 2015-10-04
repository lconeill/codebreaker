﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeSlider : MonoBehaviour
{

    public float lifeTime = 20;                     // How much seconds it takes the life slider to reach zero
    public int matchSliderAmount = 1;               // The amount to increase/decrease the life slider for correct/incorrect matches

    private Slider lifeSlider;                      // The life slider GameObject
    private WheelRotation wheelRotation;            // The wheel rotation GameObject

    private float timeKeeper;                       // Counter used to increase/decrease the life slider value
    private int previousCorrectMatches;             // The number of correct matches of the last rendered frame
    private int previousIncorrectMatches;           // The number of incorrect matches of the last rendered frame

    // Find references to the GameObjects
    void Start()
    {
        GameObject temp = GameObject.Find("lifeSlider");
        GameObject temp_1 = GameObject.Find("wheel_01");

        if (temp != null) { lifeSlider = temp.GetComponent<Slider>();}
        if (lifeSlider != null) { lifeSlider.value = 0.5f; }

        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }
        if (wheelRotation != null) 
        {
            previousCorrectMatches = wheelRotation.match_count;
            previousIncorrectMatches = wheelRotation.mismatched_count;
        }
    }

    // Constantly decrease the value of life slider and end game when value is 1
    // Increase/decrease the value based on whether a correct/incorrect match was made
    void Update()
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
            Debug.Log("Game Over Sucka");
        }

        previousCorrectMatches = wheelRotation.match_count;
        previousIncorrectMatches = wheelRotation.mismatched_count;
    }

    // Used to decrease the slider when a bad reward from the slot game is acquired
    public void decreaseSlider()
    {
        timeKeeper += 5;
    }

    // Used to injcrease the slider when a good reward from the slot game is acquired
    public void increaseSlider()
    {
        timeKeeper -= 5;
    }
}