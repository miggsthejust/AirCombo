using UnityEngine;
using System.Collections;

public class RoundEnd : MonoBehaviour
{
    private ScreenFade sFade;
    private Main gameMain;
    public float koDelay = 5.0f;
    public float lastHitTime = 2.0f;
    public float lastHitTimescale = 0.2f;
    public int pOneRounds = 0;
    public int pTwoRounds = 0;
    public int gameCount = 0;
    private bool bKo = false;
    private GameObject winText;
    private GameObject koText;
    public GameObject readyTextFab;
    public GameObject koTextFab;
    public int roundsToWin;
    public string winString;
    public GameObject cam;
    public bool bDouble;

    public GameObject hud;
    public GameObject roundCounter;
    public Vector3[] counterPOS;

    void Awake()
    {
        sFade = gameObject.GetComponent<ScreenFade>();
        gameMain = gameObject.GetComponent<Main>();

    }

    void Update()
    {
        if (bKo)
        {
            Time.timeScale = lastHitTimescale;
        }

    }
    public void KoCalled(bool f1)
    {
        StartCoroutine("playerKo", f1);
    }
    // when a player health drops to zero, do this stuff.
    IEnumerator playerKo(bool f1)
    {
        yield return new WaitForSeconds(koDelay);

        koText = Instantiate(koTextFab, new Vector3(cam.transform.position.x, 8.0f, -10.0f), Quaternion.identity) as GameObject;
        koText.transform.parent = cam.transform;


        Debug.Log("Player 2 Defeated");

        bKo = true;
        Time.timeScale = lastHitTimescale;
        yield return new WaitForSeconds(lastHitTime);
        bKo = false;
        Time.timeScale = gameMain.gameSpeed;
        //Time.timeScale = 1.0f;
        StartCoroutine(roundEndRoutine());
        Destroy(koText.gameObject);
    }

    public void Cancel()
    {
        StopAllCoroutines();
        bKo = false;
    }

    IEnumerator GameWin(bool p2win)
    {

        //gameMain.bRunGame = false;
        gameMain.EndRoutines();
        yield return new WaitForSeconds(0.5f);
        // give round counter to winner.
        // highlight game win counter for correct player

        yield return new WaitForSeconds(0.2f);
        winText = Instantiate(readyTextFab, new Vector3(cam.transform.position.x, 8.0f, -10.0f), Quaternion.identity) as GameObject;
        winText.transform.parent = cam.transform;

        if (p2win == false)
        {
            winString = "Player One Wins";

        }
        else
        {
            winString = "Player Two Wins";

        }

        winText.GetComponent<TextMesh>().text = winString;

        // cam move in and fighter anim
        yield return new WaitForSeconds(1.5f);
        Destroy(winText.gameObject);
        gameMain.gameOver = true;

        if (!gameMain.isSpectator)
        {
            gameMain.GameEndMenu();
            gameMain.gameEndMenu = true;
        }
    }

    IEnumerator roundEndRoutine()
    {
        // stop gameplay
        //gameMain.bRunGame = false;
        gameMain.EndRoutines();

        yield return new WaitForSeconds(0.5f);
        // give round counter to winner.

        sFade.fadeOut = true;
        yield return new WaitForSeconds(1.2f);

        gameMain.RestartRound();
    }
}