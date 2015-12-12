using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class RestartLevel : MonoBehaviour 
{
	private int death_count = 0;
	
	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.GetInt("Player Deaths") == 0)
		{
			PlayerPrefs.SetInt("Player Deaths", 0);
		}
	
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
		
		if(PlayerPrefs.GetInt("Player Deaths") == 3)
		{
			Debug.Log("This is death number: " + death_count);
			ShowAd();
			
			death_count = 0;
			
			PlayerPrefs.SetInt("Player Deaths", death_count);
		}
		
		Application.LoadLevel("gameScreen");
	}
}
