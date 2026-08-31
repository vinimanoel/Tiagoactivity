
using UnityEngine;

public class SlotPanelController : MonoBehaviour
{
    public GameObject painelSlots;
    public GameObject painelEscolhaSlot;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    public void AbrirSlots()
    {
        painelSlots.SetActive(true);

        painelEscolhaSlot.SetActive(true);

        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);

        Debug.Log("Painel de escolha de slot aberto.");
    }

    public void AbrirSlot1()
    {
        painelEscolhaSlot.SetActive(false);
        slot1.SetActive(true);
    }

    public void AbrirSlot2()
    {
        painelEscolhaSlot.SetActive(false);
        slot2.SetActive(true);
    }

    public void AbrirSlot3()
    {
        painelEscolhaSlot.SetActive(false);
        slot3.SetActive(true);
    }

    public void VoltarParaEscolha()
    {
        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);

        painelEscolhaSlot.SetActive(true);
    }

    public void FecharSlots()
    {
        painelSlots.SetActive(false);

        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);
    }
}

