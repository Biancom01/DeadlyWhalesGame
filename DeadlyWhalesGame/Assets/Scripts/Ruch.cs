using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Ruch : MonoBehaviour
{
    public float speed = 6.0f;          // Pr�dko�� poruszania si� postaci
    public float gravity = -9.81f;      // Grawitacja, aby posta� nie "fruwa�a"
    public float jumpHeight = 1.0f;     // Wysoko�� skoku
    public Camera playerCamera;         // Kamera gracza
    public LayerMask groundLayer;       // Warstwa pod�o�a, na kt�rej posta� si� porusza
    public float rotationSpeed = 5f;    // Pr�dko�� obrotu kamery i postaci

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Sprawdzenie, czy posta� jest na ziemi
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zapewnia, �e posta� trzyma si� pod�o�a
        }

        // Poruszanie postaci niezale�nie od jej rotacji
        MovePlayer();

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Obracanie postaci w kierunku kursora
        RotateTowardsMouse();
    }

    void MovePlayer()
    {
        // Odczytanie wej�cia gracza (WSAD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Poruszamy postaci� w p�aszczy�nie XZ kamery (nie w kierunku, w kt�rym patrzy posta�)
        Vector3 move = playerCamera.transform.right * moveX + playerCamera.transform.forward * moveZ;
        move.y = 0; // Zerujemy komponent Y, aby poruszanie by�o tylko w p�aszczy�nie XZ

        // Przesuni�cie postaci
        controller.Move(move * speed * Time.deltaTime);
    }

    void RotateTowardsMouse()
    {
        // Tworzymy promie� od kamery do pozycji kursora
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        // Przechowuje informacj� o trafionym punkcie
        RaycastHit hit;

        // Sprawdzamy, czy promie� uderzy� w pod�o�e
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // Pobieramy pozycj�, na kt�r� wskazuje kursor
            Vector3 targetPosition = hit.point;

            // Obliczamy kierunek obrotu, ignoruj�c wysoko�� (y)
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; // Ignorujemy komponent Y, aby posta� obraca�a si� tylko w p�aszczy�nie XZ

            // Sprawdzamy, czy kierunek nie jest zerowy
            if (direction != Vector3.zero)
            {
                // Obr�t gracza w kierunku kursora z kontrolowan� pr�dko�ci�
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // P�ynny obr�t przy u�yciu Slerp, aby kontrolowa� pr�dko��
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
