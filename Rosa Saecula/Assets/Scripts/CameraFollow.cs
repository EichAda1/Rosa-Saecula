using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float  followSpeed = 0.1f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float yMaxBounds;
    [SerializeField] private float yMinBounds;
    [SerializeField] private float xMaxBounds;
    [SerializeField] private float xMinBounds;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);
    }
}
