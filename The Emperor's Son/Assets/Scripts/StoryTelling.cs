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

    public bool ok = true;

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject == NewPlayer.Instance.gameObject && ok){
            animator.SetBool ("active", true);
            
            StartCoroutine(Type());
            ok = false;
        }
    }

    void OnTriggerExit2D(Collider2D col){
       
		if (col.gameObject == NewPlayer.Instance.gameObject) {
		    animator.SetBool ("active", false);
        }
	}


    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
    }


}
