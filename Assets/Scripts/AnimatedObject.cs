using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float animationTime;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private MovementType movementType;

    private enum MovementType { SLIDE, ROTATE };
    private Hashtable iTweenArgs;

    private void Start()
    {
        iTweenArgs = iTween.Hash();
        iTweenArgs.Add("rotation", openPosition);
        iTweenArgs.Add("time", animationTime);
        iTweenArgs.Add("isLocal", true);
    }

    public void AnimateObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                iTweenArgs["rotation"] = closedPosition;
            }
            else
            {
                iTweenArgs["rotation"] = openPosition;
            }

            isOpen = !isOpen;

            switch(movementType)
            {
                case MovementType.SLIDE:
                    iTween.MoveTo(gameObject, iTweenArgs);
                    break;
                case MovementType.ROTATE:
                    iTween.RotateTo(gameObject, iTweenArgs);
                    break;
            }
        }
    }
}
