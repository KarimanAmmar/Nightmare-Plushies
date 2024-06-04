using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] arenas;
    [SerializeField] Transform[] playerNewPositions;
    [SerializeField] Transform playerCurrentPos;
    [SerializeField] GameEvent levelCompleted;
    [SerializeField] CharacterController characterController;

    private WaitForSeconds waitTime;
    int currentArena;

    private void Start()
    {
        currentArena = 0;
        waitTime = new WaitForSeconds(2);
    }

    private void OnEnable()
    {
        levelCompleted.GameAction += StartChange;
    }

    private void OnDisable()
    {
        levelCompleted.GameAction -= StartChange;
    }
    void StartChange()
    {
        StartCoroutine(ChangeLevel());
    }
    IEnumerator ChangeLevel()
    {
        yield return waitTime;
        for (int i = 0; i < arenas.Length; i++)
        {
            if (arenas[i].activeSelf)
            {
                arenas[i].SetActive(false);
            }
        }
        if (currentArena < arenas.Length - 1)
        {
            currentArena++;
        }
        // Activate the current arena
        if (currentArena < arenas.Length)
        {
            arenas[currentArena].SetActive(true);
            if (currentArena - 1 < playerNewPositions.Length)
            {
                if (characterController != null)
                {
                    characterController.enabled = false;
                    playerCurrentPos.position = playerNewPositions[currentArena - 1].position;
                    characterController.enabled = true;
                }
                else
                {
                    playerCurrentPos.position = playerNewPositions[currentArena - 1].position;
                }
            }
        }
    }
}
