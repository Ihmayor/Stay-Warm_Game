using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndFollow : MonoBehaviour
{

    //Modified Code from: https://www.youtube.com/watch?v=iFAyb6x-a3Q 2D Flocking with Unity Part 1/2 by Holistic3d

    private Vector2 velocity;
    private float neighbourDistance;
    private bool turnBack;
    private Rigidbody2D rb2d;
    private Vector2 location;
    private Vector2 currentForce;

    // Use this for initialization
    void Awake()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        velocity = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        if (gameObject.GetComponent<Rigidbody2D>() == null)
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        else
            rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        currentForce = new Vector2(0.01f, 0.002f);
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("Floating");
        location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        StartCoroutine("FloatObject");
    }

    IEnumerator FloatObject()
    {
        while (transform.position == FollowManager.Instance.Target.transform.position)
            yield return null;

        yield return new WaitForSeconds(Random.Range(1, 1.5f));
        float floatingFactor = Random.Range(0.001f, 0.003f);
        for (float i = 0; i <= 2f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.1f);
            Vector2 varianceGoal = Seek((new Vector2(0, FollowManager.Instance.Target.transform.position.y + floatingFactor) - location)).normalized;
            ApplyForce(varianceGoal);
        }

        for (float i = 0; i <= 2f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.1f);
            Vector2 varianceGoal = Seek((new Vector2(0, FollowManager.Instance.Target.transform.position.y - floatingFactor) - location)).normalized;
            ApplyForce(varianceGoal);
        }
        yield return new WaitForSeconds(Random.Range(0.3f, 2f));

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
        return new Vector2(0.0000000001f, 0.00000001f);
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
        return new Vector2(0.0000000001f, 0.00000001f);
    }

    Vector2 Seek(Vector2 target)
    {
        return (target - location);
    }

    void Flock()
    {
        location = this.transform.position;
        velocity = rb2d.velocity;
        Vector2 gl = Seek(FollowManager.Instance.Target.transform.position);
        Vector2 align = Align();
        Vector2 cohesion = Cohesion();
        currentForce = gl + align + cohesion + new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.3f, 0.3f));
        currentForce = currentForce.normalized;
        ApplyForce(currentForce);

    }


    void Update()
    {
        if (transform.position != FollowManager.Instance.Target.transform.position)
            Flock();
        else
            Debug.Log("screeech");
    }

}
