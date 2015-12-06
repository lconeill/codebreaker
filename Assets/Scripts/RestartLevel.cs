using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class RestartLevel : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
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
		Application.LoadLevel("gameScreen");
		ShowAd();
	}
}
