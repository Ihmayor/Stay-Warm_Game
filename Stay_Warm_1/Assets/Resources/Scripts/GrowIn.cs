using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveObject(int puzzleNum, Vector3 newPosition)
    {
        this.gameObject.transform.localScale = Vector2.zero;
        this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Stage" + puzzleNum;
        this.gameObject.transform.position = newPosition - new Vector3(0,0.3f);
        this.gameObject.GetComponent<Animator>().SetTrigger("Shrink");
        StartCoroutine(TriggerGrowAnimation(newPosition));
    }


     IEnumerator TriggerGrowAnimation(Vector3 newPosition)
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Animator>().SetTrigger("Grow");
        yield return new WaitForSeconds(0.24f);
        this.gameObject.GetComponent<Animator>().ResetTrigger("Grow");
        this.gameObject.transform.position = newPosition;
        this.gameObject.GetComponent<Animator>().SetTrigger("Normal");
        this.gameObject.GetComponent<Animator>().ResetTrigger("Normal");
        yield return null;
    }

}
