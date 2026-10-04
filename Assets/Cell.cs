using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetNewTarget();




    }
    public float pushBackForce = 30;
    public int speed = 3;
    public void addToList()
    {
        GameManager.Instance.Cells.Add(this);
        
    }
    IEnumerator Move()
    {
        Debug.Log("started moving towards " + Target);
        while (true)
        {

            Vector3 move = Vector3.MoveTowards(rb.position, Target.transform.position, speed * Time.fixedDeltaTime);

            rb.MovePosition(move);
            //Debug.Log("moving");



            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        StopAllCoroutines();
        Debug.Log("hit Cell");
        Vector3 SpawnPoint = transform.position;
        int spawnNewCell = random.Next(0, 2);

        //Vector3 pushBack =
        //Debug.Log(pushBack);
        //rb.AddForce(pushBack);
        if (spawnNewCell == 1)
        {
            Debug.Log("spawning new cell");

        }


        GetNewTarget();
    }
}
