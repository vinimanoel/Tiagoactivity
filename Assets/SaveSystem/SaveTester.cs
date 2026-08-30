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

        SaveManager.Instance.RestaurarSave(data);

        Debug.Log("===== SAVE RESTAURADO =====");

        Debug.Log("Fase: " + data.fase);
        Debug.Log(
            "Checkpoint: " +
            data.checkpointAtivado
        );

        Debug.Log(
            "Moedas: " +
            data.moedasNoCheckpoint
        );
    }
}