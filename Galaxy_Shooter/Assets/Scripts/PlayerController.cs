using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Editor variables
    [SerializeField]
    private float movement_speed = 1.0f;
    [SerializeField]
    private float movement_bound_xmin = -5.0f;
    [SerializeField]
    private float movement_bound_xmax = 5.0f;
    [SerializeField]
    private float movement_bound_ymin = -5.0f;
    [SerializeField]
    private float movement_bound_ymax = 5.0f;
    [SerializeField]
    private bool debug = false;

    public GameObject laserPrefab;
    public Vector3 relativePositionOfLaser = Vector3.zero;

    // Initialize
    private void Start()
    {
        // Debug message
        if (debug)
        {
            Debug.Log("[INFO] PlayerController initialized");
        }
    }
    
    // Update
    private void Update()
    {
        // Control movement
        MovementController();

        // Control laser
        LaserController();
    }

    // MovementController
    private void MovementController()
    {
        // Input parameters
        float input_xaxis = Input.GetAxis("Horizontal");
        float input_yaxis = Input.GetAxis("Vertical");

        // Movement in xy plane
        transform.Translate(Vector3.right * movement_speed * input_xaxis * Time.deltaTime);
        transform.Translate(Vector3.up * movement_speed * input_yaxis * Time.deltaTime);

        // Movement boundary
        if (transform.position.x < movement_bound_xmin)
        {
            transform.position = new Vector3(movement_bound_xmin, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > movement_bound_xmax)
        {
            transform.position = new Vector3(movement_bound_xmax, transform.position.y, transform.position.z);
        }

        if (transform.position.y < movement_bound_ymin)
        {
            transform.position = new Vector3(transform.position.x, movement_bound_ymin, transform.position.z);
        }
        else if (transform.position.y > movement_bound_ymax)
        {
            transform.position = new Vector3(transform.position.x, movement_bound_ymax, transform.position.z);
        }
    }

    // LaserController
    private void LaserController()
    {
        // Instantiate laser
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position + relativePositionOfLaser, Quaternion.identity);
        }
    }
}
