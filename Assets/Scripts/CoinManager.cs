
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TextMeshProUGUI contadorTexto;

    private Coin[] moedas;

    private int moedasColetadas = 0;
    private int totalMoedas = 0;

    private HashSet<string> moedasColetadasIDs =
        new HashSet<string>();

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
        EncontrarMoedas();

        AtualizarInterface();
    }


    private void EncontrarMoedas()
    {
        moedas = FindObjectsByType<Coin>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        totalMoedas = moedas.Length;
    }


    public void ColetarMoeda(Coin moeda)
    {
        if (moedasColetadasIDs.Contains(moeda.GetID()))
            return;

        moedasColetadasIDs.Add(moeda.GetID());

        moedasColetadas++;

        AtualizarInterface();

        Debug.Log(
            "Moedas: " +
            moedasColetadas +
            "/" +
            totalMoedas
        );
    }

    public int GetMoedas()
    {
        return moedasColetadas;
    }

    public HashSet<string> GetMoedasColetadasIDs()
    {
        return new HashSet<string>(
            moedasColetadasIDs
        );
    }

    public void DefinirMoedas(int quantidade)
    {
        moedasColetadas = quantidade;

        AtualizarInterface();
    }

    public void DefinirMoedasColetadas(
        HashSet<string> ids)
    {
        // Garante que as moedas já foram encontradas
        if (moedas == null)
        {
            EncontrarMoedas();
        }

        moedasColetadasIDs =
            new HashSet<string>(ids);

        moedasColetadas =
            moedasColetadasIDs.Count;

        foreach (Coin moeda in moedas)
        {
            if (moedasColetadasIDs.Contains(
                moeda.GetID()))
            {
                moeda.MarcarComoColetada();
            }
            else
            {
                moeda.Restaurar();
            }
        }

        AtualizarInterface();
    }

    public void RestaurarMoedasDoCheckpoint(
    HashSet<string> idsCheckpoint,
    int quantidadeCheckpoint)
    {
        // Garante que as moedas da cena atual foram encontradas
        EncontrarMoedas();

        moedasColetadasIDs =
            new HashSet<string>(idsCheckpoint);

        moedasColetadas =
            quantidadeCheckpoint;

        foreach (Coin moeda in moedas)
        {
            if (moeda == null)
                continue;

            if (moedasColetadasIDs.Contains(moeda.GetID()))
            {
                moeda.MarcarComoColetada();
            }
            else
            {
                moeda.Restaurar();
            }
        }

        AtualizarInterface();

        Debug.Log(
            "Moedas restauradas: " +
            moedasColetadas +
            " | IDs salvos: " +
            moedasColetadasIDs.Count
        );
    }

    private void AtualizarInterface()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text =
                moedasColetadas +
                "/" +
                totalMoedas;
        }
    }
}
