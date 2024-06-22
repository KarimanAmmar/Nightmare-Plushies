using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCamerasTransition : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] virtualCamera;
    [SerializeField] GameObject[] Slides;

    int currentCam = 0;

    int on = 100;
    int off = 0;
    private void Start()
    {
        virtualCamera[currentCam].Priority = on;
        Slides[currentCam].SetActive(true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            CamIn();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CamOut();
        }
    }
    private void CamIn()
    {
        OffAllCams();
        if (currentCam < virtualCamera.Length - 1)
        {
            currentCam++;
        }
        virtualCamera[currentCam].Priority = on;
        Slides[currentCam].SetActive(true);
    }
    private void CamOut()
    {
        OffAllCams();
        if (currentCam > 0)
        {
            currentCam--;
        }
        virtualCamera[currentCam].Priority = on;
        Slides[currentCam].SetActive(true);
    }
    void OffAllCams()
    {
        for (int i = 0; i < virtualCamera.Length; i++)
            virtualCamera[i].Priority = off;

        for (int i = 0; i < Slides.Length; i++)
            Slides[i].SetActive(false);
    }
}
