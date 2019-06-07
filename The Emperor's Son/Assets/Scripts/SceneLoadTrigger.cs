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

	void OnTriggerStay2D(Collider2D col){
			if (col.gameObject == NewPlayer.Instance.gameObject ) {
				if (GameManager.Instance.killCounter <= 5){
				GameManager.Instance.playerUI.animator.SetTrigger ("coverScreen");
				GameManager.Instance.playerUI.loadSceneName = loadSceneName;
				enabled = false;
				}
				else
				{
				GameManager.Instance.playerUI.animator.SetTrigger ("coverScreen");
				GameManager.Instance.playerUI.loadSceneName = "WinMenu";
				enabled = false;
				}
			}
}
}
