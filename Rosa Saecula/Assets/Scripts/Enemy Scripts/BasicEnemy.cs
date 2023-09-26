using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BasicEnemy : EnemyController
{

    


    void Update()

    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStartingPos = GameObject.FindGameObjectWithTag("EnemyStartingPos").transform;
        float playerDistance = Vector2.Distance(player.position, transform.position);
        float enemyStartingPosDis = Vector2.Distance(enemyStartingPos.position, transform.position);


        // makes the enemy follow the player if they are close enough
        if (playerDistance < enemySightRange)
            
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }
        // if the player is out of sight range and there starting position is too they will move twords there original pos until it is in range
        else if (playerDistance > enemySightRange && enemyStartingPosDis > enemySightRange )
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyStartingPos.position, enemySpeed * Time.deltaTime);
        }


        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }


    }




    // this is for determining the sight range for the enemies / testing it visibly
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySightRange);

    }



}
