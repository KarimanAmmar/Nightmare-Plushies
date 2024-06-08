using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnding : MonoBehaviour
{
    [SerializeField] Animator Fading;
    [SerializeField] GameEvent levelcomp;
    [SerializeField] string fadeInAnimName;
    [SerializeField] string fadeOutAnimName;
    private WaitForSeconds waitTime;

    private void Start() => waitTime = new WaitForSeconds(1.8f);
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.PlayerTag))
        {
            levelcomp.GameAction?.Invoke();
            Fading.SetBool(fadeInAnimName, true);
            Fading.SetBool(fadeOutAnimName, false);
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut()
    {
        yield return waitTime;
        Fading.SetBool(fadeOutAnimName, true);
        Fading.SetBool(fadeInAnimName, false);
    }
}