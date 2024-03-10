using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdaptor : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the main camera
        mainCamera = Camera.main;

        // Call the AdaptCamera method initially
        AdaptCamera();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the AdaptCamera method continuously
        AdaptCamera();
    }

    // Method to adapt camera properties based on screen size or aspect ratio
    void AdaptCamera()
    {
        // Calculate the desired aspect ratio (e.g., 16:9)
        float targetAspect = 16f / 9f;

        // Get the current aspect ratio of the screen
        float currentAspect = (float)Screen.width / Screen.height;

        // Calculate the desired field of view to maintain the target aspect ratio
        float fov = mainCamera.fieldOfView;

        if (currentAspect > targetAspect)
        {
            // If the current aspect ratio is wider than the target, adjust the field of view
            fov *= targetAspect / currentAspect;
        }

        // Apply the calculated field of view to the camera
        mainCamera.fieldOfView = fov;
    }
}
