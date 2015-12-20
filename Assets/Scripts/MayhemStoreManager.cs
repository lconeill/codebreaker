using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;

public class MayhemStoreManager : MonoBehaviour {

    // TODO: MAKE SURE TO RESET ITEM BALANCES TO ZERO BEFORE PUBLISH
    // TODO: PUT THIS CLASS ON A GAME OBJECT THAT IS ACTIVE AND ONLY LOADS ONCE (SPLASH SCREEN)

	// Use this for initialization
	void Start () 
    {
        // TODO: Comment the below initialization out
        SoomlaStore.Initialize(new MayhemStoreAssets());
        StoreEvents.OnItemPurchased += onItemPurchased;
        DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update () 
    {
        
	}

    //void onEnable()
    //{
    //    Time.timeScale = 0;
    //}


    public void onItemPurchased(PurchasableVirtualItem pvi, string payload)
    {
        PowerUpManager.changeBalanceText(pvi.ItemId);
    }

    public void buySlowTimerPack()
    {
        StoreInventory.BuyItem(MayhemStoreAssets.SLOW_TIMER_FIVE_PACK_PRODUCT_ID);   
    }

    public void buySlowTimer()
    {
        bool can_afford = StoreInventory.CanAfford(MayhemStoreAssets.SLOW_TIMER_ITEM_ID);

        if (can_afford)
        {
            StoreInventory.BuyItem(MayhemStoreAssets.SLOW_TIMER_ITEM_ID);
        }

        else
        {
            Debug.Log("You don't have enough coins to afford slow timer item!");
        }
    }

    public void buyIncreaseSlider()
    {
        bool can_afford = StoreInventory.CanAfford(MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID);

        if (can_afford)
        {
            StoreInventory.BuyItem(MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID);
        }

        else
        {
            Debug.Log("You don't have enough coins to afford increase slider item!");
        }

    }

    public void buyReduceShape()
    {
        bool can_afford = StoreInventory.CanAfford(MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID);

        if (can_afford)
        {
            StoreInventory.BuyItem(MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID);
        }

        else
        {
            Debug.Log("You don't have enough coins to afford reduce shape item!");
        }

    }

    public void buyDoublePoint()
    {
        bool can_afford = StoreInventory.CanAfford(MayhemStoreAssets.DOUBLE_POINT_ITEM_ID);

        if (can_afford)
        {
            StoreInventory.BuyItem(MayhemStoreAssets.DOUBLE_POINT_ITEM_ID);
        }

        else
        {
            Debug.Log("You don't have enough coins to afford double points item!");
        }

    }
}
//Soomla secret: X5rl73jZ8xD86GEabD7Lh3Dm4LBeQG7v
//Keystore pwd: t@R3p3D&uO