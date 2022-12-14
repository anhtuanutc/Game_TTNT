using UnityEngine;
using System.Collections;
public enum EnumFishAction {Boi, CanCau, DopMoi, NhayVaoGio};
public enum EnumStateGame {Play, Pause, Win, Lose, Menu};

public delegate void OnStateChangeHandler();

public class CGameManager : MonoBehaviour {
	public int[] scoreLevels;
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	public string keyLevelNow = "levelNow";
	public string keyLevelMax = "levelMax";
	public string keyNumberLevel = "numberLevel";
 
	public event OnStateChangeHandler OnStateChange;
	public EnumStateGame gameState { get; private set; }
	public int score { get; private set; }
	public int level { get; private set; }
	public int maxScore { get; private set; }
	public int timePlay { get; private set; }
	public int typeLuoiCau { get; private set; }

	public static CGameManager Instance { get; private set; }
	

	private void Awake() {
		if (Instance != null) {
			DestroyImmediate(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void SetGameState(EnumStateGame gameState) {
		this.gameState = gameState;
		if(OnStateChange!=null) {
			OnStateChange();
		}
	}

	public void SetScore(int newScore) {
		this.score = newScore;
	}

	public void SetLevel(int newLevel) {
		this.level = newLevel;
	}

	public void SetMaxScore(int newMaxScore) {
		this.maxScore = newMaxScore;
	}

	public void SetTimePlay(int newTime) {
		this.timePlay = newTime;
	}

	public void SetTypeLuoiCau(int type) {
		this.typeLuoiCau = type;
	}
}
