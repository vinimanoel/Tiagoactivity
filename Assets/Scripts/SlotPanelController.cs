using UnityEngine;

public class SlotPanelController : MonoBehaviour
{
    public GameObject painelSlots;

    public void AbrirSlots()
    {
        painelSlots.SetActive(true);

        Debug.Log("Painel de slots aberto.");
    }

    public void FecharSlots()
    {
        painelSlots.SetActive(false);

        Debug.Log("Painel de slots fechado.");
    }
}
