using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get 
        { 
            if(instance == null) 
                instance = FindObjectOfType<PoolManager>(); 
            return instance; 
        }
    }

private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] private List<GameObject> poolData = new List<GameObject>();

    private void Awake()
    {
        if(instance != null) {
            Debug.LogError("Multiple PoolManager Instance is running.");
            Destroy(gameObject);
        }
        instance = this;
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < poolData.Count; i++)
        {
            pools.Add(poolData[i].name, new Queue<GameObject>());
            for(int j = 0; j < 5; j++)
            {
                GameObject obj = Instantiate(poolData[i], transform);
                pools[poolData[i].name].Enqueue(obj);
                obj.SetActive(false);
                obj.name = obj.name.Replace("(Clone)", null);
            }
        }
    }

    public GameObject Pop(string name, Transform parent = null)
    {
        GameObject result;
        if(pools[name].Count > 0)
        {
            result = pools[name].Dequeue();
        }
        else 
        {
            result = Instantiate(poolData.Find(d => d.name == name));
            result.name = name.Replace("(Clone)", null);
        }
        result.SetActive(true);
        result.transform.parent = parent;
        return result;
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        pools[obj.name].Enqueue(obj);
        obj.transform.SetParent(transform);
    }
}
