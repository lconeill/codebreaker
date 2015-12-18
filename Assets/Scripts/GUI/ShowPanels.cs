using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject game_over_panel;
	public GameObject medal_ref;
	
	private ShowMedals medal_script_ref;

    public static bool in_menu;
	
	void Start()
	{
		medal_script_ref = medal_ref.GetComponent<ShowMedals>();
	}

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
        in_menu = true;
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
        in_menu = false;
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
        in_menu = true;
	}
	
	// Call this function when the player loses 
	// and show the Gameover UI.
	
	public void ShowGameOver()
	{
		game_over_panel.SetActive (true);
		medal_script_ref.ShowEarnedMedal();
        in_menu = true;
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
        in_menu = false;
	}
	
	// Call this function to hide the
	// Game over UI
	
	public void HideGameOver()
	{
		game_over_panel.SetActive (false);
        in_menu = false;
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
        in_menu = true;
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);
        in_menu = false;
	}
}
