
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// in this script :
/// handle how the upgrades are selected for each the player can upgrade 
/// apply the upgrade by invoking an event that transfer data to a script 
/// that hold all the players data that can be upgraded(coming soon)
/// listens to an event to trigger the selection process of the upgrades
/// after the play chooses the upgrade the manager should remove the upgrade he chose 
/// or decrease the chance to appear again
/// there are 2 type of upgrade:
/// the ones that appear during the level (normal)
/// and the ones that appear after deafeatinng the boss of the level(higher or rare)
/// </summary>

public enum UpgradeType
{
    health,
    speed,
    damageReduction,
    projectiles,
    GhostFriend
}
[Serializable]
public struct Upgrade_Option
{
   [SerializeField] private Text UpgradeTypeName;
   [SerializeField] private Text UpgradeValue;
   [SerializeField] private Button UpgradeButton;
   // private Image UpgradeImage;
    public void setType(UpgradeType type)
    {
        UpgradeTypeName.text = type.ToString();
    }
    public void setValue(string value)
    {
        UpgradeValue.text = value;

    }
    public Button GetButton()
    {
       return UpgradeButton;
    }
    //public void setUpgradeImage(Sprite Icon) 
    //{ 
    //    UpgradeImage.sprite= Icon;
    //}

}
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Upgrade[] Upgrades_List;
    [SerializeField] private HealthSystem PlyrHelth;
    [SerializeField] private CharacterMovementManager movementManager;
    [SerializeField] private GameEvent UI_Activate_Event;
    [SerializeField] private GameEvent UI_Deactivate_Event;
    [SerializeField] private GameEvent projectile_event;
    [SerializeField] private GameEvent GhostFriend_event;
    [SerializeField] private Upgrade_list_event List_Event;

    private void OnEnable()
    {
        UI_Activate_Event.GameAction += StartUI;
    }

    

    private void OnDisable()
    {
        UI_Activate_Event.GameAction -= StartUI;
    }
    public void select_upgrade(Upgrade upgrade)
    {      
        //Logging.Log($"your{upgrade.GetUpgradeType()}is incresed by{upgrade.GetValue()}%");
        apply_Upgrade(upgrade);
        UI_Deactivate_Event.GameAction.Invoke();
    }

    private void apply_Upgrade(Upgrade upgrade)
    {
        switch (upgrade.GetUpgradeType())
        {
            case UpgradeType.health:
                PlyrHelth.UpgradeHealth(upgrade.GetValue());
            break;
            case UpgradeType.speed:
                movementManager.UpgradeSpeed(upgrade.GetValue());
            break;
            case UpgradeType.damageReduction:
                PlyrHelth.Upgrade_DamageReduction(upgrade.GetValue());
            break;
            case UpgradeType.projectiles:
                projectile_event.GameAction.Invoke();
            break;
            case UpgradeType.GhostFriend:
                GhostFriend_event.GameAction.Invoke();
            break;
        }
    }
    private Dictionary<UpgradeType, bool> selectedUpgrades = new Dictionary<UpgradeType, bool>();
    private Upgrade[] draw_Upgrades()
    {
        
        Upgrade[] selected_upgrades = new Upgrade[3];
       

        selectedUpgrades.Clear(); // Clear selections for each new draw
        if (Upgrades_List.Length <= 3)
        {
            for (int i = 0; i < Upgrades_List.Length; i++)
            {
                selected_upgrades[i] = Upgrades_List[i];
            }
        }
        else
        {
            for (int i = 0; i < selected_upgrades.Length; i++)
            {
                UpgradeType randomType;
                do
                {
                    randomType = (UpgradeType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(UpgradeType)).Length);
                }
                while (selectedUpgrades.ContainsKey(randomType)); // Keep looping if type already selected

                selectedUpgrades.Add(randomType, true); // Mark type as selected

                selected_upgrades[i] = Upgrades_List.Where(upgrade => upgrade.GetUpgradeType() == randomType).FirstOrDefault();
                Logging.Log($"{selected_upgrades[i]}");
                // Select first upgrade of the chosen randomType from Upgrades_List
            }
        }

        return selected_upgrades;
    
    }
    private void StartUI()
    {
        List_Event.Raise(draw_Upgrades());
    }
}
