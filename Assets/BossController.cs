using TMPro;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss HP")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private TMP_Text hpText;

    private int currentHP;

    public float HPRatio => (float)currentHP / maxHP;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();
    }

    public void TakeDamage(int damage)
    {
        if (GameManager.Instance.IsGameEnded)
        {
            return;
        }

        currentHP -= damage;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        UpdateHPText();

        if (currentHP <= 0)
        {
            DefeatBoss();
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "BOSS HP : " + currentHP
                        + " / " + maxHP;
        }
    }

    void DefeatBoss()
    {
        GameManager.Instance.Victory();

        Destroy(gameObject);
    }
}
