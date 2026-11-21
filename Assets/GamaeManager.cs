using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TMP_Text resultText;

    public bool IsGameEnded { get; private set; }

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        resultText.text = "";
    }

    public void Victory()
    {
        EndGame("WIN");
    }

    public void Defeat()
    {
        EndGame("LOSE");
    }

    void EndGame(string message)
    {
        if (IsGameEnded)
        {
            return;
        }

        IsGameEnded = true;

        resultText.text = message;

        Time.timeScale = 0f;

        Debug.Log(message);
    }
}
