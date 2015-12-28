using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

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
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	
	public void Restart()
	{
		death_count = death_count + 1;
		
		PlayerPrefs.SetInt("Player Deaths", death_count);
		
		if(PlayerPrefs.GetInt("Player Deaths") == 2)
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
