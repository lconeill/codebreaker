using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;

public class Store_Init : MonoBehaviour 
{
	public GameObject image_ref;
	private Animator image_anim;
	private Animation fadeColorAnimationClip;
	
	// Use this for initialization
	void Start () 
	{
		//image_anim = image_ref.GetComponent<Animator>();
		
		//fadeColorAnimationClip = image_ref.GetComponent<Animation>();
		
		//fadeColorAnimationClip.Play ("FadeToColor");
		
		SoomlaStore.Initialize(new MayhemStoreAssets());
		
		Invoke("LoadDelayed", 8.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void LoadDelayed()
	{
		//Load the selected scene, by scene index number in build settings

        if (PlayerPrefs.GetInt("Theme") == 0)
        {
            Application.LoadLevel(1); // Load the classic start screen
            Debug.Log("Classic theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }
        
        else if (PlayerPrefs.GetInt("Theme") != ThemeManager.WINTER_THEME)
        {
            Application.LoadLevel(4); // Load the winter start screen
            Debug.Log("Winter theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }

        else
        {
            Application.LoadLevel(1); // Load the classic start screen
            Debug.Log("Classic theme selected. Level to load: " + PlayerPrefs.GetInt("Theme"));
        }
	}
}
