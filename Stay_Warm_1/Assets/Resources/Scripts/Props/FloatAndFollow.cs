using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndFollow : MonoBehaviour
{

    private Vector2 velocity;
    private float neighbourDistance;
    private bool turnBack;
    private Rigidbody2D rb2d;
    private Vector2 location;
    private Vector2 currentForce;
    // Use this for initialization
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        rb2d = gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        currentForce = new Vector2(0.01f, 0.002f);
        //   gameObject.AddComponent<CircleCollider2D>();
        location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //        turnBack = false;

            StartCoroutine("FloatObject");
    }

    IEnumerator FloatObject()
    {
        yield return new WaitForSeconds(1.5f);
        for (float i = 0; i <= 2f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.1f);
            rb2d.AddForce(Seek((new Vector2(0, FollowManager.Instance.Target.transform.position.y + 0.0004f) - location)).normalized);
        }

        for (float i = 0; i <= 2f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.1f);
            rb2d.AddForce(Seek((new Vector2(0, FollowManager.Instance.Target.transform.position.y - 0.0004f) - location)).normalized);
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("FloatObject");
    }

    void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        if (force.magnitude > FollowManager.Instance.maxForce)
        {
            force = force.normalized;
            force *= FollowManager.Instance.maxForce;
        }
        rb2d.AddForce(force);
        if (rb2d.velocity.magnitude > FollowManager.Instance.maxVelocity)
        {
            rb2d.velocity = rb2d.velocity.normalized;
            rb2d.velocity *= FollowManager.Instance.maxVelocity;
        }
        Debug.DrawRay(this.transform.position, force, Color.white);
    }

    Vector2 Align()
    {
        float neighbourDist = FollowManager.Instance.Neighbour;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (FloatAndFollow obj in FindObjectsOfType(typeof(FloatAndFollow)))
        {
            GameObject other = obj.gameObject;
            if (other.gameObject != this.gameObject)
            {
                float d = Vector2.Distance(location, other.GetComponent<FloatAndFollow>().location);
                if (d < neighbourDist)
                {
                    sum += other.GetComponent<FloatAndFollow>().velocity;
                    count++;
                }

            }
        }

        if (count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }
        return Vector2.zero;
    }

    Vector2 Cohesion()
    {
        float neighbourDist = FollowManager.Instance.Neighbour;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (object obj in FindObjectsOfType(typeof(FloatAndFollow)))
        {
            FloatAndFollow other = (FloatAndFollow)(obj);
            if (other.gameObject != this.gameObject)
            {
                float d = Vector2.Distance(location, other.GetComponent<FloatAndFollow>().location);
                if (d < neighbourDist)
                {
                    sum += other.GetComponent<FloatAndFollow>().location;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= count;
            return Seek(sum);
        }
        return Vector2.zero;
    }

    Vector2 Seek(Vector2 target)
    {
        return (target - location);
    }

    void Flock()
    {
        location = this.transform.position;
        velocity = rb2d.velocity;
        currentForce = new Vector2(0.3f, 0.5f);
        Vector2 gl = Seek(FollowManager.Instance.Target.transform.position);
        Vector2 align = Align();
        Vector2 cohesion = Cohesion();
        currentForce = gl + align + cohesion;
        currentForce = currentForce.normalized;
        ApplyForce(currentForce);

    }

    // Update is called once per frame
    void Update()
    {
        Flock();
    }
}
