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
}