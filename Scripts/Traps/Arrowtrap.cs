using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;

    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject[] arrows; 

    private float coolDownTimer;


    [Header("SFX")]

    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        coolDownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);

        arrows[FindArrow()].transform.position = firePoint.position;

        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow() 
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)

                return i;
        }
        return 0;

    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;


        if (coolDownTimer >= attackCooldown)
            Attack();
    }
}
