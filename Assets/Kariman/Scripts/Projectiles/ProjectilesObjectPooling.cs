using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesObjectPooling : Singleton<ProjectilesObjectPooling>
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parent;
    [SerializeField] int poolSize;
    private List<GameObject> pooledObjects; 
    private WaitForSeconds waitTime;

    protected override void Awake()
    {
        base.Awake();
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent.transform);
            ProjectileBehavior projectileBehavior = obj.GetComponent<ProjectileBehavior>();
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    private void Start()
    {
        waitTime = new WaitForSeconds(10.0f);
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public void ActivatePooledObject(GameObject obj)
    {
        obj.SetActive(true);
        StartCoroutine(DeactivatePooledObject(obj));
    }

    IEnumerator DeactivatePooledObject(GameObject obj)
    {
        yield return waitTime;
        obj.SetActive(false);
    }
}
