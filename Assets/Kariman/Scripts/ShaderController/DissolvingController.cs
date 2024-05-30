using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class DissolvingController : MonoBehaviour
{
    [SerializeField] VisualEffect VFXGraph;
    [SerializeField] MeshRenderer playerMesh;
    [SerializeField] float dissolveRate = 0.0125f;
    [SerializeField] float refreshRate = 0.025f;
    private Material[] meshMaterial;
    private WaitForSeconds waitTime;


    private void Start()
    {
        waitTime = new WaitForSeconds(refreshRate);

        if (playerMesh != null)
            meshMaterial = playerMesh.materials;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(DissolveControl());
        }
    }
    IEnumerator DissolveControl()
    {
        if(VFXGraph != null)
        {
            VFXGraph.Play();
        }
        if (meshMaterial.Length > 0)
        {
            float counter = 0;
            while (meshMaterial[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;

                for (int i = 0; i < meshMaterial.Length; i++)
                {
                    meshMaterial[i].SetFloat("_DissolveAmount", counter);
                }
                yield return waitTime;
            }
        }
    }
}
