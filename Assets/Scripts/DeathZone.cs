using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player morreu!");

        RespawnPlayer(other.gameObject);
    }

    private void RespawnPlayer(GameObject player)
    {
        if (CheckpointManager.Instance.TemCheckpoint())
        {
            Vector3 posicao =
                CheckpointManager.Instance.GetPosicaoCheckpoint();

            int moedas =
                CheckpointManager.Instance.GetMoedasCheckpoint();

            var moedasColetadas =
                CheckpointManager.Instance
                .GetMoedasColetadasCheckpoint();

            player.transform.position = posicao;

            Rigidbody rb =
                player.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CoinManager.Instance
                .RestaurarMoedasDoCheckpoint(
                    moedasColetadas,
                    moedas
                );

            Debug.Log("Player voltou ao checkpoint.");
            Debug.Log(
                "Moedas restauradas: " + moedas
            );
        }
        else
        {
            GameObject startPoint =
                GameObject.Find("StartPoint");

            if (startPoint != null)
            {
                player.transform.position =
                    startPoint.transform.position;
            }

            Rigidbody rb =
                player.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CoinManager.Instance.DefinirMoedas(0);

            Debug.Log(
                "Nenhum checkpoint. Voltando ao início."
            );
        }
    }
}