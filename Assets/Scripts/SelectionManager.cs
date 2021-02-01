using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private ISelectionResponse _selectionResponse;
    private Transform _selection;
    [SerializeField] private float _distanceFromObject = 10f;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane * 20));
        _selection = null;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _distanceFromObject))
        {
            var selection = hit.transform;
            IInteractable interactable = selection.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _selection = selection;
                if (InputManager.Instance.GetMouseClick() && hit.distance <= _distanceFromObject)
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
