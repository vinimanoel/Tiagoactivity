using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool coletada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (coletada)
            return;

        if (other.CompareTag("Player"))
        {
            coletada = true;

            CoinManager.Instance.ColetarMoeda(this);

            gameObject.SetActive(false);
        }
    }
}