using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFixed = false; // 상태: 멈췄는지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 연결되지 않았습니다!");
        }
    }

    void Update()
    {
        // 상태가 true(멈춘 상태)일 때 y좌표가 -4 이하이면 Restart 씬으로 전환
        if (isFixed && transform.position.y <= -4f)
        {
            Debug.Log("프리팹이 멈췄고 y <= -4이므로 Restart 씬으로 전환합니다.");
            SceneManager.LoadScene("Restart");
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
            isFixed = true; // 상태를 true로 변경
            Debug.Log($"{gameObject.name}이(가) {collision.gameObject.name}과(와) 충돌하여 멈췄습니다.");

            // 충돌한 오브젝트의 태그 출력 (필요한 경우)
            if (!string.IsNullOrEmpty(collision.gameObject.tag))
            {
                Debug.Log($"충돌한 오브젝트의 태그: {collision.gameObject.tag}");
            }

            // 같은 종류의 프리팹이 3개 연속으로 연결되어 있는지 확인
            CheckAndDestroyConnectedPrefabs();
        }
    }

    void CheckAndDestroyConnectedPrefabs()
    {
        // 연결된 프리팹을 추적하기 위한 리스트
        List<GameObject> connectedPrefabs = new List<GameObject>();
        FindConnectedPrefabs(gameObject, connectedPrefabs);

        // 같은 종류의 프리팹이 3개 이상 연결되어 있으면 삭제
        if (connectedPrefabs.Count >= 3)
        {
            foreach (GameObject prefab in connectedPrefabs)
            {
                Destroy(prefab);
            }
            Debug.Log($"{connectedPrefabs.Count}개의 {gameObject.name} 프리팹이 삭제되었습니다.");
        }
    }

    void FindConnectedPrefabs(GameObject current, List<GameObject> connectedPrefabs)
    {
        // 이미 리스트에 추가된 경우 중복 방지
        if (connectedPrefabs.Contains(current)) return;

        // 현재 프리팹 추가
        connectedPrefabs.Add(current);

        // 현재 프리팹의 태그
        string currentTag = current.tag;

        // 현재 프리팹과 충돌한 모든 오브젝트 확인
        Collider2D[] colliders = Physics2D.OverlapCircleAll(current.transform.position, 0.8f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != current && collider.gameObject.CompareTag(currentTag))
            {
                FindConnectedPrefabs(collider.gameObject, connectedPrefabs);
            }
        }
    }
}