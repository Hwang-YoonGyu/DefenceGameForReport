using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float movingSpeed = 10.0f;
    public float camTurnSpeed = 4.0f;
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    public GameObject Character;
    public GameObject Camera;
    public Animator ani;
    public ParticleSystem part;
    public GameObject bullet;
    private bool moveSwitch = true;
    private bool shootCool = true;
    private int bulletCount = 30;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.transform.position = new Vector3(Character.transform.position.x, 25, Character.transform.position.z-15);
        if (moveSwitch)
        {
            Move();
        }
        if (!Input.anyKey) {
            ani.SetBool("isMove", false);
        }
        if (Input.GetMouseButtonDown(0)) {
            if (shootCool)
            {
                ani.SetBool("isMove", false);
                moveSwitch = false;
                ani.SetBool("shoot", true);
                RaycastHit hit;
                if (Physics.Raycast(Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),
                            out hit, Mathf.Infinity))
                {
                    Character.transform.LookAt(hit.point + new Vector3(0, 2, 0) - new Vector3(0,hit.point.y,0));
                    bulletCount--;
                    text.text = bulletCount +" / 30";
                    StartCoroutine(shotGun());
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            moveSwitch = true;
            ani.SetBool("shoot", false);

        }
        if (Input.GetKey(KeyCode.R)) {
            StartCoroutine(reloading());

        }

    }

    IEnumerator shotGun() {
        shootCool = false;
        float t = 0.0f;
        part.gameObject.SetActive(true);
        Instantiate(bullet, part.gameObject.transform.position, part.gameObject.transform.rotation);
        while (true) {
            t += Time.deltaTime;
            if (t >= 0.15f) {
                shootCool = true;
                part.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    IEnumerator reloading()
    {
        float t = 0.0f;
        moveSwitch = false;
        while (true)
        {
            t += Time.deltaTime;
            if (t >= 1.0f)
            {
                if (bulletCount == 0)
                {
                    bulletCount = 30;
                }
                else {
                    bulletCount = 31;

                }
                text.text = bulletCount + " / 30";
                moveSwitch = true;
                break;
            }
            yield return null;
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 moveHorizontal = Vector3.right * h;
            Vector3 moveVertical = Vector3.forward * v;
            Vector3 velocity = (moveHorizontal + moveVertical).normalized;

            transform.LookAt(transform.position + velocity);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                velocity *= 2.0f;
            }
            transform.Translate(velocity * movingSpeed * Time.deltaTime, Space.World);
            //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);
            ani.SetBool("isMove", true);

        }

    }

}
