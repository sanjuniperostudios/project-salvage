using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera thisCamera;
    public float cameraSpeed;
    public float normSpeed;
    public float fastSpeed;
    public float movementTime;
    public float zoomAmount;


    public Vector3 cameraFollowPosition;
    public Vector3 curFrameMousePosition;
    public Vector3 lastFrameMousePosition;
    public Vector3 dragDifference;
    public float newZoom;

    void Start()
    {
        cameraFollowPosition = transform.position;
        newZoom = thisCamera.orthographicSize;
        Debug.Log("Orthographic size is: " + thisCamera.orthographicSize);
        Debug.Log("Camera speed is: " + cameraSpeed + ", norm speed is: " + normSpeed + ", fast speed is: " + fastSpeed);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        curFrameMousePosition = thisCamera.ScreenToWorldPoint(Input.mousePosition);

        GetMouseMovement();
        GetKeyboardMovement();


        transform.position = Vector3.Lerp(transform.position, cameraFollowPosition, Time.deltaTime * movementTime);
        

        lastFrameMousePosition = thisCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void GetMouseMovement()
    {

        if(Input.GetMouseButton(2))
        {

            dragDifference = lastFrameMousePosition - curFrameMousePosition;
            cameraFollowPosition += dragDifference * 2;
        }

        newZoom  = Mathf.Clamp(newZoom += (-Input.mouseScrollDelta.y) * zoomAmount, 1, 50);

        //Linearly interpolate the value of the old zoom level and the new zoom level by movementTime divided by 5
        thisCamera.orthographicSize = Mathf.Clamp(Mathf.Lerp(thisCamera.orthographicSize, newZoom, Time.deltaTime * movementTime), 1, 50);

    }

    void GetKeyboardMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {cameraSpeed = fastSpeed;}
        else {cameraSpeed = normSpeed;}

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            cameraFollowPosition += transform.up * cameraSpeed * (thisCamera.orthographicSize/3);
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            cameraFollowPosition += transform.up * -cameraSpeed * (thisCamera.orthographicSize/3);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            cameraFollowPosition += transform.right * cameraSpeed * (thisCamera.orthographicSize/3);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            cameraFollowPosition += transform.right * -cameraSpeed * (thisCamera.orthographicSize/3);
        }
    }
}
