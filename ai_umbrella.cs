using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_umbrella : MonoBehaviour
{
    public bool isOpen = true;
    public float inkyness = 0;
    public float Openness = 0;
    public SkinnedMeshRenderer ourSkinnedMesh;
    private MaterialPropertyBlock materialBlock;

    private void Update()
    {
        if (!isOpen && Openness != 100)
        {
            Openness = Mathf.Clamp(Openness += 500 * Time.deltaTime, 0, 100);
        }
        if (isOpen && Openness != 0)
        {
            Openness = Mathf.Clamp(Openness -= 500 * Time.deltaTime, 0, 100);
        }
        ourSkinnedMesh.SetBlendShapeWeight(0, Openness);

        if (inkyness > 0) inkyness = Mathf.Clamp(inkyness -= 0.6f * Time.deltaTime, 0, 1);
        UpdateMaterialBlock();
    }

    private void UpdateMaterialBlock()
    {
        if (ourSkinnedMesh == null) ourSkinnedMesh = GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        if (materialBlock == null) materialBlock = new MaterialPropertyBlock();

        //set the color property
        materialBlock.SetFloat("_Inkyness", inkyness);
        //reassign the material to the renderer
        ourSkinnedMesh.SetPropertyBlock(materialBlock);
    }
}
