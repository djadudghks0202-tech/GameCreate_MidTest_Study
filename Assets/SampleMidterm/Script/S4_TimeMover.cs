using UnityEngine;

// 오브젝트의 이동 방식을 구분하기 위한 Enum
public enum MoveType { CubeA_FrameBased, CubeB_DeltaTime, CubeC_FixedDeltaTime }

public class TimeMover : MonoBehaviour
{
    // 🔹 Inspector 설정: Speed
    public float Speed = 5.0f;
    // 🔹 Inspector 설정: 이동 방식 선택
    public MoveType moveType;

    private Rigidbody2D rb;

    void Start()
    {
        // CubeC만 Rigidbody를 사용
        if (moveType == MoveType.CubeC_FixedDeltaTime)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // CubeA, CubeB의 이동 (게임 로직 타이밍: Update)
    void Update()
    {
        // Vector3.right는 (1, 0, 0) 방향입니다.

        if (moveType == MoveType.CubeA_FrameBased)
        {
            // 🔹 CubeA (빨강): Update마다 움직이는 수치를 Speed로 지정 (프레임률 기반)
            // Time.deltaTime을 곱하지 않아 프레임이 높을수록(성능 좋을수록) 빨라집니다.
            transform.Translate(Vector3.right * Speed);
        }
        else if (moveType == MoveType.CubeB_DeltaTime)
        {
            // 🔹 CubeB (초록): DeltaTime의 수치에 따라 움직이는 Speed로 지정
            // Time.deltaTime을 곱하여 프레임률에 관계없이 일정한 속도로 움직입니다.
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }

    // CubeC의 이동 (물리 연산 타이밍: FixedUpdate)
    void FixedUpdate()
    {
        if (moveType == MoveType.CubeC_FixedDeltaTime && rb != null)
        {
            // 🔹 CubeC (파랑): DeltaTime의 수치에 따라 움직이는 Speed로 지정
            // Rigidbody.velocity를 사용합니다.
            // Time.timeScale이 0.2로 바뀌면 Time.fixedDeltaTime도 0.2배 작아지며,
            // velocity를 설정하면 물리 연산 타이밍에 맞춰 부드럽게 이동합니다.
            rb.linearVelocity = new Vector2(Speed, rb.linearVelocity.y);

            // 💡 참고: Time.timeScale이 velocity에 자동으로 적용되므로,
            // 별도로 Time.fixedDeltaTime을 곱할 필요는 없습니다.
        }
    }
}