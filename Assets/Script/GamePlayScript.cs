using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class GamePlayScript : MonoBehaviour {
	public static GamePlayScript instance;

	public TextMeshProUGUI time_text;
	public List<TextMeshProUGUI> score_texts;
	public List<GameObject> arrows;
	public List<DayCau> ropes; 
	public int time;
	public int round_max;
	public bool round_player_1 = true;

	int cur_time;

	public GameObject []levelsVang;
	private void Awake()
	{
		instance = this;
	}
	
	void Start () {
		time = 15;
		cur_time = time;
		startGame();
		StartCoroutine(CountDown());
	}
	public IEnumerator CountDown ()
	{
		while(true){
			yield return new WaitForSeconds (1);
			if(cur_time > 0) {
				cur_time --;
			}
			else {
				round_player_1 = !round_player_1;
				cur_time = time;
				SetTurn();
			}
			time_text.text = cur_time + "s";
		}
	}

	void startGame() {
		round_max = 10;
		round_player_1 = true;
		SetTurn();
		foreach (var item in score_texts)
		{
			item.text = "Score: 0";
		}
		//Random vi tri vang di
		for (int i = 0; i < levelsVang.Length; i++) {
			
		}
	}

	public void SetScoretTxt(int point)
    {
		score_texts[round_player_1 ? 0 : 1].text = "Score:" + point;
    }

	void SetTurn()
    {
		ropes[round_player_1 ? 0 : 1].ChangeTurn(true);
		ropes[round_player_1 ? 1 : 0].ChangeTurn(false);
		arrows[round_player_1 ? 0 : 1].SetActive(true);
		arrows[round_player_1 ? 1 : 0].SetActive(false);
	}

	public void replay() {
		startGame();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
