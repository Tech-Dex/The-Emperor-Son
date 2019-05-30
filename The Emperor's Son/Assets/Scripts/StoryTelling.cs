using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StoryTelling : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public bool ok = true ;
    Coroutine MyCoroutineReference;
    [SerializeField] private Animator icon;
    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject == NewPlayer.Instance.gameObject && ok){
            icon.SetBool ("active", true);
            if ((Input.GetAxis ("Submit") > 0) || (Input.GetKeyDown("joystick button 1") )) {
                textDisplay.text = string.Empty;
                animator.SetBool ("active", true);
                icon.SetBool ("active", false);
                MyCoroutineReference = StartCoroutine(Type());
                ok = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
       
		if (col.gameObject == NewPlayer.Instance.gameObject) {
		    animator.SetBool ("active", false);
            icon.SetBool ("active", false);
            StopCoroutine(MyCoroutineReference);
            textDisplay.ClearMesh();
            ok = true;
        }
	}


    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


}
