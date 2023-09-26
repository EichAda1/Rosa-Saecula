using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
<<<<<<< Updated upstream
=======
    public Transform playerTransform;
    public int _currentAge;
    public int sceneID;
>>>>>>> Stashed changes
    public SerializableDictionary<string, bool> itemCollection;

    public GameData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
<<<<<<< Updated upstream
=======
        playerTransform = null;
        _currentAge = 0;
        sceneID = 0;
>>>>>>> Stashed changes
        itemCollection = new SerializableDictionary<string, bool>();
    }
}
