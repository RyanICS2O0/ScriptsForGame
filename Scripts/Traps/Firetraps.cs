using UnityEngine;

using System.Collections;

public class Firetraps : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]

    [SerializeField] private float activationDelay;

    [SerializeField] private float activeTime;

    private Animator anim;

    private SpriteRenderer spriteRend;


    [Header("SFX")]

    [SerializeField] private AudioClip firetrapSound;


    private bool triggered; // When the trap gets triggered

    private bool active; // When the trap is active and can hurt the player


    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        spriteRend = GetComponent<SpriteRenderer>();
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
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                StartCoroutine(ActiveFiretrap());
            }
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
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

        active = true;

        anim.SetBool("activated", true);


        // Wait until X seconds, deactivate trap and reset all variables and animator

        yield return new WaitForSeconds(activeTime);

        active = false;

        triggered = false;

        anim.SetBool("activated", false);
    }
}
