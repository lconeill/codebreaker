using UnityEngine;
using System.Collections;

public class WheelLogic : MonoBehaviour 
{
	// This script is attached to Each match area
	// The function of the script in its current state is to
	// check for collision when a tile enters its collider.
	// When a correct match is made the circular timer script is
	// invoked and calls the functions to move the tiels
	// and reset the timer.
	
	public GameObject circular_timer_ref;
	
	private CircularTimer circular_timer_Script;

	void Start () 
	{
		circular_timer_Script = circular_timer_ref.GetComponent<CircularTimer>();
	}
	
	// This fucntion is called when a trigger collider enters this objects collider.

	void OnTriggerEnter2D(Collider2D col)
	{
		if(this.gameObject.tag == col.tag.Replace("Source", "Target"))
		{
			Debug.Log(col.tag);

			circular_timer_Script.moveTile.moveTiles();
			circular_timer_Script.Reset();
		}
		
		else
		{
			Debug.Log("This is not a match!");
		}
	}
}
