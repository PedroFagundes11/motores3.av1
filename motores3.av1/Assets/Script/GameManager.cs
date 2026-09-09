using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Padrão Singleton
    public static GameManager Instance { get; private set; }

    [Header("Configurações de Cena")]
    [Tooltip("Nome da cena principal de Gameplay")]
    public string GameplaySceneName = "Gameplay";

    [Tooltip("Nome da cena de interface (GUI)")]
    public string GUISceneName = "GUI";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CarregarJogo();
    }

    public void CarregarJogo()
    {
        // Reseta as pontuações no PlayerOM antes de carregar
        PlayerOM.ResetScores();

        // 1. Carrega a cena de Gameplay como principal
        SceneManager.LoadScene(GameplaySceneName, LoadSceneMode.Single);

        // 2. Carrega a cena de GUI aditivamente sobre a Gameplay
        SceneManager.LoadScene(GUISceneName, LoadSceneMode.Additive);

        Debug.Log("Cenas Gameplay e GUI carregadas com sucesso!");
    }

    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        CarregarJogo();
    }
}