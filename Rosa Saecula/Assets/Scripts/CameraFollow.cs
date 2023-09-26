using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float  followSpeed = 0.1f;
    [SerializeField] public Vector3 offset;
    [SerializeField] private float yMaxBounds;
    [SerializeField] private float yMinBounds;
    [SerializeField] private float xMaxBounds;
    [SerializeField] private float xMinBounds;

    public static CameraFollow Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);
    }
}
