using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    static ResourceManager _instance;
    public static ResourceManager Instance => _instance;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // Resource에서 불러오기
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // Resource에서 불러와서 인스턴스화
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"불러오기 실패 : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    // 오브젝트 삭제
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}