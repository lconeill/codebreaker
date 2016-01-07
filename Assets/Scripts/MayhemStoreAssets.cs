using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class MayhemStoreAssets : IStoreAssets {

    /// You need to bump the version after ANY change in <c>IStoreAssets</c> for the local
    /// database to realize it needs to refresh its data.
    /// </summary>
    /// <returns>the version of your specific <c>IStoreAssets</c>.</returns>
    public int GetVersion()
    {
        return 2;
    }

    /// <summary>
    /// Retrieves the array of your game's virtual currencies.
    /// </summary>
    /// <returns>All virtual currencies in your game.</returns>
    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { MAYHEM_CURRENCY };
    }

    /// <summary>
    /// Retrieves the array of all virtual goods served by your store (all kinds in one array).
    /// </summary>
    /// <returns>All virtual goods in your game.</returns>
    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] { SLOW_TIMER_PACK, INCREASE_SLIDER_PACK, REDUCE_SHAPE_PACK, DOUBLE_POINT_PACK, 
                                   NO_ADS_LTVG, WINTER_THEME, SLOW_TIMER, INCREASE_SLIDER, REDUCE_SHAPE, DOUBLE_POINT };
    }

    /// <summary>
    /// Retrieves the array of all virtual currency packs served by your store.
    /// </summary>
    /// <returns>All virtual currency packs in your game.</returns>
    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { };
    }

    /// <summary>
    /// Retrieves the array of all virtual categories handled in your store.
    /// </summary>
    /// <returns>All virtual categories in your game.</returns>
    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] { GENERAL_CATEGORY };
    }


    /** Static Final Members **/

    public const string MAYHEM_CURRENCY_ITEM_ID = "mayhem_coins";

    public const string SLOW_TIMER_ITEM_ID = "slow_timer";
    public const string SLOW_TIMER_FIVE_PACK_PRODUCT_ID = "slow_timer_5_pack";

    public const string INCREASE_SLIDER_ITEM_ID = "increase_slider";
    public const string INCREASE_SLIDER_FIVE_PACK_PRODUCT_ID = "increase_slider_5_pack";

    public const string REDUCE_SHAPE_ITEM_ID = "reduce_shape";
    public const string REDUCE_SHAPE_FIVE_PACK_PRODUCT_ID = "reduce_shape_5_pack";

    public const string DOUBLE_POINT_ITEM_ID = "double_point";
    public const string DOUBLE_POINT_FIVE_PACK_PRODUCT_ID = "double_point_5_pack";

    public const string NO_ADS_LIFETIME_PRODUCT_ID = "match_mayhem_no_ads";

    public const string WINTER_THEME_LIFETIME_PRODUCT_ID = "match_mayhem_winter_theme";

    /** Virtual Currencies **/

    public static VirtualCurrency MAYHEM_CURRENCY = new VirtualCurrency(
                "Coins",										// name
                "Mayhem Currency",								// description
                MAYHEM_CURRENCY_ITEM_ID							// item id
    );


    /** Power-ups packs purchased with real money **/

    // Slow time power-up pack
    public static VirtualGood SLOW_TIMER_PACK = new SingleUsePackVG(
                SLOW_TIMER_ITEM_ID,                                             // Item ID of associated good
                10,                                                             // Amount of goods in pack
                "Slow Timer 5 Pack",                                            // Name
                "A 5 pack of the slow timer power-up",                          // Description
                SLOW_TIMER_FIVE_PACK_PRODUCT_ID,                                // Product ID (name in google dev console)
                new PurchaseWithMarket(SLOW_TIMER_FIVE_PACK_PRODUCT_ID, 0.99)   // Purchase type
    ); 

    // Increase life slider power-up pack
    public static VirtualGood INCREASE_SLIDER_PACK = new SingleUsePackVG(
                INCREASE_SLIDER_ITEM_ID,
                10,
                "Increase Slider 5 Pack",
                "A 5 pack of the life slider power-up",
                INCREASE_SLIDER_FIVE_PACK_PRODUCT_ID,
                new PurchaseWithMarket(INCREASE_SLIDER_FIVE_PACK_PRODUCT_ID, 0.99)
    ); 

    // Reduce tile shape power-up pack
    public static VirtualGood REDUCE_SHAPE_PACK = new SingleUsePackVG(
               REDUCE_SHAPE_ITEM_ID,
               10,
               "Reduce Shape 5 Pack",
               "A 5 pack of the reduce shapes power-up",
               REDUCE_SHAPE_FIVE_PACK_PRODUCT_ID,
               new PurchaseWithMarket(REDUCE_SHAPE_FIVE_PACK_PRODUCT_ID, 0.99)
    );

    // Double points power-up pack
    public static VirtualGood DOUBLE_POINT_PACK = new SingleUsePackVG(
               DOUBLE_POINT_ITEM_ID,
               10,
               "Double Points 5 Pack",
               "A 5 pack of the double points power-up",
               DOUBLE_POINT_FIVE_PACK_PRODUCT_ID,
               new PurchaseWithMarket(DOUBLE_POINT_FIVE_PACK_PRODUCT_ID, 0.99)
    );


    /** Single Use power-ups purchased with virtual currency (coins) **/

    public static VirtualGood SLOW_TIMER = new SingleUseVG(
        "Slow Timer",                                                   // Name
        "Slow the timer down, getting more time to make matches!",      // Description
        SLOW_TIMER_ITEM_ID,                                             // Item ID
        new PurchaseWithVirtualItem(MAYHEM_CURRENCY_ITEM_ID, 5000)      // Purchase type
    );

    public static VirtualGood INCREASE_SLIDER = new SingleUseVG(
        "Increase Slider",                                                  
        "Increases the life slider, giving more chances to make matches!",
        INCREASE_SLIDER_ITEM_ID,                                             
        new PurchaseWithVirtualItem(MAYHEM_CURRENCY_ITEM_ID, 5000)
    );

    public static VirtualGood REDUCE_SHAPE = new SingleUseVG(
        "Reduce Shapes",
        "Only two shapes will show up for 10 seconds!",
        REDUCE_SHAPE_ITEM_ID,
        new PurchaseWithVirtualItem(MAYHEM_CURRENCY_ITEM_ID, 5000)
    );

    public static VirtualGood DOUBLE_POINT = new SingleUseVG(
        "Double Points",
        "All matches and match streaks are worth double points for 10 seconds!",
        DOUBLE_POINT_ITEM_ID,
        new PurchaseWithVirtualItem(MAYHEM_CURRENCY_ITEM_ID, 5000)
    );


    /** LifeTimeVGs - can only be purchased once and lasts forever **/

    // Remove all ads from the game
    public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
		"No Ads", 														// name
		"No More Ads!",				 									// description
        NO_ADS_LIFETIME_PRODUCT_ID,										// item id
		new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 1.99)	    // the way this virtual good is purchased
    );

    public static VirtualGood WINTER_THEME = new LifetimeVG(
        "Winter Theme", 														// name
        "Winter inspired theme featuring brand new tiles, menus, background, particle effects, and many other changes!",  // description
        WINTER_THEME_LIFETIME_PRODUCT_ID,										// item id
        new PurchaseWithMarket(WINTER_THEME_LIFETIME_PRODUCT_ID, 2.99)	    // the way this virtual good is purchased
        );


    /** Virtual Categories **/

    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
        "General",
        new List<string>(new string[] { SLOW_TIMER_ITEM_ID, INCREASE_SLIDER_ITEM_ID, REDUCE_SHAPE_ITEM_ID, DOUBLE_POINT_ITEM_ID })
    );

}