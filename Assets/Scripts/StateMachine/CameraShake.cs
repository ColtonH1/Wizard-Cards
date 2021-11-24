using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public Canvas canvas;
    private Camera mainCamera;
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
        mainCamera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        /*
        Transform[] canvasChildren = new Transform[canvas.transform.childCount];
        initialPos = new Vector3[canvas.transform.childCount];

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvasChildren[i] = canvas.gameObject.transform.GetChild(i).transform;
            Debug.Log(canvasChildren[i]);
            Debug.Log(canvas.transform.childCount);
        }*/
        if (shakeTime > 0)
        {
 
            for(int i =0; i < canvas.transform.childCount; i++)
            {
                initialPos = new Vector3(canvas.gameObject.transform.position.x, canvas.gameObject.transform.position.y, 0);
                canvas.transform.position = Random.insideUnitSphere * shakeAmount + initialPos;
            }
            //canvas.transform.position = Random.insideUnitSphere * shakeAmount + initialPos;

            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;
            for(int i = 0; i < canvas.transform.childCount; i++)
            {
                canvas.transform.position = initialPos;
            }
                
            canvas.transform.position = initialPos;

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        /*
        transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y, transform.position.z);

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        //Vector3 vector3 = new Vector3(rectTransform.rect.xMin, rectTransform.rect.yMin, 0);
        Bounds canvasBounds = new Bounds(new Vector3(canvasRectTransform.rect.xMin, canvasRectTransform.rect.yMin, 0), new Vector3(canvasRectTransform.rect.xMax, canvasRectTransform.rect.yMax, 0));

        //float cameraDistance = 2.0f; // Constant factor
        Vector3 objectSizes = canvasBounds.max - canvasBounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        //float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * mainCamera.fieldOfView); // Visible height 1 meter in front
        //float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        //distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        //mainCamera.transform.position = canvasBounds.center - distance * mainCamera.transform.forward;
        mainCamera.fieldOfView = objectSize;*/

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
