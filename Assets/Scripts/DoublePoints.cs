using UnityEngine;
using System.Collections;

public class DoublePoints : MonoBehaviour {

    public GameObject[] match_tiles = new GameObject[4];

    // Reference to wheel logic script on each gameobject
    private WheelLogic tile_01;         
    private WheelLogic tile_02;
    private WheelLogic tile_03;
    private WheelLogic tile_04;

	// Use this for initialization
	void Start () 
    {
        tile_01 = match_tiles[0].GetComponent<WheelLogic>();
        tile_02 = match_tiles[1].GetComponent<WheelLogic>();
        tile_03 = match_tiles[2].GetComponent<WheelLogic>();
        tile_04 = match_tiles[3].GetComponent<WheelLogic>();
	}
	
    // Since the wheel logic script which contains the score logic is on multiple game objects
    // I need to get reference to each script and activate the reward on each of them
    public void activateReward()
    {
        tile_01.increaseMultiplier();
        tile_02.increaseMultiplier();
        tile_03.increaseMultiplier();
        tile_04.increaseMultiplier();
    }
}
