using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

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
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	
	public void LoadMainMenu()
	{
		
		Destroy(UI_ref);
		
		Application.LoadLevel("startScreen");
		
		if(PlayerPrefs.GetInt("Player Deaths") == 1)
		{
			ShowAd();
		}
		
		
	}
}
