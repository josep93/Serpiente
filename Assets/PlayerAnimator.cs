using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;
    private PlayerScript player;
    private SpriteRenderer sprite;
    private string[,] animation = { { "Idle_E", "Idle_N", "Idle_W", "Idle_S" }, { "Move_E", "Move_N", "Move_W", "Move_S" } };
    private string currentAnimation;

    public PlayerAnimator(PlayerScript player)
    {
        this.animator = player.Animator;
        this.player = player;
        this.sprite = player.Sprite;
    }

    public void Animate()
    {
        Play(animation[player.AnimState,player.Direction]);
        sprite.sortingOrder = -((int)player.transform.position.y);
    }

    private void Play(string animation)
    {
        if (animation != currentAnimation)
        {
            animator.Play(animation);
        }
    }
}
