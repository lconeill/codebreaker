using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Soomla.Store;

public class PowerUpManager : MonoBehaviour {

    private static Button freeze_timer_button;
    private static Button increase_slider_button;
    private static Button double_point_button;
    private static Button reduce_shape_button;
    
    private static Text freeze_timer_text;
    private static Text increase_slider_text;
    private static Text double_point_text;
    private static Text reduce_shape_text;

	// Use this for initialization
	void Start () 
    {

        GameObject temp_1 = GameObject.Find("freeze_timer_power_up");
        if (temp_1 != null) { freeze_timer_button = temp_1.GetComponent<Button>(); }

        GameObject temp_2 = GameObject.Find("increase_slider_power_up");
        if (temp_2 != null) { increase_slider_button = temp_2.GetComponent<Button>(); }

        GameObject temp_3 = GameObject.Find("double_point_power_up");
        if (temp_3 != null) { double_point_button = temp_3.GetComponent<Button>(); }

        GameObject temp_4 = GameObject.Find("reduce_shape_power_up");
        if (temp_4 != null) { reduce_shape_button = temp_4.GetComponent<Button>(); }

        GameObject temp_5 = GameObject.Find("freeze_timer_text");
        if (temp_5 != null) { freeze_timer_text = temp_5.GetComponent<Text>(); }

        GameObject temp_6 = GameObject.Find("increase_slider_text");
        if (temp_6 != null) { increase_slider_text = temp_6.GetComponent<Text>(); }

        GameObject temp_7 = GameObject.Find("double_point_text");
        if (temp_7 != null) { double_point_text = temp_7.GetComponent<Text>(); }

        GameObject temp_8 = GameObject.Find("reduce_shape_text");
        if (temp_8 != null) { reduce_shape_text = temp_8.GetComponent<Text>(); }


        // Determine if user is playing for the first time or not
        //if (PlayerPrefs.GetInt("FirstTime") == 1)
        //{
        //    PlayerPrefs.SetInt("FirstTime", 2);
        //    giveFreePowerup();
        //}

        changeBalanceText("slow_timer");
        changeBalanceText("increase_slider");
        changeBalanceText("reduce_shape");
        changeBalanceText("double_point");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


     //Give the user 5 free power-up if first time playing
    //private void giveFreePowerup()
    //{
    //    string freeze_itemID = MayhemStoreAssets.SLOW_TIMER_ITEM_ID;
    //    string reduce_itemID = MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID;
    //    string double_itemID = MayhemStoreAssets.DOUBLE_POINT_ITEM_ID;
    //    string slider_itemID = MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID;

    //    for (int i = 0; i <= 4; i++)
    //    {
    //        StoreInventory.BuyItem(freeze_itemID);
    //        StoreInventory.BuyItem(reduce_itemID);
    //        StoreInventory.BuyItem(double_itemID);
    //        StoreInventory.BuyItem(slider_itemID);

    //        Debug.Log(">>>>>>User gets 1 of each power up");
    //    }

        //string freeze_itemID = MayhemStoreAssets.SLOW_TIMER_ITEM_ID;
        //string reduce_itemID = MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID;
        //string double_itemID = MayhemStoreAssets.DOUBLE_POINT_ITEM_ID;
        //string slider_itemID = MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID;

        //StoreInventory.GiveItem(freeze_itemID, 5);
        //StoreInventory.GiveItem(reduce_itemID, 5);
        //StoreInventory.GiveItem(double_itemID, 5);
        //StoreInventory.GiveItem(slider_itemID, 5);

        // Update the inventory text for the power-ups
        //setInventoryText(5, 5, 5, 5);
    //}


    // Set the power-up inventory text manually (SOOMLA onBalanceChanged doesn't work properly)
    //public static void setInventoryText(int freezeBalance, int increaseBalance, int doubleBalance, int reduceBalance)
    //{
    //    freeze_timer_text.text = freezeBalance.ToString();
    //    increase_slider_text.text = increaseBalance.ToString();
    //    double_point_text.text = doubleBalance.ToString();
    //    reduce_shape_text.text = reduceBalance.ToString();
    //}


    // Updates the inventory balance text on the gamescreen for each power up
    public static void changeBalanceText(string itemID)
    {
        if (Application.loadedLevelName == "gameScreen" || Application.loadedLevelName == "gameScreenWinter")
        {
            // If a pack is bought change the product_id to the corresponding item_id 
            switch (itemID)
            {
                case "slow_timer_5_pack":
                    itemID = "slow_timer";
                    break;

                case "increase_slider_5_pack":
                    itemID = "increase_slider";
                    break;

                case "reduce_shape_5_pack":
                    itemID = "reduce_shape";
                    break;

                case "double_point_5_pack":
                    itemID = "double_point";
                    break;
            }

            switch (itemID)
            {
                case "slow_timer":
                    int freeze_timer_balance = StoreInventory.GetItemBalance(MayhemStoreAssets.SLOW_TIMER_ITEM_ID);
                    freeze_timer_text.text = freeze_timer_balance.ToString();

                    freeze_timer_button.enabled = (freeze_timer_balance == 0) ? false : true;
                
                    //Debug.Log(StoreInventory.GetItemBalance(itemID).ToString());
                    break;

                case "increase_slider":
                    int increase_slider_balance = StoreInventory.GetItemBalance(MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID);
                    increase_slider_text.text = increase_slider_balance.ToString();

                    Debug.Log("Slider balance: " + increase_slider_balance);

                    increase_slider_button.enabled = (increase_slider_balance == 0) ? false : true;

                    //Debug.Log(StoreInventory.GetItemBalance(itemID).ToString());
                    break;

                case "reduce_shape":
                    int reduce_shape_balance = StoreInventory.GetItemBalance(MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID);
                    reduce_shape_text.text = reduce_shape_balance.ToString();

                    // TODO: This caused buttons to be enabled even though it was still in effect ()
                    reduce_shape_button.enabled = (reduce_shape_balance == 0) ? false : true;

                    //Debug.Log(StoreInventory.GetItemBalance(itemID).ToString());
                    break;

                case "double_point":
                    int double_point_balance = StoreInventory.GetItemBalance(MayhemStoreAssets.DOUBLE_POINT_ITEM_ID);
                    double_point_text.text = double_point_balance.ToString();

                    double_point_button.enabled = (double_point_balance == 0) ? false : true;

                    //Debug.Log(StoreInventory.GetItemBalance(itemID).ToString());
                    break;
            }
        }
    }
}
