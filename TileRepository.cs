using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTiles;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;


namespace CustomTiles
{
    public class TileRepository
    {

    /*
        A detailed repository system for defining objects for use in-game. Tiles created will read from here to generate data about the tiles.
    */


    
    //All tiles require:
    //A human-readable string ID
    //An easily-serializeable numerical ID
    //A human-readable simple description of what this new tile is

    public static List<BasicTile> constructedStructureRepo = new()
    {

        new ConstructedStructureTile
        {
            ID = "RustedSteelWall",
            numID = 0,
            shortDesc = "A rusted steel wall, capable of being harvested for simple sheets which may be smelted down into alloys.",
            //Position defaults to null
            //Sprite unset! Sprites are built at runtime and loaded from resources.

            //Requires data on what it should drop when salvaged!
            //Should also have data on time it takes to create a salvage chunk, and how many salvage chunks it should have
        },

        new ConstructedStructureTile
        {
            ID = "WoodWall",
            numID = 1,
            shortDesc = "A constructed wooden wall, highly flammable but can resist the hot temperatures of the sun."

            //...
        },

        new ConstructedStructureTile
        {
            ID = "StoneWall",
            numID = 2,
            shortDesc = "A sturdy mortar-and-brick wall. Completely invulnerable to fire and very tough.",

            //...
        }
    };

    public static List<BasicTile> naturalStructureRepo = new()
    {
        //blah
        new NaturalStructureTile
        {
            //blah
            ID = "DirtFloor"
        }
    };

    public static List<BasicTile> constructedFloorRepo = new()
    {
        //blah
    };

    public static List<BasicTile> naturalFloorRepo = new()
    {
        //blah
    };

    public static List<BasicTile> farmableFloorRepo = new()
    {
        //blah
    };

    /*
    public static List<BasicTile> exampleTileRepository = new()
    {
        While the type for these is not necessarily given, it is best to keep only one type of tile
        in each repository. Be careful if you mix types!

        Example tile:
        [0] = new exampleTileType
        {
            Tile data goes here. If your tile type inherits from one of the base types (structure, floor...)
            then data like health or walk speed modifier can be accessed.

            Because this tile is of index [0], it should be given the numeric ID of 0. Index and numeric ID
            should always be the same, for save/load purposes.

            Note that all tile data here is not ever changed or access directly, rather copied when an instance 
            of this type of tile is created (either through generation or by building, etc). So, this essentially
            works as "default" tile data (what this tile starts out with).

            Some data, particularly data that is gathered at runtime from a folder (like sprites, what to drop), is 
            not initialized here. It is initialized on game startup, done by TileManager.cs.

            Head to TileClasses.cs to learn more about how the subclass structure works, and how to make your own
            unique tile types and components.
        }
    };
    */

    public static List<BasicTile>[] masterTileRepository =
    {
        constructedStructureRepo,
        naturalStructureRepo,
        constructedFloorRepo,
        naturalFloorRepo,
        farmableFloorRepo

        //Other repositories may be added!
    };

    

    public string[] info;

    //Warning! TileSprites is currently empty; cannot define sprites with sprite list being empty!

    public static ConstructedStructureTile rustedSteelWall = new()
    {
        ID = "RustedSteelWall",
        numID = 0,
        shortDesc = "A rusted steel wall, capable of being harvested for simple sheets which may be smelted down into alloys.",
        //Position unset!
        //Sprite unset!

        //Requires data on what it should drop when salvaged!
        //Should also have data on time it takes to create a salvage chunk, and how many salvage chunks it should have

    };

    public static FarmableFloorTile richDirt = new()
    {
        ID = "RichDirt",
        numID = 1,
        shortDesc = "A square yard of dirt, rich enough to grow most plants in.",
        //Position unset!
        //Sprite unset!

        //Requires a list of plants that are allowed to grow on this tile!

    };

    public static ConstructedStructureTile scrapWoodWall = new()
    {
        ID = "ScrapWoodWall",
        numID = 2,
        shortDesc = "A wall built from scraps torn from various furniture and structures.",
        //Position unset!
        //Sprite unset!

        //Requires the builder who constructed this!
        //Requires how much health it should have!

    };

    public static ConstructedStructureTile cobbledStoneWall = new()
    {
        ID = "CobbledStoneWall",
        numID = 3,
        shortDesc = "A wall built from various pieces of stone cobbled together. Slightly sturdy, but clearly built by an amateur.",
        //Position unset!
        //Sprite unset!

        //Requires the builder who constructed this!
        //Requires how much health it should have!
    };

    //And so forth... completely modular

} 

}