using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    private float alfa = 1;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        StartCoroutine("FadeIn");
        PlayerScript.current.state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        PlayerScript.current.state = 0;
        for (int i = 0; i < 40; i++)
        {
            alfa -= 0.025f;
            var black = Color.black;
            black.a = alfa;
            sprite.color = black;
            yield return new WaitForSeconds(0.03f);
        }
        PlayerScript.current.state = 1;
    }
}
