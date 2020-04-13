using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierController : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 dropPosition;
    public Vector3 endPosition;
    public GameObject cargoPrefab;
    private GameObject _cargo;

    private Vector3 _targetPosition;
    
    private bool _hasReachingDropPosition = false;
    private bool _hasReachedEndPosition = false;

    private readonly Vector3 _offset = new Vector3()
    {
        x = 5f,
        y = 15f,
        z = 35f,
    };

    // Start is called before the first frame update
    void Awake()
    {
        dropPosition.z = transform.position.z;
        _targetPosition = dropPosition;
        var platformPosition = transform.position;

        _cargo = Instantiate(cargoPrefab, GetWithOffset(platformPosition), Quaternion.identity);
    }

    void Update()
    {
        if (_hasReachingDropPosition && _hasReachedEndPosition)
        {
            return;
        }

        float step =  speed * Time.deltaTime; // calculate distance to move
        var nextPosition = Vector3.MoveTowards(transform.position, _targetPosition, step);
        
        transform.position = nextPosition;

        if (!_hasReachingDropPosition)
        {
            _cargo.transform.position = GetWithOffset(nextPosition);
        }

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, _targetPosition) < 0.001f)
        {
            if (!_hasReachingDropPosition)
            {
                _targetPosition = endPosition;
                _hasReachingDropPosition = true;
                
                _cargo.AddComponent<BoxCollider>();
                var rigidBody = _cargo.AddComponent<Rigidbody>();
            }
            else
            {
                _hasReachedEndPosition = true;
            }
        }
    }

    private Vector3 GetWithOffset(Vector3 source)
    {
        return new Vector3(source.x + _offset.x, source.y - _offset.y,source.z + _offset.z);
    }
}
