using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using CustomTiles;
using UnityEngine;

//This script should only handle data. This should not handle rendering at all, or use any rendering information if possible
public class GameMapData : MonoBehaviour
{
    //The GameMap only talks to the renderer to transfer things like sprites / world locations into tile locations in the grid
    public GameMapRenderer mapRenderer;

    public BasicTile[,] structureTiles;

    public int curTileSelected = -1;

    //Default constructor, map has 128x128 (16,384) tiles by default
    public GameMapData()
    {
        const int MAP_HEIGHT = 256;
        const int MAP_WIDTH = 256;

        //Intialize a new map based on the map height and width parameters
        structureTiles = new BasicTile[MAP_HEIGHT, MAP_WIDTH];
    }

    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            curTileSelected = -1;
        }
    }

    public void ChangeTileSelected(int newTileSelected)
    {
        Debug.Log("ChangeTileSelected was called");
        curTileSelected = newTileSelected;
    }
    

    public BasicTile GetStructureTileInstance(Vector3Int location)
    {
        return structureTiles[location.x, location.y];
    }


    public void PlaceStructureTile(int positionX, int positionY)
    {
        if(curTileSelected == -1)
        {
            Debug.LogError("Tried to place a tile while no tile is selected. Something wasn't supposed to call PlaceTile.");
            return;
        }

        //This should always call the GameMapRenderer to update the sprite.

        structureTiles[positionX, positionY] = TileRepository.constructedStructureRepo[curTileSelected];

        mapRenderer.UpdateBuildingTileSprite(new Vector3Int(positionX, positionY, 0), TileRepository.constructedStructureRepo[curTileSelected].tileSprite);
        
    }

    public void WorldPlaceStructureTile(int positionX, int positionY)
    {
        if(positionX % 2 == 0)
        {
            //place Wood
            structureTiles[positionX, positionY] = TileRepository.constructedStructureRepo[0];
            mapRenderer.UpdateBuildingTileSprite(new Vector3Int(positionX, positionY, 0), TileRepository.constructedStructureRepo[0].tileSprite);
        }
        else
        {
            //place Metal
            structureTiles[positionX, positionY] = TileRepository.constructedStructureRepo[1];
            mapRenderer.UpdateBuildingTileSprite(new Vector3Int(positionX, positionY, 0), TileRepository.constructedStructureRepo[1].tileSprite);
        }
    }

    

    public void GenerateMap()
    {
        Debug.Log("Generating!");
        for(int i = 0; i < structureTiles.GetLength(0); i++)
        {
            for(int j = 0; j < structureTiles.GetLength(1); j++)
            {
                WorldPlaceStructureTile(i, j);
            }
        }
    }
}
