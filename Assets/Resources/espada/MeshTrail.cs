using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    bool activateTrail = false;
    bool isTrailActice = false;
    [SerializeField] SkinnedMeshRenderer skinnedMesh;
    [SerializeField] float refreshRate;
    [SerializeField] Material mat;
    [SerializeField] float destroyDelay;
    [SerializeField] string alphaRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uCore.Action.GetKeyDown(KeyCode.O))
        {
            activateTrail = true;
            StartCoroutine(ActivateTrail());
        }
        if (uCore.Action.GetKeyDown(KeyCode.P))
        {
            activateTrail = false;
        }
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
            mr.material = mat;
            skinnedMesh.materials[1] = mat;

            StartCoroutine(AnimateMaterialAlpha(mr.material, 0.1f, 0.05f));

            Destroy(obj, destroyDelay);

            yield return new WaitForSeconds(refreshRate);
        }
        skinnedMesh.materials[1] = null;
    }
    IEnumerator AnimateMaterialAlpha(Material material, float rate, float freshRate)
    {
        material.SetFloat(alphaRef, 1);
        float valueToAnimate = material.GetFloat(alphaRef);

        while (valueToAnimate > 0)
        {
            Debug.Log(material.GetFloat(alphaRef));
            valueToAnimate -= rate;
            material.SetFloat(alphaRef, valueToAnimate);
            yield return new WaitForSeconds(freshRate);
        }
    }
}
