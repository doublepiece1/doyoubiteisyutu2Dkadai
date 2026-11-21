using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float lifeTime = 8f;
    [SerializeField] private int damage = 1;

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        bulletSpeed = speed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position +=
            (Vector3)(moveDirection * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerLifeController player =
            other.GetComponent<PlayerLifeController>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
