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

<<<<<<< Updated upstream
=======
    public Rigidbody2D Player;
    public int _currentAge;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

        if (PlayerController.instance == null)
        {
            Instantiate(Player, DataPersistenceManager.instance.gameData.playerTransform);
            PlayerController.instance._currentAge = DataPersistenceManager.instance.gameData._currentAge;
        }
        transform.position = PlayerController.instance.transform.position + offset;
>>>>>>> Stashed changes
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);
    }
}
