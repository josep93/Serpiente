using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthShade : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private Color color;
    [SerializeField] private float alfa;
    [SerializeField] private int minY, maxY;
    [SerializeField] bool inversed = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var playerPos = PlayerScript.current.transform.position.y;
        if (!inversed)
        {
            if (playerPos > minY)
            {
                alfa = (1 - ((float)maxY - playerPos)) / 2;
            }
            else
            {
                alfa = 0;
            }
        }
        else
        {
            if (playerPos < maxY)
            {
                alfa = (((float)minY - playerPos)) / 2;
            }
            else
            {
                alfa = 0;
            }
        }
        color.a = alfa;
        sprite.color = color;
    }
}
