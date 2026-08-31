
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

            if (botaoCarregar != null)
            {
                botaoCarregar.interactable = false;
            }

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

            if (botaoCarregar != null)
            {
                botaoCarregar.interactable = false;
            }

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

        if (botaoCarregar != null)
        {
            botaoCarregar.interactable = true;
        }

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

        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        SaveData data =
            SaveManager.Instance.CarregarJogo(numeroSlot);

        if (data == null)
        {
            Debug.LogWarning(
                "Não foi possível carregar o slot."
            );

            return;
        }

        if (SaveLoader.Instance == null)
        {
            Debug.LogWarning(
                "SaveLoader não encontrado."
            );

            return;
        }

        // Guarda o save para restaurar depois que a Fase1 carregar
        SaveLoader.Instance.PrepararSave(data);

        // Garante que o jogo não fique pausado
        Time.timeScale = 1f;

        // Vai para a Fase1
        SceneManager.LoadScene("Fase1");

        Debug.Log(
            "Slot " +
            (numeroSlot + 1) +
            " preparado para carregamento!"
        );
    }

    private void ApagarSlot()
    {
        Debug.Log(
            "Apagando Slot " +
            (numeroSlot + 1)
        );

        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        SaveManager.Instance.ApagarSave(numeroSlot);

        Atualizar();

        Debug.Log(
            "Slot " +
            (numeroSlot + 1) +
            " apagado!"
        );
    }
}

