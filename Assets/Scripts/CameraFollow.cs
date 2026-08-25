using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0, 12, -10);

    public float velocidade = 10f;

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 destino = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );

        transform.LookAt(player);
    }
}