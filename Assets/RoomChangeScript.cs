using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangeScript : MonoBehaviour
{
    [SerializeField] private int initialRoomID, finalRoomID;
    [SerializeField] private SpriteRenderer spriteInitial, spriteFinal;
    [SerializeField] bool returnable;
    [SerializeField] int value = 0;
    [SerializeField] public static int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && state == 0 && value==0)
        {
            RoomChange();
            state = 1;
        }
        if (collision.tag == "Proyectile" && state == 1 && value == 1)
        {
            RoomChange();
            state = 2;
        }
        if (collision.tag == "Proyectile" && value == 2)
        {
            CameraPositionScript.current.MakeFollow(collision.gameObject);
            state = 3;
        }
        if (collision.tag == "Proyectile" && state == 3 && value == 3)
        {
            StartCoroutine("FadeOutInitial");
            state = 4;
        }
    }

    private void RoomChange()
    {
        StartCoroutine("FadeOutInitial");
        StartCoroutine("FadeInFinal");
        CameraPositionScript.current.Move(initialRoomID, finalRoomID);
    }

    private IEnumerator FadeOutInitial()
    {
        var alfa = 0f;
        PlayerScript.current.state = 0;
        for (int i = 0; i < 40; i++)
        {
            alfa += 0.025f;
            var black = Color.black;
            black.a = alfa;
            spriteInitial.color = black;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        PlayerScript.current.state = 1;
    }

    private IEnumerator FadeInFinal()
    {
        var alfa = 1f;
        PlayerScript.current.state = 0;
        for (int i = 0; i < 40; i++)
        {
            alfa -= 0.025f;
            var black = Color.black;
            black.a = alfa;
            spriteFinal.color = black;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        PlayerScript.current.state = 1;
    }
}
