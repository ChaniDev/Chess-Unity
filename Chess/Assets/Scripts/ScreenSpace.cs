using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpace : MonoBehaviour
{
    [SerializeField] private Camera mainCam;


    void Start()
    {
        float screenRatio = mainCam.aspect;

        print(screenRatio);
    }
}
