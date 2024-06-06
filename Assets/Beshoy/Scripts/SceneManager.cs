using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
