using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManagerGengo : MonoBehaviour {

	public string gengoName;　// 曲名
	private CSVReader gengoData = new CSVReader ();　// 譜面データ
	public GameObject answer; //回答パネル
    public GameObject Mondai; //問題テキスト
    public GameObject GengoButton1;
    public GameObject GengoButton2;
    public GameObject GengoButton3;
    public int count_quiz =1; //問題数
	public int count_quiz_answer =0; //正解数
    public int count_quiz_answer_highScore = 0; //ハイスコア数
    public bool GameisPlay = false; //ゲームプレイ状態
	public bool mondaiisPlay = true; //ゲームプレイ状態
	public Text mondaitext; //問題テキスト
	public Text onetext; //問題テキスト左
	public Text twotext; //問題テキスト右
	public int[] mondaiDate = new int[2]; //乱数問題テキスト
    public int[] answeroneDate = new int[2]; //答え配列
	public int[] answertwoDate = new int[2]; //答え配列
	public int mondaisyurui; //問題の種類
	public int mondai; //
	public int one; //左
	public int two;//右
    public int start;
    public int end;
    public bool iTweenMovingMain = false;
    public Text Text_answer1;
	public Text Text_answer2;
    public AudioSource seSource;
    public AudioClip[] seClip;
    public bool isBackButton = false;
    public bool isReplayButton = false;
    public bool isRanking = false;
    public bool isScoreEnd = false; 
    public bool isScoreEnd2 = false;

    public UnityEngine.Audio.AudioMixer mixer;


    void Start() {
        isBackButton = false;
        isReplayButton = false;
        Mondai.SetActive(false);
        GengoButton1.SetActive(false);
        GengoButton2.SetActive(false);
        GengoButton3.SetActive(false);
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat(TitleManager.Music, 0));
        mixer.SetFloat("SEVolume", PlayerPrefs.GetFloat(TitleManager.SE, 0));
        if (TitleManager.loop == 0)
        {
            start = 0;
            end = 18;
        }
        else if (TitleManager.loop == 1)
        {
            start = 18;
            end = 106;
        }
        else if (TitleManager.loop == 2)
        {
            start = 106;
            end = 155;
        }
        else if (TitleManager.loop == 3)
        {
            start = 155;
            end = 192;
        }
        else if (TitleManager.loop == 4)
        {
            start = 192;
            end = 208;
        }
        else if (TitleManager.loop == 5)
        {
            start = 208;
            end = 249;
        }
        else
        {
            start = 0;
            end = 249;
            isRanking = true;
        }

        mondaiDate[0] = 0; mondaiDate[1] = 4;
        mondaiisPlay = true;
        answer = GameObject.FindGameObjectWithTag("answer");
        gengoData.CsvRead(gengoName);
        /*
        for (int i = 0; i < gengoData.csvDatas.Count; i++)
        {
            for (int j = 0; j < gengoData.csvDatas[i].Length; j++)
            {
            }
        }
        */
        genngouRandom();
        answer.SetActive(false);
    }
    void Update(){
        if (mondaiisPlay == true)
        {
            mondaiisPlay = false;
            genngouRandom();
        }
	}
	void genngouRandom(){
        mondaisyurui = Random.Range(0, 2);
		mondai = Random.Range(0, 2);
		one = Random.Range(start, end);
		two = Random.Range(start, end);
		if(one == two){
			two = Random.Range(start, end);
		}
        if (mondaiDate [mondai] == 0 && mondaisyurui == 0) {
			mondaitext.text = "新しいのはどっち？";
			answeroneDate [0] = int.Parse(gengoData.csvDatas [one] [0]);
			answertwoDate [0] = int.Parse(gengoData.csvDatas [two] [0]);
		}
		if (mondaiDate [mondai] == 0 && mondaisyurui == 1){
			mondaitext.text = "古いのはどっち？";
			answeroneDate[0] = int.Parse(gengoData.csvDatas[one] [0]);
			answertwoDate [0] = int.Parse(gengoData.csvDatas [two] [0]);
		}
		if (mondaiDate [mondai] == 4 && mondaisyurui == 0){
			mondaitext.text = "年数が長いのはどっち？";
			answeroneDate [1] = int.Parse(gengoData.csvDatas [one] [4]);
			answertwoDate [1] = int.Parse(gengoData.csvDatas [two] [4]);
		}
		if (mondaiDate [mondai] == 4 && mondaisyurui == 1){
			mondaitext.text = "年数が短いのはどっち？";
			answeroneDate [1] = int.Parse(gengoData.csvDatas [one] [4]);
			answertwoDate [1] = int.Parse(gengoData.csvDatas [two] [4]);
		}
        onetext.text = gengoData.csvDatas[one][6] + "\n" + gengoData.csvDatas[one][7];
        twotext.text = gengoData.csvDatas[two][6] + "\n" + gengoData.csvDatas[two][7];
        Text_answer1.text = gengoData.csvDatas[one][5] + "\n" + gengoData.csvDatas[one][1] + "\n" + gengoData.csvDatas[one][2] + "～" + gengoData.csvDatas[one][3] + "\n" + gengoData.csvDatas[one][4] + "年";
        Text_answer2.text = gengoData.csvDatas[two][5] + "\n" + gengoData.csvDatas[two][1] + "\n" + gengoData.csvDatas[two][2] + "～" + gengoData.csvDatas[two][3] + "\n" + gengoData.csvDatas[two][4] + "年";
    }
}