using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;

public class Store_Init : MonoBehaviour 
{
	public GameObject image_ref;
	private Animator image_anim;
	private Animation fadeColorAnimationClip;
	
	// Use this for initialization
	void Start () 
	{
		//image_anim = image_ref.GetComponent<Animator>();
		
		//fadeColorAnimationClip = image_ref.GetComponent<Animation>();
		
		//fadeColorAnimationClip.Play ("FadeToColor");
		
		SoomlaStore.Initialize(new MayhemStoreAssets());

        // Determine if user is playing for the first time or not
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            giveFreeCurrency();
        }
		
		//AdBuddizBinding.SetTestModeActive();
		
		AdBuddizBinding.SetAndroidPublisherKey("3a0d8f7a-9823-41ec-8b62-cfc7c02d53ef");
        AdBuddizBinding.SetIOSPublisherKey("6ead862d-ed50-402e-ad2e-7ce8931e128a");
		
		AdBuddizBinding.CacheAds();
		
		Invoke("LoadDelayed", 8.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


    // Give the user 5 free power-up if first time playing
    private void giveFreeCurrency()
    {
        //string currency_ID = MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID;
        //StoreInventory.GiveItem(currency_ID, 100000);

        //Debug.Log(">>>>>>User gets 100000 coins");

        string freeze_itemID = MayhemStoreAssets.SLOW_TIMER_ITEM_ID;
        string reduce_itemID = MayhemStoreAssets.REDUCE_SHAPE_ITEM_ID;
        string double_itemID = MayhemStoreAssets.DOUBLE_POINT_ITEM_ID;
        string slider_itemID = MayhemStoreAssets.INCREASE_SLIDER_ITEM_ID;

        StoreInventory.GiveItem(freeze_itemID, 5);
        StoreInventory.GiveItem(reduce_itemID, 5);
        StoreInventory.GiveItem(double_itemID, 5);
        StoreInventory.GiveItem(slider_itemID, 5);
    }

	
	public void LoadDelayed()
	{
		//Load the selected scene, by scene index number in build settings

        if (PlayerPrefs.GetInt("Theme") == 0)
        {
            Application.LoadLevel(1); // Load the classic start screen
            Debug.Log("Classic theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }
        
        else if (PlayerPrefs.GetInt("Theme") == ThemeManager.WINTER_THEME)
        {
            Application.LoadLevel(4); // Load the winter start screen
            Debug.Log("Winter theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }

        else
        {
            Application.LoadLevel(1); // Load the classic start screen
            Debug.Log("Classic theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }
	}
}
