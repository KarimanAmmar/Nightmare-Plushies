using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator PlayerAnimator;

    [SerializeField] string flootingName;
    [SerializeField] string shootingName;
    [SerializeField] string slashingName;

    [SerializeField] GameEvent Flooting;
    [SerializeField] GameEvent Shooting;
    [SerializeField] GameEvent Slashing;

    bool isFlooting = false;
    bool isShooting = false;

    private void OnEnable()
    {
        Flooting.GameAction += PlayFlooting;
        Shooting.GameAction += PlayShooting;
        Slashing.GameAction += PlaySlashing;
    }
    private void OnDisable()
    {
        Flooting.GameAction -= PlayFlooting;
        Shooting.GameAction -= PlayShooting;
        Slashing.GameAction -= PlaySlashing;
    }
    void PlayFlooting()
    {
        if (isFlooting)
        {
            PlayerAnimator.SetBool(flootingName, true);
            isFlooting= false;
        }
        else
        {
            PlayerAnimator.SetBool(flootingName, false);
            isFlooting = true;
        }
    }
    void PlayShooting()
    {
        if (isShooting)
        {
            PlayerAnimator.SetBool(shootingName, true);
            isShooting = false;
        }
        else
        {
            PlayerAnimator.SetBool(shootingName, false);
            isShooting = true;
        }
    }
    void PlaySlashing()
    {
        PlayerAnimator.SetTrigger(slashingName);
    }
}
