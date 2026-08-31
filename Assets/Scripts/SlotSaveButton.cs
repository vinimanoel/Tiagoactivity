
using UnityEngine;

public class SlotSaveButton : MonoBehaviour
{
    public int slot;

    public void SalvarNesteSlot()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager não encontrado.");
            return;
        }

        SaveManager.Instance.SalvarJogo(slot);

        Debug.Log("Jogo salvo no Slot " + (slot + 1));
    }
}
