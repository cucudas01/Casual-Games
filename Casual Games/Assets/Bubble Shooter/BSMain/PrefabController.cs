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
        // 논리적으로 'line'과 연결되어 있지 않으면 삭제
        if (isFixed)
        {
            HashSet<GameObject> visited = new HashSet<GameObject>();
            if (!IsConnectedToLineRecursive(gameObject, visited))
            {
                Debug.Log($"{gameObject.name}이(가) 'line'과 연결되어 있지 않아 삭제됨.");
                DestroyWithScoreCheck();
                return;
            }
        }

        // 상태가 true(멈춘 상태)일 때 y좌표가 -4 이하이면 Restart 씬으로 전환
        if (isFixed && transform.position.y <= -4f)
        {
            Debug.Log("프리팹이 멈췄고 y <= -4이므로 Restart 씬으로 전환합니다.");
            SceneManager.LoadScene("Restart");
        }
    }

    // 논리적 연결(재귀) 검사 함수 - 태그와 상관없이 PrefabController가 붙은 모든 프리팹을 따라감
    bool IsConnectedToLineRecursive(GameObject obj, HashSet<GameObject> visited)
    {
        if (visited.Contains(obj)) return false;
        visited.Add(obj);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(obj.transform.position, 0.9f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == obj) continue;
            if (collider.CompareTag("line")) return true;

            // PrefabController가 붙어있는 오브젝트라면 태그와 상관없이 재귀적으로 검사
            if (collider.GetComponent<PrefabController>() != null)
            {
                if (IsConnectedToLineRecursive(collider.gameObject, visited))
                    return true;
            }
        }
        return false;
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

            // 같은 종류의 프리팹이 3개 연속으로 연결되어 있는지 확인
            CheckAndDestroyConnectedPrefabs();
        }
    }

    void CheckAndDestroyConnectedPrefabs()
    {
        // 같은 종류(같은 태그)로 연속 연결된 프리팹만 찾기
        List<GameObject> connectedPrefabs = new List<GameObject>();
        FindConnectedPrefabs(gameObject, connectedPrefabs);

        // 같은 종류의 프리팹이 3개 이상 연결되어 있으면 삭제
        if (connectedPrefabs.Count >= 3)
        {
            foreach (GameObject prefab in connectedPrefabs)
            {
                PrefabController pc = prefab.GetComponent<PrefabController>();
                if (pc != null)
                    pc.DestroyWithScoreCheck();
                else
                    Destroy(prefab);
            }
            Debug.Log($"{connectedPrefabs.Count}개의 {gameObject.name} 프리팹이 삭제되었습니다.");
        }
    }

    // 같은 태그(종류)끼리만 연속적으로 따라감
    void FindConnectedPrefabs(GameObject current, List<GameObject> connectedPrefabs)
    {
        if (connectedPrefabs.Contains(current)) return;
        connectedPrefabs.Add(current);

        string currentTag = current.tag;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(current.transform.position, 0.8f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != current && collider.gameObject.CompareTag(currentTag))
            {
                FindConnectedPrefabs(collider.gameObject, connectedPrefabs);
            }
        }
    }

    // y좌표 -4 이상에서 삭제될 때 ScoreCounter에 알림
    public void DestroyWithScoreCheck()
    {
        if (transform.position.y >= -4f)
        {
            ScoreCounter scoreCounter = FindObjectOfType<ScoreCounter>();
            if (scoreCounter != null)
            {
                scoreCounter.AddDestroyedCount();
            }
        }
        Destroy(gameObject);
    }
}