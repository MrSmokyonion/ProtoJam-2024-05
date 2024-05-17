using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // Ŭ���� ���� T Ÿ���� ��� <T>�� ��ü
    // �̱����� T�� ������Ʈ Ÿ��

    /// <summary>
    /// ù �������� Ȯ���ϴ� ����
    /// </summary>
    private bool initialized;

    /// <summary>
    /// �̹� ���� ó���� ������ Ȯ���ϱ� ���� ����
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// �̱����� ��ü
    /// </summary>
    private static T instance;

    /// <summary>
    /// �̱����� ��ü�� �б� ���� ������Ƽ 
    /// </summary>
    public static T Ins
    {
        get
        {
            if (isShutDown)      // ���� ó���� �� ��Ȳ���� Ȯ��
            {
                Debug.LogWarning("�̱����� �̹� �����Ǿ���");
                return null;
            }

            if (instance == null)    // instance�� ����
            {
                T singleton = FindObjectOfType<T>();
                if (singleton == null)  // ������ ������ ���� ������Ʈ ����
                {
                    GameObject obj = new GameObject();
                    obj.name = $"{typeof(T).Name} Singleton";
                    singleton = obj.AddComponent<T>();
                }

                instance = singleton;       // intance�� ã�Ұų� ������� ��ü ����
                DontDestroyOnLoad(instance.gameObject);     //���� �������� ���� ���� �ʰ� ����
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)  // �ڱ��ڽ��� ������µ� instance�� ���� ��
        {
            // ���� ��ġ�Ǿ� �ִ� ù��° �̱����� �ڱ� �ڽ����� ���
            // Singleton<T> �� T�� ���� ���ϹǷ� ĳ���� �õ�
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // instance�� �ִµ� �װ� �ڱ� �ڽ��� �ƴ� ��
            if (instance != this) Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!initialized)
        {
            OnPreInitalize();
        }

        if (mode != LoadSceneMode.Additive)
        {
            OnInitalize();
        }
    }

    /// <summary>
    /// ���� ������ �� �ҷ��� �Լ�
    /// </summary>
    protected virtual void OnInitalize()
    {
        
    }

    /// <summary>
    /// �̱����� ����� ���� ���ѹ��� ȣ��� �Լ�
    /// </summary>
    protected virtual void OnPreInitalize()
    {
        initialized = true;
    }

    private void OnApplicationQuit()
    {
        isShutDown = true;
    }
}