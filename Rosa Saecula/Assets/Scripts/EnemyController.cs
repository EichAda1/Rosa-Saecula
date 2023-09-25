using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [HideInInspector]
    public bool isFacingRight = false;

    public float enemySightRange;
    public float enemySpeed;
    public float enemyJumpHeight;
    public int enemyHealth;
    public int enemyDamage;

    



    // makes enemies change the way there looking
    public void Turning()
    {
        isFacingRight = !isFacingRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x = enemyScale.x * -1;
        transform.localScale = enemyScale;
    }

}
