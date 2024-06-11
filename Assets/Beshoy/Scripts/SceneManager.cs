using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] GameObject loading_screen;
    [SerializeField] GameObject Menu_screen;
    [SerializeField] Slider loading_bar;
    
    public void OpenScene()
    {
        Menu_screen.SetActive(false);
        StartCoroutine(OpenAsyncScene());
    }
    IEnumerator OpenAsyncScene() 
    {
        loading_screen.SetActive(true);
        yield return new WaitForSeconds(2);
        
        while (loading_bar.value!=1)
        {
            loading_bar.value += 0.1f;
            yield return null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    }
}
