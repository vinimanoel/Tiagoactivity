
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject painelSlots;

    private void Start()
    {
        // Garante que o painel de slots começa fechado
        painelSlots.SetActive(false);
    }

    public void NovoJogo()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void CarregarJogo()
    {
        painelSlots.SetActive(true);

        Debug.Log("Painel de slots aberto.");
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");

        Application.Quit();
    }
}

