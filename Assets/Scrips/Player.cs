//using UnityEngine;

//[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
//public class Player : MonoBehaviour
//{
//    private AudioSource jump;
//    private CharacterController character;
//    private Vector3 direction;
//    public Animator animator;
//    bool isAlive = true;

//    public float jumpForce = 8f;
//    public float gravity = 9.81f * 2f;

//    private void Awake()
//    {
//        character = GetComponent<CharacterController>();
//    }

//    private void OnEnable()
//    {
//        direction = Vector3.zero;
//    }

//    private void Start()
//    {
//        jump = GetComponent<AudioSource>();
//        if (jump == null)
//        {
//            Debug.LogError("AudioSource component not found on the GameObject.");
//        }
//    }

//    private void Update()
//    {
//        if (isAlive)
//        {
//        // Apply gravity
//        direction += gravity * Time.deltaTime * Vector3.down;

//        if (character.isGrounded)
//        {
//            direction = Vector3.down;

//            if (Input.GetButton("Jump"))
//            {
//                direction = Vector3.up * jumpForce;
//                if (jump != null)
//                {
//                    jump.Play(); // Play the jump sound when the player jumps
//                }
//                else
//                {
//                    Debug.LogError("Jump AudioSource is not assigned.");
//                }
//            }
//        }

//        // Move the character
//        character.Move(direction * Time.deltaTime);
//        }

//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Obstacle"))
//        {
//            isAlive = false;
//            animator.SetBool("IsDead", true);
//            GameManager.Instance.GameOver();
//        }
//    }
//    public void newgame()
//    {
//        isAlive = true;
//        animator.SetBool("IsDead", false);
//        animator.SetFloat("playerSpeed",1);


//    }
//    public void changeSpeed(float speed)
//    {
//                animator.SetFloat("playerSpeed",1);
//    }
//}

//using UnityEngine;

//[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
//public class Player : MonoBehaviour
//{
//    private AudioSource audioSource;
//    private CharacterController character;
//    private Vector3 direction;
//    public Animator animator;
//    bool isAlive = true;

//    public float jumpForce = 8f;
//    public float gravity = 9.81f * 2f;

//    public AudioClip jump;
//    public AudioClip deadSound;

//    private void Awake()
//    {
//        character = GetComponent<CharacterController>();
//        audioSource = GetComponent<AudioSource>();
//    }

//    private void Start()
//    {
//        CheckAudioSource();
//        CheckAudioClip(jump, "Jump");
//        CheckAudioClip(deadSound, "Death");
//    }

//    private void Update()
//    {
//        if (!isAlive) return;

//        ApplyGravity();
//        HandleJump();
//        MoveCharacter();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Obstacle"))
//        {
//            Die();
//        }
//        else if (other.CompareTag("Collectible"))
//        {
//            CollectItem(other.gameObject);
//        }
//    }

//    public void newgame()
//    {
//        isAlive = true;
//        animator.SetBool("IsDead", false);
//        animator.SetFloat("playerSpeed", 1);
//    }

//    public void changeSpeed(float speed)
//    {
//        animator.SetFloat("playerSpeed", speed / 5);
//    }

//    private void CheckAudioSource()
//    {
//        if (audioSource == null)
//        {
//            Debug.LogError("AudioSource component not found on the GameObject.");
//        }
//    }

//    private void CheckAudioClip(AudioClip clip, string clipName)
//    {
//        if (clip == null)
//        {
//            Debug.LogError($"{clipName} AudioClip is not assigned.");
//        }
//    }

//    private void ApplyGravity()
//    {
//        direction += gravity * Time.deltaTime * Vector3.down;
//    }

//    private void HandleJump()
//    {
//        if (character.isGrounded)
//        {
//            direction = Vector3.down;

//            if (Input.GetButton("Jump"))
//            {
//                direction = Vector3.up * jumpForce;
//                PlaySound(jump, "Jump");
//                Debug.Log("JUMP JUMP JUMP JUMP");
//            }
//        }
//    }

//    private void MoveCharacter()
//    {
//        character.Move(direction * Time.deltaTime);
//    }

//    private void Die()
//    {
//        isAlive = false;
//        animator.SetBool("IsDead", true);
//        GameManager.Instance.GameOver();
//        PlaySound(deadSound, "Death");
//    }

//    private void PlaySound(AudioClip clip, string clipName)
//    {
//        if (audioSource == null)
//        {
//            Debug.LogError("AudioSource component is missing.");
//            return;
//        }

//        if (clip == null)
//        {
//            Debug.LogError($"{clipName} AudioClip is not assigned.");
//            return;
//        }

//        Debug.Log($"Playing {clipName} sound");
//        audioSource.PlayOneShot(clip);
//    }

//    private void CollectItem(GameObject collectible)
//    {
//        GameManager.Instance.AddGoldScore(1); // Assuming each collectible gives 1 gold score
//        Destroy(collectible);
//    }
//}
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    private CharacterController character;
    private Vector3 direction;
    public Animator animator;
    bool isAlive = true;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    public AudioClip jump;
    public AudioClip deadSound;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CheckAudioSource();
        CheckAudioClip(jump, "Jump");
        CheckAudioClip(deadSound, "Death");
    }

    private void Update()
    {
        if (!isAlive) return;

        ApplyGravity();
        HandleJump();
        MoveCharacter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (other.CompareTag("Collectible"))
        {
            CollectItem(other.gameObject);
        }
    }

    public void newgame()
    {
        isAlive = true;
        animator.SetBool("IsDead", false);
        animator.SetFloat("playerSpeed", 1);
    }

    public void changeSpeed(float speed)
    {
        animator.SetFloat("playerSpeed", speed / 5);
    }

    private void CheckAudioSource()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
        }
    }

    private void CheckAudioClip(AudioClip clip, string clipName)
    {
        if (clip == null)
        {
            Debug.LogError($"{clipName} AudioClip is not assigned.");
        }
    }

    private void ApplyGravity()
    {
        direction += gravity * Time.deltaTime * Vector3.down;
    }

    private void HandleJump()
    {
        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
                PlaySound(jump, "Jump");
                Debug.Log("JUMP JUMP JUMP JUMP");
            }
        }
    }

    private void MoveCharacter()
    {
        character.Move(direction * Time.deltaTime);
    }

    private void Die()
    {
        isAlive = false;
        animator.SetBool("IsDead", true);
        GameManager.Instance.GameOver();
        PlaySound(deadSound, "Death");
    }

    private void PlaySound(AudioClip clip, string clipName)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
            return;
        }

        if (clip == null)
        {
            Debug.LogError($"{clipName} AudioClip is not assigned.");
            return;
        }

        Debug.Log($"Playing {clipName} sound");
        audioSource.PlayOneShot(clip);
    }

    private void CollectItem(GameObject collectible)
    {
        GameManager.Instance.AddGoldScore(1); // Assuming each collectible gives 1 gold score
        Destroy(collectible);
    }
}


