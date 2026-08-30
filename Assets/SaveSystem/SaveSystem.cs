using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private static string GetSavePath(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save_" + slot + ".dat"
        );
    }

    private static string chave = "BOLINHAS_ATTACK_SAVE_KEY_2026";

    public static void SaveGame(SaveData data, int slot)
    {
        string json = JsonUtility.ToJson(data);

        string dadosCriptografados = Criptografar(json);

        string path = GetSavePath(slot);

        File.WriteAllText(path, dadosCriptografados);

        Debug.Log("Jogo salvo com sucesso!");
        Debug.Log("Save criptografado em: " + path);
    }

    public static SaveData LoadGame(int slot)
    {
        string path = GetSavePath(slot);

        if (!File.Exists(path))
        {
            Debug.Log("Nenhum save encontrado no slot " + slot);
            return null;
        }

        try
        {
            string dadosCriptografados =
                File.ReadAllText(path);

            string json =
                Descriptografar(dadosCriptografados);

            SaveData data =
                JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Jogo carregado com sucesso!");

            return data;
        }
        catch (Exception erro)
        {
            Debug.LogError(
                "Erro ao carregar o save: " +
                erro.Message
            );

            return null;
        }
    }

    public static bool HasSave(int slot)
    {
        string path = GetSavePath(slot);

        return File.Exists(path);
    }

    public static void DeleteSave(int slot)
    {
        string path = GetSavePath(slot);

        if (File.Exists(path))
        {
            File.Delete(path);

            Debug.Log(
                "Save do slot " +
                slot +
                " deletado."
            );
        }
    }

    private static string Criptografar(string texto)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GerarChave();
            aes.IV = new byte[16];

            using (ICryptoTransform encryptor =
                   aes.CreateEncryptor())
            {
                byte[] dados =
                    Encoding.UTF8.GetBytes(texto);

                byte[] criptografado =
                    encryptor.TransformFinalBlock(
                        dados,
                        0,
                        dados.Length
                    );

                return Convert.ToBase64String(
                    criptografado
                );
            }
        }
    }

    private static string Descriptografar(
        string textoCriptografado)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GerarChave();
            aes.IV = new byte[16];

            using (ICryptoTransform decryptor =
                   aes.CreateDecryptor())
            {
                byte[] dados =
                    Convert.FromBase64String(
                        textoCriptografado
                    );

                byte[] descriptografado =
                    decryptor.TransformFinalBlock(
                        dados,
                        0,
                        dados.Length
                    );

                return Encoding.UTF8.GetString(
                    descriptografado
                );
            }
        }
    }

    private static byte[] GerarChave()
    {
        using (SHA256 sha256 =
               SHA256.Create())
        {
            return sha256.ComputeHash(
                Encoding.UTF8.GetBytes(chave)
            );
        }
    }
}