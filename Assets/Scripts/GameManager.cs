using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text heartText;
    public GameObject player;

    private Vector3 respawnPoint;
    private int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        respawnPoint = new Vector3(0, 5, 235);
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        UpdateHealthUI();
        RespawnPlayer();

        if (currentHealth <= 0)
        {
            //
        }
    }

    // Respawn the player at the starting position
    void RespawnPlayer()
    {
        player.transform.position = respawnPoint;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    // Update the health UI text
    void UpdateHealthUI()
    {
        heartText.text = "x" + currentHealth;
    }

    public void AddLife(int lives)
    {
        currentHealth += lives;
        UpdateHealthUI();
    }
}
