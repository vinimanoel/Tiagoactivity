
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader : MonoBehaviour
{
    public static SaveLoader Instance;

    private SaveData saveParaRestaurar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += AoCarregarCena;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PrepararSave(SaveData data)
    {
        saveParaRestaurar = data;

        Debug.Log("Save preparado para restauração.");
    }

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo)
    {
        if (cena.name != "Fase1")
            return;

        if (saveParaRestaurar == null)
            return;

        Debug.Log("Fase1 carregada. Restaurando save...");

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.RestaurarSave(
                saveParaRestaurar
            );
        }

        saveParaRestaurar = null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
        }
    }
}

