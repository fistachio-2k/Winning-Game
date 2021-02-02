using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecipeBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject boxLib;
    [SerializeField] private GameObject boxBody;
    [SerializeField] private GameObject recipe;
    [SerializeField] public bool isLocked = true;
    [SerializeField] float duration = 2f;

    private bool _isOpen = false;
    private Vector3 _baseRotation;

    private AudioManager audioManager;

    void Start()
    {
        _baseRotation = boxLib.transform.rotation.eulerAngles;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Interact()
    {
        StartCoroutine(OpenClose());
    }

    IEnumerator OpenClose()
    {
        if (!isLocked)
        {
            //_isOpen = !_isOpen;
            //if (_isOpen)
            //{
            //    transform.DORotate(_baseRotation, duration);
            //}
            if (!_isOpen)
            {
                boxLib.transform.DORotate(_baseRotation + (Vector3.up * 90f) + (Vector3.forward * 3f) + (Vector3.right * -3f), duration);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                recipe.AddComponent<Collect>();
            }
            else
            {
                if(recipe != null)
                {
                    recipe.GetComponent<Collect>().Interact();
                }
            }
        }
        yield return null;
    }
}
