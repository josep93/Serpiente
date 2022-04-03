using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectableScript : MonoBehaviour
{

    [SerializeField] private GameObject proyectile, proyectable;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private bool pushable = false;

    bool initiated = false;

    private Collider2D objectCollider;
    private Animator animator;

    private string[,] idleAnimations = { { "Clam", "Clam", "Clam", "Clam" },
        { "Rock_W", "Rock_S", "Rock_E", "Rock_N" },
        { "Bell_W", "Bell_S", "Bell_E", "Bell_N" } };
    private string[,] deathAnimations = { { "Clam_Death", "Clam_Death", "Clam_Death", "Clam_Death" },
        { "Rock_Death_W", "Rock_Death_S", "Rock_Death_E", "Rock_Death_N" },
        { "Bell_Death_W", "Bell_Death_S", "Bell_Death_E", "Bell_Death_N" } };
    private string currentAnimation;

    [SerializeField] private int type;
    [SerializeField] private int direction;

    // Start is called before the first frame update
    void Start()
    {
        if (!initiated)
        {
            objectCollider = GetComponent<Collider2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            sprite.sortingOrder = -(int)(transform.position.y * 10 + sprite.size.y * 10 / 3);
            SelectSprite();
        }
    }

    public void Initiate(int oldType, int oldDirection)
    {
        objectCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sprite.sortingOrder = -(int)(transform.position.y * 10 + sprite.size.y * 10 / 3);
        initiated = true;
        switch (PlayerScript.current.Direction)
        {
            case 0:
                direction = 2;
                break;
            case 1:
                direction = 3;
                break;
            case 2:
                direction = 0;
                break;
            case 3:
                direction = 1;
                break;
        }
        if (oldType != 0)
        {
            switch (oldDirection)
            {
                case 0:
                    PlayerScript.current.Direction = 2;
                    break;
                case 1:
                    PlayerScript.current.Direction = 3;
                    break;
                case 2:
                    PlayerScript.current.Direction = 0;
                    break;
                case 3:
                    PlayerScript.current.Direction = 1;
                    break;
            }
        }
        else
        {
            PlayerScript.current.Direction = 3;
        }
        type = PlayerScript.current.Helmet;
        PlayerScript.current.Helmet = oldType;
        PlayerScript.current.Raise();
        Play(deathAnimations[type, direction]);
    }

    private void SelectSprite()
    {
        var selection = type;
        if (type == 2)
            selection += direction;
        Play(idleAnimations[type, direction]);
    }

    public void Collide(GameObject gameobject, int direction)
    {
        if (type == 2 && this.direction != direction)
        {
            if (pushable)
            {
                Vector3 movVector = Vector3.zero;
                switch (direction)
                {
                    case 0:
                        movVector = Vector2.left;
                        break;

                    case 1:
                        movVector = Vector2.down;
                        break;

                    case 2:
                        movVector = Vector2.right;
                        break;

                    case 3:
                        movVector = Vector2.up;
                        break;
                }
                movVector *= 0.25f;
                transform.position -= movVector;
            }
            Bounce(gameobject, direction);
        }
        else
        {
            Possess(gameobject);
        }
        //PlayerScript.current.Unlock();
    }

    public void Possess(GameObject gameobject)
    {
        PlayerScript.current.Unlock();
        Destroy(gameobject);
        var newProy = Instantiate(proyectable, PlayerScript.current.transform.position, PlayerScript.current.transform.rotation);
        var newProyScrip = newProy.GetComponent<ProyectableScript>();
        newProyScrip.Initiate(type, direction);
        PlayerScript.current.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void Bounce(GameObject gameobject, int direction)
    {
        objectCollider.enabled = false;
        StartCoroutine("ColliderReactivation");
        var proyectileInstance = Instantiate(proyectile, gameobject.transform.position, Quaternion.identity);
        var proyectileScript = proyectileInstance.GetComponent<ProyectileScript>();
        var posY = transform.position.y - sprite.size.y / 4;
        int exitDirection = 0;

        switch (direction % 2)
        {
            case 0:
                exitDirection = gameobject.transform.position.y < posY ? 3 : 1;
                break;
            case 1:
                exitDirection = gameobject.transform.position.x < transform.position.x ? 2 : 0;
                break;

        }
        //var direction = gameobject.transform.position.y < transform.position.y ? 3:1 ;
        proyectileScript.Initialize(exitDirection);
    }

    private IEnumerator ColliderReactivation()
    {
        yield return new WaitForSeconds(0.5f);
        objectCollider.enabled = true;
    }

    private void Play(string animation)
    {
        if (animation != currentAnimation)
        {
            animator.Play(animation);
            currentAnimation = animation;
        }
    }
}
