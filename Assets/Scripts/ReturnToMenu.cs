using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour 
{
	// This Function will load the main menu
	// screen and hide the Gameover UI
	
	public GameObject UI_ref;
	
	void Start()
	{

	}
	
	public void LoadMainMenu()
	{
		Application.LoadLevel("startScreen");
		
		Time.timeScale = 1;	
		Destroy(UI_ref);

	}
}
