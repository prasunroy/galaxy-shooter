using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    // Editor variables
    public float speed = 1.0f;

    [SerializeField]
    private float visibleBoundary = 10.0f;
    [SerializeField]
    private bool debug = false;

    // Initialize
    void Start ()
    {
        // Debug message
        if (debug)
        {
            Debug.Log("[INFO] LaserController initialized");
        }
    }

    // Update
    void Update ()
    {
        // Move laser
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Destroy laser if out of visible boundary
        if (transform.position.y > visibleBoundary)
        {
            Destroy(gameObject);
        }
    }
}
