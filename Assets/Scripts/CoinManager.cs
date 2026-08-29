using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TextMeshProUGUI contadorTexto;

    private int moedasColetadas = 0;
    private int totalMoedas = 0;

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
        Coin[] moedas = FindObjectsByType<Coin>(FindObjectsSortMode.None);

        totalMoedas = moedas.Length;

        AtualizarInterface();
    }

    public void ColetarMoeda(Coin moeda)
    {
        moedasColetadas++;

        AtualizarInterface();

        Debug.Log("Moedas: " + moedasColetadas + "/" + totalMoedas);
    }

    public int GetMoedas()
    {
        return moedasColetadas;
    }

    public void DefinirMoedas(int quantidade)
    {
        moedasColetadas = quantidade;

        AtualizarInterface();
    }

    private void AtualizarInterface()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text = moedasColetadas + "/" + totalMoedas;
        }
    }
}