using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject[] tutorial_pages;
    public Button next_button;
    public Button previous_button;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
	
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
