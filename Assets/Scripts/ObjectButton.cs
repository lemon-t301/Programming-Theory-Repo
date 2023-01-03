using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public Material mEnterMaterial;
    public Material mDownMaterial;
    //public Material mOverMaterial;

    private MeshRenderer _meshRenderer;
    private Material _startMaterial;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _startMaterial = _meshRenderer.material;
    }

    private void OnMouseEnter()
    {
        ChangeMaterial(mEnterMaterial);
    }
    private void OnMouseDown()
    {
        ChangeMaterial(mDownMaterial);
    }

    private void OnMouseUpAsButton()
    {
        ChangeMaterial(mEnterMaterial);
        EventManager.TriggerEvent("ObjectItemClicked", gameObject);
    }

    private void OnMouseExit()
    {
        ResetMaterial();
    }

    private void ResetMaterial()
    {
        ChangeMaterial(_startMaterial);
    }

    private void ChangeMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
