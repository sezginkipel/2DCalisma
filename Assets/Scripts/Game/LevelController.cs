using System.Collections;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Level değişkenleri.
    public int currentLevel = 1;
    public int levelTime = 60;

    public TMP_Text levelCountdownText;

    void Start()
    {
        StartCoroutine(LevelTimeCountdown());
    }

    // Level time countdown IEnumerator
    public IEnumerator LevelTimeCountdown()
    {
        while (levelTime > 0)
        {
            yield return new WaitForSeconds(1);
            levelTime--;
            levelCountdownText.text = levelTime.ToString();
            if (levelTime < 10)
            {
                levelCountdownText.color = Color.red;
            }
        }

        // Süre dolduğunda yapılacak işlemler
        Debug.Log("Süre doldu! Seviyeyi tamamlayın.");

    }
    
}
