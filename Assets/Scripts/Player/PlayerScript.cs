using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb2;
    [SerializeField] private float speed = 0.1f;
    private InputSystem input;
    private Vector2 movement, movementAxis;
    [SerializeField] private int direction;
    private Animator animator;
    private PlayerAnimator playerAnimator;
    private int animState = 0;

    public int Direction { get => direction;}
    public int AnimState { get => animState;}

    private void Awake()
    {
        input = new InputSystem();
        input.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Player.Move.performed += ctxMove => MovePlayer(ctxMove);
        input.Player.Move.canceled += ctxNotMove => NotMove();
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(this,animator);
    }

    /// <summary>
    /// Detenemos el movimiento del jugador
    /// </summary>
    private void NotMove()
    {
        movement = Vector2.zero;
        animState = 0;
    }

    /// <summary>
    /// Detectamos la dirección a la que se mueve el juagdor
    /// </summary>
    /// <param name="ctxMove"></param>
    private void MovePlayer(UnityEngine.InputSystem.InputAction.CallbackContext ctxMove)
    {
        movement = ctxMove.ReadValue<Vector2>();
        animState = 1;
        MovementToAxis();
    }

    private void MovementToAxis()
    {
        var absX = Mathf.Abs(movement.x);
        var absY = Mathf.Abs(movement.y);

        if (absY == absX)
        {
            return;
        }
        direction = 0;
        if (absY > absX)
        { direction++; }
        if (direction == 0 && movement.x < 0)
        {
            direction += 2;
        }
        if (direction == 1 && movement.y < 0)
        {
            direction += 2;
        }

        /*
        switch (direction)
        {
            case 0:
                movementAxis = Vector2.right;
                return;
            case 1:
                movementAxis = Vector2.up;
                return;
            case 2:
                movementAxis = Vector2.left;
                return;
            case 3:
                movementAxis = Vector2.down;
                return;
        }
        */
    }

    /// <summary>
    /// Realizamos el movimiento del jugador
    /// </summary>
    private void FixedUpdate()
    {
        rgb2.MovePosition(rgb2.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        playerAnimator.Animate();
    }

}
