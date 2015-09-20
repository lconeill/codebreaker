using UnityEngine;
using System.Collections;

public class SpawnTile : MonoBehaviour
{

    public GameObject[] tiles;
    private Vector2[] defaultPositions = new Vector2[4] { new Vector2(7, 3), new Vector2(7, 0), 
                                                          new Vector2 (7,-3), new Vector2 (0,0) };
    private GameObject[] clonedTiles = new GameObject[4];

    void Start()
    {
        initializeTiles();
    }

    public void initializeTiles()
    {
        for (int j = 0; j <= 3; j++)
        {
            int i = Random.Range(0, tiles.Length);
            GameObject clone = (GameObject) Instantiate(tiles[i], defaultPositions[j], Quaternion.identity);
            clonedTiles[j] = clone;
        }
    }

    public void moveTiles()
    {

        for (int n = 2; n >= 0; n--)
        {
            if (n == 2)
            {
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
