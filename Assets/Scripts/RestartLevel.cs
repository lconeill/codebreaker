using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using Soomla.Store;

public class RestartLevel : MonoBehaviour 
{
	private int death_count = 0;
    private int gamescreen_theme_to_load;
	
	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.GetInt("Player Deaths") == 0)
		{
			PlayerPrefs.SetInt("Player Deaths", 0);
		}

        gamescreen_theme_to_load = PlayerPrefs.GetInt("Theme");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
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
	
	public void Restart()
	{
		death_count = death_count + 1;
		
		PlayerPrefs.SetInt("Player Deaths", death_count);
		
		if(PlayerPrefs.GetInt("Player Deaths") == 4)
		{
			Debug.Log("This is death number: " + death_count);
			AdBuddizBinding.ShowAd();
		}
		
		if(PlayerPrefs.GetInt("Player Deaths") == 6)
		{
			Debug.Log("This is death number: " + death_count);
			ShowAd();
			
			death_count = 0;
			
			PlayerPrefs.SetInt("Player Deaths", death_count);
		}
		
        //Application.LoadLevel("gameScreen");
        Application.LoadLevel(gamescreen_theme_to_load);
	}
}
