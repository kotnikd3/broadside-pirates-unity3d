using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Harbor : MonoBehaviour {

	public GameObject player;
	public GameObject upgradeCanvas;
	public GameObject coinGoalText;

	public Text speedLevelText;
	public Text projectileLevelText;
	public Text shotLevelText;
	public Text lifeLevelText;

	public int coinGoal = 3;
	public int coinIncrement = 1;

	public GameObject[] projectileSources;

	private int maxLevel = 5;
	private int shipSpeedLevel = 1;
	private int projectileSpeedLevel = 1;
	private int shotSpeedLevel = 1;
	private int lifeLevel = 1;

	// Use this for initialization
	void Start () {
		coinGoalText.GetComponent<TextMesh> ().text = coinGoal.ToString ();

		speedLevelText.text = "Level " + shipSpeedLevel.ToString ();
		projectileLevelText.text = "Level " + projectileSpeedLevel.ToString ();
		shotLevelText.text = "Level " + shotSpeedLevel.ToString ();
		lifeLevelText.text = "Level " + lifeLevel.ToString ();
	}

	// Update is called once per frame
	void Update () 
	{
		// exit if there is a game manager and the game is over
		if (player) {
			int playerCoins = GameManager.gm.score;
			Vector3 posHarbor = this.transform.position;
			Vector3 posPlayer = player.transform.position;
			posHarbor.y = 0f;
			posPlayer.y = 0f;
			
			float distance = Vector3.Distance (posHarbor, posPlayer);
			
			if (playerCoins >= coinGoal && distance < 40) {
				upgradeCanvas.SetActive (true);
			} else {
				upgradeCanvas.SetActive (false);
			}
		}
	}

	public void UpgradeBoatSpeed ()
	{
		if (shipSpeedLevel < maxLevel) {
			Boat ladja = player.GetComponent<Boat> ();
			ladja.accellerateSpeed = ladja.accellerateSpeed + 100;
			
			GameManager.gm.Steal (coinGoal);
			coinGoal = coinGoal + coinIncrement;
			coinGoalText.GetComponent<TextMesh> ().text = coinGoal.ToString ();
			
			shipSpeedLevel = shipSpeedLevel + 1;
			speedLevelText.text = "Level " + shipSpeedLevel.ToString ();
		}
	}

	public void UpgradeProjectileSpeed ()
	{
		if (projectileSpeedLevel < maxLevel) {
			foreach (GameObject source in projectileSources) {
				Shooter s = source.GetComponent<Shooter> ();
				s.power = s.power + 10;
			}
			
			GameManager.gm.Steal (coinGoal);
			coinGoal = coinGoal + coinIncrement;
			coinGoalText.GetComponent<TextMesh> ().text = coinGoal.ToString ();
			
			projectileSpeedLevel = projectileSpeedLevel + 1;
			projectileLevelText.text = "Level " + projectileSpeedLevel.ToString ();
		}
	}

	public void UpgradeRateOfFire ()
	{
		if (shotSpeedLevel < maxLevel) {
			foreach (GameObject source in projectileSources) {
				Shooter s = source.GetComponent<Shooter> ();
				s.cooldown = s.cooldown - 0.1f;
			}
			
			GameManager.gm.Steal (coinGoal);
			coinGoal = coinGoal + coinIncrement;
			coinGoalText.GetComponent<TextMesh> ().text = coinGoal.ToString ();
			
			shotSpeedLevel = shotSpeedLevel + 1;
			shotLevelText.text = "Level " + shotSpeedLevel.ToString ();
		}
	}

	public void UpgradeLives () 
	{
		if (lifeLevel < maxLevel) {
			Health h = player.GetComponent<Health> ();
			h.ApplyBonusHealth (1);
			
			GameManager.gm.Steal (coinGoal);
			coinGoal = coinGoal + coinIncrement;
			coinGoalText.GetComponent<TextMesh> ().text = coinGoal.ToString ();
			
			lifeLevel = lifeLevel + 1;
			lifeLevelText.text = "Level " + lifeLevel.ToString ();
		}
	}
}
