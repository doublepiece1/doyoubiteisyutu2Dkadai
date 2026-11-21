using TMPro;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [Header("Player HP")]
    [SerializeField] private int maxHP = 3;
    [SerializeField] private TMP_Text hpText;

    [Header("Invincibility")]
    [SerializeField] private float invulnerabilityTime = 0.75f;

    private int currentHP;
    private float invulnerableUntil;

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

        if (Time.time < invulnerableUntil)
        {
            return;
        }

        currentHP -= damage;

        invulnerableUntil =
            Time.time + invulnerabilityTime;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        UpdateHPText();

        if (currentHP <= 0)
        {
            GameManager.Instance.Defeat();
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text =
                "PLAYER HP : " + currentHP
                + " / " + maxHP;
        }
    }
}
