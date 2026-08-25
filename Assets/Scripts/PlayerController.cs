using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float velocidade = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 movimento = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movimento.y += 1;

        if (Keyboard.current.sKey.isPressed)
            movimento.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            movimento.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movimento.x += 1;

        movimento = movimento.normalized;

        Vector3 direcao = new Vector3(
            movimento.x,
            0,
            movimento.y
        );

        Vector3 velocidadeAtual = rb.linearVelocity;

        velocidadeAtual.x = direcao.x * velocidade;
        velocidadeAtual.z = direcao.z * velocidade;

        rb.linearVelocity = velocidadeAtual;
    }
}