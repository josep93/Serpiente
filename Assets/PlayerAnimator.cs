using UnityEngine;

public class PlayerAnimator
{
    private Animator animator;
    private PlayerScript player;
    private SpriteRenderer sprite;
    private bool forced, endpoint = false;
    private string[,,] animation = { { { "Idle_E", "Idle_N", "Idle_W", "Idle_S" },
            { "Move_E", "Move_N", "Move_W", "Move_S" },
        { "Throw_E", "Throw_N", "Throw_W", "Throw_S" },
        { "Raise", "Raise", "Raise", "Raise" }},
        {{"Rock_Idle_E", "Rock_Idle_N", "Rock_Idle_W", "Rock_Idle_S" },
            { "Rock_Move_E", "Rock_Move_N", "Rock_Move_W", "Rock_Move_S" },
        {"Rock_Throw_E", "Rock_Throw_N", "Rock_Throw_W", "Rock_Throw_S" } ,
        { "Rock_Raise", "Rock_Raise", "Rock_Raise", "Rock_Raise" }},
        {{"Bell_Idle_E", "Bell_Idle_N", "Bell_Idle_W", "Bell_Idle_S" },
            { "Bell_Move_E", "Bell_Move_N", "Bell_Move_W", "Bell_Move_S" },
        {"Bell_Throw_E", "Bell_Throw_N", "Bell_Throw_W", "Bell_Throw_S" },
        { "Bell_Raise_E", "Bell_Raise_N", "Bell_Raise_W", "Bell_Raise_S" } } };
    private string endAnimation = "Clam_Death";
    private string currentAnimation;


    public PlayerAnimator(PlayerScript player)
    {
        this.animator = player.Animator;
        this.player = player;
        this.sprite = player.Sprite;
    }

    public void Animate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).loop && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            forced = false;
        }
        if (!forced && !endpoint)
        {
            Play(animation[player.Helmet, player.AnimState, player.Direction]);
            sprite.sortingOrder = -(int)(player.transform.position.y * 10);
        }
    }

    public void ThrowUsed()
    {
        PlayToTheEnd(animation[player.Helmet, 2, player.Direction]);
    }

    public void Raise()
    {
        PlayToTheEnd(animation[player.Helmet, 3, player.Direction]);
    }

    private void Play(string animation)
    {
        if (animation != currentAnimation && !forced)
        {
            animator.Play(animation);
            currentAnimation = animation;
        }
    }

    private void PlayToTheEnd(string animation)
    {
        forced = true;
        if (animation != currentAnimation)
        {
            animator.Play(animation);
            currentAnimation = animation;
        }
    }

    public void CurtainFall()
    {
        forced = true;
        endpoint = true;
        animator.Play(endAnimation);
        currentAnimation = (endAnimation);
    }
}
