using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    private NavMeshAgent agent;

    [SerializeField]
    private float speed = 3.5f; // Pr�dko�� przeciwnika (mo�na dostosowa� w edytorze)

    void Start()
    {
        // Znajdujemy NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Ustawiamy pr�dko�� agenta
        agent.speed = speed;

        // Je�li nie przypisali�my gracza w edytorze, szukamy go automatycznie po tagu "Player"
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Ustawiamy cel agenta na pozycj� gracza
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
