using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform pontoRespawn;

    public int slotSave = 0;

    private bool ativado = false;

    private Vector3 posicaoCheckpoint;

    private int moedasNoCheckpoint;

    private HashSet<string> moedasColetadasNoCheckpoint =
        new HashSet<string>();

    private void Start()
    {
        if (pontoRespawn != null)
        {
            posicaoCheckpoint = pontoRespawn.position;
        }
        else
        {
            posicaoCheckpoint = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ativado)
            return;

        ativado = true;

        moedasNoCheckpoint =
            CoinManager.Instance.GetMoedas();

        moedasColetadasNoCheckpoint =
            CoinManager.Instance.GetMoedasColetadasIDs();

        CheckpointManager.Instance.AtivarCheckpoint(this);

        SaveManager.Instance.SalvarJogo(slotSave);

        Debug.Log("Checkpoint ativado!");

        Debug.Log(
            "Moedas no checkpoint: " +
            moedasNoCheckpoint
        );
    }

    public Vector3 GetPosicao()
    {
        return posicaoCheckpoint;
    }

    public int GetMoedas()
    {
        return moedasNoCheckpoint;
    }

    public HashSet<string> GetMoedasColetadas()
    {
        return new HashSet<string>(
            moedasColetadasNoCheckpoint
        );
    }

    public bool FoiAtivado()
    {
        return ativado;
    }

    public void RestaurarDados(
    int moedas,
    HashSet<string> moedasColetadas)
    {
        ativado = true;

        moedasNoCheckpoint = moedas;

        moedasColetadasNoCheckpoint =
            new HashSet<string>(moedasColetadas);
    }

}