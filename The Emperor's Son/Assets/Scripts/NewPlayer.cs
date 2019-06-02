using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
public class NewPlayer : PhysicsObject {

	public AudioSource audioSource;
	private AnimatorFunctions animatorFunctions;
	private Vector3 origLocalScale;
	public bool frozen = false;
	private float launch = 1;
	public int health;
	public int maxHealth;
	[SerializeField] private ParticleSystem deathParticles;
	[SerializeField] Vector2 launchPower;
	public GameObject attackHit;
	public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
	public float secondJumpTakeOffSpeed = 7;
	private bool canDoubleJump;
	public bool recovering;
	public float recoveryCounter;
	public float recoveryTime = 2;
	public CameraEffects cameraEffect;
	public string groundType = "grass";
	public AudioClip stepSound;
	public AudioClip jumpSound;
	public AudioClip grassSound;
	public AudioClip stoneSound;
	[SerializeField] private float launchRecovery;
	[SerializeField] private GameObject graphic;
	[SerializeField] private Animator animator;
	[SerializeField] private bool jumping;

	public RaycastHit2D ground;

	private static NewPlayer instance;
	public static NewPlayer Instance{
		get 
		{ 
			if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>(); 
			return instance;
		}
	}

	void Start(){
        ScoreScript.scoreValue = 0;
        GamePad.SetVibration(0, 0f, 0f);
        health = maxHealth;
		audioSource = GetComponent<AudioSource>();
		animatorFunctions = GetComponent<AnimatorFunctions>();
		origLocalScale = transform.localScale;
		SetGroundType ();
	}

    protected override void ComputeVelocity()
    {
		//Player movement && attack
		Vector2 move = Vector2.zero;

		ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up);
		launch += (0 - launch) * Time.deltaTime*launchRecovery;

		if (!frozen) {
			move.x = Input.GetAxis ("Horizontal") + launch;

			if (Input.GetButtonDown ("Space") || Input.GetKeyDown("joystick button 0")) {
				if(grounded){
				velocity.y = jumpTakeOffSpeed;
				PlayJumpSound ();
				canDoubleJump = true;
				}
				else
				{
					if(canDoubleJump){
						velocity.y = secondJumpTakeOffSpeed;
						PlayJumpSound ();
						canDoubleJump = false;
					}
				}
				PlayStepSound ();
			}

			if (move.x > 0.01f) {
				if (graphic.transform.localScale.x < 0) {
					graphic.transform.localScale = new Vector3 (origLocalScale.x, transform.localScale.y, transform.localScale.z);
				}
			} else if (move.x < -0.01f) {
				if (graphic.transform.localScale.x > 0) {
					graphic.transform.localScale = new Vector3 (-origLocalScale.x, transform.localScale.y, transform.localScale.z);
				}
			}

			//Attack

			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown("joystick button 2") ) {
				animator.SetTrigger ("attack");
			}
		} else {
			launch = 0;
		}

		if(recovering){
			recoveryCounter += Time.deltaTime;
			if (recoveryCounter >= recoveryTime) {
				recoveryCounter = 0;
				recovering = false;
			}
		}
			
		animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
    }

	public void SetGroundType(){
		switch (groundType) {
		case "Grass":
			stepSound = grassSound;
			break;
		case "Stone":
			stepSound = stoneSound;
			break;
		}
	}

	public void Freeze(bool freeze){
		frozen = freeze;
		launch = 0;
	}

	public void PlayStepSound(){
		audioSource.pitch = (Random.Range(0.6f, 1f));
		audioSource.PlayOneShot(NewPlayer.Instance.stepSound);
	}

	public void PlayJumpSound(){
		audioSource.pitch = (Random.Range(0.6f, 1f));
		audioSource.PlayOneShot(NewPlayer.Instance.jumpSound);
	}

	public void Hit(int launchDirection){
		if (!recovering) {
			cameraEffect.Shake (100, 1);
			animator.SetTrigger ("hurt");
			velocity.y = launchPower.y;
			launch = launchDirection * (launchPower.x);
			recoveryCounter = 0;
			recovering = true;
			GamePad.SetVibration(0, 1f, 1f);
			StartCoroutine(timer());
			if (health <= 0) {
				GamePad.SetVibration(0, 2f, 2f);
				StartCoroutine(timer());
				Die ();
			} else {
				
				health -= 1;
				
			}
			GameManager.Instance.playerUI.HealthBarHurt ();
		}
	}

	public void Die(){
        GamePad.SetVibration(0, 0f, 0f);
        deathParticles.gameObject.SetActive (true);
		deathParticles.Emit (10);
		deathParticles.transform.parent = transform.parent;
		GameManager.Instance.playerUI.animator.SetTrigger ("coverScreen");
		if(SceneManager.GetActiveScene().name != "EndlessMode")
        	GameManager.Instance.playerUI.loadSceneName = SceneManager.GetActiveScene().name;
		else
    		SceneManager.LoadScene("DeathMenu");
        GameManager.Instance.playerUI.spawnToObject = "SpawnStart";
		GameManager.Instance.playerUI.resetPlayer = true;
		GetComponent<MeshRenderer> ().enabled = false;
		Freeze (true);
	}

	public void ResetLevel(){
		Freeze (false);
		health = maxHealth;
		deathParticles.gameObject.SetActive (false);
		GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Instance.gemAmount = 0;
		GameManager.Instance.inventory.Clear ();
	}

	IEnumerator timer()
    {
        yield return new WaitForSeconds(0.5f);
        GamePad.SetVibration(0, 0f, 0f);
    }
}