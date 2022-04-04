using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public static ShakeScript current;
    public bool rumbling = false;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CameraShake(float time, float magnitude)
    {
        Vector3 originalPosition = CameraPositionScript.current.transform.localPosition;

        float elapsed = 0f;
        rumbling = true;
        while (elapsed < time)
        {
            float x = Random.Range(-1f,1f)*magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x+ originalPosition.x, y+originalPosition.y, originalPosition.z);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }
        rumbling = false;
        transform.localPosition = originalPosition;
    }
}
