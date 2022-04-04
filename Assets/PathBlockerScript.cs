using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlockerScript : MonoBehaviour
{
    Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collider.enabled = true;
        }
    }
}
