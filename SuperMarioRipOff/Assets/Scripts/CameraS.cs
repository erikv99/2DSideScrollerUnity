using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour {

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform myCamera;

    [SerializeField]
    private float zOffset = -2f;

    [SerializeField]
    private float yOffset = 2.5f;

    private Vector3 newCameraPos;
    private Vector3 oldCameraPos;

    // Update is called once per frame
    void Update()
    {
        // checking if the old camera pos is set
        if (oldCameraPos != Vector3.zero)
        {
            // checking if the player his x coordinate is lower then the previous camera position
            // this means the player is walking backwards and the camera shouldnt move.
            if (player.position.x < oldCameraPos.x)
            {
                return;
            }
        }

        oldCameraPos = new Vector2(myCamera.position.x, myCamera.position.y);
        
        // getting the x coordinate from the player
        float playerX = player.position.x;
        // setting the new position for the camera, 
        newCameraPos.Set(playerX, yOffset, zOffset);
        // putting the camera on its new position
        myCamera.position = newCameraPos;
    }
}
