using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float FadeSpeed = 0.1f;

    /// <summary>
    /// Fade Element Effect
    /// </summary>
    /// <param name="delaySeconds">Seconds to Before Starting Fade</param>
    /// <param name="increaseFadeFactor">Factor to Multiply FadeSpeed </param>
    /// <returns></returns>
    private IEnumerator DoFadeIn(float delaySeconds, float increaseFadeFactor)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
        {
            yield return new WaitForSeconds(FadeSpeed);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.a + (FadeSpeed*increaseFadeFactor));
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

    /// <summary>
    /// Fade Out Effect of Element
    /// </summary>
    /// <param name="delaySeconds">Delay of Seconds before Effect Occurs</param>
    /// <param name="increaseFadeFactor">Factor to Multiply FadeSpeed </param>
    /// <returns></returns>
    private IEnumerator DoFadeOut(float delaySeconds,float increaseFadeSpeed)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (this.gameObject.GetComponent<SpriteRenderer>().color.a > 0)
        {
            yield return new WaitForSeconds(FadeSpeed);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.GetComponent<SpriteRenderer>().color.a - (FadeSpeed*increaseFadeSpeed));
            
        }
        yield return null;
    }

    public void ReactivateMomentarily(float DelaySeconds)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.48f, 0.435f, 0.435f, 0f);
        StartCoroutine(DoFadeIn(0,100000));
        StartCoroutine(DoFadeOut(DelaySeconds, 10000*8));
    }

    public void Reactivate()
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        this.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn(0,1));
    }

    public void MoveObject(Vector3 NewPosition)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = new Vector3(NewPosition.x, this.gameObject.transform.position.y, 0);
        this.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn(0,1));
        this.gameObject.GetComponent<AudioSource>().Play();
    }


}
