using UnityEngine;
using System.Collections;

public class MuteGUI : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Mute()
	{
		PlayerPrefs.SetInt("Mute", 0);
	}
	
	public void Unmute()
	{
		PlayerPrefs.SetInt("Mute", 1);
	}
}
