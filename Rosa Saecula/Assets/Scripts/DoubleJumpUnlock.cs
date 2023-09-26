using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUnlock : MonoBehaviour
{
    bool collected;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance.Doublejump)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collected)
        {
            collected = true;
            PlayerController.Instance.Doublejump = true;

            Destroy(gameObject);
        }
    }
}
