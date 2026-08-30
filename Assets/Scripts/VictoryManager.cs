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
        if (painelVitoria != null)
        {
            painelVitoria.SetActive(false);
        }
    }

    public void MostrarVitoria()
    {
        if (vitoriaAtiva)
            return;

        vitoriaAtiva = true;

        int moedas = CoinManager.Instance.GetMoedas();

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

        SceneManager.LoadScene("Fase2");
    }
}