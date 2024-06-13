using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// this is a scriptable object that will hold :
/// the values of the upgrades that will be added to the player stats 
/// image & description of the upgrade itself(optional)
/// </summary>

[Serializable]
public struct UpggradeValues
{
   [SerializeField] private UpgradeType type;
   [SerializeField] private float value;
   public UpgradeType GetUpgradeType() { return type; }
   public float GetValue() { return value; }
   public UpgradeType Type { get { return type; } }
    
}


[CreateAssetMenu(menuName = "upgrades/upgrade", order = 0)]
public class Upgrade : ScriptableObject
{
   [SerializeField] private UpggradeValues Upggrade;

   public UpgradeType GetUpgradeType()
   {
        return Upggrade.GetUpgradeType();
   }
    public float GetValue()
    {
        return Upggrade.GetValue();
    }
    public string GetString()
    {
        switch(GetUpgradeType())
        {
            case UpgradeType.projectiles:
                return $"+{GetValue()} Projectile";
            case UpgradeType.health:
                return $"+{GetValue()}%";
            case UpgradeType.speed:
                return $"+{GetValue()}%";
            case UpgradeType.damageReduction:
                return $"+{GetValue()}%";    
            case UpgradeType.GhostFriend:
                return $"+{GetValue()} Ghost";
                
            default:
                return$"not found";
        }
    }
}
