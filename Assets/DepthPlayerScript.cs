using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthPlayerScript : MonoBehaviour
{
    [SerializeField] private float sizeScale;
    [SerializeField] private int depthStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Redimensionate();
    }

    private void Redimensionate()
    {
        if (transform.position.y > depthStart)
        {
            transform.localScale = Vector3.one/2 - Vector3.one/2 * sizeScale * (transform.position.y - depthStart);
        }
    }
}
