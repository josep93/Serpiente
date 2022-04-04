using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnRun : MonoBehaviour
{
    [SerializeField] bool active = true;

    private void Start()
    {
        gameObject.SetActive(active);
    }
}
