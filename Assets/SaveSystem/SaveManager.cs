using System.Collections.Generic;
using UnityEngine;

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
        SaveData data = new SaveData();

        data.fase = 1;

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

        SaveSystem.SaveGame(data, slot);
    }

    public SaveData CarregarJogo(int slot)
    {
        SaveData data = SaveSystem.LoadGame(slot);

        if (data == null)
        {
            Debug.Log("Não foi possível carregar o jogo.");
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

        if (!data.checkpointAtivado)
        {
            Debug.Log("O save não possui checkpoint.");
            return;
        }

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

        CheckpointManager.Instance.RestaurarCheckpoint(
            posicaoCheckpoint,
            data.moedasNoCheckpoint,
            moedasColetadas
        );

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.RestaurarMoedasDoCheckpoint(
                moedasColetadas,
                data.moedasNoCheckpoint
            );
        }

        Debug.Log("Save restaurado com sucesso!");
    }

}