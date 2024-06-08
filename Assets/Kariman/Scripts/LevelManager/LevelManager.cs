using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] arenas;
    [SerializeField] Transform[] playerNewPositions;
    [SerializeField] Transform playerCurrentPos;
    [SerializeField] GameEvent levelCompleted;
    [SerializeField] AudioClip[] LevelMusic;
    private WaitForSeconds waitTime;

    int currentArena;
    [SerializeField] CharacterController characterController;

    private void Start()
    {
        currentArena = 0;
        waitTime = new WaitForSeconds(1f);
        StartCoroutine(playMusic());
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
            yield return waitTime;
            arenas[currentArena - 1].SetActive(false);
            
        }
    }
    IEnumerator playMusic()
    {
        for (int i=0;i<LevelMusic.Length;)
        {
            AudioManager.Instance.PlyMusic(LevelMusic[i]);
            yield return new WaitForSeconds(LevelMusic[i].length);
            i ++;
            if(i == LevelMusic.Length)
            {
                i= 0;
            }
        }
    }
}
