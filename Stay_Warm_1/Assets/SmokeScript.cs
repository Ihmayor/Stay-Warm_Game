using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour {

    public void SetSmokeColor(Color c)
    {
        c.a = 0.1f;
        GetComponent<ParticleSystem>().startColor = c;
    }


    public void ActivateSmoke()
    {
        this.gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        firstIncrease = true;
        StartCoroutine(Grow(0, 0.06f));
    }


    public void ShrinkSmoke(float ShrinkSmokeFactor)
    {
        StartCoroutine(Shrink(0, ShrinkSmokeFactor));
    }

    public void GrowSmoke(float GrowSmokeFactor)
    {
        StartCoroutine(Grow(0, GrowSmokeFactor));
    }

    private float maxSize = 0.3f;
    private float minSize = 0.08f;
    private bool firstIncrease = false;


    private IEnumerator Grow(float delaySeconds, float SizeSpeed)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (this.gameObject.transform.localScale.x < maxSize)
        {
            yield return new WaitForSeconds(SizeSpeed);
            this.gameObject.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);
        }
        if (firstIncrease)
            firstIncrease = false;
        yield return null;
    }

    private IEnumerator Shrink(float delaySeconds, float SizeSpeed)
    {
        yield return new WaitForSeconds(delaySeconds);
        while (this.gameObject.transform.localScale.x > minSize)
        {
            yield return new WaitForSeconds(SizeSpeed);
            if (!firstIncrease)
                this.gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
        }
        yield return null;
    }


}
