using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    public Transform followTransform;
    public Transform cameraTransform;
    
    public float normalSpeed;
    public float fastSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    
    private Vector3 _newPosition;
    private Quaternion _newRotation;
    private Vector3 _newZoom;
    
    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;

    private Vector3 _rotateStartPosition;
    private Vector3 _rotateCurrentPosition;

    private Camera _mainCamera;
    
    
    // Update is called once per frame
    private void Start()
    {
        instance = this;
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = cameraTransform.localPosition;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMovementInput();
            HandleMouseInput();   
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0f)
        {
            var nextValue = _newZoom + Input.mouseScrollDelta.y * zoomAmount;

            if (nextValue.z >= minZ && nextValue.z <= maxZ && nextValue.y >= minY && nextValue.y <= maxY)
            {
                _newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            _rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            _rotateCurrentPosition = Input.mousePosition;
            var difference = _rotateStartPosition - _rotateCurrentPosition;

            _rotateStartPosition = _rotateCurrentPosition;
            
            _newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 20f));
        }
        
    }

    private void HandleMovementInput()
    {
        var movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            _newZoom += zoomAmount;
        }
        
        if (Input.GetKey(KeyCode.F))
        {
            _newZoom -= zoomAmount;
        }
        
        var lerpTime = 0.2f;
        
        transform.position = Vector3.Lerp(transform.position, _newPosition, lerpTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, lerpTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _newZoom, lerpTime);
    }
}