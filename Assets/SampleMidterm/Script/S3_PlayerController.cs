using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsPlayerController : MonoBehaviour
{
    // 🔹 Inspector 설정 변수
    public float MoveAcceleration = 10f;
    public float MaxMovePower = 5f;
    public float JumpAcceleration = 1.5f;
    public float MaxJumpPower = 10f;

    // 🔹 키를 뗄 때 서서히 멈추는 감속 변수
    public float StopDamping = 0.9f;

    // 🔹 내부 변수
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector2 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Rigidbody2D 설정: 중력 적용 (1.5로 적용)
        rb.gravityScale = 1.5f;
        rb.freezeRotation = true;
    }

    // 🔹 1) 키보드 입력 감지 (Update에서 처리)
    void Update()
    {
        // 1. 좌우 입력 감지 (화살표 키와 A/D 키 모두 사용)
        float moveX = 0f;

        // 🔹 왼쪽 이동 (좌 화살표 또는 A 키)
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            moveX -= 1f;
        }
        // 🔹 오른쪽 이동 (우 화살표 또는 D 키)
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            moveX += 1f;
        }

        inputDirection.x = moveX;

        // 2. Space로 점프 구현
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            if (rb.linearVelocity.y < MaxJumpPower)
            {
                rb.AddForce(Vector2.up * JumpAcceleration, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    // 🔹 1) 이동, 가속, 감속 구현 (FixedUpdate에서 처리)
    void FixedUpdate()
    {
        // 1. 현재 속도 가져오기
        Vector2 currentVelocity = rb.linearVelocity;
        float h = inputDirection.x;

        // 2. 수평 가속도 및 최고 속도 제한 구현
        if (h != 0)
        {
            // 목표 속도 계산 (최고 속도 MaxMovePower 적용)
            float targetX = h * MaxMovePower;

            currentVelocity.x = Mathf.MoveTowards(
                currentVelocity.x,
                targetX,
                MoveAcceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            // 3. 키를 떼면 서서히 멈추도록 짠다 (감속/Damping)
            currentVelocity.x *= StopDamping;
            if (Mathf.Abs(currentVelocity.x) < 0.05f)
            {
                currentVelocity.x = 0;
            }
        }

        // 4. 새로운 속도 적용
        rb.linearVelocity = currentVelocity;
    }

    // 🔹 3) 빨간 벽 (안 뚫리는 벽) 충돌 및 땅 체크
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅 체크
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // 빨간 벽 (Collision 이벤트)
        if (collision.gameObject.CompareTag("RedWall"))
        {
            Debug.Log("🟥 Console Log: 빨간 벽에 부딪힘 (OnCollisionEnter2D)");
        }
    }

    // 🔹 2) 녹색 벽 (뚫리는 벽) 충돌 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 녹색 벽 (Trigger 이벤트)
        if (other.gameObject.CompareTag("GreenWall"))
        {
            Debug.Log("🟩 Console Log: 녹색 벽을 뚫고 지나가는 중 (OnTriggerEnter2D)");
        }
    }
}