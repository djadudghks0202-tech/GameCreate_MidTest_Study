using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForceMover : MonoBehaviour
{
    public float Speed = 100.0f; // 🔹 힘의 크기 (Inspector에서 조절)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 불필요한 회전 방지
    }

    void FixedUpdate()
    {
        // 🔹 신형 입력 시스템: Keyboard.current 사용
        float moveX = 0f;
        float moveY = 0f;

        // 키를 누르는 동안 힘을 지속적으로 가합니다.
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;
        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;

        // 힘을 가할 방향 계산
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;

        // AddForce로 힘을 가하여 가속
        Vector2 force = moveDir * Speed;
        rb.AddForce(force, ForceMode2D.Force);

        // 💡 팁: 키를 떼면 미끄러지므로, Rigidbody2D의 Linear Drag를 2~5 정도로 설정하면 더 자연스럽습니다.
    }
}