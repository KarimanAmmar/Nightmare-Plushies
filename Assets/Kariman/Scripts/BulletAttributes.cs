using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BUlletType
{
    blue,
    green, 
    red
}

[CreateAssetMenu(menuName = "Attributes/Bullet Attributes", order = 2)]
public class BulletAttributes : ScriptableObject
{
    [SerializeField] string B_Name;
    [SerializeField] string B_Description;
    [SerializeField] int B_Damage;
    [SerializeField] int B_Speed;
    [SerializeField] GameObject B_Prefab;
    [SerializeField] BUlletType B_type;
}
