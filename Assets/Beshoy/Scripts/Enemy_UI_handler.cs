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
    public void Update_UI(float amount)
    {
        hp_bar.fillAmount=amount;
    }
}
