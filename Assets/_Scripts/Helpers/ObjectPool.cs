using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Transform parent;
    private readonly Stack<T> objects;

    public ObjectPool(T prefab, Transform parent, int initialSize)
    {
        this.prefab = prefab;
        this.parent = parent;
        objects = new Stack<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            objects.Push(obj);
        }
    }

    public T Get()
    {
        T obj;
        if (objects.Count > 0)
        {
            obj = objects.Pop();
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parent);
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Push(obj);
    }
}
