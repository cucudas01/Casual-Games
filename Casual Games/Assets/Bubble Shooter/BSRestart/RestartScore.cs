using UnityEngine;
using TMPro;

public class RestartScore : MonoBehaviour
{
    public TMP_Text destroyedCountText; // UI에 표시할 TextMeshPro 텍스트

    void Start()
    {
        UpdateDestroyedCountUI();
    }

    void UpdateDestroyedCountUI()
    {
        if (destroyedCountText != null)
        {
            destroyedCountText.text = ScoreCounter.destroyedCount.ToString();
        }
    }
}