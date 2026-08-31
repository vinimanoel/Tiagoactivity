
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static bool abrirSlotsAoEntrar = false; 
    public GameObject painelSlots;

    private void Start() { painelSlots.SetActive(false); if (abrirSlotsAoEntrar) { painelSlots.SetActive(true); abrirSlotsAoEntrar = false; Debug.Log("Painel de slots aberto automaticamente."); } }

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

