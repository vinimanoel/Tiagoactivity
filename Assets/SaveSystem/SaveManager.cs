
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SalvarJogo(int slot)
    {
        string cenaAtual =
            SceneManager.GetActiveScene().name;

        SaveData data = new SaveData();

        // Descobre em qual fase o jogador está
        if (cenaAtual == "Fase1")
        {
            data.fase = 1;
        }
        else if (cenaAtual == "Fase2")
        {
            data.fase = 2;
        }
        

    else
    {
        // No Menu, usa o autosave como base
        SaveData autosave =
            SaveSystem.LoadGame(0);

        if (autosave == null)
        {
            Debug.LogWarning(
                "Não existe autosave para copiar."
            );

            return;
        }

        data = autosave;

        Debug.Log(
            "Save copiado do Autosave."
        );
    }
                

        // Salva os dados do checkpoint
        if (CheckpointManager.Instance != null &&
            CheckpointManager.Instance.TemCheckpoint())
        {
            data.checkpointAtivado = true;

            Vector3 posicao =
                CheckpointManager.Instance.GetPosicaoCheckpoint();

            data.checkpointX = posicao.x;
            data.checkpointY = posicao.y;
            data.checkpointZ = posicao.z;

            data.moedasNoCheckpoint =
                CheckpointManager.Instance.GetMoedasCheckpoint();

            HashSet<string> ids =
                CheckpointManager.Instance
                .GetMoedasColetadasCheckpoint();

            data.moedasColetadas =
                new List<string>(ids);
        }
        else
        {
            data.checkpointAtivado = false;

            if (CoinManager.Instance != null)
            {
                data.moedasNoCheckpoint =
                    CoinManager.Instance.GetMoedas();

                data.moedasColetadas =
                    new List<string>(
                        CoinManager.Instance
                        .GetMoedasColetadasIDs()
                    );
            }
        }

        // ==============================
        // SALVA NO SLOT ESCOLHIDO
        // ==============================

        SaveSystem.SaveGame(data, slot);

        Debug.Log(
            "Jogo salvo no Slot " +
            (slot + 1) +
            " | Fase: " +
            data.fase
        );

        // ==============================
        // ATUALIZA O AUTOSAVE
        // ==============================

        if (slot != 0)
        {
            SaveSystem.SaveGame(data, 0);

            Debug.Log(
                "Autosave atualizado pelo Slot " +
                (slot + 1)
            );
        }
    }

    public SaveData CarregarJogo(int slot)
    {
        SaveData data =
            SaveSystem.LoadGame(slot);

        if (data == null)
        {
            Debug.Log(
                "Não foi possível carregar o Slot " +
                (slot + 1)
            );

            return null;
        }

        return data;
    }

    public bool ExisteSave(int slot)
    {
        return SaveSystem.HasSave(slot);
    }

    public void ApagarSave(int slot)
    {
        SaveSystem.DeleteSave(slot);
    }

    public void RestaurarSave(SaveData data)
    {
        if (data == null)
            return;

        // Se o save possui checkpoint,
        // restaura posição e moedas do checkpoint.
        if (data.checkpointAtivado)
        {
            Vector3 posicaoCheckpoint =
                new Vector3(
                    data.checkpointX,
                    data.checkpointY,
                    data.checkpointZ
                );

            HashSet<string> moedasColetadas =
                new HashSet<string>(
                    data.moedasColetadas
                );

            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance
                    .RestaurarCheckpoint(
                        posicaoCheckpoint,
                        data.moedasNoCheckpoint,
                        moedasColetadas
                    );
            }

            if (CoinManager.Instance != null)
            {
                CoinManager.Instance
                    .RestaurarMoedasDoCheckpoint(
                        moedasColetadas,
                        data.moedasNoCheckpoint
                    );
            }

            Debug.Log(
                "Save restaurado com checkpoint! " +
                "Fase: " + data.fase +
                " | Moedas: " +
                data.moedasNoCheckpoint
            );
        }
        else
        {
            // O save não possui checkpoint.
            // Nesse caso, não tentamos restaurar uma posição
            // que não existe.
            //
            // Mas ainda restauramos as moedas salvas.

            HashSet<string> moedasColetadas =
                new HashSet<string>(
                    data.moedasColetadas
                );

            if (CoinManager.Instance != null)
            {
                CoinManager.Instance
                    .RestaurarMoedasDoCheckpoint(
                        moedasColetadas,
                        data.moedasNoCheckpoint
                    );
            }

            Debug.Log(
                "Save restaurado sem checkpoint. " +
                "Fase: " + data.fase +
                " | Moedas: " +
                data.moedasNoCheckpoint
            );
        }
    }


}

