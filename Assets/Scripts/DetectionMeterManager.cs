using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionMeterManager : MonoBehaviour
{
    private Image detectionBar;

    private void Start()
    {
        detectionBar = transform.GetChild(0).GetComponent<Image>();
        detectionBar.fillAmount = 0;
    }

    private void Update()
    {
        detectionBar.fillAmount = GlobalVariables.instance.detectionMeterValue / 100f;
    }
}
