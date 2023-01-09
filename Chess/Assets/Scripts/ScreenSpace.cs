using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpace : MonoBehaviour
{
    [SerializeField] private Camera mainCam;


    void Start()
    {
        ScreenSize();
    }

    void ScreenSize()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        if(camWidth < 4.15 || camWidth > 4.25 )
        {
            FixSize(camWidth);
        }
    }

    void FixSize(float cameraWidth)
    {
        if(cameraWidth < 4.15)
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
