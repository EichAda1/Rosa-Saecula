using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    GameObject aiming;
    public float projectileSpeed;
    Rigidbody2D projectileRigidBody;




    void start()
    {


        projectileRigidBody = GetComponent<Rigidbody2D>();
        aiming = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (aiming.transform.position - transform.position) * projectileSpeed;
        projectileRigidBody.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 25);


    }

}
