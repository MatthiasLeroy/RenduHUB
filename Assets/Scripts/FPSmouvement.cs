using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5f; // vitesse de mouvement
    public float mouseSensitivity = 2f; // sensibilité de la souris
    public float jump = 2f; // hauteur du saut
    private float yRotation = 0f; // rotation verticale (pour la caméra)
    private float xRotation = 0f; // rotation horizontale (pour la caméra)
    private Vector3 velocity; // vitesse de mouvement du joueur
    private bool isGrounded;  // detecte si le joueur est au sol
    public CharacterController charactercontroller;

    void Start()
    {
        //charactercontroller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // verrouille le curseur

    }
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // prend les input de la souris
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // prend les input de la souris

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limite de la rotation pour eviter de se retourner
        yRotation += mouseX;

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotation de la caméra
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f); // rotation du joueur


        isGrounded = charactercontroller.isGrounded; // detecte si le joueur est sur le sol

        float moveX = Input.GetAxis("Horizontal"); // mouvement avec Q et D 
        float moveZ = Input.GetAxis("Vertical");   // mouvement avec Z et S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

       
        charactercontroller.Move(move * moveSpeed * Time.deltaTime); // formule qui appplique le mouvement

      

        if (isGrounded && velocity.y < 0) // lorsque le joueur est sur le sol, on applique le vecteur de velocité pour le saut
        {
            velocity.y = -2f; // poids de la gravité
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // si la touche espace est enfoncée et que le personnage touche le sol 
        {
            velocity.y = Mathf.Sqrt(jump * -2f * Physics.gravity.y); // formule du saut
        }

    
        
        velocity.y += Physics.gravity.y * Time.deltaTime; // permet d'appliquer la gravité
        charactercontroller.Move(velocity * Time.deltaTime);
    }
}  
