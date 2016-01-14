using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class Currency : MonoBehaviour {

    private int previousCorrectMatches;
    private int previousMatchStreak;

    private ScoreLogic scoreLogic;
    private WheelRotation wheelRotation; 

    private int currency;
    private string stringCurrency = "x0";
    private Text currencyText;

    public GameObject coin;
    private GameObject coin_sfx_ref;
    private AudioSource coin_sfx;

	// Use this for initialization
	void Start () 
    {

        GameObject temp = GameObject.Find("score_display");
        GameObject temp_1 = GameObject.Find("wheel_01");
        
		coin_sfx_ref = GameObject.Find("Coin_SFX_01");
		coin_sfx = coin_sfx_ref.GetComponent<AudioSource>();

        if (temp != null) { scoreLogic = temp.GetComponent<ScoreLogic>(); }

        if (scoreLogic != null)
        {
            previousMatchStreak = scoreLogic.match_streak_counter;
        }

        if (temp_1 != null) { wheelRotation = temp_1.GetComponent<WheelRotation>(); }

        if (wheelRotation != null)
        {
            previousCorrectMatches = wheelRotation.match_count;
        }

        currencyText = gameObject.GetComponent<Text>();
        stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
        currencyText.text = stringCurrency;
	}
	
	// Update is called once per frame
	void Update () 
    {

        stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
        currencyText.text = stringCurrency;

        if (wheelRotation.match_count > previousCorrectMatches)
        {
            if (wheelRotation.match_count % 5 == 0)
            {
                //currency += 50;
                
                StoreInventory.GiveItem(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID, 50);
                StartCoroutine(coinAnimation());
				coin_sfx.Play();
                //stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
                //currencyText.text = stringCurrency;
            }
        }

        if (scoreLogic.match_streak_counter > previousMatchStreak)
        {
            for (int i = 5; i < 200; i += 5)
            {
                if (scoreLogic.match_streak_counter % i == 0)
                {
                    //currency += 5 + i - 5;
                    
                    StoreInventory.GiveItem(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID, 5 + i - 5);
                    //stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
                    //currencyText.text = stringCurrency;
                    //break;
                }
            }
        }

        previousCorrectMatches = wheelRotation.match_count;
        previousMatchStreak = scoreLogic.match_streak_counter;
	}

    IEnumerator coinAnimation()
    {
        coin.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        coin.SetActive(false);
    }
}
