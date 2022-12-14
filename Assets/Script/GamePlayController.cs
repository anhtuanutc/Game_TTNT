using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class GamePlayController : MonoBehaviour {
	public static GamePlayController instance;

	public TextMeshProUGUI time_text, round_text;
	public List<TextMeshProUGUI> score_texts;
	public List<GameObject> arrows;
	public List<DayCau> ropes; 
	public int time, round_max;
	public bool round_player_1 = true;
	public List<Animator> ani_player;
	public Transform parent_resource;
	public List<GameObject> resources;
	public List<GameObject> rocks;

	public Vector2 limit_x, limit_y;

	public bool is_end;

	int cur_time;
	int cur_round;

	private void Awake()
	{
		instance = this;
	}
	
	void Start () {
		StartGame();
		StartCoroutine(CountDown());
		ropes[1].computer = Prefs.NumberPlayer == 1;
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
			round_text.text = cur_round.ToString();
		}
	}

	public int GetRound()
    {
		return cur_round;
    }

	void StartGame()
	{
		is_end = false;
		cur_time = time;
		cur_round = round_max;
		round_player_1 = true;
		SetTurn();
		foreach (var item in score_texts)
		{
			item.text = "Score: 0";
		}
		//Random vi tri vang di--------------------------------
		for (int i = 0; i < 7; i++) {
			Vector2 pos = new Vector2(Random.Range(limit_x.x, limit_x.y), Random.Range(limit_y.x, limit_y.y));
            while (true)
            {
				int count = 0;
                foreach (Transform item in parent_resource)
                {
					if (Vector2.Distance(item.position, pos) < 1.8f)
					{
						pos = new Vector2(Random.Range(limit_x.x, limit_x.y), Random.Range(limit_y.x, limit_y.y));
						break;
					}
					count++;
				}
                if (count == parent_resource.childCount)
                {
					break;
                }
            }
			int index = Random.Range(0, resources.Count);
			var obj = Instantiate(resources[index], pos, Quaternion.identity, parent_resource);
		}
		for (int i = 0; i < 10; i++)
		{
			Vector2 pos = new Vector2(Random.Range(limit_x.x, limit_x.y), Random.Range(limit_y.x, limit_y.y));
			while (true)
			{
				int count = 0;
				foreach (Transform item in parent_resource)
				{
					if (Vector2.Distance(item.position, pos) < 1.8f)
					{
						pos = new Vector2(Random.Range(limit_x.x, limit_x.y), Random.Range(limit_y.x, limit_y.y));
						break;
					}
					count++;
				}
				if (count == parent_resource.childCount)
				{
					break;
				}
			}
			int index = Random.Range(0, rocks.Count);
			var obj = Instantiate(rocks[index], pos, Quaternion.identity, parent_resource);
		}
	}

	public void SetScoretTxt(int point)
    {
		score_texts[round_player_1 ? 0 : 1].text = "Score: " + point;
    }

	void SetTurn()
    {
		ropes[round_player_1 ? 0 : 1].ChangeTurn(true);
		ropes[round_player_1 ? 1 : 0].ChangeTurn(false);
		arrows[round_player_1 ? 0 : 1].SetActive(true);
		arrows[round_player_1 ? 1 : 0].SetActive(false);
		if (round_player_1)
		{
			cur_round--;
		}
        if (cur_round <= 0 && !is_end)
        {
			CanvasController.instance.Win(ropes[0].score, ropes[1].score);
			is_end = true;
        }
	}

	public void CheckResource()
    {
        if (parent_resource.childCount <= 0 && !is_end)
        {
			CanvasController.instance.Win(ropes[0].score, ropes[1].score);
			is_end = true;
		}
    }

	//public void Replay() {
	//	StartGame();
	//}

	public void AniPull(bool miss = true)
    {
		string name_ani = miss ? "player_miss" : "player_pull";
		ani_player[round_player_1 ? 0 : 1].Play(name_ani);
	}

	public void StopAni()
    {
        foreach (var item in ani_player)
        {
			item.Play("New State");
		}
	}
}
