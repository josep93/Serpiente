using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AltarScript : MonoBehaviour, Activable
{
    private SpriteRenderer sprite;
    [SerializeField]private SpriteRenderer fadeOutSprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        PlayerScript.current.EndIt();
        sprite.enabled = true;
        StartCoroutine(CommenceTheRumbling());
    }

    IEnumerator CommenceTheRumbling()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(ShakeScript.current.CameraShake(5f, 0.1f));
        yield return new WaitWhile(() => ShakeScript.current.rumbling);
        StartCoroutine(ShakeScript.current.CameraShake(5f, 0.3f));
        yield return new WaitWhile(() => ShakeScript.current.rumbling);
        StartCoroutine(ShakeScript.current.CameraShake(10f, 1f));
        StartCoroutine(FadeOutInitial(fadeOutSprite));
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Playa2");
        asyncLoad.allowSceneActivation = false;
        yield return new WaitForSeconds(4f);
        asyncLoad.allowSceneActivation = true;
    }

    private IEnumerator FadeOutInitial(SpriteRenderer fadeOutSprite)
    {
        var alfa = 0f;
        PlayerScript.current.state = 0;
        for (int i = 0; i < 40; i++)
        {
            alfa += 0.025f;
            var black = Color.black;
            black.a = alfa;
            fadeOutSprite.color = black;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        PlayerScript.current.state = 1;
    }
}
