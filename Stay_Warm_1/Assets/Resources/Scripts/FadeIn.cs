using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float FadeSpeed = 0.1f;

    private IEnumerator DoFadeIn(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (this.gameObject.GetComponent<SpriteRenderer>().color.a < 255)
        {
            yield return new WaitForSeconds(FadeSpeed);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, this.gameObject.GetComponent<SpriteRenderer>().color.a + FadeSpeed);
        }
        yield return null;
    }

    private IEnumerator DoFadeInImage(float delaySeconds,Image img)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (img.color.a < 1)
        {
            yield return new WaitForSeconds(FadeSpeed*10000);
            img.color = new Color(1, 1, 1, img.color.a + FadeSpeed);
        }
        yield return null;
    }

    private IEnumerator DoFadeOut(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (this.gameObject.GetComponent<SpriteRenderer>().color.a > 0)
        {
            yield return new WaitForSeconds(FadeSpeed);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, this.gameObject.GetComponent<SpriteRenderer>().color.a - FadeSpeed);
        }
        yield return null;
    }

    public void ReactivateMomentarily(float DelaySeconds)
    {
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        StartCoroutine(DoFadeInImage(0, this.gameObject.GetComponent<Image>()));
     //   StartCoroutine(DoFadeOut(DelaySeconds));
 //       this.gameObject.SetActive(false);
    }

    public void Reactivate()
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        this.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn(0));
    }

    public void MoveObject(Vector3 NewPosition)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = new Vector3(NewPosition.x, this.gameObject.transform.position.y, 0);
        this.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn(0));
        this.gameObject.GetComponent<AudioSource>().Play();
    }


}
