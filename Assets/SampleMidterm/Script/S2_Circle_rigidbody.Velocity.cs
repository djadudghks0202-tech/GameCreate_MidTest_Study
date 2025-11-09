using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityForceLike : MonoBehaviour
{
    // Inspector 설정 변수
    public float Speed = 5.0f;          // 최고 속도 제한
    public float Acceleration = 50.0f;   // 가속력 (속도 변화율)

    private Rigidbody2D rb;
    private Vector2 targetVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 🚨 Linear Drag 강제 설정 (Inspector에 필드가 없을 때 사용)
        // 이 코드가 마찰력을 5.0으로 설정하여 키를 뗄 때 서서히 멈추게 합니다.
        rb.linearDamping = 5.0f;

        rb.freezeRotation = true; // 회전 방지
    }

    void Update()
    {
        // Update에서 목표 속도 계산 (키 입력 감지)
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX += 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY -= 1f;

        targetVelocity = new Vector2(moveX, moveY).normalized * Speed;
    }

    void FixedUpdate()
    {
        // Velocity를 이용하여 AddForce와 동일한 점진적 가속 구현
        Vector2 newVelocity = Vector2.MoveTowards(
            rb.linearVelocity,        // 현재 속도
            targetVelocity,     // 목표 속도
            Acceleration * Time.fixedDeltaTime // 가속력 적용
        );
        rb.linearVelocity = newVelocity;
    }
}