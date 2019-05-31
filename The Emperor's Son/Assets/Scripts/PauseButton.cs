using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctionsMenu animatorFunctions;
	[SerializeField] int thisIndex;
    public GameObject pauseMenuUI;

    public PauseMenu something;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetKeyDown("joystick button 6") || Input.GetAxisRaw ("Submit") == 1){
				animator.SetBool ("pressed", true);
					StartCoroutine(Coroutine());
					if(thisIndex == 0){
						something.Resume();
					}
					if(thisIndex == 1){
						Scene resetLevel = SceneManager.GetActiveScene();
    					SceneManager.LoadScene (resetLevel.buildIndex);
						GameManager.Instance.killCounter = 0;
						something.Resume();
						
					}
					if(thisIndex == 2){
						SceneManager.LoadScene("Menu");
						GameManager.Instance.killCounter = 0;
						something.Resume();
						
					}
					if(thisIndex == 3)
						Application.Quit();

			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
}


	private IEnumerator Coroutine()
{
    yield return new WaitForSeconds(1f);
}
	
}
