using UnityEngine;
using System.Collections;

public class MatchFX : MonoBehaviour 
{
	private ParticleSystem match_particle;
	
	// Use this for initialization
	void Start () 
	{
		match_particle = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Run()
	{
		match_particle.Play();
	}
}
