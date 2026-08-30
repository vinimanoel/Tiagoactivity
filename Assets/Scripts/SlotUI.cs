using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public int numeroSlot;

    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoInformacoes;

    public Button botaoCarregar;
    public Button botaoApagar;

    private void Start()
    {
        Atualizar();

        if (botaoCarregar != null)
        {
            botaoCarregar.onClick.AddListener(CarregarSlot);
        }

        if (botaoApagar != null)
        {
            botaoApagar.onClick.AddListener(ApagarSlot);
        }
    }

    public void Atualizar()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        bool existe =
            SaveManager.Instance.ExisteSave(numeroSlot);

        if (!existe)
        {
            textoTitulo.text =
                "SLOT " + (numeroSlot + 1);

            textoInformacoes.text =
                "VAZIO";

            botaoCarregar.interactable = false;

            if (botaoApagar != null)
            {
                botaoApagar.interactable = false;
            }

            return;
        }

        SaveData data =
            SaveManager.Instance.CarregarJogo(numeroSlot);

        if (data == null)
        {
            textoInformacoes.text =
                "SAVE INVÁLIDO";

            botaoCarregar.interactable = false;

            if (botaoApagar != null)
            {
                botaoApagar.interactable = false;
            }

            return;
        }

        textoTitulo.text =
            "SLOT " + (numeroSlot + 1);

        textoInformacoes.text =
            "Fase: " + data.fase +
            "\nMoedas: " +
            data.moedasNoCheckpoint;

        botaoCarregar.interactable = true;

        if (botaoApagar != null)
        {
            botaoApagar.interactable = true;
        }
    }

    private void CarregarSlot()
    {
        Debug.Log(
            "Carregando Slot " +
            (numeroSlot + 1)
        );

        SaveData data =
            SaveManager.Instance.CarregarJogo(numeroSlot);

        if (data == null)
        {
            Debug.LogWarning(
                "Não foi possível carregar o slot."
            );

            return;
        }

        SaveManager.Instance.RestaurarSave(data);

        Debug.Log(
            "Slot " +
            (numeroSlot + 1) +
            " carregado!"
        );
    }

    private void ApagarSlot()
    {
        Debug.Log(
            "Apagando Slot " +
            (numeroSlot + 1)
        );

        SaveManager.Instance.ApagarSave(numeroSlot);

        Atualizar();

        Debug.Log(
            "Slot " +
            (numeroSlot + 1) +
            " apagado!"
        );
    }
}