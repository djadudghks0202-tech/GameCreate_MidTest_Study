using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class BallBounceAnimator : MonoBehaviour
{
    private Animator animator;
    // Rigidbody는 필요하지만, 여기서는 애니메이션만 제어하므로 필수 변수만 선언
    // private Rigidbody2D rb; 

    void Start()
    {
        animator = GetComponent<Animator>();
        // rb = GetComponent<Rigidbody2D>(); // 물리 이동이 없으므로 주석 처리
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. 충돌한 오브젝트의 태그가 "Ground"인지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 2. 애니메이션 재생 명령 (가장 간단한 형태)
            //    -> 이 코드는 Ground에 닿을 때마다, 심지어 튕겨 오르는 도중에도 호출됩니다.
            animator.SetTrigger("Bounce");
        }
    }
}