using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using Soomla.Store;

public class ReturnToMenu : MonoBehaviour 
{
	// This Function will load the main menu
	// screen and hide the Gameover UI
	public GameObject UI_ref;
	//private StartOptions start_options;
	
	void Start()
	{
		UI_ref = GameObject.Find("UI");
	}
	
	public void ShowAd()
	{

        string remove_ad_id = MayhemStoreAssets.NO_ADS_LIFETIME_PRODUCT_ID;

        if (StoreInventory.GetItemBalance(remove_ad_id) == 0)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
	}
	
	public void LoadMainMenu()
	{
		
		Destroy(UI_ref);
		
		Application.LoadLevel("startScreen");
		
		if(PlayerPrefs.GetInt("Player Deaths") == 1)
		{
			PlayerPrefs.SetInt("Player Deaths", 0);
			ShowAd();
		}
		
		
	}
}
