using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject doorContainer;
    public float doorAnimationDuration;
    public AnimationCurve curve;

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key") && !isOpen)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        YieldInstruction instruction = new WaitForEndOfFrame();

        Vector3 origin = doorContainer.transform.eulerAngles;
        Vector3 destination = origin;
        destination.y -= 90;
        Vector3 currentRotation;

        float currentLerpTime = 0;
        float clampLerpTime;

        while (true)
        {
            currentLerpTime += Time.deltaTime;

            if(currentLerpTime > doorAnimationDuration)
            {
                isOpen = true;
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / doorAnimationDuration);
            currentRotation = Vector2.Lerp(origin, destination, curve.Evaluate(clampLerpTime));

            doorContainer.transform.eulerAngles = currentRotation;
            yield return instruction;
        }
    }
}