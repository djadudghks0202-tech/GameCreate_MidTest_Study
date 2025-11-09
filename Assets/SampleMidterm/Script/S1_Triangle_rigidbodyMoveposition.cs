using UnityEngine;
using UnityEngine.InputSystem;

// ⚙️ RigidbodyMover
// Rigidbody2D 를 이용한 물리적 이동.
// ✅ 충돌, 중력, 마찰 영향 O
[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMover : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 회전 방지
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        // Rigidbody의 속도에 반영 → 중력, 마찰 등 물리효과 유지
        rb.linearVelocity = moveDir * moveSpeed;
    }
}
