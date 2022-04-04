using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionScript : MonoBehaviour
{
    public static CameraPositionScript current;
    public Vector3 currentObjective;
    public GameObject currentObjectiveGO;
    private bool follow;
    private Vector3[] posiciones = { new Vector3(-0.59f, 0.03f, -10f), new Vector3(24.91f, 1.78f, -10f), new Vector3(20.16f, 18.78f, -10f) };

    // Start is called before the first frame update
    void Start()
    {
        transform.position = posiciones[0];
        current = this;
    }

    public void Move(int initial, int final)
    {
        if (initial == 1 && final == 2)
        {
            MoveOneToTwo();
        }
        if (initial == 2 && final == 3)
        {
            MoveTwoToThreeReal();
        }
    }

    private void Update()
    {
        if (follow)
        {
            CameraToObjective();
        }
    }

    private void CameraToObjective()
    {
        if (currentObjectiveGO != null)
        {
            currentObjective = currentObjectiveGO.transform.position + Vector3.back * 10;
            transform.position = Vector3.MoveTowards(transform.position, currentObjective, 0.6f);
        }
        else
        {
            StartCoroutine("CancelFollow");
        }
    }

    public void MakeFollow(GameObject gameobject)
    {
        currentObjectiveGO = gameobject;
        follow = true;
    }

    private void MoveOneToTwo()
    {
        currentObjective = posiciones[1];
        StartCoroutine("CameraMove");
    }


    private void MoveTwoToThreeReal()
    {
        currentObjective = posiciones[2];
        StartCoroutine("CameraRealMove");
    }

    private void MoveTwoToThree()
    {
        currentObjective = posiciones[2];
        StartCoroutine("CameraMove");
    }

    IEnumerator CameraMove()
    {
        while (transform.position != currentObjective)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentObjective, 0.3f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator CameraRealMove()
    {
        Time.timeScale = 0;
        while (transform.position != currentObjective)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentObjective, 0.3f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Time.timeScale = 1;
    }

    IEnumerator CancelFollow()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        if (currentObjectiveGO == null)
        {
            follow = false;
            MoveTwoToThree();
        }
    }
}
