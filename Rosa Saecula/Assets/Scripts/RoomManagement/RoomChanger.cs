
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChanger : MonoBehaviour
{
    [SerializeField]
    private RoomConnection _connection;

    [SerializeField]
    private string _targetSceneName;

    [SerializeField]
    private Transform _spawnPoint;

    public int _currentAge;

    public Room[] RoomList;

    //public int _currentAge = 0;

    private void Start()
    {
        if (_connection == RoomConnection.ActiveConnection) 
        {
            PlayerController.Instance.transform.position = _spawnPoint.position;         
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerController.Instance)
        {
            RoomConnection.ActiveConnection = _connection;
            SceneManager.LoadScene(_targetSceneName);
        }
    }
}
