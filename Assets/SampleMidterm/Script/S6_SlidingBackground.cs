using UnityEngine;
using UnityEngine.InputSystem;

public class SlidingBackground : MonoBehaviour
{
    // Inspector에서 조절할 슬라이딩 속도
    public float ScrollSpeed = 0.1f;

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        // 현재 Material의 Offset 값을 가져와 초기 offset으로 사용합니다.
        // _MainTex는 기본 텍스처를 의미합니다.
        offset = rend.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        // 1. 키보드 입력 감지 (WASD 또는 화살표 키)
        float h = 0f;
        float v = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) h = -1f;
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) h = 1f;
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) v = 1f;
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed) v = -1f;

        // 2. Offset 계산
        // 키 입력 방향에 따라 offset을 변경합니다. (Time.deltaTime으로 속도 일정 유지)
        // 입력 방향과 반대로 offset을 이동시켜 배경이 움직이는 착시 효과를 만듭니다.
        offset.x -= h * ScrollSpeed * Time.deltaTime;
        offset.y -= v * ScrollSpeed * Time.deltaTime;

        // 3. Material에 새로운 Offset 적용
        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}