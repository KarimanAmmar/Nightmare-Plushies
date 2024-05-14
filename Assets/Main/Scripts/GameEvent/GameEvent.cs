using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    UnityAction gameAction;
    public UnityAction GameAction { get { return gameAction; } set { gameAction = value; } }

}
