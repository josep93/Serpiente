using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, Activable
{
    [SerializeField] private GameObject door;
    private Collider2D collider;
    private SpriteRenderer sprite;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Activate()
    {
        sprite.sortingLayerName="Foreground";
        collider.enabled = false;
        StartCoroutine("OpenDoor");
    }

    private IEnumerator OpenDoor(){
    for(float i = 0; i < 2; i += 0.2f)
        {
            door.transform.position += new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
