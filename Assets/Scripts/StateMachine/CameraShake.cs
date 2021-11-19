using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public Canvas canvas;
    public float shakeAmount;
    float shakeTime;

    Vector3 initialPos;

    public void VibrateForTime(float time)
    {
        shakeTime = time;

        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        canvas.renderMode = RenderMode.WorldSpace;
    }

    private void Start()
    {
        initialPos = canvas.transform.position;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            canvas.transform.position = Random.insideUnitSphere * shakeAmount + initialPos;

            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;

            canvas.transform.position = initialPos;

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    /*
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Debug.Log("Camera is shaking");
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }*/
}
