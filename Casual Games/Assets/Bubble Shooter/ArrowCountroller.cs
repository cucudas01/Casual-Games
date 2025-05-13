using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCountroller : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도
    private Vector3 pivotPoint = new Vector3(0f, -8f, 0f); // 회전 기준점
    private float currentAngle = 0f; // 현재 회전 각도
    private float minAngle = -50f; // 왼쪽 최대 방향
    private float maxAngle = 50f; // 오른쪽 최대 방향

    // Update is called once per frame
    void Update()
    {
        // 방향키 입력에 따라 회전
        if (Input.GetKey(KeyCode.LeftArrow) && currentAngle > minAngle)
        {
            // 왼쪽 방향키: 반시계방향 회전
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivotPoint, Vector3.forward, rotationStep);
            currentAngle -= rotationStep;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && currentAngle < maxAngle)
        {
            // 오른쪽 방향키: 시계방향 회전
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivotPoint, Vector3.back, rotationStep);
            currentAngle += rotationStep;
        }

        // 각도 제한 보정
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
    }

    // 현재 각도를 반환하는 메서드
    public float GetCurrentAngle()
    {
        return currentAngle;
    }
}