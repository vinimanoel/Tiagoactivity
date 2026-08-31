
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    public GameObject painelVitoria;
    public TextMeshProUGUI textoMoedas;

    private bool vitoriaAtiva = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        vitoriaAtiva = false;

        if (painelVitoria != null)
        {
            painelVitoria.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void MostrarVitoria()
    {
        if (vitoriaAtiva)
            return;

        vitoriaAtiva = true;

        int moedas = 0;

        if (CoinManager.Instance != null)
        {
            moedas = CoinManager.Instance.GetMoedas();
        }

        Coin[] moedasDaFase = FindObjectsByType<Coin>(
            FindObjectsSortMode.None
        );

        int total = moedasDaFase.Length;

        if (textoMoedas != null)
        {
            textoMoedas.text =
                "VITÓRIA!\n\n" +
                "Moedas: " + moedas + "/" + total + "\n\n" +
                "Pressione ENTER para continuar";
        }

        if (painelVitoria != null)
        {
            painelVitoria.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!vitoriaAtiva)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Continuar();
        }
    }

    private void Continuar()
    {
        Time.timeScale = 1f;

        string cenaAtual =
            SceneManager.GetActiveScene().name;

        // Terminou a Fase 1 → vai para a Fase 2
        if (cenaAtual == "Fase1")
        {
            SceneManager.LoadScene("Fase2");
        }
        // Terminou a Fase 2 → termina o jogo
        else if (cenaAtual == "Fase2")
        {
            EncerrarJogo();
        }
    }

    private void EncerrarJogo()
    {
        Debug.Log("JOGO FINALIZADO!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
