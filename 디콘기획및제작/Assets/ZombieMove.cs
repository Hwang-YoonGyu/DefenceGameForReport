using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{
	// Start is called before the first frame update
	// ������ ��� Object
	private Transform playerTr;
	// ������ Object
	private Transform tr;
	// ���� Object�� ����� NavMeshAgent ������Ʈ
	private NavMeshAgent nvAgent;
	public Animator ani;
	private bool islive = true;
	// Use this for initialization
	void Start()
	{
		nvAgent = GetComponent<NavMeshAgent>();
		tr = GetComponent<Transform>();
		playerTr = GameObject.Find("Player").GetComponent<Transform>();

		//���� Object�� ����� NavMeshAgent ������Ʈ�� ������� ����         
		nvAgent.destination = playerTr.position;
	}
	// Update is called once per frame     
	void Update()
	{
		if (islive)
		{
			nvAgent.destination = playerTr.position;

			if (Vector3.Distance(playerTr.position, transform.position) <= 2.0f)
			{
				nvAgent.isStopped = true;
				ani.SetBool("attack", true);
			}
			else
			{
				nvAgent.isStopped = false;
				ani.SetBool("attack", false);

			}
		}
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "bullet") {
			Debug.Log("�Ѿ˿� �¾���");

			nvAgent.isStopped = true;
			islive = false;
			Invoke("dest",3.0f);
			ani.SetBool("die", true);
		}

    }
    private void dest()
    {
		Destroy(this.gameObject);
    }
}
