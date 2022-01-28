using UnityEngine;

using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    [SerializeField] private Image totalhealthBar;

    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
