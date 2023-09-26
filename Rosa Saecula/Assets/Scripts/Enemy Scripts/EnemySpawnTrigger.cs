using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject[] gameObjects;
    public bool isSpawned = false;


    //spawns enemys if player touches the trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && isSpawned == false)
        {
            isSpawned = true;
            foreach (GameObject gameObject in gameObjects)
            {

                gameObject.SetActive(true);
            }
        }
    }
}
