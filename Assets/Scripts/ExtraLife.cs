using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public int extraLives = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddLife(extraLives);
            }

            Destroy(gameObject);
        }
    }
}
