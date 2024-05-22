using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text count_Ui;
    [SerializeField] private Image hp_bar;
    [SerializeField] private int_Event int_Event;
    [SerializeField] private Float_event HP_UI_event;
    [SerializeField] private Upgrade_Option[] options;
    [SerializeField] private GameObject MovementPanel;
    [SerializeField] private GameObject UpgradePanel;
    private void OnEnable()
    {
        int_Event.RegisterListener(Update_Count);
        HP_UI_event.RegisterListener(Update_Hp);
    }
    private void OnDisable()
    {
        int_Event.UnregisterListener(Update_Count);
        HP_UI_event.UnregisterListener(Update_Hp);
    }
    private void Start()
    {
        count_Ui.text = "coins: 0";
    }
    private void Update_Count(int count)
    {
        count_Ui.text = $"coins: {count}";
    }
    private void Update_Hp(float amount)
    {
        hp_bar.fillAmount=amount;
    }
}
