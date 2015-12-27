using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;

public class StoreCurrencyText : MonoBehaviour {

    private string stringCurrency = "x0";
    private Text currencyText;

	// Use this for initialization
	void Start () 
    {
        currencyText = gameObject.GetComponent<Text>();
        stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
        currencyText.text = stringCurrency;	
	}
	
	// Update is called once per frame
	void Update () 
    {
        stringCurrency = "x" + StoreInventory.GetItemBalance(MayhemStoreAssets.MAYHEM_CURRENCY_ITEM_ID);
        currencyText.text = stringCurrency;
	}
}
