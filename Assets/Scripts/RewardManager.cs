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
    private DoublePoints double_points_script;

	private GameObject hide_sfx_ref;
	private AudioSource hide_sfx;
	
	private GameObject reduce_sfx_ref;
	private AudioSource reduce_sfx;
	
	private GameObject increase_life_sfx_ref;
	private AudioSource increase_life_sfx;
	
	private GameObject decrease_life_sfx_ref;
	private AudioSource decrease_life_sfx;
	
	private GameObject double_points_sfx_ref;
	private AudioSource double_points_sfx;

	private GameObject fire_sfx_ref;
	private AudioSource fire_sfx;
    
    public bool from_slot_game = false;



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

        GameObject temp_5 = GameObject.Find("double_points_script");
        if (temp_5 != null) { double_points_script = temp_5.GetComponent<DoublePoints>(); }
        
		hide_sfx_ref = GameObject.Find("Hide_SFX_01");
		hide_sfx = hide_sfx_ref.GetComponent<AudioSource>();
		
		reduce_sfx_ref = GameObject.Find("ReduceShape_SFX_01");
		reduce_sfx = reduce_sfx_ref.GetComponent<AudioSource>();
		
		increase_life_sfx_ref = GameObject.Find("IncreaseLife_SFX_01");
		increase_life_sfx = increase_life_sfx_ref.GetComponent<AudioSource>();
		
		decrease_life_sfx_ref = GameObject.Find("DecreaseLife_SFX_01");
		decrease_life_sfx = decrease_life_sfx_ref.GetComponent<AudioSource>();
		
		double_points_sfx_ref = GameObject.Find("DoublePoints_SFX_01");
		double_points_sfx = double_points_sfx_ref.GetComponent<AudioSource>();
		
		fire_sfx_ref = GameObject.Find("Fire_SFX_01");
		fire_sfx = fire_sfx_ref.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}


    // TODO: add powerup manager change balance call to decrease rotation rewards
    public void returnReward(string reward)
    {
        switch(reward)
        {
            case "freezeTimer":
                Debug.Log("You got all freezeTimer: Slowing down the timer!");

                string freeze_itemID = MayhemStoreAssets.SLOW_TIMER_ITEM_ID;

                // If the reward is from slot game, give the item for free
                if (from_slot_game)
                {
                    // This wasn't refreshing the balance fast enough to activate the reward when inventory is zero
                    // StoreInventory.GiveItem(freeze_itemID, 1);
                    circularTimer.increaseFillTime = true;
                    from_slot_game = false;
                }

                //if (StoreInventory.GetItemBalance(freeze_itemID) > 0)
                else if (StoreInventory.GetItemBalance(freeze_itemID) > 0)
                {
                    circularTimer.increaseFillTime = true;
                    StoreInventory.TakeItem(freeze_itemID, 1);
                }

                //StoreInventory.TakeItem(freeze_itemID, 1);
                PowerUpManager.changeBalanceText(freeze_itemID);
                
                break;


            case "shapeReduction":
                Debug.Log("You got all shapeReduction: Reducing tile shapes!");

                string reduce_itemID = MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID;

                // If the reward is from slot game, give the item for free
                if (from_slot_game)
                {
                    // This wasn't refreshing the balance fast enough to activate the reward when inventory is zero
                    //StoreInventory.GiveItem(reduce_itemID, 1);
                    spawnTile.reduceTileShape = true;
                    from_slot_game = false;
                }

                //if (StoreInventory.GetItemBalance(reduce_itemID) > 0)
                else if (StoreInventory.GetItemBalance(reduce_itemID) > 0)
                {
                    spawnTile.reduceTileShape = true;
                    StoreInventory.TakeItem(reduce_itemID, 1);
                }

                //StoreInventory.TakeItem(reduce_itemID, 1);
                PowerUpManager.changeBalanceText(reduce_itemID);
                
                reduce_sfx.Play();
                
                break;


            case "doublePoints":
                Debug.Log("You got all doublePoints: Multiplying the score by 2!");

                string double_itemID = MayhemStoreAssets.DOUBLE_POINT_ITEM_ID;

                // If the reward is from slot game, give the item for free
                if (from_slot_game)
                {
                    // This wasn't refreshing the balance fast enough to activate the reward when inventory is zero
                    //StoreInventory.GiveItem(double_itemID, 1);
                    double_points_script.activateReward();
                    from_slot_game = false;
                }

                //if (StoreInventory.GetItemBalance(double_itemID) > 0)
                else if (StoreInventory.GetItemBalance(double_itemID) > 0)
                {
                    //wheelLogic.increaseMultiplier();
                    double_points_script.activateReward();
                    StoreInventory.TakeItem(double_itemID, 1);
                }

                //StoreInventory.TakeItem(double_itemID, 1);
                PowerUpManager.changeBalanceText(double_itemID);
				double_points_sfx.Play();
                //wheelLogic.increaseMultiplier();
                break;


            case "increaseLife":

                Debug.Log("You got all increaseLife: Increasing the slider!");

                string slider_itemID = MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID;

                // If the reward is from slot game, give the item for free
                if (from_slot_game)
                {
                    // This wasn't refreshing the balance fast enough to activate the reward when inventory is zero
                    //StoreInventory.GiveItem(slider_itemID, 1);
                    lifeSlider.increaseSlider();
                    from_slot_game = false;
                }

                //if (StoreInventory.GetItemBalance(slider_itemID) > 0)
                else if (StoreInventory.GetItemBalance(slider_itemID) > 0)
                {
                    lifeSlider.increaseSlider();
                    StoreInventory.TakeItem(slider_itemID, 1);
                }

                //StoreInventory.TakeItem(slider_itemID, 1);
                PowerUpManager.changeBalanceText(slider_itemID);
                
                increase_life_sfx.Play();

                break;


            case "decreaseLife":
                Debug.Log("You got all decreaseLife: Decreasing the life slider!");
                lifeSlider.decreaseSlider();
                decrease_life_sfx.Play();
                break;

            case "decreaseRotation":
                Debug.Log("You got all decreaseRotation: Decreasing wheel rotation speed!");
                fire_sfx.Play();
                wheelRotation.slowRotationReward();
                break;

            case "hideTiles":
                Debug.Log("You got all hideTiles: Hiding the upcoming!");
                hide_sfx.Play();
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
