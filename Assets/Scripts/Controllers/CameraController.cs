using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float height = 2f;
    public float yawSpeed = 100f;

    private float zoomLevel = 1f;
    private float currentYaw = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        zoomLevel -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset * zoomLevel;
        transform.LookAt(target.position + Vector3.up * height);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
