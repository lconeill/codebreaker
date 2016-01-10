using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class SpawnTile : MonoBehaviour
{
    public GameObject[] tiles;
    private Vector2[] defaultPositions = new Vector2[4] { new Vector2(-5, 2), new Vector2(-5, 0), 
                                                          new Vector2 (-5,-2), new Vector2 (0,0) };
    public GameObject[] clonedTiles = new GameObject[4];

    public GameObject hide_tile_image;                          // the image shown when the hide tile effect is active
    private GameObject[] hide_tile_clones = new GameObject[3];  // holds reference to instantiated "hide tile" gameobject
    private bool hide_tile_flag = false;                        // flag used to start effect countdown
    private float hide_tile_counter = 0;                        // used to terminate the effect
    private float hide_reward_effect_time = 10;                 // the time the effect is active for

    public int spawnRange = 4;                  // the current tile spawn range
    public int spawnStartRange = 0;             // the current tile spawn start range

    private int spawnRangeBefore;               // the tile spawn range before the effect
    private int spawnStartRangeBefore;          // the tile spawn start range before the effect
    public bool reduceTileShape = false;        // flag used to enable the reduce shape effect
    private bool endReward = false;             // flag used to track length of reduce shape effect and reset tile ranges

    private float rewardEffectTime = 7;         // how long the reduce tile shapes effect lasts for
    private float timeIncrement = 0;            // counter used to to track length of the effect

    private Button reduce_shape_button;         // button component of reduce tile shape power up game object

    private MoveScript move_script;             // reference to MoveScript used to move the active game tile
    private slotManager slot_manager;
	
    void Start()
    {
        initializeTiles();

        GameObject temp = GameObject.Find("reduce_shape_power_up");
        if (temp != null) { reduce_shape_button = temp.GetComponent<Button>(); }

        GameObject temp_01 = GameObject.Find("slotManager");
        if (temp_01 != null) { slot_manager = temp_01.GetComponent<slotManager>(); }
    }
    

    // Checks to see if reduce tile shape reward has been triggered, activates, and terminates the reward
    void Update()
    {
        if (!slot_manager.inMiniGame)
        {

            // Reduce shape reward 
            if (reduceTileShape)
            {
                timeIncrement += Time.deltaTime;
                spawnRangeBefore = spawnRange;
                spawnStartRangeBefore = spawnStartRange;
                spawnStartRange = Random.Range(0, 4);

                if (spawnStartRange == 3)
                {
                    spawnStartRange = 1;
                    spawnRange = 3;
                }

                else
                {
                    spawnRange = spawnStartRange + 2;
                }

                reduceTileShape = false;
                endReward = true;
                //reduce_shape_button.enabled = false;

                reduceUpcomingTiles();
                spawnArray();

            }

            if (endReward)
            {
                timeIncrement += Time.deltaTime;
                reduce_shape_button.enabled = false; //temporary solution

                if (timeIncrement >= rewardEffectTime)
                {
                    spawnRange = spawnRangeBefore;
                    spawnStartRange = spawnStartRangeBefore;
                    timeIncrement = 0;
                    endReward = false;
                    reduce_shape_button.enabled = true;

                    Debug.Log("From within counter:" + reduce_shape_button.enabled);
                }
            }

            // Hide tile negative reward
            if (hide_tile_flag)
            {
                hide_tile_counter += Time.deltaTime;

                if (hide_tile_counter >= hide_reward_effect_time)
                {
                    hide_tile_flag = false;
                    destroyHideTiles();
                }
            }
        }
    }


    // Randomly initializes the tiles in their deault positions
    public void initializeTiles()
    {
        for (int j = 0; j <= 3; j++)
        {
            int i = Random.Range(spawnStartRange, spawnRange);
            GameObject clone = (GameObject)Instantiate(tiles[i], defaultPositions[j], Quaternion.identity);
            clonedTiles[j] = clone;
            
            if(j == 3)
            {
            	// Based on the Initial tile get reference to the
            	// MoveScript and enable it.
            	
				clonedTiles[j].transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            	
				move_script = clonedTiles[j].GetComponent<MoveScript>();
				move_script.enabled = true;
            }

        }
    }


    // Moves the tiles and scales the active tile
    public void moveTiles()
    {

        for (int n = 2; n >= 0; n--)
        {
            if (n == 2)
            {
            	// For every tile 'In Play', add a reference to
            	// the Move script and enable it.
            	
				clonedTiles[n].transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
				
				if(clonedTiles[n].tag == "bomb")
				{
					clonedTiles[n].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
				}
				
				move_script = clonedTiles[n].GetComponent<MoveScript>();
				move_script.enabled = true;
                Destroy(clonedTiles[n + 1]);
            }

            clonedTiles[n].transform.position = defaultPositions[n + 1];
            clonedTiles[n + 1] = clonedTiles[n];
            if (n == 0)
            {
                int i = Random.Range(spawnStartRange, spawnRange);
                GameObject clone = (GameObject)Instantiate(tiles[i], defaultPositions[n], Quaternion.identity);
                clonedTiles[n] = clone;
            }
        }
    }


    // This function reduces the chances of the bomb / different shape / different color tiles appearing
    // Giving it a 65% chance for now
    public void extendSpawnRange(int defaultSpawnRange, int newSpawnRange)
    {
        // This function only runs when the reduce shape reward is not in effect
        if (!endReward)
        {
            int rand = Random.Range(1, 21);

            if (rand >= 13)
            {
                spawnRange = defaultSpawnRange;
            }

            else
            {
                spawnRange = newSpawnRange;
            }
        }
    }


    // hide tiles when effect activated in slot game
    public void hideTiles()
    {
        hide_tile_flag = true;

        for (int i = 0; i <= 2; i++)
        {
            GameObject clone = (GameObject)Instantiate(hide_tile_image, defaultPositions[i], Quaternion.identity);
            hide_tile_clones[i] = clone;
        }
    }


    // reveal tiles after hide tiles effect is up
    public void destroyHideTiles()
    {
        for (int i = 0; i <= 2; i++)
        {
            Destroy(hide_tile_clones[i]);
        }
    }


    // Used to trigger double point particle effects in WheelLogic and NonSwipeActions scripts
    public bool reduceShapeActivated()
    {
        return endReward;
    }


    // This function reduces the tiles in the upcoming tiles area when the reduce shape reward is activated
    private void reduceUpcomingTiles()
    {
        for (int j = 0; j <= 2; j++)
        {
            Destroy(clonedTiles[j]);

            int i = Random.Range(spawnStartRange, spawnRange);
            GameObject clone = (GameObject)Instantiate(tiles[i], defaultPositions[j], Quaternion.identity);
            clonedTiles[j] = clone;
        }
    }


    // Returns array of base tiles that will no longer appear when reduce shapes is activated
    // Used to 'X' out tiles no longer used when reduce tile reward is in effect  
    public int[] spawnArray()
    {

        int[] tileSpawnRange = new int[4] {0, 1, 2, 3};
        int[] reduceRange = new int[2] {spawnStartRange, spawnStartRange + 1};

        int[] crossedArray = new int[2];

        IEnumerable<int> result = tileSpawnRange.Except(reduceRange);

        crossedArray = result.ToArray();

        //string sarray11 = string.Format("These are the tiles that spawn: {0}, {1}", spawnStartRange, spawnRange-1);
        //string sarray = string.Format("This is the tiles to cross out: {0}, {1}", crossedArray[0], crossedArray[1]);

        //Debug.Log(sarray11);
        //Debug.Log(sarray);

        return crossedArray;
    }
}


