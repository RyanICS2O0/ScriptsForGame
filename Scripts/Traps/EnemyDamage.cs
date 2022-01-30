using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;//makes private float damage 

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")//Checks if trap has collided with player

            collision.GetComponent<Health>().TakeDamage(damage);//accesses health and decreases it
    }
}
