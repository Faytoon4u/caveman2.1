using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int goldValue = 1;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddGoldScore(goldValue);

            // Play the collect sound
            AudioSource playerAudioSource = other.GetComponent<AudioSource>();
            if (playerAudioSource != null && collectSound != null)
            {
                playerAudioSource.PlayOneShot(collectSound);
            }

            Destroy(gameObject);
        }
    }
}
