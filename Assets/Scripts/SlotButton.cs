
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public int slot;

    public Button botaoCarregar;

    private void Start()
    {
        AtualizarBotao();

        if (botaoCarregar != null)
        {
            botaoCarregar.onClick.AddListener(CarregarSlot);
        }
    }

    private void AtualizarBotao()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager não encontrado.");
            return;
        }

        bool existeSave =
            SaveManager.Instance.ExisteSave(slot);

        if (botaoCarregar != null)
        {
            botaoCarregar.interactable = existeSave;
        }
    }

    private void CarregarSlot()
    {
        Debug.Log("Carregando Slot " + (slot + 1));

        SaveData data =
            SaveManager.Instance.CarregarJogo(slot);

        if (data == null)
        {
            Debug.LogWarning(
                "Nenhum save encontrado neste slot."
            );

            return;
        }

        // Guarda o save para restaurar depois que a cena carregar
        SaveLoader.Instance.PrepararSave(data);

        SceneManager.LoadScene("Fase1");
    }
}


