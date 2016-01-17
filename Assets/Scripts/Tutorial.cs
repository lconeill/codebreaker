using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject[] tutorial_pages;
    public Button next_button;
    public Button previous_button;
    private bool pause = false;


	// Use this for initialization
	void Start () 
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            Debug.Log("Player is viewing tutorial on the start screen for the first time.");
        }
	}
	

	// Update is called once per frame
	void Update () 
    {
        if (pause)
        {
            Time.timeScale = 0;
            pause = false;
            Debug.Log("Pause the game screen while tutorial is on!");
            Debug.Log(Application.loadedLevelName);
        }
	}


    // When user clicks the exit button on the tutorial screen
    public void deactivateTutorial()
    {
        gameObject.SetActive(false);

        if (Application.loadedLevel == ThemeManager.CLASSIC_THEME || Application.loadedLevel == ThemeManager.WINTER_THEME)
        {
            Time.timeScale = 1;
        }

    }

    // Activates the tutorial for the first time the user plays
    public void activateTutorial()
    {
        // Playing for first time, set pref
        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            Debug.Log("Player has already played before. Don't activate tutorial.");
        }

        else
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            gameObject.SetActive(true);
            pause = true;
            Debug.Log("Player has never played before. Activate tutorial.");
            
        }
    }


    // Displays the next tutorial screen while not on the last page
    public void nextLogic()
    {
        int index;
        previous_button.gameObject.SetActive(true);

        for (index = 0; index < tutorial_pages.Length; index++)
        {
            if (tutorial_pages[index].activeSelf) 
            {
                if (index == tutorial_pages.Length - 2)
                {
                    next_button.gameObject.SetActive(false);
                }

                tutorial_pages[index].SetActive(false);
                tutorial_pages[index + 1].SetActive(true);
                break;
            }
        }
    }


    // Displays previous tutorial screen while not on the first page
    public void previousLogic()
    {
        int index;
        next_button.gameObject.SetActive(true);

        for (index = 0; index < tutorial_pages.Length; index++)
        {
            if (tutorial_pages[index].activeSelf)
            {
                if (index == 1)
                {
                    previous_button.gameObject.SetActive(false);
                }

                tutorial_pages[index].SetActive(false);
                tutorial_pages[index - 1].SetActive(true);
                break;
            }
        }
    }
}
