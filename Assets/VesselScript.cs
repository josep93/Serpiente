using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselScript : MonoBehaviour
{
    public static VesselScript current;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        animator = GetComponent<Animator>();
    }
}
