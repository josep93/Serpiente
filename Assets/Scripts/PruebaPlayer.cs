using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb2;
    [SerializeField] private float speed = 0.1f;
    private Vector2 posticion;
    private InputSystem input;
    private Vector2 movePosition = Vector2.zero;

    private void Awake()
    {
        // Inicializamos el sistema de inputs y lo activamos
        input = new InputSystem();
        input.Player.Enable();
    }

    private void Start()
    {
        posticion = transform.position;
        
        // Asignar el evento a un método del script
        input.Player.Move.performed += ctxAccion => move(ctxAccion);
        input.Player.Move.canceled += ctxNotMove => NotMove();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    /// <summary>
    /// Detenemos el movimmiento del elemento
    /// </summary>
    private void NotMove()
    {
        movePosition = Vector2.zero;
    }

    /// <summary>
    /// Detectamos el botón pulsado por el jugador
    /// </summary>
    /// <param name="ctxAccion"></param>
    private void move(UnityEngine.InputSystem.InputAction.CallbackContext ctxAccion)
    {
        movePosition = ctxAccion.ReadValue<Vector2>();
    }

    /// <summary>
    /// Aplicamos el movimiento al elemeto
    /// </summary>
    private void FixedUpdate()
    {
        rgb2.MovePosition(rgb2.position + movePosition * speed);
    }

}
