using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private bool jogadorEntrou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (jogadorEntrou)
            return;

        jogadorEntrou = true;

        VictoryManager.Instance.MostrarVitoria();
    }
}