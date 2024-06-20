using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject); // Destroy the obstacle
            Destroy(gameObject); // Destroy the laser
        }
    }
}
