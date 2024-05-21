
using UnityEngine;
using UnityEngine.Events;
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
    movementspeed,
    damageReduction
}
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Upgrade[] Upgrades_List;
    [SerializeField] private HealthSystem PlyrHelth;
    [SerializeField] private GameEvent UI_Activate_Event;

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
        UpggradeValues[] selected = (UpggradeValues[])upgrade.getupgrade();
        foreach (UpggradeValues v in selected)
        {
            Logging.Log($"your{v.GetUpgradeType()}is incresed by{v.GetValue()/100}%");
            apply_Upgrade(v);
        }
        
    }

    private void apply_Upgrade(UpggradeValues values)
    {
        switch (values.GetUpgradeType())
        {
            case UpgradeType.health:
                PlyrHelth.UpgradeHealth(values.GetValue());
            break;
            case UpgradeType.movementspeed:
                Logging.Log($"upgrading speed");
            break;
            case UpgradeType.damageReduction:
                PlyrHelth.Upgrade_DamageReduction(values.GetValue());
            break;
        }
    }
    private void draw_Upgrades()
    {
        Upgrade[] selected_upgrades= new Upgrade[3];
        for (int i = 0; i < selected_upgrades.Length; i++)
        {
            selected_upgrades[i]= Upgrades_List[Random.Range(0,Upgrades_List.Length)];
        }
    }
    private void StartUI()
    {
        Logging.Log("UI activated");
    }
}
