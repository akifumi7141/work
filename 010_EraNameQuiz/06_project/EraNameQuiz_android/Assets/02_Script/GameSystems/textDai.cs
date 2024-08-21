using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class textDai : MonoBehaviour
{
    public GameManagerGengo gamemanagergengo;
    public Text dainanmonText;

	// Use this for initialization
	void Start () {
		dainanmonText.text = "第1問";
	}
	
	// Update is called once per frame
	void Update () {
		dainanmon ();
	}
	public void dainanmon(){
		dainanmonText.text = "第"+ gamemanagergengo.count_quiz+"問";
	}
}
