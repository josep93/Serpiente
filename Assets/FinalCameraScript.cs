using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraScript : MonoBehaviour
{
    public static FinalCameraScript current;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        animator = GetComponent<Animator>();
    }
}
