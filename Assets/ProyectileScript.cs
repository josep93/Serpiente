using UnityEngine;

public class ProyectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private int direction;
    [SerializeField] private int velocity;
    [SerializeField] private Sprite[] sprites;

    private void Start()
    {

    }

    public void Initialize(int direction)
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        this.direction = direction;
        if (direction == 1)
        {
            sprite.sortingOrder = PlayerScript.current.Sprite.sortingOrder - 1;
        }
        else{
            sprite.sortingOrder = PlayerScript.current.Sprite.sortingOrder;
        }
        rb.velocity = new Vector2(-10, 0);
        switch (direction)
        {
            case 0:
                rb.velocity = Vector2.right * velocity;
                sprite.sprite = sprites[0];
                break;
            case 1:
                rb.velocity = Vector2.up * velocity;
                sprite.sprite = sprites[1];
                break;
            case 2:
                rb.velocity = Vector2.left * velocity;
                sprite.sprite = sprites[0];
                break;
            case 3:
                rb.velocity = Vector2.down * velocity;
                sprite.sprite = sprites[1];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Proyectable")
        {
            ProyectableScript proyectableScript = collision.GetComponent<ProyectableScript>();
            proyectableScript.Collide(gameObject, direction);
            Destroy(this.gameObject);
            return;
        }
        else if (collision.gameObject.tag != "PureTrigger")
        {
            PlayerScript.current.Unlock();
            Destroy(this.gameObject);
        }
    }
}
