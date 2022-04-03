using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlaceCamera();
        CheckBoundaries();
    }

    /// <summary>
    /// Checks MC location and places camera over him
    /// </summary>
    private void PlaceCamera()
    {
        transform.position = new Vector3(PlayerScript.current.transform.position.x, PlayerScript.current.transform.position.y, -2);
    }

    /// <summary>
    /// If camera get out of boundaries, it's draged back to it's limits.
    /// </summary>
    private void CheckBoundaries()
    {
        if (transform.position.x < xMin) { transform.position = new Vector3(xMin, transform.position.y, -2); }
        if (transform.position.x > xMax) { transform.position = new Vector3(xMax, transform.position.y, -2); }
        if (transform.position.y < yMin) { transform.position = new Vector3(transform.position.x, yMin, -2); }
        if (transform.position.y > yMax) { transform.position = new Vector3(transform.position.x, yMax, -2); }
    }
}
