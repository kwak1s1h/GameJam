using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public static PlanetManager Instance
    {
        get 
        {
            if(instance == null)
                instance = FindObjectOfType<PlanetManager>();
            return instance;
        }
    }

    private List<Planet> planets = new List<Planet>();

    private int count = 0;
    public int Count
    {
        get => count;
        set 
        {
            count = value;
            if(PlayerPrefs.GetInt("Point", 0) < count)
            {
                newText.gameObject.SetActive(true);
            }
            if (count >= 4) {
                KillPlanet(planets[0]);
                CreatePlanet();
            }
            countUI.text = count.ToString();
        }
    }
    
    [SerializeField] private float nowY = 0f;
    private const float x = 3f;

    [SerializeField] private TextMeshProUGUI countUI = null;
    [SerializeField] private TextMeshProUGUI newText;
    [SerializeField] private AudioClip ingameBGM;

    private void Awake()
    {
        if(instance != null) {
            Debug.LogError("Multiple PlanetManager Instance is running.");
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Update()
    {
    }

    public void StartGame()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        canvas.Find("Title").GetComponent<RectTransform>().DOAnchorPosY(300f, 0.5f).SetEase(Ease.OutQuart);
        canvas.Find("showBest").GetComponent<RectTransform>().DOAnchorPosY(100f, 0.4f).SetEase(Ease.OutQuart);
        canvas.Find("StartBtn").GetComponent<RectTransform>().DOAnchorPosY(-400f, 1f).SetEase(Ease.OutQuart).OnComplete(() => {
            AudioSource source = GameObject.Find("BG").GetComponent<AudioSource>();
            source.clip = ingameBGM;
            source.Play();
            Init();
        });
    }

    public void Retry()
    {
        Init();
        newText.gameObject.SetActive(false);
    }

    public void Reset()
    {
        Count = 0;
        nowY = 0;
        KillAllPlanet();
        countUI.gameObject.SetActive(false);
        ComboSystem.Instance.ResetCombo();
    }

    private void KillAllPlanet()
    {
        for(int i = 0; i < planets.Count; i++)
        {
            PoolManager.Instance.Push(planets[i].gameObject);
        }
        planets.Clear();
    }

    private void Init()
    {
        Destroy(GameObject.Find("Planet"));
        
        for(int i = 0; i < 10; i++)
        {
            CreatePlanet(i);
        }
        ComboSystem.Instance.count = -1;
        GameManager.Instance.Player.NowPlanet = planets[0];
        countUI.gameObject.SetActive(true);
        GameManager.Instance.OnPlay = true;
        GameObject.Find("Canvas").transform.Find("RestartButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("PauseBtn").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("GameOff").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("GameRay").gameObject.SetActive(true);
    }

    public void CreatePlanet(int idx)
    {
        Planet obj = PoolManager.Instance.Pop("Planet", transform).GetComponent<Planet>();
        planets.Add(obj);
        obj.gameObject.SetActive(true);
        obj.Init(Random.Range(0.75f, 1.5f));
        obj.transform.position = new Vector2(idx % 2 == 0 ? x : -x, nowY);
        obj.OnLeft = (idx % 2 == 0);
        nowY += Random.Range(6, 7.5f);
        if (idx != 0)
            planets[idx - 1].NextPlanet = obj;
    }
    public void CreatePlanet()
    {
        Planet obj = PoolManager.Instance.Pop("Planet", transform).GetComponent<Planet>();
        planets.Add(obj);
        obj.gameObject.SetActive(true);
        obj.Init(Random.Range(0.75f, 1.5f));
        obj.transform.position = new Vector2(planets[^1].OnLeft ? x : -x, nowY);
        obj.OnLeft = !planets[^1].OnLeft;
        nowY += Random.Range(6, 7.5f);
            planets[planets.Count - 2].NextPlanet = obj;
    }

    public void KillPlanet(Planet p)
    {
        PoolManager.Instance.Push(p.gameObject);
        planets.Remove(p);
    }
}
