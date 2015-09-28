using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

    public CircularTimer circularTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void returnReward(string reward)
    {
        switch(reward)
        {
            case "Bell":
                increaseFillTime();
                break;

            case "Fruit Gum":
                reduceTileShapes();
                break;

            case "Lemon":
                reduceTileVariation();
                break;

            case "Cherry1":
                increaseSlider();
                break;

            case "Grape":
                decreaseFillTime();
                break;

            case "Orange":
                emptyTileScroller();
                break;

            case "Cherry2":
                removeDifficultyVariable();
                break;

            case "noMatch":
                noMatch();
                break;

            case "twoMatch":
                twoMatch();
                break;
        }
    }

    public void increaseFillTime()
    {
        Debug.Log("Selected all Bells, so increase the fill time by 1/2");
        circularTimer.fillSpeed = 20;
    }

    public void reduceTileShapes()
    {
        Debug.Log("Selected all Fruit Gums, so there should only be two different tiles");
    }

    public void reduceTileVariation()
    {
        Debug.Log("Selected all Lemons, so the chances of getting the same tile increases by 50%");
    }

    public void increaseSlider()
    {
        Debug.Log("Selected all Cherry1s, so increase the life slider by x amount");
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
