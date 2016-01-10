using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReduceShapeUI : MonoBehaviour 
{
	public GameObject non_tile_ref_01;
	public GameObject non_tile_ref_02;
	public GameObject non_tile_ref_03;
	public GameObject non_tile_ref_04;
	
	private SpriteRenderer non_tile_sprite_01;
	private SpriteRenderer non_tile_sprite_02;
	private SpriteRenderer non_tile_sprite_03;
	private SpriteRenderer non_tile_sprite_04;
	
	public GameObject spawn_tile_ref;
	private SpawnTile spawn_tile_scipt;
	
	private bool is_reduced = true;

	// Use this for initialization
	void Start () 
	{
	
		non_tile_sprite_01 = non_tile_ref_01.GetComponent<SpriteRenderer>();
		non_tile_sprite_02 = non_tile_ref_02.GetComponent<SpriteRenderer>();
		non_tile_sprite_03 = non_tile_ref_03.GetComponent<SpriteRenderer>();
		non_tile_sprite_04 = non_tile_ref_04.GetComponent<SpriteRenderer>();
		
		spawn_tile_scipt = spawn_tile_ref.GetComponent<SpawnTile>();
	
	}
	
	public void HideReducedShapes()
	{
		int[] shape_array = spawn_tile_scipt.spawnArray();
	
		Debug.Log ("This is the shapes that are reduced: " + shape_array[0] + ", " + shape_array[1]);
		
		foreach(int item in shape_array)
		{
			if(item == 0)
			{
				non_tile_sprite_01.gameObject.SetActive(true);
			}
			
			if(item == 1)
			{
				non_tile_sprite_02.gameObject.SetActive(true);
			}
			
			if(item == 2)
			{
				non_tile_sprite_03.gameObject.SetActive(true);
			}
			
			if(item == 3)
			{
				non_tile_sprite_04.gameObject.SetActive(true);
			}
		}
		
		is_reduced = false;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if(spawn_tile_scipt.reduceShapeActivated() == true)
		{
			if(is_reduced == true)
			{
				HideReducedShapes();
			}
		}
		else
		{
			non_tile_sprite_01.gameObject.SetActive(false);
			non_tile_sprite_02.gameObject.SetActive(false);
			non_tile_sprite_03.gameObject.SetActive(false);
			non_tile_sprite_04.gameObject.SetActive(false);
			
			is_reduced = true;
		}
	}
}
