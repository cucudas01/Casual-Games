using UnityEngine;

public class PrefabController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 연결되지 않았습니다!");
        }
    }

    // 충돌 이벤트 처리
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb != null)
        {
            // 프리팹의 y 좌표가 -6보다 작으면 멈추지 않음
            if (transform.position.y < -6f)
            {
                Debug.Log($"{gameObject.name}이(가) y 좌표가 -6보다 작아 충돌 무시됨.");
                return;
            }

            // 속도를 0으로 설정하고 물리 시뮬레이션 중지
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log($"{gameObject.name}이(가) {collision.gameObject.name}과(와) 충돌하여 멈췄습니다.");

            // 충돌한 오브젝트의 태그 출력 (필요한 경우)
            if (!string.IsNullOrEmpty(collision.gameObject.tag))
            {
                Debug.Log($"충돌한 오브젝트의 태그: {collision.gameObject.tag}");
            }
        }
    }
}