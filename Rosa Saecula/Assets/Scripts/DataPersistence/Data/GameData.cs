using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public Transform playerTransform;
    public int sceneID;
    public SerializableDictionary<string, bool> itemCollection;
    public SerializableDictionary<string, bool> roomDictionary;

    public GameData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
        playerTransform = null;
        sceneID = 0;
        itemCollection = new SerializableDictionary<string, bool>();
        roomDictionary = new SerializableDictionary<string, bool>();
    }
}
