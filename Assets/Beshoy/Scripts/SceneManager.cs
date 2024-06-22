using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Scenemanager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] GameObject loading_screen;
    [SerializeField] GameObject Menu_screen;
    [SerializeField] UnityEngine.UI.Slider loading_bar;
    
    public void OpenScene()
    {
        Menu_screen.SetActive(false);
        loading_screen.SetActive(true);
        StartCoroutine(OpenAsyncScene());
    }
    IEnumerator OpenAsyncScene() 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loading_bar.value = progress;
            yield return new WaitForSeconds(0.5f);
        }

    }
}
