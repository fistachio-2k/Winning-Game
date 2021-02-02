using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecipeBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject boxLid;
    [SerializeField] private GameObject boxBody;
    [SerializeField] private GameObject recipe;
    [SerializeField] public bool isLocked = true;
    [SerializeField] float duration = 2f;
    private bool _isOpen = false;
    private bool _recipeTaken = false;
    private Vector3 _baseLidRotation;
    private AudioManager audioManager;

    void Start()
    {
        _baseLidRotation = boxLid.transform.rotation.eulerAngles;
        audioManager = FindObjectOfType<AudioManager>();
        recipe.GetComponent<BoxCollider>().enabled = false;
        recipe.GetComponent<MeshCollider>().enabled = false;
    }

    public void Interact()
    {
        _recipeTaken = GameEventsManager._instance.isRecipeCollected();
        Outline outline = boxLid.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
        StartCoroutine(OpenClose());
    }

    IEnumerator OpenClose()
    {
        if (!isLocked)
        {
            if (_isOpen && _recipeTaken)
            {
                boxLid.transform.DORotate(_baseLidRotation, duration);
                _isOpen = false;
            }
            else if(_isOpen && !_recipeTaken)
            {
                recipe.GetComponent<Collect>().Interact();
                _isOpen = true;
            }
            else
            {
                boxLid.transform.DORotate(_baseLidRotation + (Vector3.up * 90f) + (Vector3.forward * 3f) + (Vector3.right * -3f), duration);
                if(recipe == null)
                {
                    recipe.GetComponent<BoxCollider>().enabled = true;
                    recipe.GetComponent<MeshCollider>().enabled = true;
                }
                yield return new WaitForSeconds(1f);
                _isOpen = true;
            }
        }
        yield return null;
    }
}
