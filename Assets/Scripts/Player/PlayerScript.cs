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
    [SerializeField] private int helmet = 0;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerAnimator playerAnimator;
    private int animState = 0;

    [SerializeField] private GameObject actionColliderGO;
    [SerializeField] private Collider2D actionCollider;

    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip sonidoLanzamiento;
    [SerializeField] private AudioClip sonidoPasos;

    /// <summary>
    /// 0 = stunlocked
    /// 1 = free
    /// </summary>
    public int state = 1;

    [SerializeField] private GameObject proyectile;

    [SerializeField] public static PlayerScript current;

    public int Direction { get => direction; set => direction = value; }
    public int AnimState { get => animState;}
    public SpriteRenderer Sprite { get => sprite; }
    public Animator Animator { get => animator; }
    public int Helmet { get => helmet; set => helmet = value; }

    private void Awake()
    {
        input = new InputSystem();
        input.Enable();
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Player.Run.performed += ctxRun => Run(true);
        input.Player.Run.canceled += ctxWalk => Run(false);
        input.Player.Action.performed += ctxAction => ActionPlayer();
        input.Player.Throw.performed += ctxThrow => Throw();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnimator = new PlayerAnimator(this);
        sound.pitch = 1.7f;
    }

    private void ActionPlayer()
    {
        actionCollider.enabled = true;
        StartCoroutine("DeactivateActivate");
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
            sound.pitch = 2;
            return;
        }
        sound.pitch = 1.7f;
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
            sound.clip = sonidoLanzamiento;
            sound.Play();
            playerAnimator.ThrowUsed();
            state = 0;
            StartCoroutine("TimedUnlock");
            NotMove();
        }
    }

    public void Unlock()
    {
        state = 1;
    }

    /// <summary>
    /// Detectamos la dirección a la que se mueve el juagdor
    /// </summary>
    private void MovePlayer()
    {
        if (state == 1)
        {
            movement = input.Player.Move.ReadValue<Vector2>();
            if (movement.magnitude > 0.1)
            {
                if (!sound.isPlaying)
                {
                    sound.clip = sonidoPasos;
                    sound.Play();
                }
                animState = 1;
                MovementToAxis();
            }
            else
            {
                if (sound.isPlaying)
                {
                    sound.Stop();
                }
                animState = 0;
            }
        }
    }

    public void Die()
    {

    }

    private void MovementToAxis()
    {
        var absX = Mathf.Abs(movement.x);
        var absY = Mathf.Abs(movement.y);

        if (absX == 0 && absY == 0)
        {

        }

        if (absY == absX)
        {
            rgb2.MovePosition(rgb2.position + movement * speed * Time.fixedDeltaTime);
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

        rgb2.MovePosition(rgb2.position + movement * speed * Time.fixedDeltaTime);

    }

    /// <summary>
    /// Realizamos el movimiento del jugador
    /// </summary>
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        playerAnimator.Animate();
        PlaceActionCollider();
    }

    private void PlaceActionCollider()
    {
        switch (direction)
        {
            case 0:
                actionColliderGO.transform.position = transform.position +  Vector3.right;
                break;
            case 1:
                actionColliderGO.transform.position = transform.position + Vector3.up;
                break;
            case 2:
                actionColliderGO.transform.position = transform.position + Vector3.left;
                break;
            case 3:
                actionColliderGO.transform.position = transform.position + Vector3.down;
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activable")
        {
            var activable = collision.gameObject.GetComponent<Activable>();
            activable.Activate();
        }
    }

    public void EndIt()
    {
        state = 0;
        playerAnimator.CurtainFall();
    }

    public void Raise()
    {
        playerAnimator.Raise();
    }

    private IEnumerator TimedUnlock()
    {
        yield return new WaitForSeconds(0.5f);
        Unlock();
    }
    private IEnumerator DeactivateActivate()
    {
        yield return new WaitForSeconds(0.1f);
        actionCollider.enabled = false;
    }
}
