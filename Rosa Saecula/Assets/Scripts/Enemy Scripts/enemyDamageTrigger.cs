using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamageTrigger : MonoBehaviour
{

    public int enemyProjectileDamage = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            Destroy(gameObject);

        }

        if (collider.tag == "Ground")
        {

            Destroy(gameObject);

        }
    }
}
