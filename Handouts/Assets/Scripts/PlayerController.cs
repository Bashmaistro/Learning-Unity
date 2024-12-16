using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionEffect;
    public ParticleSystem dirtParticle;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    

    public bool isOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop(); 
            audioSource.PlayOneShot(jumpSound);
            isOnGround = false;
            
        } }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound);
            explosionEffect.Play();
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int" , 1);
        } }
}
