using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this script is used to update the UI health bar of the enemy
/// </summary>
public class Enemy_UI_handler : MonoBehaviour
{
    [SerializeField] private Image hp_bar;
    [SerializeField] private RectTransform canvas;
    private Camera cam;
    private void Start()
    {
        hp_bar.fillAmount = 1;
        cam = Camera.main;
        //canvas.transform.rotation = Quaternion.LookRotation(cam.transform.position);
        Logging.Log(cam);
        canvas.LookAt(cam.transform.position);
    }
    private void LateUpdate()
    {
        // Update the rotation of the canvas to face the camera every frame
        canvas.LookAt(canvas.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
    public void Update_UI(float amount)
    {
        hp_bar.fillAmount=amount;
    }
}
