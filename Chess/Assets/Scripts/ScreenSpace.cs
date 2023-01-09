using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpace : MonoBehaviour
{
    [SerializeField] private Camera mainCam;


    void Start()
    {
        ScreenSize();

        print("Done");
    }

        //-- It is used to fix the screen size to prevent any clipping -- 

    void ScreenSize()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        if(camHeight < 6)
        {
            return;
        }

        if(camWidth < 4.1 || camWidth > 4.3 )
        {
            FixSize(camWidth);
        }
    }

    void FixSize(float cameraWidth)
    {
        if(cameraWidth < 4.1)
        {
            mainCam.orthographicSize = mainCam.orthographicSize + 0.1f;
        }
        else
        {
            mainCam.orthographicSize = mainCam.orthographicSize - 0.1f;
        }

        ScreenSize();
    }
}
