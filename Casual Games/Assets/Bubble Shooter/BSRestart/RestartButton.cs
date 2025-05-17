using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // 버튼 클릭 시 호출할 함수
    public void OnRestartButtonClicked()
    {
        ScoreCounter.destroyedCount = 0; // 스코어 초기화
        SceneManager.LoadScene("Bubble Shooter");
    }
}