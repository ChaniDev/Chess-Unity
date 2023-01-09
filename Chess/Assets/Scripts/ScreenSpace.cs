using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpace : MonoBehaviour
{
    [SerializeField] private Camera mainCam;


    void Start()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;
    }
}
