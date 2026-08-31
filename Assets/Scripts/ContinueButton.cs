
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public Button botaoContinuar;

    private void Start()
    {
        AtualizarBotao();

        if (botaoContinuar != null)
        {
            botaoContinuar.onClick.AddListener(ContinuarJogo);
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
            SaveManager.Instance.ExisteSave(0);

        if (botaoContinuar != null)
        {
            botaoContinuar.interactable = existeSave;
        }
    }

    private void ContinuarJogo()
    {
        Debug.Log("Continuando jogo pelo Slot 0...");

        SaveData data =
            SaveManager.Instance.CarregarJogo(0);

        if (data == null)
        {
            Debug.LogWarning("Nenhum save encontrado no Slot 0.");
            return;
        }

        // Guarda o save para ser restaurado depois que a Fase1 carregar
        SaveLoader.Instance.PrepararSave(data);

        // Vai para a fase
        SceneManager.LoadScene("Fase1");
    }
}

