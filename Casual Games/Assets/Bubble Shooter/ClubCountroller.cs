using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubCountroller : MonoBehaviour
{
    float speed = 900f; // 회전속도
    private Vector3 pivotPoint = new Vector3(1f, -9f, 0f); // 회전 기준점
    private Quaternion initialRotation; // 초기 회전값 저장
    private Vector3 initialPosition; // 초기 위치값 저장

    void Start()
    {
        Application.targetFrameRate = 60; // 프레임 제한
        initialRotation = transform.rotation; // 초기 회전값 저장
        initialPosition = transform.position; // 초기 위치값 저장
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Space 키를 눌렀을 때 회전 시작
            StartCoroutine(RotateClub());
        }
    }

    IEnumerator RotateClub()
    {
        float currentRotation = 0f; // 현재 회전 각도

        while (currentRotation < 360f)
        {
            float rotationStep = speed * Time.deltaTime; // 회전 속도 계산
            transform.RotateAround(pivotPoint, Vector3.back, rotationStep); // 기준점 기준으로 반시계방향 회전
            currentRotation += rotationStep; // 회전 각도 누적

            yield return null; // 다음 프레임까지 대기
        }

        // 회전이 끝난 후 초기 위치와 회전값으로 복원
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}