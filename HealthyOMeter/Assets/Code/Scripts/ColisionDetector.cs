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
        // if (collision.collider.name == "icecreamdrop(Clone)"){
        //     Debug.Log("icecreamdrop");
        //     if (playerHealth != null)
        //     {
        //         playerHealth.ReduceHP(damageAmount);
        //         playerHealth.CheckHP();
        //     }
        // }
        // else
        // {
        //     playerHealth.GainHP(damageAmount);
        //     playerHealth.CheckHP();
        //     Debug.Log(collision.collider.name);
        // }

        Destroy(gameObject);
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     GameObject obj1 = this.gameObject;
    //     GameObject obj2 = collision.gameObject;

    //     Debug.Log("Triggered Obj1: :" + obj1.name);
    //     Debug.Log("Triggered obj2: :" + obj2.name);
    // }
}
