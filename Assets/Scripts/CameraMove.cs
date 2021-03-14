using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // T13 Define what we want to track...Put Manny here to track his movement
    // we will be able to drag this into place, so its public
    public Transform cameraTarget;

    // T13 define how fast the camera is going to track Manny.
    // How quickly the camera moves towards Manny
    public float cameraSpeed;

    // T13 define how far to the left, on the x direction and y direction, our camera will be
    // it will be a distance of 0 to a set point in front of manny, so he always stays on the left hand side of the screen
    // Min and max X and Y movements, public so we can change it outside of here
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    // T13 Update used when dealing with Rigid Bodies, 
    // we will be moving things, with FixedUpdate
    private void FixedUpdate()
    {
        // T13 make sure we have a camera target defined
        if (cameraTarget != null)
        {
            // this is attached to the camera and defines its new position
            // Lerp smoots movement from starting position to final position
            // pass in position of the camera (transform.position)
            // then where the camera is going to go, cameraTarget.position(where Manny is at)
            // then the time it will take the camera to do the move, we will need to tweak cameraSpeed
            var newPos = Vector2.Lerp(transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);

            // then define the cameras new position, set to where the camera was (-10) set to at the start on Manny's x axis
            var vect3 = new Vector3(newPos.x, newPos.y, -10f);

            // get the cameras x position and clamp it to the min x and max x values
            // so that Manny is always positioned at the left side of the screen, and moving towards the right
            var clampX = Mathf.Clamp(vect3.x, minX, maxX);

            // we do this for the y to keep the camera from jumping, when Manny jumps, and not looking good 
            var clampY = Mathf.Clamp(vect3.y, minY, maxY);

            // set position to our clamp x and y and our original camera position -10f
            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
