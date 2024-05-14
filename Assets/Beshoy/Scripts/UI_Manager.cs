using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text count_Ui;
    [SerializeField] private int_Event int_Event;
    private void OnEnable()
    {
        int_Event.RegisterListener(Update_Count);
    }
    private void OnDisable()
    {
        int_Event.UnregisterListener(Update_Count);
    }
    private void Start()
    {
        count_Ui.text = "coins: 0";
    }
    private void Update_Count(int count)
    {
        count_Ui.text = $"coins: {count}";
    }
}
