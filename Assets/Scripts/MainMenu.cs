using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject botaoContinuar;

    private void Start()
    {
        botaoContinuar.SetActive(false);
    }

    public void NovoJogo()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void CarregarJogo()
    {
        Debug.Log("Abrir tela de carregamento.");
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");

        Application.Quit();
    }
}