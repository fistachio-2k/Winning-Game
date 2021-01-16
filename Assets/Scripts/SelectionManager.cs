using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "fridge";

    private IRayProvider _rayProvider;
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
            _selectionResponse.OnDeselect(_selection);
        }

        var ray = _rayProvider.CreateRay();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }

        if(_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }            
    }


}
