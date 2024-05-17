using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : PooledObject
{
    /// <summary>
    /// Pool�� ��� ���� ������Ʈ�� ������
    /// </summary>
    public GameObject origianlPrefab;

    /// <summary>
    /// Pool�� ũ��, ó���� �����ϴ� ������Ʈ ������ 2^n���� ��� ���� ����
    /// </summary>
    public int poolSize = 64;

    /// <summary>
    /// Pool�� ������ ������Ʈ�� ����ִ� �迭
    /// </summary>
    T[] pool;

    /// <summary>
    /// ��밡����(��Ȱ��ȭ �Ǿ� �ִ�) ������Ʈ�� ����ִ� Queue
    /// </summary>
    Queue<T> readyQueue;

    public void Initialize()
    {
        
        if(pool == null)
        {
            // Pool�� ���� ��
            pool = new T[poolSize];
            readyQueue = new Queue<T>(poolSize);

            //readyQueue.Count;         // ���� �ִ� ����
            //readyQueue.Capatity;      // ���� �̸� �غ��� ���� ����

            GenerateObject(0, poolSize, pool);
        }
        else       
        {
            // Pool�� �̹� ���� ��
            foreach(T obj in pool) 
            { 
                obj.gameObject.SetActive(false);
            }
        }
        
    }

    public T GetObject()
    {
        if (readyQueue.Count > 0)  // Queue�� �����ִ°� ���� ��
        {
            T comp = readyQueue.Dequeue();
            comp.gameObject.SetActive(true);
            return comp;
        }
        else      // Queue�� �����ִ°� ���� ��
        {
            ExpandPool();           // Pool Ȯ��
            return GetObject();     // Ȯ�� ��Ų Queue���� ������
        }
    }

    /// <summary>
    /// Ǯ�� �ι�� Ȯ���Ű�� �޼���
    /// </summary>
    void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} Ǯ ������ ����.({poolSize} -> {poolSize * 2})");

        int newSize = poolSize * 2;
        T[] newPool = new T[newSize];
        //Queue�� �˾Ƽ� �� ŭ

        for(int i = 0; i < poolSize; i++)
        {
            newPool[i] = pool[i];
        }

        GenerateObject(poolSize, newSize, newPool);
        //pool.Free();
        pool = newPool;
        poolSize = newSize;
    }

    /// <summary>
    /// Pool�� ������Ʈ�� �����ϴ� �ڵ�
    /// </summary>
    /// <param name="startIndex">���� �ε���</param>
    /// <param name="endIndex">�� �ε��� - 1</param>
    /// <param name="arr">������ �迭</param>
    protected virtual void GenerateObject(int startIndex, int endIndex, T[] arr)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject obj = Instantiate(origianlPrefab, transform);            // ���� ������Ʈ�� �ڽ����� ����
            obj.name = $"{origianlPrefab.name}_{i}";

            T comp = obj.GetComponent<T>();                                     // T�� PooledObject�� ������ �ۿ� ����(�ʱ� ���� where)
            comp.onDisable += () => readyQueue.Enqueue(comp);                   // ��Ȱ��ȭ �Ǹ� Queue�� ����

            arr[i] = comp;
            obj.SetActive(false);           // onDisable �Լ� ȣ��Ǿ� �ٷ� readyQueue�� ��(�׷��� ó������ ��Ȱ��ȭ �Ǹ� ȣ�� �ȵ�)
        }
    }
}
