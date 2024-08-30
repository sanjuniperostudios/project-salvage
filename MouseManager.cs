using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{

    private Vector3 dragStartPosition, dragEndPosition;

    public bool isDragging;

    public GameMapData mapData;
    public enum ManipulationMode
    {
        Viewing,
        Building,
        Fighting

        //etc...
    }

    public static ManipulationMode currentManipulationMode
    {
        get;
        set;
    }
    
    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Starting drag...");
            isDragging = true;
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            if(Input.GetKeyDown("escape"))
            {
                if(isDragging == false)
                {
                    mapData.curTileSelected = -1;
                    //Change to view mode
                }
                else
                {
                    //Stop the current drag like it never began (cancel it)
                    return;
                }
                return;
            }
        }

        if(Input.GetMouseButtonUp(0) && isDragging == true)
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            switch(currentManipulationMode)
            {
                case ManipulationMode.Viewing:
                {
                    //Run the event or ask the associated scripts to display the information on the tile that was clicked
                    Debug.Log("Starting view!");
                    //ViewDrag();   (doesn't currently exist)
                    break;
                }
                
                case ManipulationMode.Building:
                {
                    //Run the event or ask the associated scripts to start a build drag
                    Debug.Log("Starting build!");
                    BuildingDrag();
                    break;
                }

                case ManipulationMode.Fighting:
                {
                    //Nothing should be here, functionality not needed, just an example
                    break;
                }
            }
        }
    }

    void Start()
    {
        currentManipulationMode = ManipulationMode.Building;
    }


    void BuildingDrag()
    {
        //For some reason, the drag is off specifically by 10 on the X and 6 on the Y. Worth figuring out why this is sometime.
        int start_x =   Mathf.FloorToInt(dragStartPosition.x) + 10;
        int end_x =     Mathf.FloorToInt(dragEndPosition.x) + 10;

        int start_y =   Mathf.FloorToInt(dragStartPosition.y) + 6;
        int end_y =     Mathf.FloorToInt(dragEndPosition.y) + 6;

        //Invert the values in case they are negative - This is important so that the X and Y values stay preserved and continue to relate to the GameMapData properly

        if(end_x < start_x)
        {
            int temp = end_x;
            end_x = start_x;
            start_x = temp;
        }

        if(end_y < start_y)
        {
            int temp = end_y;
            end_y = start_y;
            start_y = temp;
        }

        for (int x = start_x; x <= end_x; x++)
        {
            for (int y = start_y; y <= end_y; y++)
            {
                //Talk to the GameMapData script and have each of the associated tiles at their locations changed

                //Use the x and y to find each associated tile and change the tile there.
                Debug.Log("Calling PlaceStructureTile at X: " + x + ", Y: " + y);
                mapData.PlaceStructureTile(x, y);
            }
        }

    }
}
