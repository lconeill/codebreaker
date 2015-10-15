using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

    public CircularTimer circularTimer;
    private LifeSlider lifeSlider;
    private WheelRotation wheelRotation;
    private SpawnTile spawnTile;

	// Use this for initialization
	void Start () 
    {
        GameObject temp = GameObject.Find("lifeSlider");
        if (temp != null) { lifeSlider = temp.GetComponent<LifeSlider>(); }

        GameObject temp_1 = GameObject.Find("wheel_01");
        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }

        GameObject temp_2 = GameObject.Find("tileSpawn");
        if (temp != null) { spawnTile = temp_2.GetComponent<SpawnTile>(); }
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void returnReward(string reward)
    {
        switch(reward)
        {
            case "Bell":
                Debug.Log("You got all bells: Slowing down the timer!");
                circularTimer.increaseFillTime = true;
                break;

            case "Fruit Gum":
                Debug.Log("You got all fruit gums: Reducing tile shapes!");
                spawnTile.reduceTileShape = true;
                break;

            case "Lemon":
                reduceTileVariation();
                break;

            case "Cherry1":
                Debug.Log("You got all Cherry1's: Increasing the slider!");
                lifeSlider.increaseSlider();
                break;

            case "Grape":
                Debug.Log("You got all Grapes: Decreasing the life slider!");
                lifeSlider.decreaseSlider();
                break;

            case "Orange":
                Debug.Log("You got all Oranges: Decreasing wheel rotation speed!");
                wheelRotation.slowRotationReward();
                break;

            case "Cherry2":
                emptyTileScroller();
                break;

            case "noMatch":
                noMatch();
                break;

            case "twoMatch":
                twoMatch();
                break;
        }
    }

    public void reduceTileShapes()
    {
        Debug.Log("Selected all Fruit Gums, so there should only be two different tiles");
    }

    public void reduceTileVariation()
    {
        Debug.Log("Selected all Lemons, so the chances of getting the same tile increases by 50%");
    }

    public void decreaseFillTime()
    {
        Debug.Log("Selected all Grapes, so decease the fill time by 1.3");
    }

    public void emptyTileScroller()
    {
        Debug.Log("Selected all Oranges, so disable the tile scroller visibility");
    }

    public void removeDifficultyVariable()
    {
        Debug.Log("Selected all Cherry2s, so remove a random current difficulty variable");
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
