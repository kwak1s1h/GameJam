using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private PlanetManager planetManager;
    public AudioSource audioSource;

    public bool OnPlay;

    public static GameManager Instance
    {
        get 
        {
            if(instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private PlayerMovement _player = null;
    [SerializeField] private TextMeshProUGUI viewBestPoint;
    [SerializeField] private GameObject effectPrefab;

    public PlayerMovement Player
    {
        get 
        {
            if(_player == null)
                _player = FindObjectOfType<PlayerMovement>();
            return _player;
        }
    }

    private void Awake()
    {
        if(instance != null) 
        {
            Debug.LogError($"Multiple GameManager Instance is running!");
            Destroy(gameObject);
        }
        instance = this;
        planetManager = GetComponent<PlanetManager>();
        audioSource = GetComponent<AudioSource>();

        if(GetBestPoint() > 0)
        viewBestPoint.text = $"최고 점수 : {GetBestPoint()}";
    }

    public void GameOver()
    {
        OnPlay = false;
        if(PlayerPrefs.GetInt("Point", 0) < planetManager.Count)
            PlayerPrefs.SetInt("Point", planetManager.Count);
        Debug.Log("gameOver");
        RePlay();
    }

    public void GameOff() => Application.Quit();

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    

    public void SetTimeScaleZero(int x) => Time.timeScale = x;

    public int GetBestPoint() => PlayerPrefs.GetInt("Point", 0);

    public void CreateEffect()
    {
        Vector2 pos = Player.transform.position;
        Quaternion angle = Player.transform.rotation;

        GameObject effect = Instantiate(effectPrefab, pos, angle);
        Destroy(effect, 1.5f);
    }

    public void RePlay()
    {
        planetManager.Reset();
        planetManager.Retry();
    }

    private IEnumerator PostLevel()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "testNode");
        form.AddField("level", PlanetManager.Instance.Count);

        UnityWebRequest www = UnityWebRequest.Post("https://localhost:9090/record", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
