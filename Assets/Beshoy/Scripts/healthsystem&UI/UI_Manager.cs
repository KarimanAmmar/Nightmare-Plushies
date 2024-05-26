using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// /new things added :
/// removing the coint UI count because its no longer needed
/// create a function to display a progress bar for the level up
/// </summary>
public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Image HpBar;
    [SerializeField] private Image LevelBar;
    [SerializeField] private Float_event HP_UI_event;
    [SerializeField] private Float_event LevelUP_UI_Event;
    [SerializeField] private Upgrade_Option[] options;
    [SerializeField] private GameObject MovementPanel;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private Upgrade_list_event List_Event;
    /// <summary>
    /// i referance the upgrade manager here to add to select upgrade function for each button
    /// </summary>
    [SerializeField] private UpgradeManager UpgradeManager;
    //when level up
    [SerializeField] private GameEvent UI_Activate_Event;
    [SerializeField] private GameEvent UI_Deactivate_Event;
    //
    private void OnEnable()
    {
        
        HP_UI_event.RegisterListener(Update_Hp);
        LevelUP_UI_Event.RegisterListener(Update_Level);
        List_Event.RegisterListener(DisplayOptions);
        UI_Activate_Event.GameAction += ActivateUpgradesPanel;
        UI_Deactivate_Event.GameAction += DeactivateUpgradesPanel;
    }
    private void OnDisable()
    {
        
        HP_UI_event.UnregisterListener(Update_Hp);
        LevelUP_UI_Event.UnregisterListener(Update_Level);
        List_Event.UnregisterListener(DisplayOptions);
        UI_Activate_Event.GameAction -=ActivateUpgradesPanel;
        UI_Deactivate_Event.GameAction -= DeactivateUpgradesPanel;
    }
    private void Start()
    {
        LevelBar.fillAmount = 0;
    }
    
    private void Update_Hp(float amount)
    {
        HpBar.fillAmount=amount;
    }
    private void Update_Level(float amount)
    {
       LevelBar.fillAmount=amount;
    }
    private void ActivateUpgradesPanel()
    {
        MovementPanel.SetActive(false);
        Time.timeScale = 0;
        UpgradePanel.SetActive(true);

    }
    private void DeactivateUpgradesPanel()
    {
        MovementPanel.SetActive(true);
        Time.timeScale = 1;
        UpgradePanel.SetActive(false);
    }
    private void DisplayOptions(Upgrade[] upgrades)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            options[i].setType(upgrades[i].GetUpgradeType());
            options[i].setValue(upgrades[i].GetValue());

            
            options[i].GetButton().onClick.RemoveAllListeners();

            Upgrade currentUpgrade = upgrades[i];

            options[i].GetButton().onClick.AddListener(() => UpgradeManager.select_upgrade(currentUpgrade));
        }

    }
}
