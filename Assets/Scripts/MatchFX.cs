﻿using UnityEngine;
using System.Collections;

public class MatchFX : MonoBehaviour 
{
	// This Script is controlling when the particle
	// system is ran.
	
	private ParticleSystem match_particle;
	
	// Use this for initialization
	void Start () 
	{
		// Get ref to the attached particle system
		
		match_particle = this.GetComponent<ParticleSystem>();
	}
	
	// Call this function from anywhere to run
	// the particle simulation
	
	public void Run()
	{
		match_particle.Play();
	}
}
