using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCountroller : MonoBehaviour
{
    public GameObject[] prefabObjects; // 프리팹 배열
    public ArrowCountroller arrowController; // ArrowCountroller 참조
    public float launchForce = 500f; // 발사 속도

    private GameObject currentPrefabInstance; // 현재 생성된 프리팹 인스턴스

    // Start is called before the first frame update
    void Start()
    {
        // ArrowCountroller 연결 상태 확인
        if (arrowController == null)
        {
            Debug.LogError("ArrowCountroller가 연결되지 않았습니다! Unity 에디터에서 Arrow Controller 필드에 올바른 GameObject를 연결하세요.");
        }
        else
        {
            Debug.Log($"ArrowCountroller가 성공적으로 연결되었습니다: {arrowController.gameObject.name}");
        }

        // 게임 시작 시 프리팹 생성
        CreatePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바를 눌렀을 때 프리팹 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchPrefab();
        }

        // 스페이스바를 뗐을 때 새로운 프리팹 생성
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreatePrefab();
        }
    }

    // 프리팹을 생성하는 메서드
    private void CreatePrefab()
    {
        // 프리팹 배열이 비어있지 않은지 확인
        if (prefabObjects != null && prefabObjects.Length > 0)
        {
            // 랜덤으로 프리팹 선택
            int randomIndex = Random.Range(0, prefabObjects.Length);
            GameObject selectedPrefab = prefabObjects[randomIndex];

            if (selectedPrefab != null)
            {
                // 프리팹 생성
                Vector3 spawnPosition = new Vector3(0, -8f, 0); // 고정된 위치
                currentPrefabInstance = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"프리팹 {selectedPrefab.name}이(가) 생성되었습니다.");
            }
            else
            {
                Debug.LogError("선택된 프리팹이 null입니다!");
            }
        }
        else
        {
            Debug.LogError("프리팹 배열이 비어있거나 null입니다! Unity 에디터에서 프리팹을 추가하세요.");
        }
    }

    // 프리팹을 발사하는 메서드
    private void LaunchPrefab()
{
    if (currentPrefabInstance != null)
    {
        // ArrowCountroller의 Transform을 기준으로 발사
        if (arrowController != null)
        {
            Vector3 direction = arrowController.transform.up; // Arrow의 로컬 Y축 방향
            Rigidbody2D rb = currentPrefabInstance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // 속도 설정
                rb.velocity = direction * launchForce; // 발사 속도 적용
                Debug.Log($"프리팹 {currentPrefabInstance.name}이(가) {direction} 방향으로 발사되었습니다. 속도: {rb.velocity}");
            }
            else
            {
                Debug.LogError("프리팹에 Rigidbody2D 컴포넌트가 없습니다!");
            }
        }
        else
        {
            Debug.LogError("ArrowCountroller가 연결되지 않았습니다!");
        }
    }
    else
    {
        Debug.LogError("현재 생성된 프리팹이 없습니다!");
    }
}
}