using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextFadeInScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private SpriteRenderer fadeIn;
    [SerializeField] private Animator snakeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FullMethod(texts));
    }

    private IEnumerator FullMethod(TextMeshProUGUI[] texts) {
        StartCoroutine(FadeInScene(fadeIn));
        yield return new WaitForSeconds(5f);
        snakeAnimator.enabled = true;
        StartCoroutine(FadeInInitial(texts[0]));
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeInInitial(texts[1]));
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeInInitial(texts[2]));
        yield return new WaitForSeconds(5f);
        FinalCameraScript.current.animator.enabled = true;
        StartCoroutine(FadeInInitial(texts[3]));
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeInInitial(texts[4]));
        yield return new WaitForSeconds(2f);
        VesselScript.current.animator.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator FadeInInitial(TextMeshProUGUI text)
    {
        var alfa = 0f;
        for (int i = 0; i < 20; i++)
        {
            alfa += 0.05f;
            var black = Color.white;
            black.a = alfa;
            text.color = black;
            yield return new WaitForSecondsRealtime(0.015f);
        }
    }

    private IEnumerator FadeInScene(SpriteRenderer sprite)
    {
        var alfa = 1f;
        for (int i = 0; i < 20; i++)
        {
            alfa -= 0.05f;
            var black = Color.black;
            black.a = alfa;
            sprite.color = black;
            yield return new WaitForSecondsRealtime(0.015f);
        }
    }
}
