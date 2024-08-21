using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    public GameManagerGengo gamemanagergengo;
    public Text Text_answer3;

	

	// Use this for initialization
	void Start () {
		Text_answer3.color = new Color (191f / 255f, 37f / 255f, 37f / 255f);
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeScript.time < 1)
        {
            if (TimeScript.timeUp == true)
            {
                IncorrectAnswer();

            }
        }
    }

    public void ButtonClick()
    {
        if (gamemanagergengo.GameisPlay == true)
        {
            switch (transform.name)
            {
                case "gengo_Button1"://右
                    answerJudgment();
                    break;
                case "gengo_Button2"://左
                    answerJudgment2();
                    break;
                case "gengo_Button3"://真ん中
                    answerJudgment3();
                    break;
                case "gengo_Button4"://次へ
                    gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[16]);
                    gamemanagergengo.mondaiisPlay = true;
                    gamemanagergengo.count_quiz += 1;
                    TimeScript.ten = true;
                    gamemanagergengo.Mondai.SetActive(true);
                    gamemanagergengo.GengoButton1.SetActive(true);
                    gamemanagergengo.GengoButton2.SetActive(true);
                    iTween.MoveFrom(gamemanagergengo.Mondai, iTween.Hash("x", -30, "time", 0.5f));
                    iTween.RotateFrom(gamemanagergengo.GengoButton1, iTween.Hash("y", -90, "time", 0.5f));
                    iTween.RotateFrom(gamemanagergengo.GengoButton2, iTween.Hash("y", 90, "time", 0.5f));
                    gamemanagergengo.answer.SetActive(false);
                    if (gamemanagergengo.isScoreEnd2 == true)
                    {
                        gamemanagergengo.isScoreEnd = true;
                    }
                    break;
                case "BackButton"://タイトルへ
                    if (gamemanagergengo.isBackButton) {
                        SceneNavigator.Instance.Change("02_Title", 0.5f);
                        gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[15]);
                    }
                    break;
                case "ReplayButton"://リトライ
                    if (gamemanagergengo.isReplayButton)
                    {
                        if (TitleManager.loop == 99)
                        {
                            if (LoginBonus.ChallengingCount > 0) {  //挑戦権が１以上である
                                LoginBonus.ChallengingCount = LoginBonus.ChallengingCount - 1;
                                PlayerPrefs.SetInt(LoginBonus.Challenging, LoginBonus.ChallengingCount);
                                PlayerPrefs.Save();
                                SceneNavigator.Instance.Change("03_Main", 0.5f);
                                gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[16]);
                            }
                            else//0であるならタイトルへ
                            {
                                SceneNavigator.Instance.Change("02_Title", 0.5f);
                                gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[15]);
                            }
                        }
                        else
                        {
                            SceneNavigator.Instance.Change("03_Main", 0.5f);
                            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[16]);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    void answerJudgment(){
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 0) {
			Debug.Log("新しいのはどっち？");
			if (gamemanagergengo.answeroneDate [0] > gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] < gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[0] == gamemanagergengo.answertwoDate[0]){
                IncorrectAnswer();
            }

        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("古いのはどっち？");
			if (gamemanagergengo.answeroneDate [0] < gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] > gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[0] == gamemanagergengo.answertwoDate[0]){
                IncorrectAnswer();
            }
        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 0){
			Debug.Log("年数が長いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] > gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] < gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[1] == gamemanagergengo.answertwoDate[1]){
                IncorrectAnswer();
            }
        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("年数が短いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] < gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] > gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[1] == gamemanagergengo.answertwoDate[1]){
                IncorrectAnswer();
            }
        }
	}
	void answerJudgment2(){
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 0) {
			Debug.Log("新しいのはどっち？");
			if (gamemanagergengo.answeroneDate [0] < gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] > gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[0] == gamemanagergengo.answertwoDate[0]){
                IncorrectAnswer();
            }
        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("古いのはどっち？");
			if (gamemanagergengo.answeroneDate [0] > gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] < gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[0] == gamemanagergengo.answertwoDate[0]){
                IncorrectAnswer();
            }
        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 0){
			Debug.Log("年数が長いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] < gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] > gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[1] == gamemanagergengo.answertwoDate[1]){
                IncorrectAnswer();
            }
        }
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("年数が短いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] > gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] < gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate[1] == gamemanagergengo.answertwoDate[1]){
                IncorrectAnswer();
            }
        }
	}
	void answerJudgment3(){
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 0) {
			Debug.Log("新しいのはどっち？");
			if (gamemanagergengo.answeroneDate [0] == gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] != gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}
		}
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 0 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("古いのはどっち？");
			if (gamemanagergengo.answeroneDate [0] == gamemanagergengo.answertwoDate [0]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [0] != gamemanagergengo.answertwoDate [0]){
				IncorrectAnswer ();
			}
		}
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 0){
			Debug.Log("年数が長いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] == gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] != gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}
		}
		if (gamemanagergengo.mondaiDate [gamemanagergengo.mondai] == 4 && gamemanagergengo.mondaisyurui == 1){
			Debug.Log("年数が短いのはどっち？");
			if (gamemanagergengo.answeroneDate [1] == gamemanagergengo.answertwoDate [1]) {
				CorrectAnswer ();
			}else if (gamemanagergengo.answeroneDate [1] != gamemanagergengo.answertwoDate [1]){
				IncorrectAnswer ();
			}
		}
	}
	void CorrectAnswer(){
        gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[11]);
        Debug.Log ("正解");
        gamemanagergengo.answer.SetActive(true);
        TimeScript.ten = false;
        Text_answer3.color = new Color(191f / 255f, 37f / 255f, 37f / 255f);
        Text_answer3.text = "正解";
		gamemanagergengo.count_quiz_answer += 1;
        TimeScript.time = 11;
        gamemanagergengo.Mondai.SetActive(false);
        gamemanagergengo.GengoButton1.SetActive(false);
        gamemanagergengo.GengoButton2.SetActive(false);
    }
	void IncorrectAnswer(){
        gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[12]);
        Debug.Log ("残念");
        gamemanagergengo.answer.SetActive(true);
        TimeScript.ten = false;
        Text_answer3.color = new Color(35f / 255f, 35f / 255f, 198f / 255f);
        Text_answer3.text = "残念";
        TimeScript.time = 11;
        gamemanagergengo.Mondai.SetActive(false);
        gamemanagergengo.GengoButton1.SetActive(false);
        gamemanagergengo.GengoButton2.SetActive(false);
        //段位戦の場合ミスった瞬間終了
        if (gamemanagergengo.isRanking == true)
        {
            gamemanagergengo.isScoreEnd2 = true;
        }
    }
    void OnCompleteHandler()
    {
        gamemanagergengo.iTweenMovingMain = false;
    }
}
