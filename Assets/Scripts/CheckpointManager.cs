using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Checkpoint checkpointAtual;

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

    public void AtivarCheckpoint(Checkpoint checkpoint)
    {
        checkpointAtual = checkpoint;
    }

    public bool TemCheckpoint()
    {
        return checkpointAtual != null;
    }

    public Vector3 GetPosicaoCheckpoint()
    {
        return checkpointAtual.GetPosicao();
    }

    public int GetMoedasCheckpoint()
    {
        return checkpointAtual.GetMoedas();
    }

    public HashSet<string> GetMoedasColetadasCheckpoint()
    {
        return checkpointAtual.GetMoedasColetadas();
    }

    public void RestaurarCheckpoint(
        Vector3 posicao,
        int moedas,
        HashSet<string> moedasColetadas)
    {
        checkpointAtual = null;

        Checkpoint[] checkpoints =
            FindObjectsByType<Checkpoint>(
                FindObjectsSortMode.None
            );

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (Vector3.Distance(
                checkpoint.GetPosicao(),
                posicao
            ) < 0.1f)
            {
                checkpointAtual = checkpoint;
                break;
            }
        }

        if (checkpointAtual != null)
        {
            checkpointAtual.RestaurarDados(
                moedas,
                moedasColetadas
            );

            Debug.Log("Checkpoint restaurado!");
        }
        else
        {
            Debug.LogWarning(
                "Checkpoint salvo não encontrado na cena."
            );
        }
    }
}