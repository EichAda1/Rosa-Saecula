using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyController
{
    public GameObject pointA;
    public GameObject pointB;
    private bool pointAB = true;











    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        float playerDistance = Vector2.Distance(player.position, transform.position);

        if (playerDistance > enemySightRange)
        {



            if (transform.position == pointA.transform.position
              && pointAB == false || transform.position ==
              pointB.transform.position && pointAB == true)
            {
                pointAB = !pointAB;
                if (isFacingRight == false)
                {
                    GetComponent<EnemyController>().Turning();
                }
            }



            if (pointAB == true)
            {
                transform.position =
                  Vector3.MoveTowards(transform.position,
                  pointB.transform.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                transform.position =
                  Vector3.MoveTowards(transform.position,
                  pointA.transform.position, enemySpeed * Time.deltaTime);
            }

        }
        else if (playerDistance < enemySightRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySightRange);

    }
}

