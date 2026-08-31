
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public GameObject pausePanel;

    private bool jogoPausado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        jogoPausado = false;

        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AlternarPausa();
        }
    }

    private void AlternarPausa()
    {
        if (jogoPausado)
        {
            ContinuarJogo();
        }
        else
        {
            PausarJogo();
        }
    }

    private void PausarJogo()
    {
        jogoPausado = true;

        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Debug.Log("Jogo pausado.");
    }

    public void ContinuarJogo()
    {
        jogoPausado = false;

        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Debug.Log("Jogo continuando.");
    }

    public void SalvarJogo()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager não encontrado.");
            return;
        }

        SaveManager.Instance.SalvarJogo(0);

        Debug.Log("Jogo salvo pelo Pause.");
    }

    public void CarregarJogo()
    {
        Time.timeScale = 1f;

        Debug.Log("Voltando ao Menu para carregar um jogo.");

        SceneManager.LoadScene("Menu");
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;

        Debug.Log("Voltando ao Menu.");

        SceneManager.LoadScene("Menu");
    }
}
