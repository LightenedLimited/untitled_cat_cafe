using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public Material prehighlight;
    public Material highlight;
    public Renderer meshRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<SkinnedMeshRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightInteractable()
    {
        Material[] mat = meshRenderer.materials;
        mat[0] = highlight;
        meshRenderer.materials = mat; 
    }
    public void unHighlightInteractable()
    {
        Material[] mat = meshRenderer.materials;
        mat[0] = prehighlight;
        meshRenderer.materials = mat;
    }
}
