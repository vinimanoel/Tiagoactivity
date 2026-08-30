using UnityEngine;

public class SaveTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Salvar();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Carregar();
        }
    }

    private void Salvar()
    {
        SaveManager.Instance.SalvarJogo(0);

        Debug.Log("SAVE REALIZADO!");
    }

    private void Carregar()
    {
        SaveData data =
            SaveManager.Instance.CarregarJogo(0);

        if (data == null)
            return;

        Debug.Log("===== SAVE =====");
        Debug.Log("Fase: " + data.fase);
        Debug.Log("Checkpoint ativado: " +
                  data.checkpointAtivado);

        Debug.Log(
            "Posição: " +
            data.checkpointX + ", " +
            data.checkpointY + ", " +
            data.checkpointZ
        );

        Debug.Log(
            "Moedas: " +
            data.moedasNoCheckpoint
        );

        Debug.Log(
            "Moedas coletadas: " +
            data.moedasColetadas.Count
        );
    }
}