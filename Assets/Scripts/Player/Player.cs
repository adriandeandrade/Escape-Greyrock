using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int keycardAmount = 0;
    public bool inRangeToInteract = false;

    [SerializeField] private float reach = 20f;
    private RaycastHit hit;
    private Camera cam;
    private AnimatedObject animatedObject;
    private UIManager uiManager;

    private void Start()
    {
        cam = Camera.main;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, reach);
        if (hit.transform)
        {
            uiManager.Activate("interactionPanel");
            animatedObject = hit.transform.GetComponent<AnimatedObject>();
        }
        else
        {
            animatedObject = null;
            inRangeToInteract = false;
            uiManager.Deactivate("interactionPanel");
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(animatedObject)
            {
                animatedObject.AnimateObject();
            }
        }
    }


}
