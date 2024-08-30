using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CustomTiles
{
        //This should just hold information on each specific tile. Things like position as a Vector3Int, health as a float, etc.
        //I think only tile-unique information should be stored here, like the ID of this tile, not generic things like if it's flammable or not (which apply to all tiles of that type).

        //This tile information will be for finished or natural tiles ONLY, this will not include build order tiles or special tiles that hold data different to other tiles.



        //This needs to have class specifications for different types of tiles. Basically, 'if' statements on whether it can be salvaged, farmed, damaged, etc.
        //Based on these if statements, host data relative to that information. If it can be salvaged, what drops from salvaging it? If it can be farmed, what type of plants can grow on it,
        //and how fast can they grow on it? If it can be damaged, how many hitpoints does it have, and what happens when it's destroyed? Does it drop items?

        //All tile types need to inherit from the basic tile, which contains an ID, a sprite, and a position. All tiles must have this.
        //Each tile type then needs to have data based 

        /*

        public abstract class BasicTile
        {
            public abstract string ID { get; set; }  //Require all derived classes have an ID

            public abstract bool IsCollidable { get; }  //Require all derived classes to specify if it's collidable or not

            public Sprite tileSprite;

            public Vector3Int position;
        }

        public class StructureTile : BasicTile
        {
            //Define an override for the ID; it now has a default ID. If set, define the new ID.

            private string newID = "Undefined structure tile";

            public override string ID 
            {
                get 
                {
                    return newID;
                } 
                set
                {
                    newID = value;
                }
            }

            //Define an override for IsCollidable; it is now collidable
            public override bool IsCollidable 
            {
                get 
                {
                    return true;
                }
                //Cannot set! Collidable is constant with all structure blocks; always collidable
            }

            //Base health should be modified by a value in the material it is made of to get maximum health
            public float baseHealth = 80;

            //a public variable which references the material it is made of
            
        }

        public class FloorTile : BasicTile
        {
            public override string ID 
            {
                get 
                {
                    return "Undefined floor tile";
                } 
                set
                {
                    ID = value;
                }
            }

            public override bool IsCollidable 
            {
                get 
                {
                    return false;
                }
                //Cannot set! Collidable is constant with all floor blocks; never collidable
            }

            //a public variable which references the material it is made of
        }


        public class SalvagedStructureTile : StructureTile
        {
            
            //Kind of like health: When salvaged, take from this count.
            public int resourceChunksLeft = 5;


            //a public variable which references what to drop at each salvage chunk
        }

        public class NaturalFloorTile : FloorTile
        {
            public float farmability;

            public float flammability;
        }

        public class ConstructedFloorTile : FloorTile
        {
            public string wasBuiltBy = "Undefined builder";

            public float baseHealth;

            //a public variable which references the material is is made of

            //
        }

        //public static


        //Every object can hold any data.

    /*
        public class CustomTile
        {
            //If any value is not defined, 
            public string ID;

            public Vector3Int position;

            public Sprite tileSprite;

            public float farmability;


        }
        


        //Having several different lists: Each for every possible type of block. Salvageable structure has a list. Constructed structure has a list. Farmable ground has a list.
        //Information on position would be store in each individual script; each information script is linked to a tile and must be. The list is dynamic, and can have as many or as little
        //members as needed. Each type of list has it's own properties, like salvageable structure tiles needing what it drops when salvaged, etc. Each list is read, and for the tile
        //at the position in the script, it's sprite is updated and information is stored on it.

        //Salvageable Structure         Would hold information on what resource is dropped when salvaged, how many times you can salvage it, etc, as well as health
        //Salvageable Floor             Would hold information on what resource is dropped when salvaged, how many times you can salvage it, etc
        //Constructed Floor             Would hold information on who constructed it, what health does it have, etc
        //Natural Floor                 Would hold information about farming on it, what can grow on it, etc
        //Constructed Structure         Would hold information on who constructed it, what health does it have, etc, as well as be collideable
        //Natural Structure             Would hold information on what it is made of, what health does it have, etc


        //Having several different tags: Each for every possible type of block. <Salvageable> would mean it needs data on what to drop when salvaged, how long it takes, etc. <Farmable>
        //would need data on how farmable, what plants can grow on it, etc

        //<Salvageable>                 Can be salvaged and harvested for resources
        //<Farmable>                    Can be farmed on, host certain crops, etc
        //<Constructed>                 Is built by someone, is made of stuff, etc
        //<Structure>                   Has health, can be collided with, holds up a roof etc
        //<Workbench??>                 Can be used as a workbench, takes up multiple tiles, etc??


        //Essentially, if there is information a tile does not need (like a wood wall does not need to have salvage information on it), then it shouldn't be stored. Only the data
        //that is necessary to be stored, should be. 

        */

        //Temporary, should probably be removed
        

        //Is there a way to assign a default reference to this sprite? Currently does not work and is null
        public static class BaseInformation
        {
            public static TileBase undefSprite;
        }

        public abstract class BasicTile
        {

            public string ID;

            public int numID;

            public string shortDesc;

            public Vector3Int position;

            public TileBase tileSprite;



            public List<BaseComponent> components = new();

            public BasicTile()
            {
                this.CreateTileData();
                ID = "Undefined basic tile";
                shortDesc = "No desc given. Has components: ";
                foreach(BaseComponent comp in components)
                {
                    shortDesc += comp.GetType().Name;
                    if(comp != components[components.Count-1])
                    {
                        shortDesc += ", ";
                    }
                }
                tileSprite = BaseInformation.undefSprite;
                position = new Vector3Int(0, 0, 0);
            }

            public List<BaseComponent> Components
            {
                get { return components; }
            }

            //Creation of a new tile, to be overriden by a further-specifying type
            public abstract void CreateTileData();
        }

        public abstract class StructureTile : BasicTile
        {
            //Has health, collision information, etc. Needs to be further specified before it's allowed to be a proper tile.
        }

        public abstract class FloorTile : BasicTile
        {
            //Has walk speed modifier, etc
        }

        public abstract class BaseComponent
        {
            //Basic information that all tags should have (maybe nothing)
            //Hello
        }


        public class FarmableFloorTile : FloorTile
        {
            public override void CreateTileData()
            {
                Components.Add(new Floor());
                Components.Add(new Natural());
                Components.Add(new Farmable());
            }
        }


        public class NaturalFloorTile : FloorTile
        {
            public override void CreateTileData()
            {
                Components.Add(new Floor());
                Components.Add(new Natural());
            }
        }

        public class ConstructedFloorTile : FloorTile
        {
            public override void CreateTileData()
            {
                Components.Add(new Floor());
                Components.Add(new Constructed());
                Components.Add(new Salvageable());
            }
        }

        public class ConstructedStructureTile : StructureTile
        {
            public override void CreateTileData()
            {
                Components.Add(new Structure());
                Components.Add(new Constructed());
                Components.Add(new Salvageable());
            }
        }

        public class NaturalStructureTile : StructureTile
        {
            public override void CreateTileData()
            {
                Components.Add(new Structure());
                Components.Add(new Natural());
            }
        }

        public class Salvageable : BaseComponent
        {
            public string tagName = "Salvageable";
            //Require salvage information
            //EX: What to drop when salvaged
        }

        class Constructed : BaseComponent
        {
            public string tagName = "Constructed";
            //Require construction information
            //EX: Who built it, what's it made of

            public float baseHealth;

            //Can it be generated by the world/can it only be built by the player?
        }

        class Natural : BaseComponent
        {
            public string tagName = "Salvageable";
            //Require nature information
            //EX: What is it made out of
        }

        class Floor : BaseComponent
        {
            public string tagName = "Floor";

            public bool isCollidable = false;

            public float walkSpeedMultiplier;


            //Require flooring information
            //EX: Walk speed, health (if non-natural)
        }

        class Farmable : BaseComponent
        {
            public string tagName = "Farmable";
            //Require farming information
            //EX: What plants can grow here

            //public List<Plants> plantsThatCanGrow = new();    A list of plants that are allowed to grow on this particular tile.
        }

        class Structure : BaseComponent
        {
            public string tagName = "Structure";
            //Require structure information
            //EX: Health

            public float baseHealth;

            public bool isCollidable = true;
        }

    }
