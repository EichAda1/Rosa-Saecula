using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> itemCollection;

    public GameData()
    {
        deathCount = 0;
        playerPosition = Vector3.zero;
        itemCollection = new SerializableDictionary<string, bool>();
    }
}
