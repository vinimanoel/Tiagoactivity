
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotUI : MonoBehaviour
{
    public int numeroSlot;

    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoInformacoes;

    public Button botaoSalvar;
    public Button botaoCarregar;
    public Button botaoApagar;

    private void Start()
    {
        Atualizar();

        if (botaoSalvar != null)
        {
            botaoSalvar.onClick.AddListener(SalvarSlot);
        }

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

        textoTitulo.text =
            "SLOT " + (numeroSlot + 1);

        bool existe =
            SaveManager.Instance.ExisteSave(numeroSlot);

        Debug.Log(
            "Verificando Slot " +
            (numeroSlot + 1) +
            " | Existe: " +
            existe
        );

        if (!existe)
        {
            textoInformacoes.text = "VAZIO";

            if (botaoSalvar != null)
                botaoSalvar.interactable = true;

            if (botaoCarregar != null)
                botaoCarregar.interactable = false;

            if (botaoApagar != null)
                botaoApagar.interactable = false;

            return;
        }

        SaveData data =
            SaveManager.Instance.CarregarJogo(numeroSlot);

        if (data == null)
        {
            textoInformacoes.text = "SAVE INVÁLIDO";

            if (botaoSalvar != null)
                botaoSalvar.interactable = true;

            if (botaoCarregar != null)
                botaoCarregar.interactable = false;

            if (botaoApagar != null)
                botaoApagar.interactable = false;

            return;
        }

        textoInformacoes.text =
            "Fase: " + data.fase +
            "\nMoedas: " + data.moedasNoCheckpoint;

        if (botaoSalvar != null)
            botaoSalvar.interactable = true;

        if (botaoCarregar != null)
            botaoCarregar.interactable = true;

        if (botaoApagar != null)
            botaoApagar.interactable = true;
    }

    private void SalvarSlot()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        Debug.Log(
            "SALVANDO NO SLOT " +
            (numeroSlot + 1)
        );

        SaveManager.Instance.SalvarJogo(numeroSlot);

        // Atualiza a interface imediatamente
        Atualizar();

        Debug.Log(
            "SLOT " +
            (numeroSlot + 1) +
            " SALVO!"
        );
    }

    private void CarregarSlot()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        Debug.Log(
            "TENTANDO CARREGAR SLOT " +
            (numeroSlot + 1)
        );

        SaveData data =
            SaveManager.Instance.CarregarJogo(numeroSlot);

        if (data == null)
        {
            Debug.LogWarning(
                "Não foi possível carregar o Slot " +
                (numeroSlot + 1)
            );

            return;
        }

        Debug.Log(
            "Slot carregado. Fase salva: " +
            data.fase
        );

        if (SaveLoader.Instance == null)
        {
            Debug.LogWarning(
                "SaveLoader não encontrado."
            );

            return;
        }

        SaveLoader.Instance.PrepararSave(data);

        Time.timeScale = 1f;

        if (data.fase == 1)
        {
            Debug.Log("Abrindo Fase1...");
            SceneManager.LoadScene("Fase1");
        }
        else if (data.fase == 2)
        {
            Debug.Log("Abrindo Fase2...");
            SceneManager.LoadScene("Fase2");
        }
        else
        {
            Debug.LogWarning(
                "Fase inválida no save: " +
                data.fase
            );
        }
    }

    private void ApagarSlot()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning(
                "SaveManager não encontrado."
            );

            return;
        }

        Debug.Log(
            "APAGANDO SLOT " +
            (numeroSlot + 1)
        );

        SaveManager.Instance.ApagarSave(numeroSlot);

        Atualizar();

        Debug.Log(
            "SLOT " +
            (numeroSlot + 1) +
            " APAGADO!"
        );
    }
}
