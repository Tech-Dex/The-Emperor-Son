using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour {

	[SerializeField] string loadSceneName;
	[SerializeField] string spawnToObject;

	private float launch = 1;
	public int health;
	public int maxHealth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
			if (col.gameObject == NewPlayer.Instance.gameObject ) {
				GameManager.Instance.playerUI.animator.SetTrigger ("coverScreen");
				GameManager.Instance.playerUI.loadSceneName = loadSceneName;
				GameManager.Instance.playerUI.spawnToObject = spawnToObject;
				enabled = false;
				Die();
				ResetLevel();
			}
	}


	public void Die(){
		GameManager.Instance.playerUI.animator.SetTrigger ("coverScreen");
		GameManager.Instance.playerUI.loadSceneName = SceneManager.GetActiveScene().name;
		GameManager.Instance.playerUI.spawnToObject = "SpawnStart";
		GameManager.Instance.playerUI.resetPlayer = true;
	}

	public void ResetLevel(){
		health = maxHealth;
		GameManager.Instance.gemAmount = 0;
		GameManager.Instance.inventory.Clear ();
	}

}
