using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, Activable
{
    private Animator animator;
    [SerializeField]private GameObject activableGO;
    private Activable activable;
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        activable = activableGO.GetComponent<Activable>();
        Debug.Log(activable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if (!activated)
        {
            Animate();
            activated = true;
            activable.Activate();
        }
    }

    private void Animate()
    {
        animator.SetTrigger("Activate");
    }
}
