using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int goldValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddGoldScore(goldValue);
            Destroy(gameObject);
        }
    }
}


