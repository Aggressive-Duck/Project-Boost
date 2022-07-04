using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip engineThrust;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    // PARAMETERS for tuning typically set in the editor
    // CACHE - e.g. references for readabillity or speed
    // STATE - private instances (member) variables

    /*we will need to initialized it*/
    Rigidbody rb;
    AudioSource audioSource;
    /*rigidbody as a variables*/


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        /*keycode.space is better than string statement because you can know if its correct or not*/
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
        /*but there will be another bug, which is if you press left and right at the same time, left will have priorities but it is fine here*/
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        /*add values on both rotation and position*/
        /*we need relative force rather than applying axis value this time*/
        /*cuz the rocket is being ratated oftenly*/
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThrust);
        }

        if (!mainBoosterParticles.isPlaying)//a property dont need (), but methods need it, ! = not doing something
        {
            //mainBoosterParticles.Stop();
            mainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);/*Vector3.forward is the short hand for 0, 0, 1*/
        if (!rightBoosterParticles.isPlaying)//a property dont need (), but methods need it, ! = not doing something
        {
            //rightBoosterParticles.Stop();
            rightBoosterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftBoosterParticles.isPlaying)//a property dont need (), but methods need it, ! = not doing something
        {
            //leftBoosterParticles.Stop();
            leftBoosterParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    /*if this is set by private, it means only the class movement monobehaviour can modify and use this method*/
    /*the parenthese means the value this method will demand when it is called*/
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing buit in rigidbody rotation so we can rotate without conflict
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over 
    }
}