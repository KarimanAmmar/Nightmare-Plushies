using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class DissolvingController : MonoBehaviour
{
    [SerializeField] MeshRenderer playerMesh;
    [SerializeField] float dissolveRate = 0.0125f;
    [SerializeField] float refreshRate = 0.025f;
    [SerializeField] GameEvent DissolveEvent;
    private Material[] meshMaterial;
    private WaitForSeconds waitTime;

    private void OnEnable() => DissolveEvent.GameAction += StartDissolve;
    private void OnDisable() => DissolveEvent.GameAction -= StartDissolve;
    private void Start()
    {
        waitTime = new WaitForSeconds(refreshRate);

        if (playerMesh != null)
            meshMaterial = playerMesh.materials;
    }
    void StartDissolve() => StartCoroutine(DissolveControl());
    IEnumerator DissolveControl()
    {
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
