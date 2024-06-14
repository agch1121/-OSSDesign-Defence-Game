using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Cleaner : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Unit"))
        {
            collision.GetComponent<UnitHP>().TakeDamage(100000);
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHP>().TakeDamage(100000);
        }
    }
}
