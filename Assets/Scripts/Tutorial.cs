using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject[] tutorial_pages;
    public Button next_button;
    public Button previous_button;
    //private bool interactive = false;
    public static GameObject[] interactive_tutorial_images = new GameObject[8];   // Used to hold the interactive tutorial images
    public GameObject[] tutorial_images = new GameObject[8]; // Static variables don't appear in the inspector
    private static bool pause = false;

    // DONE: first panel, bomb, no match, slot, reduce, life 

	// Use this for initialization
	void Start () 
    {
        // Transfer images from non-static variable to static variable
        for (int i = 0; i < tutorial_images.Length; i++)
        {
            interactive_tutorial_images[i] = tutorial_images[i];
        }
	}
	

	// Update is called once per frame
	void Update () 
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
	}


    // Displays the first interactive tutorial panel if it's the first time the user is playing
    void OnLevelWasLoaded(int level)
    {
        if (!PlayerPrefs.HasKey("Tutorial") && level == 2)
        {
            Invoke("showSwipeTutorial", 1.2f); // Need to delay so that game doesn't pause on white transition screen
            PlayerPrefs.SetString("Tutorial", "1");
        }
    }


    // Shows the first panel of the interactive tutorial
    private void showSwipeTutorial()
    {
        interactive_tutorial_images[0].gameObject.SetActive(true);
        pause = true;
        Debug.Log("Stuffs is should be of the happening.");
    }


    // Display the tutorial if it has not been displayed before
    public static void tutorialSelector(string tutorial)
    {
        switch (tutorial)
        {
            case "bomb":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("2") == -1)
                {
                    Debug.Log("Display the bomb interactive tutorial.");
                    interactive_tutorial_images[1].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "12");
                }

                break;

            case "nomatch":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("3") == -1)
                {
                    Debug.Log("Display the nomatch interactive tutorial.");
                    interactive_tutorial_images[2].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "123");
                }

                break;

            case "slot":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("4") == -1)
                {
                    Debug.Log("Display the slot interactive tutorial.");
                    interactive_tutorial_images[3].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "1234");
                }

                break;

            case "freeze":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("5") == -1)
                {
                    Debug.Log("Display the freeze interactive tutorial.");
                    interactive_tutorial_images[4].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "12345");
                }

                break;
                
            case "life":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("6") == -1)
                {
                    Debug.Log("Display the life interactive tutorial.");
                    interactive_tutorial_images[5].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "123456");
                }

                break;

            case "reduce":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("7") == -1)
                {
                    Debug.Log("Display the reduce interactive tutorial.");
                    interactive_tutorial_images[6].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "1234567");
                }

                break;

            case "double":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("8") == -1)
                {
                    Debug.Log("Display the double interactive tutorial.");
                    interactive_tutorial_images[7].SetActive(true);
                    pause = true;
                    PlayerPrefs.SetString("Tutorial", "12345678");
                }

                break;
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
