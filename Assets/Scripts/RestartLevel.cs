﻿using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Restart()
	{
		Application.LoadLevel("gameScreen");
	}
}
