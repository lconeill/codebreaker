using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class StartOptions : MonoBehaviour {


	public GameObject mute_button_ref;
	public GameObject unmute_button_ref;
	public GameObject pause_button_ref;
	
	public int sceneToStart;    										//Index number in build settings of scene to load if changeScenes is true
	public bool changeScenes;											//If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene
	public bool changeMusicOnStart;										//Choose whether to continue playing menu music or start a new music clip
	public int musicToChangeTo = 0;										//Array index in array MusicClips to change to if changeMusicOnStart is true.


	public bool inMainMenu = true;					//If true, pause button disabled in main menu (Cancel in input manager, default escape key)
	[HideInInspector] public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
	[HideInInspector] public Animator animMenuAlpha;					//Reference to animator that will fade out alpha of MenuPanel canvas group
	[HideInInspector] public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes
	[HideInInspector] public AnimationClip fadeAlphaAnimationClip;		//Animation clip fading out UI elements alpha


	private PlayMusic playMusic;										//Reference to PlayMusic script
	private float fastFadeIn = .01f;									//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

	
	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		//Get a reference to PlayMusic attached to UI object
		playMusic = GetComponent<PlayMusic> ();
		
	}

	void Start()
	{
		if(PlayerPrefs.GetInt("Mute") == 1)
		{
			PlayerPrefs.SetInt("Mute", 1);
			mute_button_ref.SetActive(true);
			unmute_button_ref.SetActive(false);
		}
		
		else
		{
			mute_button_ref.SetActive(false);
			unmute_button_ref.SetActive(true);
		}
		
		Time.timeScale = 1;
		showPanels.ShowMenu();
		StartCoroutine(PlayNewMusic(0, 0f));
		
		if (PlayerPrefs.GetInt("Mute") == 1)
		{
			playMusic.FadeUp (fastFadeIn);
		}
	}


	public void StartButtonClicked()
	{
        // Load the theme in the player prefs
        sceneToStart = PlayerPrefs.GetInt("Theme");

		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			playMusic.FadeDown(fadeColorAnimationClip.length);
			StartCoroutine(PlayNewMusic(1, 1f));
		}

		//If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
		if (changeScenes) 
		{
			//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
			Invoke ("LoadDelayed", fadeColorAnimationClip.length * .99f);
			
			Invoke("HideMuteGUI",fadeColorAnimationClip.length * .99f);
			
			Invoke("ShowMuteGUI",fadeColorAnimationClip.length * .99f);
						
			playMusic.FadeDown(fadeColorAnimationClip.length);
			
			//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
			animColorFade.SetTrigger ("fade");

			StartCoroutine(PlayNewMusic(1, 1f));
		} 

		//If changeScenes is false, call StartGameInScene
		else 
		{
			//Call the StartGameInScene function to start game without loading a new scene.
			StartGameInScene();
		}

	}
	
	public void LoadDelayed()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//Hide the main menu UI element
		showPanels.HideMenu ();

		//Load the selected scene, by scene index number in build settings
		Application.LoadLevel (sceneToStart);
		
		pause_button_ref.SetActive(true);
	}


	public void StartGameInScene()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		if (changeMusicOnStart) 
		{
			//Wait until game has started, then play new music
			StartCoroutine(PlayNewMusic(1, 1f));
		}
		//Set trigger for animator to start animation fading out Menu UI
		animMenuAlpha.SetTrigger ("fade");

		//Wait until game has started, then hide the main menu
		Invoke("HideDelayed", fadeAlphaAnimationClip.length);

		Debug.Log ("Game started in same scene! Put your game starting stuff here.");


	}


	IEnumerator PlayNewMusic(int musicToChangeTo, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		//Fade up music nearly instantly without a click 
		if (PlayerPrefs.GetInt("Mute") == 1)
		{
			playMusic.FadeUp (fastFadeIn);
		}
		
		//Play music clip assigned to mainMusic in PlayMusic script
		playMusic.PlaySelectedMusic (musicToChangeTo);
	}

	public void HideDelayed()
	{
		//Hide the main menu UI element
		showPanels.HideMenu();
	}
}
