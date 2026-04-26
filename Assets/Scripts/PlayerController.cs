using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float movementSpeed = 5f;


    private Rigidbody rb;

    private GameObject visual;
    private Animator animator;

    [SerializeField] private GameObject shootPointGo;


    float xRotation = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        visual = transform.GetChild(0).gameObject;
        animator = visual.GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        visual.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        
        transform.Rotate(Vector3.up * mouseX);
        //visual.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        //visual.transform.localRotation = Quaternion.Euler(visual.transform.rotation.x,visual.transform.rotation.y,0);

        // if(transform.GetChild(0).transform.eulerAngles.x > 90)
        // {
        //     transform.GetChild(0).transform.rotation = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
        // }
        // else if(transform.GetChild(0).transform.eulerAngles.x < -90)
        // {
        //     transform.GetChild(0).transform.rotation = Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z);
        // }

        Move(new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")));



        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Shoot());
        }
    }



    private void Move(Vector3 direction)
    {
        Vector3 localDirection = transform.TransformDirection(direction);
        Vector3 velocity = localDirection.normalized * movementSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;


        //transform.Translate(direction * Time.deltaTime * movementSpeed);
    }
    private IEnumerator Shoot()
    {
        animator.SetBool("shoot",true);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out RaycastHit hit,Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawLine(Camera.main.transform.position, hit.point);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue);
            
            if (hit.collider.CompareTag("Enemy"))
            {
                
                Debug.Log("dhd");
                Destroy(hit.collider.gameObject);
                GameObject.Find("Manager").GetComponent<GameManager>().enemiesInScene--;
                GameObject.Find("Manager").GetComponent<GameManager>().CheckEnemiesCount();
            }
        }
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("shoot", false);

    }

    private void OnDrawGizmos()
    {   
        
        //Gizmos.DrawSphere(hit.transform.position, 1);
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward );
        //Gizmos.DrawRay(ray);
        
        //Gizmos.  //DrawLine(Camera.main.transform.position, ray);
    }

    private void Jump()
    {

    }
}
