using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [System.Serializable]
    public class PoolItem
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<PoolItem> items;
    private Dictionary<string, Queue<GameObject>> pools;

    private void Awake()
    {
        Instance = this;
        pools = new Dictionary<string, Queue<GameObject>>();

        foreach (var item in items)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            pools.Add(item.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(tag)) return null;

        GameObject obj = pools[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        pools[tag].Enqueue(obj);

        return obj;
    }
}
