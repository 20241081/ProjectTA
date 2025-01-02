using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int Coin;    // ���� ����
    public int BestScore;   // �ְ� ����
    public bool[] PlayerSkin = new bool[3];   // ���� ��Ų
    public int CurrentSkin; // �������� ��Ų
}

public class gameManager : MonoBehaviour
{
    private static gameManager _instance;

    public static bool renewFile = false; // ���� ����

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
    string path;    // json ������ ��ġ
    string FileName; // json ���� �̸�
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
        if (File.Exists(path + FileName))   // ���� ���� ���� �� �ε�, ���� �� ����
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

        if (renewFile)   //���� �����Ϳ� ���� ������ ���� ��� ���� ���� 
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
