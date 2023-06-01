using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    bool activateTrail = false;
    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    [SerializeField] float refreshRate;
    [SerializeField] Material mat;
    [SerializeField] float destroyDelay;
    [SerializeField] string alphaRef;
    Material[] newMat;
    Material[] oldMat;

    // Start is called before the first frame update
    void Start()
    {
        newMat = new Material[skinnedMesh.materials.Length];
        for (int i = 0; i < newMat.Length; i++)
        {
            newMat[i] = mat;
        }

        oldMat = new Material[skinnedMesh.materials.Length];
        for (int i = 0; i < oldMat.Length; i++)
        {
            oldMat[i] = skinnedMesh.materials[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void startInvisible()
    {
        activateTrail = true;
        skinnedMesh.materials = newMat;
        StartCoroutine(ActivateTrail());
    }
    public void endInvisible()
    {
        skinnedMesh.materials = oldMat;
        activateTrail = false;
    }
    IEnumerator ActivateTrail()
    {
        while (activateTrail)
        {
            GameObject obj = new GameObject();

            obj.transform.SetPositionAndRotation(skinnedMesh.transform.position, skinnedMesh.transform.rotation);

            MeshRenderer mr = obj.AddComponent<MeshRenderer>();
            MeshFilter mf = obj.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();
            skinnedMesh.BakeMesh(mesh);
         
            
            mf.mesh = mesh;
            mr.materials = newMat;

            StartCoroutine(AnimateMaterialAlpha(mr.materials, 0.02f, 0.05f));

            Destroy(obj, destroyDelay);

            yield return new WaitForSeconds(refreshRate);
        }
        skinnedMesh.materials[1] = null;
    }
    IEnumerator AnimateMaterialAlpha(Material[] materials, float rate, float freshRate)
    {
        //material.SetFloat(alphaRef, 1);
        float valueToAnimate = 1;

        while (valueToAnimate > 0)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                Debug.Log(materials[i].GetFloat(alphaRef));
                valueToAnimate -= rate;
                materials[i].SetFloat(alphaRef, valueToAnimate);
                
            }
            yield return new WaitForSeconds(freshRate);
        }
    }
}
