using UnityEngine;
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
		match_particle.maxParticles = 10;
		match_particle.Play();
		
		/*if(!match_particle.isPlaying)
		{
			match_particle.Play();
		}*/
	}
	
	public void FalseRun()
	{
		match_particle.maxParticles = 0;
		
		match_particle.Play();
		
		/*if(!match_particle.isPlaying)
		{
			match_particle.Play();
		}*/
	}
}
