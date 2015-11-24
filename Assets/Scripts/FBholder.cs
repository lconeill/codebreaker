using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;

public class FBholder : MonoBehaviour 
{

	List<string> perms = new List<string>(){"public_profile", "email", "user_friends", "publish_actions"}; 

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
	}
	
	public void PostToFacebook()
	{
		
		FB.ShareLink(new System.Uri("https://developers.facebook.com/929788493776530"),
		             "MATCH MAYHEM!",
		             "I just got a new High Score in Match Mayhem!",
		             null,
		             ShareCallback);
		
		Debug.Log ("Posted To Facebook!!");
	}
	
	private void SetInit()
	{
		Debug.Log ("FB Init done.");
		
		if(FB.IsLoggedIn)
		{
			Debug.Log("FB Logged In");
		}
		
		else
		{
			 
		}
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		
		else
		{
			Time.timeScale = 1;
		}
	}
	
	public void FBLogin()
	{
		//FB.LogInWithReadPermissions ("user_about_me, user_birthday", AuthCallback);
		FB.LogInWithReadPermissions (perms, AuthCallback);
	}
	
	void AuthCallback(ILoginResult  result)
	{
		if(FB.IsLoggedIn)
		{
			Debug.Log ("FB Login in worked");
		}
		
		else
		{
			Debug.Log ("FB Login failed");
		}
	}
	
	private void ShareCallback (IShareResult result) 
	{
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) 
		{
			Debug.Log("ShareLink Error: "+result.Error);
		} 
		
		else if (!string.IsNullOrEmpty(result.PostId)) 
		{
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} 
		
		else 
		{
			// Share succeeded without postID 
			Debug.Log("ShareLink success!");
		}
		
	}

}