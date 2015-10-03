using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour 
{
	// This script is used for controlling the Wheels rotation logic.
	// The variables to control the wheel are made publisc to let 
	// us controll the speed and when to start or stop the spinning wheel.

	private float startRollLerp = 0;
	private Vector3 updateRotation = Vector3.zero;
	private float stopSpeed = 5.5f;
	private Vector3 rotation = Vector3.zero;
	
	private int rotationSpeed = 90;
	public bool startRoll = true;
	public bool continueRotation = false;
	
	public int match_count = 0;
	public int mismatched_count = 0;
	
	// Use this for initialization
	void Start () 
	{
		rotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startRoll)
		{
			startRollLerp += Time.deltaTime;
			
			if (startRollLerp > stopSpeed)
			{
				startRollLerp = stopSpeed;
				startRoll = false;
				continueRotation = true;
			}
			smoothStart();
		}
		
		else if (continueRotation)
		{
			speedLevel(match_count);
			rotation.z -= rotationSpeed * Time.deltaTime;
			transform.eulerAngles = rotation;
		}
		
	}
	
	public void smoothStart()
	{
		float perc = startRollLerp / stopSpeed;
		perc = 1 - Mathf.Cos(perc * Mathf.PI * 0.5f);
		
		updateRotation.z = Mathf.Lerp(rotation.z, rotation.z - 360, perc);
		
		transform.eulerAngles = updateRotation;
	}
	
	void speedLevel(int count)
	{
		if(count == 10 )
		{
			rotationSpeed = 108;
		}
		
		if(count == 20 )
		{
			rotationSpeed = 126;
		}
		
		if(count == 30 )
		{
			rotationSpeed = 144;
		}
		
		if(count == 40 )
		{
			rotationSpeed = 162;
		}
		
		if(count == 50 )
		{
			rotationSpeed = 180;
		}
		
		if(count == 60 )
		{
			rotationSpeed = 200;
		}
	}
}






