using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float  followSpeed = 0.1f;
    public Vector3 offset;
    [SerializeField] private float yMaxBounds;
    [SerializeField] private float yMinBounds;
    [SerializeField] private float xMaxBounds;
    [SerializeField] private float xMinBounds;

    public static CameraFollow Instance;

    public Rigidbody2D Player;
    public int _currentAge;

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

        if (PlayerController.Instance == null)
        {
            Instantiate(Player, DataPersistenceManager.instance.gameData.playerTransform);
            PlayerController.Instance._currentAge = DataPersistenceManager.instance.gameData._currentAge;
        }
        transform.position = PlayerController.Instance.transform.position + offset;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);
    }
}
