using UnityEngine;
using System.Collections;
public class BossAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private BossController boss;

    // ゲーム開始時に攻撃開始
    void Start()
    {
        if (boss == null)
        {
            boss = GetComponent<BossController>();
        }

        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        while (true)
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.IsGameEnded)
            {
                yield break;
            }

            float hpRatio = boss.HPRatio;

            // 第1フェーズ
            if (hpRatio > 0.5f)
            {
                yield return StartCoroutine(
                    AimedFan(5, 12f, 4.8f, 0.75f)
                );

                yield return StartCoroutine(
                    RadialAttack(20, 4.2f)
                );
            }

            // 第2フェーズ
            else if (hpRatio > 0.25f)
            {
                yield return StartCoroutine(
                    AimedFan(7, 11f, 5.6f, 0.55f)
                );

                yield return StartCoroutine(
                    SpiralAttack(55, 0.055f, 5.5f, 13f)
                );
            }

            // 最終フェーズ
            else
            {
                yield return StartCoroutine(
                    AimedFan(9, 10f, 6.3f, 0.38f)
                );

                yield return StartCoroutine(
                    RadialAttack(28, 5f)
                );

                yield return StartCoroutine(
                    SpiralAttack(70, 0.035f, 6.2f, 17f)
                );
            }
        }
    }

    // ① プレイヤーを狙う扇状弾幕
    IEnumerator AimedFan(
        int bulletCount,
        float spreadAngle,
        float speed,
        float interval)
    {
        for (int shot = 0; shot < 3; shot++)
        {
            if (player == null) yield break;

            Vector2 targetDirection =
                ((Vector2)player.position -
                (Vector2)transform.position).normalized;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle =
                    (i - (bulletCount - 1) / 2f)
                    * spreadAngle;

                Vector2 direction =
                    RotateDirection(targetDirection, angle);

                SpawnBullet(direction, speed);
            }

            yield return new WaitForSeconds(interval);
        }
    }

    // ② 全方向弾幕
    IEnumerator RadialAttack(
        int bulletCount,
        float speed)
    {
        for (int shot = 0; shot < 3; shot++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle =
                    i * (360f / bulletCount)
                    + shot * 7f;

                float radian = angle * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(
                    Mathf.Cos(radian),
                    Mathf.Sin(radian)
                );

                SpawnBullet(direction, speed);
            }

            yield return new WaitForSeconds(0.8f);
        }
    }

    // ③ 回転螺旋弾幕
    IEnumerator SpiralAttack(
        int bulletCount,
        float interval,
        float speed,
        float angleStep)
    {
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.IsGameEnded)
            {
                yield break;
            }

            float radian = angle * Mathf.Deg2Rad;

            Vector2 direction1 = new Vector2(
                Mathf.Cos(radian),
                Mathf.Sin(radian)
            );

            Vector2 direction2 = -direction1;

            SpawnBullet(direction1, speed);
            SpawnBullet(direction2, speed);

            angle += angleStep;

            yield return new WaitForSeconds(interval);
        }
    }

    // 弾を生成する
    void SpawnBullet(Vector2 direction, float speed)
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        BossBullet bossBullet =
            bullet.GetComponent<BossBullet>();

        bossBullet.Initialize(direction, speed);
    }

    // 方向を回転させる
    Vector2 RotateDirection(Vector2 direction, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        float x =
            direction.x * Mathf.Cos(radian)
            - direction.y * Mathf.Sin(radian);

        float y =
            direction.x * Mathf.Sin(radian)
            + direction.y * Mathf.Cos(radian);

        return new Vector2(x, y).normalized;
    }
}
