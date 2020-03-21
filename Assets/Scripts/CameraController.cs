using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 panLimit;
    public float bottomOffset = -40f;
    
    public float panSpeed = 100f;
    public float scrollSpeed = 100f;
    public float heightMoveSpeed = 100f;
    
    public float minY = 40f;
    public float maxY = 100f;

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = transform.position;

        var scaledPanSpeed = Input.GetKey(KeyCode.LeftControl) ? panSpeed * 1.2f : panSpeed;
        scaledPanSpeed *= Mathf.Max(cameraPosition.y / maxY, 0.4f);

        if (Input.GetKey(KeyCode.W))
        {
            cameraPosition.z += scaledPanSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            cameraPosition.z -= scaledPanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraPosition.x += scaledPanSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            cameraPosition.x -= scaledPanSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraPosition.y -= heightMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            cameraPosition.y += heightMoveSpeed * Time.deltaTime;
        }

        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        cameraPosition.y -= scrollValue * scrollSpeed * 100f *  Time.deltaTime;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);
    
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, 0, panLimit.x);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, bottomOffset, panLimit.y);
        
        transform.position = cameraPosition;
    }
}