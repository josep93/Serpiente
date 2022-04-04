using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    float alfa = 0;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        StartCoroutine("CutOut");
        RoomChangeScript.state = 0;
    }

    IEnumerator CutOut()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Templo");
        asyncLoad.allowSceneActivation = false;
        PlayerScript.current.state = 0;
        for (int i=0; i < 40; i++)
        {
            alfa += 0.025f;
            var black = Color.black;
            black.a = alfa;
            sprite.color = black;
            yield return new WaitForSeconds(0.03f);
        }
        asyncLoad.allowSceneActivation=true;
    }
}
