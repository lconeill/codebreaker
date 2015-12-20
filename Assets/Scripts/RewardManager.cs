using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;

public class RewardManager : MonoBehaviour {

    public CircularTimer circularTimer;
    private LifeSlider lifeSlider;
    private WheelRotation wheelRotation;
    private SpawnTile spawnTile;
    public GameObject vault;
    private WheelLogic wheelLogic;
    private HideTileReward hide_the_tiles;


	// Use this for initialization
	void Start () 
    {
        GameObject temp = GameObject.Find("lifeSlider");
        if (temp != null) { lifeSlider = temp.GetComponent<LifeSlider>(); }

        GameObject temp_1 = GameObject.Find("wheel_01");
        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }

        GameObject temp_2 = GameObject.Find("tileSpawn");
        if (temp_2 != null) { spawnTile = temp_2.GetComponent<SpawnTile>(); }

        hide_the_tiles = vault.GetComponent<HideTileReward>();

        GameObject temp_4 = GameObject.Find("match_01");
        if (temp_4 != null) { wheelLogic = temp_4.GetComponent<WheelLogic>(); }
	}
	
	// Update is called once per frame
	void Update () 
    {

	}


    // TODO: add powerup manager change balance call to decrease rotation rewards
    // TODO: give this method a bool argument that is false when called from the button and true when called from the slot game
    public void returnReward(string reward)
    {
        switch(reward)
        {
            case "freezeTimer":
                Debug.Log("You got all freezeTimer: Slowing down the timer!");

                string freeze_itemID = MayhemStoreAssets.SLOW_TIMER_ITEM_ID;

                if (StoreInventory.GetItemBalance(freeze_itemID) > 0)
                {
                    circularTimer.increaseFillTime = true;
                }

                StoreInventory.TakeItem(freeze_itemID, 1);
                PowerUpManager.changeBalanceText(freeze_itemID);
                
                break;

            case "shapeReduction":
                Debug.Log("You got all shapeReduction: Reducing tile shapes!");

                string reduce_itemID = MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID;

                if (StoreInventory.GetItemBalance(reduce_itemID) > 0)
                {
                    spawnTile.reduceTileShape = true;
                }

                StoreInventory.TakeItem(reduce_itemID, 1);
                PowerUpManager.changeBalanceText(reduce_itemID);
                
                break;

            case "doublePoints":
                Debug.Log("You got all doublePoints: Multiplying the score by x!");

                string double_itemID = MayhemStoreAssets.DOUBLE_POINT_ITEM_ID;

                if (StoreInventory.GetItemBalance(double_itemID) > 0)
                {
                    wheelLogic.increaseMultiplier();
                }

                StoreInventory.TakeItem(double_itemID, 1);
                PowerUpManager.changeBalanceText(double_itemID);

                wheelLogic.increaseMultiplier();
                break;

            case "increaseLife":

                Debug.Log("You got all increaseLife: Increasing the slider!");

                string slider_itemID = MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID;

                if (StoreInventory.GetItemBalance(slider_itemID) > 0)
                {
                    lifeSlider.increaseSlider();
                }

                StoreInventory.TakeItem(slider_itemID, 1);
                PowerUpManager.changeBalanceText(slider_itemID);

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
                spawnTile.hideTiles();
                //hide_the_tiles.hideTile();
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
