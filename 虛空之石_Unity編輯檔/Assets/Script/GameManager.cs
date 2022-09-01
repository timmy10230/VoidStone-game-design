using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager _instance;
    private Vector3 originPos;

    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;
    public GameObject pauseUI;
    public GameObject playerUI;
    public GameObject gameStartUI;
    public GameObject gameTeaching;

    public GameObject particle;
    public GameObject spaceA;
    public GameObject spaceB;
    public GameObject spaceAStone;
    public GameObject spaceBStone;
    public Animator spaceAStoneDispearAni;
    public Animator spaceBStoneDispearAni;
    public Animator CameraSpaceChangedAni;

    private int starsNum = 0;
    private int totalNum = 21;
    public int nowStarsNum = 0;
    public float starTime;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        gameStartUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            playerControl.pc.canMove = false;
            Time.timeScale = 0;
        }
    }

    public void ShowStar()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (; starsNum < nowStarsNum; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))){
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"),starsNum);
        }
        int sum = 0;
        for(int i = 1; i<= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum",sum);
    }

    public void PauseMenuBackToGame()
    {
        playerControl.pc.canMove = true;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void PauseMenuBackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseMenuReGame()
    {
        SceneManager.LoadScene(2);
    }

    public void SpaceAToB()
    {
        CameraSpaceChangedAni.SetBool("SpaceAToB",true);
        particle.SetActive(true);
    }

    public void SpaceBToA()
    {
        CameraSpaceChangedAni.SetBool("SpaceAToB", false);
        particle.SetActive(false);
    }
}
