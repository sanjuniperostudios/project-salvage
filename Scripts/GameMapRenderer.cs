using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTiles;
using UnityEngine.Tilemaps;


//This script should only handle rendering. It should not handle any data if possible
public class GameMapRenderer : MonoBehaviour
{

    public GameMapData mapGrid;

    public Tilemap buildingMap;

    public Tilemap previewMap;
     
    void Start()
    {
        GenerateRepositories();
        mapGrid.GenerateMap();      //This is a temporary solution. The repositories *need* to be built before
                                    //the map is generated, or else no tiles will have their sprites. Repositories
                                    //will be built on game startup, and map generated on world load, meaning
                                    //this should not be an issue later down the line (GameMapData shouldn't need
                                    //this kind of dependency in the final product).
    }


    // Called after the world map is initialized
    // On start, hook into the GameMapGrid to fetch what to render
    public void GenerateRepositories()
    {
        for(int i = 0; i < TileRepository.masterTileRepository.Length; i++)
        {
            //Access the tile in that repository
            for(int j = 0; j < TileRepository.masterTileRepository[i].Count; j++)
            {
                Debug.Log("Accessing: " + i + ", " + j);

                //Assign the accessed tile sprite as a file of type TileBase with the same name in the Tile Sprites folder
                try
                {
                    TileRepository.masterTileRepository[i][j].tileSprite = Resources.Load<TileBase>("Tile Sprites/" + TileRepository.masterTileRepository[i][j].ID);
                }

                catch
                {
                    Debug.LogError("There is not a tile in the repository at the location " + i + ", " + j + ". Are you sure the class object exists there?");
                }
                
            }

            Debug.Log("Finished intializing repository " + i);
        }
        Debug.Log("Finished initializing tile sprites for all repositories.");
    }


    public BasicTile RequestStructureTileAtLocation(Vector2 mousePosition)
    {
        Vector3Int cellPos = buildingMap.WorldToCell(mousePosition);
        
        return mapGrid.GetStructureTileInstance(cellPos);
    }

    public Vector3Int RequestTileLocationOnly(Vector2 mousePosition)
    {
        return buildingMap.WorldToCell(mousePosition);
    }


    public void UpdateBuildingTileSprite(Vector3Int location, TileBase newTileSprite)
    {
        buildingMap.SetTile(location, newTileSprite);
    }

}
