using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private string selectableTag = "Selectable";

    private ISelectionResponse _selectionResponse;
    private Transform _selection;
    private IRayProvider _rayProvider;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
        _rayProvider = GetComponent<IRayProvider>();
    }

    void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        var ray = _rayProvider.CreateRay();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            var selection = hit.transform;
            Debug.Log(selection.name);
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
            IInteractable interactable = selection.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _selection = selection;
                if (InputManager.Instance.GetMouseClick())
                {
                    interactable.Interact();
                }
            }
        }

        if(_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }            
    }


}
