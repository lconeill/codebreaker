using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiftizController : MonoBehaviour 
{
	
	public Sprite Naked, Normal, Gifted, Warning;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		ChangeGiftizButtonTexture();
	}
	
	public void ChangeGiftizButtonTexture()
	{
		SpriteRenderer button = gameObject.GetComponent<Button>();
		
		switch(GiftizBinding.giftizButtonState)
		{
			case GiftizBinding.GiftizButtonState.Invisible : button.sprite = Naked; break;
			case GiftizBinding.GiftizButtonState.Naked : button.sprite = Normal; break;
			case GiftizBinding.GiftizButtonState.Badge : button.sprite = Gifted; break;
			case GiftizBinding.GiftizButtonState.Warning : button.sprite = Warning; break;
		}
	}
	
}
