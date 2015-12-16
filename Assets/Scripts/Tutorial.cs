using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public GameObject[] tutorial_pages;
    public Button next_button;
    public Button previous_button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
