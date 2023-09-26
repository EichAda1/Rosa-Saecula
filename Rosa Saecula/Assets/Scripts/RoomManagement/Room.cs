using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private static string roomID;

    public SerializableDictionary<string, bool> roomsDictionary; //number of rooms
    private bool roomVisited = false;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (roomEntered)
    //    {
    //        gameObject.SetActive(true);
    //        VisitRoom();
    //    }
    //}

    public void LoadData(GameData data)
    {
        data.roomDictionary.TryGetValue(roomID, out roomVisited);
        if (roomVisited)
        {
            VisitRoom();
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.roomDictionary.ContainsKey(roomID))
        {
            data.roomDictionary.Remove(roomID);
        }
        data.roomDictionary.Add(roomID, roomVisited);
    }

    private void VisitRoom()
    {
        roomVisited = true;
        //TODO - implement map system using the above statement
    }
}
