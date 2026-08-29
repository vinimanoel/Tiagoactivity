using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool ativado = false;

    private Vector3 posicaoCheckpoint;
    private int moedasNoCheckpoint;

    private void Start()
    {
        posicaoCheckpoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ativado)
            return;

        ativado = true;

        moedasNoCheckpoint = CoinManager.Instance.GetMoedas();

        Debug.Log("Checkpoint ativado!");
        Debug.Log("Moedas salvas no checkpoint: " + moedasNoCheckpoint);
    }

    public Vector3 GetPosicao()
    {
        return posicaoCheckpoint;
    }

    public int GetMoedas()
    {
        return moedasNoCheckpoint;
    }

    public bool FoiAtivado()
    {
        return ativado;
    }
}