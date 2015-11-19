using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBholder : MonoBehaviour 
{

	List<string> perms = new List<string>(){"public_profile", "email", "user_friends"}; 

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
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
}















