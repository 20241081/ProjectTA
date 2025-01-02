using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int Coin;    // 보유 코인
    public int BestScore;   // 최고 점수
    public bool[] PlayerSkin = new bool[3];   // 보유 스킨
    public int CurrentSkin; // 적용중인 스킨
}

public class gameManager : MonoBehaviour
{
    private static gameManager _instance;

    public static bool renewFile = false; // 파일 갱신

    public PlayerData nowPlayer = new PlayerData();

    public static gameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(gameManager)) as gameManager;

            }
            return _instance;
        }
    }
    string path;    // json 저장할 위치
    string FileName; // json 파일 이름
    private void Awake()
    {
        nowPlayer.PlayerSkin[0] = true;
        nowPlayer.CurrentSkin = 0;
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/";
        FileName = "PlayerData.json";
    }
    void Start()
    {
        if (File.Exists(path + FileName))   // 저장 파일 존재 시 로드, 없을 시 생성
        {
            LoadData();
        }
        else
        {
            SaveData();
        }
    }

    private void Update()
    {
        string data = File.ReadAllText(path + FileName);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Stage");
        }

        if (renewFile)   //저장 데이터에 변동 사항이 있을 경우 파일 갱신 
        {
            SaveData();
            LoadData();
            renewFile = false;
        }
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + FileName);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        Debug.Log("Loaded! , " + path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + FileName, data);
        Debug.Log("Saved! , " + path);
    }
}
