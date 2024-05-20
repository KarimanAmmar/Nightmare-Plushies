using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] arenas;
    [SerializeField] Transform[] playernewPosition;
    [SerializeField] Transform playerCurrentPos;
    [SerializeField] GameEvent levelCompleted;

    int currentArena;

    private void Start()
    {
        currentArena = 0;
    }
    private void OnEnable()
    {
        levelCompleted.GameAction += ChangeLevel;
    }
    private void OnDisable()
    {
        levelCompleted.GameAction -= ChangeLevel;
    }
    void ChangeLevel()
    {
        for(int i = 0; i < arenas.Length; i++)
        {
            if (arenas[i].activeSelf) 
            { 
                arenas[i].gameObject.SetActive(false);
            }  
        }
        if(currentArena < arenas.Length)
        {
            currentArena++;
        }
        arenas[currentArena].gameObject.SetActive(true);
        playerCurrentPos.position = playernewPosition[currentArena -1].position;
    }
}
