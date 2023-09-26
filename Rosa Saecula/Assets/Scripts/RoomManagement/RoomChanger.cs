
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

    private void Start()
    {
        if (_connection == RoomConnection.ActiveConnection) 
        {
            PlayerController.instance.transform.position = _spawnPoint.position;
            _currentAge = PlayerController.instance._currentAge;
        }

        for (int i = 0; i < RoomList.Length; i++)
        {
            if (i != _currentAge)
            {
                RoomList[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerController.instance)
        {
            RoomConnection.ActiveConnection = _connection;
            SceneManager.LoadScene(_targetSceneName);
        }
    }
}
