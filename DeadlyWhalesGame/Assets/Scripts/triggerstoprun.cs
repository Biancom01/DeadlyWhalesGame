using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerrun : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sprawdzenie czy naci�ni�to jeden z klawiszy W, A, S lub D
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Je�li klawisz jest wci�ni�ty, uruchamiamy animacj� "bieg"
            if (!IsRunningAnimation("bieg"))
            {
                anim.SetTrigger("bieg");
            }
        }
        else
        {
            // Je�li �aden klawisz nie jest wci�ni�ty, uruchamiamy animacj� "idle"
            if (!IsRunningAnimation("idle"))
            {
                anim.SetTrigger("idle");
            }
        }
    }

    // Funkcja sprawdzaj�ca, czy podana animacja jest aktualnie aktywna
    bool IsRunningAnimation(string animationName)
    {
        // Sprawdzanie aktualnego stanu Animatora na pierwszej warstwie (layer 0)
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Zwraca true, je�li podana animacja jest ju� aktywna
        return stateInfo.IsName(animationName);
    }
}
