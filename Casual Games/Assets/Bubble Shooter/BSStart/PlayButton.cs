using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // UI 버튼에 연결할 함수
    public void OnPlayButtonClicked()
    {
        ScoreCounter.destroyedCount = 0; // 스코어 초기화
        SceneManager.LoadScene("Bubble Shooter");
    }
}