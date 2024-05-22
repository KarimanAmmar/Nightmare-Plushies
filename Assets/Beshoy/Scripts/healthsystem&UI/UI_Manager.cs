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
    [SerializeField] private Upgrade_list_event List_Event;
    //when level up
    [SerializeField] private GameEvent UI_Activate_Event;
    [SerializeField] private GameEvent UI_Deactivate_Event;
    //
    private void OnEnable()
    {
        int_Event.RegisterListener(Update_Count);
        HP_UI_event.RegisterListener(Update_Hp);
        List_Event.RegisterListener(DisplayOptions);
        UI_Activate_Event.GameAction += ActivateUpgradesPanel;
        UI_Deactivate_Event.GameAction += DeactivateUpgradesPanel;
    }
    private void OnDisable()
    {
        int_Event.UnregisterListener(Update_Count);
        HP_UI_event.UnregisterListener(Update_Hp);
        List_Event.UnregisterListener(DisplayOptions);
        UI_Activate_Event.GameAction -=ActivateUpgradesPanel;
        UI_Deactivate_Event.GameAction -= DeactivateUpgradesPanel;
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
    private void ActivateUpgradesPanel()
    {
        MovementPanel.SetActive(false);

        UpgradePanel.SetActive(true);

    }
    private void DeactivateUpgradesPanel()
    {
        MovementPanel.SetActive(true);

        UpgradePanel.SetActive(false);
    }
    private void DisplayOptions(Upgrade[] upgrades)
    {

    }
}
