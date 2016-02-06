using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#if UNITY_ANDROID
public class GiftizController : MonoBehaviour 
{
	
	public Sprite Naked, Normal, Gifted, Warning;
	// Use this for initialization
	void Start () 
	{
		#if UNITY_IPHONE
		
		gameObject.gameObject.SetActive(false);
		
		#endif
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		ChangeGiftizButtonTexture();
	}
	
	public void ChangeGiftizButtonTexture()
	{
		Image button = gameObject.GetComponent<Image>();
		
		switch(GiftizBinding.giftizButtonState)
		{
			case GiftizBinding.GiftizButtonState.Invisible : button.sprite = Naked; break;
			case GiftizBinding.GiftizButtonState.Naked : button.sprite = Normal; break;
			case GiftizBinding.GiftizButtonState.Badge : button.sprite = Gifted; break;
			case GiftizBinding.GiftizButtonState.Warning : button.sprite = Warning; break;
		}
	}
	
	public void GiftizButtonClicked()
	{
		GiftizBinding.buttonClicked();
	}
	
}
#endif