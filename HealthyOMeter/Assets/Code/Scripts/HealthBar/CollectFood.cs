using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectFood : MonoBehaviour
{
    // assign the PlayerHealthPoints Scriptable Object you want from the UI Inspector
    [SerializeField] private PlayerHealthPoints playerHp;
    [SerializeField] private float healAmt = 20f;
    [SerializeField] private float damageAmt = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            playerHp.Heal(healAmt);
        }
        else if (other.CompareTag("unhealthy"))
        {
            playerHp.TakeDamage(damageAmt);
        }
        
        Destroy(other.gameObject);
    }
}