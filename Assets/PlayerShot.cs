using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shotPoint;

    [SerializeField] private float shotInterval = 0.2f;

    private float shotTimer;

    void Update()
    {
        shotTimer += Time.deltaTime;

        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed)
        {
            if (shotTimer >= shotInterval)
            {
                Shoot();
                shotTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            shotPoint.position,
            Quaternion.identity
        );
    }
}
