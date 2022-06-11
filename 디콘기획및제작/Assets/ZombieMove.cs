using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{

	private Transform playerTr;

	private NavMeshAgent nvAgent;
	public Animator ani;
	private bool islive = true;
	void Start()
	{
		nvAgent = GetComponent<NavMeshAgent>();
		playerTr = GameObject.Find("Player").GetComponent<Transform>();

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
			Debug.Log("총알에 맞았음");

			nvAgent.isStopped = true;
			islive = false;
			Invoke("dest",3.0f);
			ani.SetBool("attack", false);
			ani.SetBool("die", true);
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
		if (other.tag == "Player") {
			playerTr.gameObject.GetComponent<PlayerMove>().MinusHP();
			Debug.Log("플레이어 공격함");
		}

	}
    private void dest()
    {
		Destroy(this.gameObject);
    }
}
