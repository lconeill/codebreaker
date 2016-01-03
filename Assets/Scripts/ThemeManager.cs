using UnityEngine;
using System.Collections;
using Soomla.Store;

public class ThemeManager : MonoBehaviour {

    private const int CLASSIC_THEME = 2;
    private const int WINTER_THEME = 3;

    public GameObject store_panel;

    private MayhemStoreManager store_manager; 

    //private static int theme_to_load;


	// Use this for initialization
	void Start () 
    {
        store_manager = store_panel.GetComponent<MayhemStoreManager>();

        if (PlayerPrefs.GetInt("Theme") == 0)
        {
            PlayerPrefs.SetInt("Theme", 2);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void selectTheme(string theme)
    {
        switch (theme)
        {
            case "classic":
                PlayerPrefs.SetInt("Theme", CLASSIC_THEME);
                Debug.Log("Classic theme selected.Debug Level to load: " + PlayerPrefs.GetInt("Theme"));
                break;

            case "winter":

                string winter_theme_id = MayhemStoreAssets.WINTER_THEME_LIFETIME_PRODUCT_ID;

                // If user has purchased the winter theme then set it to load
                if (StoreInventory.GetItemBalance(winter_theme_id) > 0)
                {
                    PlayerPrefs.SetInt("Theme", WINTER_THEME);
                    Debug.Log("Winter theme selected.Debug Level to load: " + PlayerPrefs.GetInt("Theme"));
                }
                
                // Take user to google play/app store purchase screen
                else
                {
                    store_manager.buyWinterTheme();
                }

                break;
        }
    }
}
