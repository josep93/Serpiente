using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectableScript : MonoBehaviour
{

    [SerializeField] private GameObject proyectile;
    [SerializeField] private SpriteRenderer sprite;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = -(int)(transform.position.y*10 + sprite.size.y *10 / 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Collide(GameObject gameobject, int direction)
    {
        Bounce(gameobject, direction);
        //PlayerScript.current.Unlock();
    }

    private void Bounce(GameObject gameobject, int direction)
    {
        collider.enabled = false;
        StartCoroutine("ColliderReactivation");
        var proyectileInstance = Instantiate(proyectile, gameobject.transform.position, Quaternion.identity);
        var proyectileScript = proyectileInstance.GetComponent<ProyectileScript>();
        var posY = transform.position.y - sprite.size.y / 4;
        int exitDirection = 0;

        switch (direction%2)
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
        collider.enabled = true;
    } 
}
