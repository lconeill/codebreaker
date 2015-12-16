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
		Application.LoadLevel (1);
	}
}
