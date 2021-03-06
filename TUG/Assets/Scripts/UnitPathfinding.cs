using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseUnit))]
[RequireComponent(typeof(NavMeshAgent))]
public class UnitPathfinding : MonoBehaviour 
{
	private NavMeshAgent myNavmeshAgent;
	public Transform destanation;
	
	private BaseUnit myBase;
	public BaseUnit agrodUnit;
	public float walkRange;
	private readonly float unitUpateRate =5f;

	protected delegate void PathfindingEvent();
	protected PathfindingEvent onInRange;

	void Start()
	{
		myBase = GetComponent<BaseUnit>();
		myNavmeshAgent = GetComponent<NavMeshAgent>();
		StartCoroutine(brainLoop());
    }
	
	IEnumerator brainLoop () 
	{
		while(gameObject.activeSelf == true)
		{
			move();
			yield return new WaitForSeconds(unitUpateRate);
		}
	}
	
	void move()
	{
		if (agrodUnit != null && agrodUnit.gameObject.activeSelf)// check if the agroed unit exists and is alive
		{
			if (Vector3.Distance (transform.position, agrodUnit.transform.position) > walkRange)// check if we are close enough based of wallk range
			{
				myNavmeshAgent.SetDestination (agrodUnit.transform.position); // move to the target
			}
			else 
			{
				myNavmeshAgent.Stop();
				if(onInRange != null)
				{
					onInRange();
				}
			}
		}
		else // if were not attacking anything 
		{
			myNavmeshAgent.SetDestination(destanation.position);// move to the destination
		}
	}
	
	void changeTarget(Collider other)
	{
		BaseUnit newAgro = other.gameObject.GetComponent <BaseUnit>();//Store the baseUnit on other
		if (newAgro != null)// check if its null
		{
			if (agrodUnit == null)// check if im attacking something
			{
				if (!myBase.checkTeam (newAgro)) // check if its on the enemy team
				{
					agrodUnit = newAgro; // store agroed unit into the new agro
				}
			}
		}

	}

	void OnTriggerEnter(Collider other) //this needs to be flocked as rigidbodys on all units will tank store when the wave is spawned then check them as a whole
	{
		changeTarget(other);
	}
	
	void OnTriggerStay(Collider other)//same as above
	{
		changeTarget(other);
	}
}
