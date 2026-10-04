using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class Cell : MonoBehaviour
{
    public void GetNewTarget()
    {
        //Debug.Log("getting new target");
        if(Target != null)
        {
            Target = null;
        }

        
        while (Target == null)
        {
            int NewTarget = random.Next(0, GameManager.Instance.Cells.Count);

            //Debug.Log("check Cell: " + NewTarget);
            if (GameManager.Instance.Cells[NewTarget] != null)
            {
                //Debug.Log($"New target {GameManager.Instance.Cells[NewTarget]}");
                if (GameManager.Instance.Cells[NewTarget] != this)
                {
                    Target = GameManager.Instance.Cells[NewTarget];
                    //Debug.Log("target Confermed");
                }
            }
            
        }
        StartCoroutine(Move());
    }
    public Cell Target;
    public System.Random random = new System.Random();
    public Rigidbody rb;
    public float BlastWait = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetNewTarget();




    }
    public float pushBackForce = 30;
    public float speed = 3;
    private bool Hit;
    public Vector3 move;
    public bool start = true;
    IEnumerator Move()
    {
        if (!start)
        {
            yield return new WaitForSeconds(BlastWait);
        }
        else
        {
            start = false;
        }
            rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Hit = false;
        //Debug.Log("started moving towards " + Target);
        while (!Hit)
        {

            move = Vector3.MoveTowards(rb.position, Target.transform.position, speed * Time.fixedDeltaTime);

            rb.MovePosition(move);
            //Debug.Log("moving to " + move);



            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Hit = true;
        StopAllCoroutines();
        Debug.Log("hit Cell");
        
        Vector3 direction = (other.transform.position-transform.position).normalized;
        Vector3 PushBack = direction * pushBackForce;

        other.gameObject.GetComponent<Rigidbody>().AddForce(PushBack);

        

        GetNewTarget();
    }
}
