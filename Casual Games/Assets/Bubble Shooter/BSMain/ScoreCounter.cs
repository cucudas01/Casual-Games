using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static int destroyedCount = 0;  // 파괴된 버블 수, static으로 선언하여 다른 스크립트, 씬에서도 접근 가능
    public TMP_Text destroyedCountText; // TextMeshPro용

    void Start()
    {
        UpdateDestroyedCountUI();
    }

    public void AddDestroyedCount()
    {
        destroyedCount++;
        Debug.Log(destroyedCount);
        UpdateDestroyedCountUI();
    }

    void UpdateDestroyedCountUI()
    {
        if (destroyedCountText != null)
        {
            destroyedCountText.text = destroyedCount.ToString();
        }
    }
}