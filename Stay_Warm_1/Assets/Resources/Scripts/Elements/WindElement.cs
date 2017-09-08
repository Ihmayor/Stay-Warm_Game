using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WindElement : CoolingElement
{

    [Range(0.026f, 0.036f)]
    private float FadeFactor;
    private float DriftSpeed;
    public bool isRightDirection;

    public override void Start()
    {
        base.Start();
        FadeFactor = 0.016f;
        DriftSpeed = 0.01f;
        isRightDirection = false;
        CoolingSound = Resources.Load<AudioClip>("Audio/wind_gust");//Default
    }

    public override void Update()
    {
        base.Update();
        float exponentIncrease = 1.5f; //How fast it fades. This is the ideal value found.
        Color oldColor = this.GetComponent<SpriteRenderer>().color;
        if (oldColor.a > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - Mathf.Pow(FadeFactor, exponentIncrease));
            Vector3 PositionChange = new Vector3(DriftSpeed, 0, 0);
            if (isRightDirection)
                PositionChange *= -1;
            this.gameObject.transform.position -= PositionChange;
        }
        else
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Invoke("Deactivate", Resources.Load<AudioClip>("Audio/wind_gust").length);
        }
    }

    /// <summary>
    /// Instead of Destroying/Creating Wind instances which slows down the game's runtime, I'll simple deactivate reposition.
    /// </summary>
    /// <returns></returns>
    private void Deactivate()
    {
        Color oldColor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        this.gameObject.SetActive(false);
        CancelInvoke("Deactivate");
    }

    /// <summary>
    /// Reactivate cooling element after deactivation
    /// </summary>
    public void Reactivate()
    {
        CancelInvoke("Deactivate");
        Color oldColor = this.GetComponent<SpriteRenderer>().color;
        if (oldColor.a == 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        this.gameObject.SetActive(true);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.CompareTag("Player") &&
         !collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock)
        {
            Vector3 WindPositionChange = new Vector3(DriftSpeed * 4f, 0, 0);
            if (isRightDirection)
                WindPositionChange *= -1;
            collision.gameObject.transform.position -= WindPositionChange;

            //Jitter player if they are fighting against it.
            Vector3 JitterPositionChange = new Vector3(0, UnityEngine.Random.Range(-DriftSpeed, DriftSpeed), 0);
            var horizontal = Input.GetAxis("Horizontal");
            if ((horizontal > 0 && !isRightDirection) || (horizontal < 0 && isRightDirection))
                collision.gameObject.transform.position += JitterPositionChange;
        }
    }

}
