using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    //private string selectableTag = "Selectable";

    private ISelectionResponse _selectionResponse;
    private Transform _selection;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    void Update()
    {
        if (_selection != null)
        {
            //_selectionResponse.OnDeselect(_selection);
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane * 20));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            var selection = hit.transform;
            IInteractable interactable = selection.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _selection = selection;
                if (InputManager.Instance.GetMouseClick() && hit.distance <= 5f)
                {
                    interactable.Interact();
                }
            }
        }

        if(_selection != null)
        {
            //_selectionResponse.OnSelect(_selection);
        }            
    }


}
