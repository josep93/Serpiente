using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rgb2;
    [SerializeField] private float speed = 2.5f;
    private InputSystem input;
    private Vector2 movement, movementAxis;
    [SerializeField] private int direction;
    [SerializeField] private int helmet;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerAnimator playerAnimator;
    private int animState = 0;

    /// <summary>
    /// 0 = stunlocked
    /// 1 = free
    /// </summary>
    private int state = 1;

    [SerializeField] private GameObject proyectile;

    [SerializeField] public static PlayerScript current;

    public int Direction { get => direction; }
    public int AnimState { get => animState; }
    public SpriteRenderer Sprite { get => sprite; }
    public Animator Animator { get => animator; }
    public int Helmet { get => helmet; }

    private void Awake()
    {
        input = new InputSystem();
        input.Enable();
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Player.Move.performed += ctxMove => MovePlayer(ctxMove);
        input.Player.Move.canceled += ctxNotMove => NotMove();
        input.Player.Run.performed += ctxRun => Run(true);
        input.Player.Run.canceled += ctxWalk => Run(false);
        input.Player.Action.performed += ctxAction => ActionPlayer();
        input.Player.Throw.performed += ctxThrow => Throw();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnimator = new PlayerAnimator(this);
    }

    private void ActionPlayer()
    {
        
    }

    /// <summary>
    /// Incrementamos o decrementamos la velocidad del jugador
    /// </summary>
    /// <param name="run"></param>
    private void Run(bool run)
    {
        if (run)
        {
            speed = 5f;
            return;
        }

        speed = 2.5f;
    }

    /// <summary>
    /// Detenemos el movimiento del jugador
    /// </summary>
    private void NotMove()
    {
        movement = Vector2.zero;
        animState = 0;
    }

    private void Throw()
    {
        if (state == 1)
        {
            var proyectilePosition = transform.position - new Vector3(0, sprite.size.y / 10, 0);
            switch (direction)
            {
                case 1:
                    proyectilePosition = transform.position + new Vector3(sprite.size.x / 20, 0);
                    break;
                case 3:
                    proyectilePosition = transform.position + new Vector3(sprite.size.x / 20, 0);
                    break;
            }
            var proyectileInstance = GameObject.Instantiate(proyectile, proyectilePosition, Quaternion.identity);
            var proyectileScript = proyectileInstance.GetComponent<ProyectileScript>();
            proyectileScript.Initialize(direction);
            playerAnimator.ThrowUsed();
            state = 0;
        }
    }

    public void Unlock()
    {
        state = 1;
    }

    /// <summary>
    /// Detectamos la dirección a la que se mueve el juagdor
    /// </summary>
    /// <param name="ctxMove"></param>
    private void MovePlayer(UnityEngine.InputSystem.InputAction.CallbackContext ctxMove)
    {
        if (state == 1)
        {
            movement = ctxMove.ReadValue<Vector2>();
            animState = 1;
            MovementToAxis();
        }
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
