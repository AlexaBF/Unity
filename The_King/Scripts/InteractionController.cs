using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    Material originalMaterial;
    public Material overMaterial;
    MeshRenderer meshRenderer;
    public bool removeLimits = false;
    public bool applyLimits = false;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void OnMouseEnter()
    {
        meshRenderer.material = overMaterial;
    }

    private void OnMouseExit()
    {
        meshRenderer.material = originalMaterial;
    }

    private void OnMouseUpAsButton()
    {
        SendMessage("DoAction");
        if (removeLimits)
            SendMessage("RemoveSomeLimits");
        if (applyLimits)
            SendMessage("ApplySomeLimits");
    }
}
