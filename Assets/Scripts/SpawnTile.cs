using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnTile : MonoBehaviour
{
    public GameObject[] tiles;
    private Vector2[] defaultPositions = new Vector2[4] { new Vector2(6, 2), new Vector2(6, 0), 
                                                          new Vector2 (6,-2), new Vector2 (0,0) };
    public GameObject[] clonedTiles = new GameObject[4];
    public int spawnRange = 4;
    public int spawnStartRange = 0;

    private int spawnRangeBefore;
    private int spawnStartRangeBefore;
    public bool reduceTileShape = false;
    private bool endReward = false;

    private float rewardEffectTime = 5;
    private float timeIncrement = 0;

    private Button fruitgum_button;
    
    // Added the variable to hold a reference to the MoveScript.
    
	private MoveScript move_script;
	
    void Start()
    {
        initializeTiles();
        
        GameObject temp = GameObject.Find("fruitgum_power_up");
        if (temp != null){ fruitgum_button = temp.GetComponent<Button>(); }
    }
    
    void Update()
    {
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
            fruitgum_button.enabled = false;
        }

        if (endReward)
        {
            timeIncrement += Time.deltaTime;

            if (timeIncrement >= rewardEffectTime)
            {
                spawnRange = spawnRangeBefore;
                spawnStartRange = spawnStartRangeBefore;
                timeIncrement = 0;
                endReward = false;
                fruitgum_button.enabled = true;
            }
        }


    }

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
					clonedTiles[n].transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
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
}
