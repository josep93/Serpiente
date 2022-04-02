using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb2;
    [SerializeField] private float speed = 0.1f;
    private InputSystem input;
    private Vector2 movement;

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
    }

    /// <summary>
    /// Detenemos el movimiento del jugador
    /// </summary>
    private void NotMove()
    {
        movement = Vector2.zero;
    }

    /// <summary>
    /// Detectamos la dirección a la que se mueve el juagdor
    /// </summary>
    /// <param name="ctxMove"></param>
    private void MovePlayer(UnityEngine.InputSystem.InputAction.CallbackContext ctxMove)
    {
        movement = ctxMove.ReadValue<Vector2>();
    }

    /// <summary>
    /// Realizamos el movimiento del jugador
    /// </summary>
    private void FixedUpdate()
    {
        rgb2.MovePosition(rgb2.position + movement * speed);
    }

}
