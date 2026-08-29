using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private string id;

    private bool coletada = false;

    private void Awake()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
        }
    }

    public string GetID()
    {
        return id;
    }

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

    public void Restaurar()
    {
        coletada = false;
        gameObject.SetActive(true);
    }

    public void MarcarComoColetada()
    {
        coletada = true;
        gameObject.SetActive(false);
    }
}