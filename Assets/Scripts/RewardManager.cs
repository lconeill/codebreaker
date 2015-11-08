﻿using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

    public CircularTimer circularTimer;
    private LifeSlider lifeSlider;
    private WheelRotation wheelRotation;
    private SpawnTile spawnTile;
    private HideTileReward hideTileReward;
    private WheelLogic wheelLogic;

	// Use this for initialization
	void Start () 
    {
        GameObject temp = GameObject.Find("lifeSlider");
        if (temp != null) { lifeSlider = temp.GetComponent<LifeSlider>(); }

        GameObject temp_1 = GameObject.Find("wheel_01");
        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }

        GameObject temp_2 = GameObject.Find("tileSpawn");
        if (temp_2 != null) { spawnTile = temp_2.GetComponent<SpawnTile>(); }

        GameObject temp_3 = GameObject.Find("vault");
        if (temp_3 != null) { hideTileReward = temp_3.GetComponent<HideTileReward>(); }

        GameObject temp_4 = GameObject.Find("match_01");
        if (temp_4 != null) { wheelLogic = temp_4.GetComponent<WheelLogic>(); }

	}
	
	// Update is called once per frame
	void Update () 
    {

	}


    // TODO: change the names to exactly match the prefab images!! 
    public void returnReward(string reward)
    {
        switch(reward)
        {
            case "freezeTimer":
                Debug.Log("You got all freezeTimer: Slowing down the timer!");
                circularTimer.increaseFillTime = true;
                break;

            case "shapeReduction":
                Debug.Log("You got all shapeReduction: Reducing tile shapes!");
                spawnTile.reduceTileShape = true;
                break;

            case "doublePoints":
                Debug.Log("You got all doublePoints: Multiplying the score by x!");
                wheelLogic.increaseMultiplier();
                break;

            case "increaseLife":
                Debug.Log("You got all increaseLife: Increasing the slider!");
                lifeSlider.increaseSlider();
                break;

            case "decreaseLife":
                Debug.Log("You got all decreaseLife: Decreasing the life slider!");
                lifeSlider.decreaseSlider();
                break;

            case "decreaseRotation":
                Debug.Log("You got all decreaseRotation: Decreasing wheel rotation speed!");
                wheelRotation.slowRotationReward();
                break;

            case "hideTiles":
				Debug.Log("You got all hideTiles: Hiding the upcoming!");
                hideTileReward.hideTile();
                break;

            case "noMatch":
                noMatch();
                break;

            case "twoMatch":
                twoMatch();
                break;
        }
    }

    public void noMatch()
    {
        Debug.Log("You have zero matches!");
    }

    public void twoMatch()
    {
        Debug.Log("You have 2 matches- no reward!");
    }
}
