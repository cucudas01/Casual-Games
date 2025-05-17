using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int destroyedCount = 0;
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