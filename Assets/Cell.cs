using JetBrains.Annotations;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public void GetNewTarget()
    {
        Debug.Log("getting new target");
        if(Target != null)
        {
            Target = null;
        }

        
        while (Target == null)
        {
            int NewTarget = random.Next(0, GameManager.Instance.Cells.Count);

            Debug.Log("check Cell: " + NewTarget);
            if (GameManager.Instance.Cells[NewTarget] != null)
            {
                Debug.Log($"New target {GameManager.Instance.Cells[NewTarget]}");
                if (GameManager.Instance.Cells[NewTarget] != this)
                {
                    Target = GameManager.Instance.Cells[NewTarget];
                    Debug.Log("target Confermed");
                }
            }
            
        }
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
    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            Vector3 Move = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
            rb.MovePosition(Move);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit Cell");
        Vector3 SpawnPoint = transform.position;
        int spawnNewCell = random.Next(0, 2);

        Vector3 pushBack = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);

        rb.AddForce(-pushBack * pushBackForce);
        if (spawnNewCell == 1)
        {
            Debug.Log("spawning new cell");
        }


        GetNewTarget();
    }
}
