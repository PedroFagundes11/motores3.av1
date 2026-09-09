using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ugui : MonoBehaviour
{
    // Singleton para a interface
    public static Ugui Instance { get; private set; }

    [Header("Contadores de Moedas (TextMeshPro)")]
    [Tooltip("Texto no lado ESQUERDO para o Player 1")]
    public TextMeshProUGUI p1ScoreText;

    [Tooltip("Texto no lado DIREITO para o Player 2")]
    public TextMeshProUGUI p2ScoreText;

    [Header("Painel de Vitória")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;
    public Button restartButton;

    [Header("Regra de Vitória")]
    [Tooltip("Meta de moedas para vencer (0 para jogo sem limite automático)")]
    public int targetScore = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (winnerPanel != null)
        {
            winnerPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    private void OnEnable()
    {
        // Inscreve-se nos eventos da estrutura estática (Observer)
        PlayerOM.OnCoinCountChanged += AtualizarTextoMoedas;
        PlayerOM.OnPlayerWon += ExibirVencedor;
    }

    private void OnDisable()
    {
        // Cancela inscrições ao desativar
        PlayerOM.OnCoinCountChanged -= AtualizarTextoMoedas;
        PlayerOM.OnPlayerWon -= ExibirVencedor;
    }

    private void Start()
    {
        AtualizarTextoMoedas(1, PlayerOM.GetCoins(1));
        AtualizarTextoMoedas(2, PlayerOM.GetCoins(2));
    }

    private void AtualizarTextoMoedas(int playerID, int totalMoedas)
    {
        if (playerID == 1 && p1ScoreText != null)
        {
            p1ScoreText.text = $"P1 Moedas: {totalMoedas}";
        }
        else if (playerID == 2 && p2ScoreText != null)
        {
            p2ScoreText.text = $"P2 Moedas: {totalMoedas}";
        }

        // Verifica vitória se houver meta estabelecida
        if (targetScore > 0 && totalMoedas >= targetScore)
        {
            PlayerOM.TriggerWin(playerID);
        }
    }

    public void ExibirVencedor(int winnerPlayerID)
    {
        if (winnerPanel != null)
        {
            winnerPanel.SetActive(true);
        }

        if (winnerText != null)
        {
            winnerText.text = $"JOGADOR {winnerPlayerID} VENCEU!";
        }

        Time.timeScale = 0f; // Pausa o jogo
    }

    private void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarPartida();
        }
    }
}