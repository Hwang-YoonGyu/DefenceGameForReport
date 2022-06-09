using UnityEngine;
using UnityEngine.UI;
//---------------------윈도우 개발 종료로 안씀-----------------------------------------//  
public class TPSforWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    [SerializeField]
    public float movingSpeed = 8.0f;

    [SerializeField]
    private GameObject character;

    float camTurnSpeed = 1.5f;

    private float xRotate = 0.0f;

    public Animator animator;

    AudioSource Walk;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        Walk = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime *movingSpeed);
            animator.SetBool("isMove", true);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movingSpeed);
            animator.SetBool("isMove", true);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movingSpeed);
            animator.SetBool("isMove", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movingSpeed);
            animator.SetBool("isMove", true);

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movingSpeed = 3.0f;
            animator.SetBool("isRun", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isMove", false);
        }

        Vision();


    }
    void Vision()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * camTurnSpeed;
        float yRotate = cameraArm.transform.eulerAngles.y + yRotateSize;
        float xRotateSize = -Input.GetAxis("Mouse Y") * camTurnSpeed;

        xRotate = Mathf.Clamp(xRotate + xRotateSize, -15, 80);
        cameraArm.transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

    }


}
