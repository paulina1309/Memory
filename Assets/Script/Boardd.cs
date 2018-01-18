using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Boardd : MonoBehaviour 
{
	public GameObject TilePrefab;

	public int Width = 6;
	public int Height = 4;
	public Sprite[] sprites;
	Tile[] Tiles;
	public Vector2 TilesOffset = Vector2.one;
	public bool CanMowe = false;
	public TextMesh dol;
	IEnumerator Start ()
	{

		dol.GetComponent<Renderer> ().enabled = false;

			
		CreateTiles();
		ShuffleTiles ();
		PlaceTiles ();
		CanMowe = false;
		yield return new WaitForSeconds (2f);
		CanMowe = true;
		HideTiles ();


	}




	void CreateTiles()
	{
		var length = Width * Height;
		Tiles = new Tile[length];

		for (int i = 0; i < length; i++) 
		{
			var sprite = sprites [i / 2];
			Tiles [i] = CreateTile (sprite);
		}
			
	}


	Tile CreateTile(Sprite faceSprite)
	{	
		var gameobject = Instantiate (TilePrefab);
		gameObject.transform.parent = transform;

		var tile = gameobject.GetComponent<Tile> ();
		tile.Active = true;
		tile.frontFace = faceSprite;



		return tile;

	}

	void ShuffleTiles()
	{
		for (int i = 0; i < 1000; i++)
		{
			int index1 = Random.Range (0, Tiles.Length);
			int index2 = Random.Range (0, Tiles.Length);

			var tile1 = Tiles [index1];
			var tile2 = Tiles [index2];

			Tiles [index1] = tile2;
			Tiles [index2] = tile1;

		}
	}




	void PlaceTiles()
	{
		for (int i = 0; i < Width * Height; i++) 
		{
			int x = i % Width;
			int y = i / Width;
			Tiles[i].transform.localPosition = new Vector3 (x * TilesOffset.x, y * TilesOffset.y, 0);

		}

	}

	void HideTiles()
	{
		Tiles.ToList ().ForEach (tile => tile.Active = false);
	}

	bool CheckIfEnd()
	{ 
		return Tiles.All (tile => tile.aktywny == false);

	}


	public void CheckPair()
	{
		StartCoroutine (CheckPairCoroutine());
	}



	IEnumerator CheckPairCoroutine()
	{
		var tilesActive = Tiles
			.Where(tile=>tile.aktywny)
			.Where (tile => tile.Active)
			.ToArray ();

		if (tilesActive.Length != 2)
			yield break;
		var tile1 = tilesActive [0];
		var tile2 = tilesActive [1];

		CanMowe = false;
		yield return new WaitForSeconds (0.3f);
		CanMowe = true;


		if (tile1.frontFace == tile2.frontFace) 
		{
			
			tile1.aktywny = false;
			tile2.aktywny = false; 
		} 
		else 
		{
			tile1.Active = false;
			tile2.Active = false;

		}


		if (CheckIfEnd ())
		{
			CanMowe = true;
			dol.GetComponent<Renderer> ().enabled = true;
			yield return new WaitForSeconds (5f);
			Application.Quit ();

		}



	}
}
