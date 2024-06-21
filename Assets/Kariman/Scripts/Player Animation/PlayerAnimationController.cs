using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator PlayerAnimator;

    [SerializeField] string floatingName;
    [SerializeField] string shootingName;
    [SerializeField] string slashingName;

    [SerializeField] GameEvent Floating;
    [SerializeField] GameEvent Shooting;
    [SerializeField] GameEvent Slashing;
    //event to invoke to ply the slash effects
    [SerializeField] GameEvent SlashEffect;
    [SerializeField] AudioClip slachClip;
    //
    bool isFloating = true;
    bool isShooting = true;

    private void OnEnable()
    {
        Floating.GameAction += PlayFlooting;
        Shooting.GameAction += PlayShooting;
        Slashing.GameAction += PlaySlashing;
        SlashEffect.GameAction += PlaySlashSfX;
    }
    private void OnDisable()
    {
        Floating.GameAction -= PlayFlooting;
        Shooting.GameAction -= PlayShooting;
        Slashing.GameAction -= PlaySlashing;
        SlashEffect.GameAction -= PlaySlashSfX;
    }
    void PlayFlooting()
    {
        if (isFloating)
        {
            PlayerAnimator.SetBool(floatingName, true);
            isFloating= false;
        }
        else
        {
            PlayerAnimator.SetBool(floatingName, false);
            isFloating = true;
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

    void PlaySlashSfX()
    {
        AudioManager.Instance.PlySfx(slachClip);
    }
}
