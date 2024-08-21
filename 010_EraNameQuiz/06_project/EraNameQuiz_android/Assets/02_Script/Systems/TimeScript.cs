using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimeScript : MonoBehaviour
{
    public GameManagerGengo gamemanagergengo;
	public InterstitialAd interstitialAd;
	static public float time = 10;
	static public int count = 0;
	public ScoreScript ScoreScript;
	public Text countTime;
	public GameObject gameOverText;
	public GameObject scorescreen;
	public Text scoreEnd;
	public GameObject ready;
	public GameObject go;
	static public bool readygoCount = false;
	bool readygo = true;
	bool readygoEnd = false;
	static public bool timeUp = true;
	bool timeUpVois = true;
	static public bool ten = true;//10問までtrue
	public string ad = "AD COUNT";
	public int adcount;

	private int num;



	void Start()
	{
		countTime.text = "";
		gameOverText.SetActive(false);
		scorescreen.transform.localScale = new Vector3(0, 0, 1);
		scorescreen.SetActive(false);
		ready.SetActive(false);
		ready.transform.localScale = new Vector3(0, 0, 1);
		go.SetActive(false);
		ten = true;
		timeUp = true;
		//初期値60を表示
		//float型からint型へCastし、String型に変換して表示
		GetComponent<Text>().text = ((int)time).ToString();
		adcount = PlayerPrefs.GetInt(ad, 0);
	}

    void Update()
    {
		//READY
		if (readygo == true)
		{
			readygo = false;
			StartCoroutine("readyGo");
		}
		//通常カウントダウン
		if (time >= 0) {
			GetComponent<Text> ().text = ((int)time).ToString ();
		}
        //1秒に1ずつ減らしていく
		if(readygoEnd == true && ten == true){
        time -= Time.deltaTime;
		}

        //マイナスは表示しない
		if (time < 0) {
			time = 0;
		}
		//10問終わったら終了→結果表示
		if(gamemanagergengo.count_quiz > 10 && gamemanagergengo.isRanking != true)//一人プレイは10問まで
        {
			gamemanagergengo.count_quiz = 0;
			sucoreEnd ();
		}
        if (gamemanagergengo.isScoreEnd == true)//段位戦はミスった時点で終了
        {
			gamemanagergengo.isScoreEnd = false;
			sucoreEnd();
        }
    }
	public void GibUp(){
		time = 0;
	}

	IEnumerator readyGo(){
		ready.SetActive (true);
		iTween.ScaleTo (ready, iTween.Hash ("x", 1, "y", 1,"time",1));
        gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[13]);
        yield return new WaitForSeconds (3.0f);
        gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[14]);
		//パネル表示
        gamemanagergengo.Mondai.SetActive(true);
        gamemanagergengo.GengoButton1.SetActive(true);
        gamemanagergengo.GengoButton2.SetActive(true);
		gamemanagergengo.GengoButton3.SetActive(true);
		//パネル動き
		iTween.MoveFrom(gamemanagergengo.Mondai, iTween.Hash("x", -30, "time", 0.4f));
        iTween.RotateFrom(gamemanagergengo.GengoButton1, iTween.Hash("y", -90, "time", 0.5f));
        iTween.RotateFrom(gamemanagergengo.GengoButton2, iTween.Hash("y", 90, "time", 0.5f));
		//事後
        ready.SetActive (false);
		go.SetActive (true);
		iTween.ScaleFrom (go, iTween.Hash ("x", 2, "y",2, "delay", 0, "time", 0.1f, "easeType", "easeInOutQuad"));
		readygoEnd = true;
		gamemanagergengo.GameisPlay = true;
		yield return new WaitForSeconds (0.5f);
		go.SetActive (false);
	}
	void sucoreEnd(){
        ten = false;
        timeUp = false;
		gameOverText.SetActive (false);
		scorescreen.SetActive (true);
		iTween.ScaleTo (scorescreen, iTween.Hash ("x", 1, "y", 1,"time",0.3));
		timeUp = false;
		ScoreScript.kyuText();
		newReco();
		//ハイスコア更新処理
		if (gamemanagergengo.isRanking == true)
		{
			ScoreScript.Save();
		}
		//広告
		if (adcount <= 0)
		{
			PlayerPrefs.SetInt(ad, 3);
			interstitialAd.ShowAd();
		}
        else
        {
			adcount--;
			PlayerPrefs.SetInt(ad, adcount);
		}
		
	}
	public int Number{
		set {
			num = value;
			scoreEnd.text = "" + num;
		}
		get {
			return num;
		}
	}
	void newReco(){
		/*if (ScoreScript.score == ScoreScript.highScore) {
			newreco.SetActive (true);
		}*/
	}
}
