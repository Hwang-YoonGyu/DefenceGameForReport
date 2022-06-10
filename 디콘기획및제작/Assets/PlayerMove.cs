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
    public bool moveSwitch;
    private bool shootCool;
    public int bulletCount;
    public int hp;
    public float time;
    private bool islive = false;

    public TextMeshProUGUI megagine;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI DieText;
    public TextMeshProUGUI ScoreText;
    public Button gameStartBtn;

    public SpawnZombie s1;
    public SpawnZombie s2;
    public SpawnZombie s3;
    public SpawnZombie s4;
    // Start is called before the first frame update
    void Start()
    {
        gameStartBtn.onClick.AddListener(gameStart);
    }
    public void gameStart() {
        hp = 5;
        islive = true;
        shootCool = true;
        time = 0.0f;
        moveSwitch = true;
        s1.GameStart();
        s2.GameStart();
        s3.GameStart();
        s4.GameStart();
        gameStartBtn.gameObject.SetActive(false);
    }
    public void MinusHP() {
        hp--;
        string temp = "";
        for (int i = 0; i < hp; i++) {
            temp += "♥";
        }
        hpText.text = temp;
        if (hp == 0) {
            PrintDie();
        }
    }

    void PrintDie() {
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        float t = 0;
        islive = false;
        moveSwitch = false;
        while (true) {
            t += Time.deltaTime;
            DieText.color = new Color(1,0,0,t*0.5f);

            if (t > 2.0f) {
                break;
            }
            yield return null;
        }
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
            if (shootCool && (bulletCount > 0))
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
                    megagine.text = bulletCount +" / 30";
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
        if (islive)
        {
            time += Time.deltaTime;
            ScoreText.text = "Score : " + ((int)time).ToString();
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
               bulletCount = 30;
               megagine.text = bulletCount + " / 30";
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
