using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance => _instance;

    [SerializeField] GameData _gameData;
    public GameData GameData { get { return _gameData; } set { _gameData = value; } }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Init()
    {
        Load();
    }

    public void Save()
    {
        // Json 직렬화 하기
        string json = JsonUtility.ToJson(_gameData, true); // 정렬된 형태

        // 외부 폴더에 접근해서 Json 파일 저장하기
        // Application.persistentDataPath : 특정 운영체제에서 앱이 사용할 수 있도록 허용한 경로
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "GameData.json"), json);
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");

        // 파일 존재하는 경우
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            // 파일 없으면 새로 만들기
            Debug.Log($"{path}에 저장된 파일이 없습니다.");
            _gameData = new GameData();
            Save();
            Debug.Log("새로운 파일을 생성했습니다.");
        }
    }
}
