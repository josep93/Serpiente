using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaPlayer : MonoBehaviour
{
    private InputSystem input;

    private void Awake()
    {
        // Inicializamos el sistema de inputs y lo activamos
        input = new InputSystem();
        input.Player.Enable();

    }

    private void Start()
    {
        // Asignar el evento a un método del script
        input.Player.Accion.performed += ctxAccion => mostrarAviso();
    }


    private void mostrarAviso()
    {
        Debug.Log("Me has pulsado");
    }

}
