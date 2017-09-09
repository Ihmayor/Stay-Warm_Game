using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    public float FadeInSpeed = 0.1f;

    private IEnumerator DoFadeIn()
    {
        while (this.gameObject.GetComponent<SpriteRenderer>().color.a < 255)
        {
            yield return new WaitForSeconds(FadeInSpeed);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, this.gameObject.GetComponent<SpriteRenderer>().color.a + FadeInSpeed);
        }
        yield return null;
    }

    public void MoveObject(Vector3 NewPosition)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = new Vector3(NewPosition.x, this.gameObject.transform.position.y, 0);
        this.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn());
        this.gameObject.GetComponent<AudioSource>().Play();
    }


}
