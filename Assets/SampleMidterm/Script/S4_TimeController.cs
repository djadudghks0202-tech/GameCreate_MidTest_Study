using UnityEngine;
using UnityEngine.InputSystem; // 🔹 T 키 입력 감지를 위해 추가

public class TimeController : MonoBehaviour
{
    // 🔹 Inspector 설정: TimeSpeed (Time.timeScale 값)
    public float TimeSpeed = 1.0f;
    public float SlowMotionScale = 0.2f; // 슬로우 모션 전환 값

    void Start()
    {
        // 게임 시작 시 Time.timeScale 초기 설정
        Time.timeScale = TimeSpeed;
    }

    void Update()
    {
        // 🔹 T 키를 눌렀을 때, Time.timeScale 값을 1.0 ↔ 0.2로 전환
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            if (Time.timeScale > 0.5f) // 현재 일반 속도(1.0)일 때
            {
                Time.timeScale = SlowMotionScale; // 0.2로 전환
                Debug.Log($"🕒 슬로우 모션 켜짐: Time.timeScale = {Time.timeScale}");
            }
            else // 현재 슬로우 모션(0.2)일 때
            {
                Time.timeScale = 1.0f; // 1.0으로 전환
                Debug.Log($"🕒 일반 속도 켜짐: Time.timeScale = {Time.timeScale}");
            }

            // Inspector의 TimeSpeed 변수도 업데이트하여 현재 상태를 보여줌
            TimeSpeed = Time.timeScale;
        }
    }
}