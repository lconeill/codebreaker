using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject[] tutorial_pages;
    public Button next_button;
    public Button previous_button;
    //private bool interactive = false;
    public static Image[] interactive_tutorial_images = new Image[8];   // Used to hold the interactive tutorial images


	// Use this for initialization
	void Start () 
    {
        // Don't show the interactive tutorials
        if (PlayerPrefs.GetString("Tutorial") == "0")
        {
            PlayerPrefs.SetString("Tutorial", "1");
        }
	}
	

	// Update is called once per frame
	void Update () 
    {

	}


    // Displays the first interactive tutorial panel if it's the first time the user is playing
    void OnLevelWasLoaded(int level)
    {
        if (level == 1 && PlayerPrefs.GetString("Tutorial") == "1")
        {
            interactive_tutorial_images[0].gameObject.SetActive(true);
        }
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
                    interactive_tutorial_images[1].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "12");
                }

                break;

            case "nomatch":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("3") == -1)
                {
                    Debug.Log("Display the nomatch interactive tutorial.");
                    interactive_tutorial_images[2].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "123");
                }

                break;

            case "slot":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("4") == -1)
                {
                    Debug.Log("Display the slot interactive tutorial.");
                    interactive_tutorial_images[3].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "1234");
                }

                break;

            case "freeze":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("5") == -1)
                {
                    Debug.Log("Display the freeze interactive tutorial.");
                    interactive_tutorial_images[4].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "12345");
                }

                break;
                
            case "life":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("6") == -1)
                {
                    Debug.Log("Display the life interactive tutorial.");
                    interactive_tutorial_images[5].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "123456");
                }

                break;

            case "reduce":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("7") == -1)
                {
                    Debug.Log("Display the reduce interactive tutorial.");
                    interactive_tutorial_images[6].gameObject.SetActive(true);
                    PlayerPrefs.SetString("Tutorial", "1234567");
                }

                break;

            case "double":

                if (PlayerPrefs.GetString("Tutorial").IndexOf("8") == -1)
                {
                    Debug.Log("Display the double interactive tutorial.");
                    interactive_tutorial_images[7].gameObject.SetActive(true);
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
