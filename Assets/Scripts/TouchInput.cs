using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour 
{

	// This is purly for testing with touch input.
	// the script will display the ID, phase, 
	// position etc on a GUI texture overlaying
	// on the screen.
	
	void OnGUI()
	{
		foreach(Touch touch in Input.touches)
		{
			string message = "";
			message += "ID: " + touch.fingerId + "\n";
			message += "Phase: " + touch.phase.ToString() + "\n";
			message += "TapCount: " + touch.tapCount + "\n";
			message += "Pos X: " + touch.position.x + "\n";
			message += "Pos Y: " + touch.position.y + "\n";
			
			int num = touch.fingerId;
			GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
		}
	}
}
