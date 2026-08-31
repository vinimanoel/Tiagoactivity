
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

        Debug.Log(
            "Save preparado. Fase: " + data.fase
        );
    }

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo)
    {
        if (saveParaRestaurar == null)
            return;

        string nomeFaseEsperada = "";

        if (saveParaRestaurar.fase == 1)
        {
            nomeFaseEsperada = "Fase1";
        }
        else if (saveParaRestaurar.fase == 2)
        {
            nomeFaseEsperada = "Fase2";
        }

        if (cena.name != nomeFaseEsperada)
            return;

        Debug.Log(
            "Cena carregada: " + cena.name +
            ". Restaurando save..."
        );

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
