using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityMover : MonoBehaviour
{
    public float Speed = 5.0f; // 🔹 최고 이동 속도 (Inspector에서 조절)
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // 🔹 신형 입력 시스템: Keyboard.current 사용
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;
        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        // Velocity를 직접 설정하여 즉시 속도 변경
        rb.linearVelocity = moveDir * Speed;

        // 💡 팁: 키를 떼면 오브젝트가 즉시 멈춥니다 (Linear Drag가 0일 경우).
    }
}