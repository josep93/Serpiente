using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;
    private PlayerScript player;
    private string[,] animation = { { "Idle_E", "Idle_N", "Idle_W", "Idle_S" }, { "Move_E", "Move_N", "Move_W", "Move_S" } };
    private string currentAnimation;

    public PlayerAnimator(PlayerScript player, Animator animator)
    {
        this.animator = animator;
        this.player = player;
    }

    public void Animate()
    {
        Play(animation[player.AnimState,player.Direction]);
    }

    private void Play(string animation)
    {
        if (animation != currentAnimation)
        {
            animator.Play(animation);
        }
    }
}
