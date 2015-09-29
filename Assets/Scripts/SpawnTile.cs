using UnityEngine;
using System.Collections;

public class SpawnTile : MonoBehaviour
{
    public GameObject[] tiles;
    private Vector2[] defaultPositions = new Vector2[4] { new Vector2(7, 3), new Vector2(7, 0), 
                                                          new Vector2 (7,-3), new Vector2 (0,0) };
    private GameObject[] clonedTiles = new GameObject[4];
    
    // Added the variable to hold a reference to the MoveScript.
    
	private MoveScript move_script;
	
    void Start()
    {
        initializeTiles();
    }
    
    void Update()
    {
    }

    public void initializeTiles()
    {
        for (int j = 0; j <= 3; j++)
        {
            int i = Random.Range(0, tiles.Length);
            GameObject clone = (GameObject) Instantiate(tiles[i], defaultPositions[j], Quaternion.identity);
            clonedTiles[j] = clone;
            
            if(j == 3)
            {
            	// Based on the Initial tile get reference to the
            	// MoveScript and enable it.
            	
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
            	
				move_script = clonedTiles[n].GetComponent<MoveScript>();
				move_script.enabled = true;
                Destroy(clonedTiles[n + 1]);
            }

            clonedTiles[n].transform.position = defaultPositions[n + 1];
            clonedTiles[n + 1] = clonedTiles[n];
            if (n == 0)
            {
                int i = Random.Range(0, tiles.Length);
                GameObject clone = (GameObject)Instantiate(tiles[i], defaultPositions[n], Quaternion.identity);
                clonedTiles[n] = clone;
            } 
        }
        
    }
}
