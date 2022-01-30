using UnityEngine;

using System.Collections;

public class Firetraps : MonoBehaviour
{
    [SerializeField] private float damage; //damage variable

    [Header("Firetrap Timers")]

    [SerializeField] private float activationDelay; //delay time

    [SerializeField] private float activeTime; //amount of time trap stays active

    private Animator anim; //reference to animator

    private SpriteRenderer spriteRend; //reference to spriterender


    [Header("SFX")]

    [SerializeField] private AudioClip firetrapSound;


    private bool triggered; // When the trap gets triggered

    private bool active; // When the trap is active and can hurt the player


    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>(); //grabs reference

        spriteRend = GetComponent<SpriteRenderer>(); //grabs reference
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //checks if trap collides with player
        {
            playerHealth = collision.GetComponent<Health>(); 

            if (!triggered)
            {
                StartCoroutine(ActiveFiretrap()); //triggers trap
            }
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage); //damages player
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")

            playerHealth = null;
     
    }

    private IEnumerator ActiveFiretrap() 
    {
        // Turn sprite red notify the player and trigger the trap

        triggered = true;

        spriteRend.color = Color.red; //turn the sprite red to notify the player 


        yield return new WaitForSeconds(activationDelay);

        SoundManager.instance.PlaySound(firetrapSound);

        spriteRend.color = Color.white; //turn the sprite back to its initial colour

        active = true; //trap will be activated 

        anim.SetBool("activated", true);


        // Wait until X seconds, deactivate trap and reset all variables and animator

        yield return new WaitForSeconds(activeTime);

        active = false;

        triggered = false;

        anim.SetBool("activated", false);
    }
}
