using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaTrigger1 : MonoBehaviour
{
    [SerializeField] private Collider secondTriggerCollider;
    [SerializeField] private RecipeBox recipeBox;

    private void OnTriggerEnter(Collider other)
    {
        recipeBox.isLocked = false;
        GameEventsManager._instance.PlayMamaEstherScene(gameObject);
        secondTriggerCollider.enabled = true;
    }
}
