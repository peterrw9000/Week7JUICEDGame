using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public IEnumerator HitFreeze(float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            Time.timeScale = 0;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
    }
}
