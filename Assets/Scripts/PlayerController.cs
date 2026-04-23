using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float movementSpeed = 5f;


    private Rigidbody rb;


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


        
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        GameObject visual = transform.GetChild(0).gameObject;
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
            Shoot();
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
    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                GameObject.Find("GameManager").GetComponent<GameManager>().enemiesInScene--;
                GameObject.Find("GameManager").GetComponent<GameManager>().CheckEnemiesCount();
            }
        }
    }
}
