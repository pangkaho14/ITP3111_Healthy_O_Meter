using UnityEngine;

public class ColisionDetector : MonoBehaviour
{
    public int damageAmount = 10; // The amount of HP to be reduced

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //When hit by the food
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.ReduceHP(damageAmount);
            playerHealth.CheckHP();
        }

        Destroy(gameObject);
    }
}
