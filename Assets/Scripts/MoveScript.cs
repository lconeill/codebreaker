using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour 
{
	// This Script handles the Movement of the tiles
	// in gameplay. When a tile appears the player can
	// swip or flick in the direction of the match area
	// they are trying to get to and the tile will hold 
	// at a specified max distacne even if the player
	// continues to move their finger in a direction.
	// if the player lifts their finger before reaching
	// the match area the tile will jump back to its
	// starting point.
	
	private float horizontal_limit = 2.0f;
	private float vertical_limit = 2.0f;
	private float drag_speed = 0.01f;
	
	public bool is_touch_start =false;
	
	Transform cached_transform;
	Vector3 starting_pos;
	
	// Initializing the tiles position.
	
	void Start()
	{
		cached_transform = transform;
		
		starting_pos = cached_transform.position;
	}
	
	
	void Update () 
	{
		// Thourgh a switch statmet get the current 
		// state of the Touch Input. If state is 
		// Moved then call the DragObject function
		// to move the tile, any othe rcase the object
		// will not move.
		
		if(Input.touchCount == 1)
		{
			// Get the delta of when the player touches the 
			// screen and what direction they are moving.
			
			Vector2 deltaPosition = Input.GetTouch (0).deltaPosition;
			
			switch(Input.GetTouch (0).phase)
			{
				case TouchPhase.Began:
					is_touch_start = true;
					break;
				
				case TouchPhase.Moved:
				
					if(is_touch_start == true)
					{
						DragObject (deltaPosition);
					}
					
					break;
				
				case TouchPhase.Ended:
					is_touch_start = false;
					break;
			}
		}
		
		else
		{
			// When the player lifts their finger set
			// the tiles positoin back to its starting point.
			
			cached_transform.position = starting_pos;
		}

	}
	
	// Move the tile by changing its position based on the 
	// touch input of the player and limit the distance
	// the tile can go in either directin with a clamp.
	
	void DragObject(Vector2 deltaPosition)
	{
		cached_transform.position = new Vector3(Mathf.Clamp((deltaPosition.x * drag_speed) + cached_transform.position.x,
		                                                    starting_pos.x - horizontal_limit, starting_pos.x + horizontal_limit),
		                                                    Mathf.Clamp((deltaPosition.y * drag_speed) + cached_transform.position.y,
		                                                    starting_pos.y - vertical_limit, starting_pos.y + vertical_limit),
		                                        			cached_transform.position.z);
	}
}
