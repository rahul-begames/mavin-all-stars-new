using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour {

    public float speedModifier = 0.05f;
    public float cameraSpeed = 0.05f;

    public Vector3 cameraRotation;
    public Vector3 cameraPosition;

    public float timeScoreMultiplier = 10f;
    public float crouchDuration = 1f;

    public float pursuerPositionOffset = 4f;

    public float initialHitSpeed = -10f;
    public float hitSpeedDecreaseSpeed = 80f;

    public LayerMask levelLayer;
    public AudioClip jumpSideClip;
    public AudioClip jumpClip;
    public AudioClip gameOverClip;
    public AudioClip crouchClip;

    public BoxCollider[] boxCollider;
    public CapsuleCollider[] capsuleColliders;

    public float mouseAndTouchMoveDistance = 80f;

    public bool enableKeyboardMovement = true;
    public bool enableMouseMovement = true;

    [SerializeField]
    protected float m_GroundCheckDistance = 0.1f;
    protected float m_OrigGroundCheckDistance;
    protected Vector3 m_GroundNormal;
    [SerializeField]
    protected float m_JumpPower = 12f;
    [SerializeField]
    protected float stepDistance = 1.5f;
    
    
    public float speed = 3f;

    public int skinPrice = 1000;

    protected Animator anim;

    protected float posX = 0.0f;
    protected float startTime;
    protected float jumpSpeed;
    protected bool m_Jump;
    protected Rigidbody m_Rigidbody;

    protected bool m_PreviouslyGrounded, m_IsGrounded;
    protected Collider m_Collider;
    protected Vector3 m_GroundContactNormal;

    protected bool dead;

    protected int coins;
    protected float time;
    protected bool crouching;
    protected float crouchDeltaHeight = 1f;
    protected Manager manager;

    protected float jumpMultiplier = 1f;
    protected float timeMultiplier = 1f;
    protected float coinsMultiplier = 1f;

    protected float jumpMultiplierTime;
    protected float timeMultiplierTime;
    protected float coinsMultiplierTime;

    protected AudioSource audioSource;
   
    protected Vector2 fp;  // first finger position
    protected Vector2 lp;  // last finger position
    protected bool swiped = true;

    protected float animForward = 0.5f;
   
    protected float hitSpeedLoc;
    [HideInInspector]
    public Transform thisTransform;

    protected virtual void Awake()
    {
        thisTransform = transform;
    }

    protected virtual void Start ()
    {       
        hitSpeedLoc = speed;
        anim = GetComponentInChildren<Animator>();
        anim.applyRootMotion = false;

        m_Collider = GetComponent<CapsuleCollider>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_OrigGroundCheckDistance = m_GroundCheckDistance;
        manager = GameObject.Find("Manager").GetComponent<Manager>();

        audioSource = GetComponent<AudioSource>(); 
    }
    
    

    protected void PullDown()
    {
        // Apply a strong downward force to bring the player down quickly
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, -m_JumpPower * 2f, m_Rigidbody.velocity.z);

        // Ensure ground check distance allows landing detection
        m_GroundCheckDistance = m_OrigGroundCheckDistance;
    }
    
    protected virtual void Crouch()
    {
        audioSource.PlayOneShot(crouchClip);
        anim.SetBool("Crouch", true);
        crouching = true;
        
        //Switch To Slide Camera
        if (manager != null)
            if (manager.DefaultCam_CM != null)
                if (manager.SlideCam_CM != null)
                    CinemachineExtensionClass.SwitchCineCamera(manager.DefaultCam_CM,manager.SlideCam_CM);

        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].size -= new Vector3(0, crouchDeltaHeight, 0);
            boxCollider[i].center -= new Vector3(0, crouchDeltaHeight / 2, 0);
        }
        for (int i = 0; i < capsuleColliders.Length; i++)
        {
            capsuleColliders[i].height -= crouchDeltaHeight;
            capsuleColliders[i].center -= new Vector3(0, crouchDeltaHeight / 2, 0);
        }

        Invoke("stopCrouching", crouchDuration);
    }

    protected virtual void stopCrouching()
    {
        anim.SetBool("Crouch", false);
        crouching = false;
        
        //Switch To default Camera
        if (manager != null)
            if (manager.DefaultCam_CM != null)
                if (manager.SlideCam_CM != null)
                    CinemachineExtensionClass.SwitchCineCamera(manager.SlideCam_CM, manager.DefaultCam_CM);

        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].size += new Vector3(0, crouchDeltaHeight, 0);
            boxCollider[i].center += new Vector3(0, crouchDeltaHeight / 2, 0);
        }
        for (int i = 0; i < capsuleColliders.Length; i++)
        {
            capsuleColliders[i].height += crouchDeltaHeight;
            capsuleColliders[i].center += new Vector3(0, crouchDeltaHeight / 2, 0);
        }
    }

    protected virtual void Jump()
    {
        RaycastHit hitInfo;
        if (!Physics.Raycast(thisTransform.position + Vector3.up, Vector3.up, out hitInfo, 2f, levelLayer))
        {
            m_Jump = true;
            if (crouching)
            {
                CancelInvoke("stopCrouching");
                stopCrouching();
            }
        }
    }

    protected virtual void MoveSide(bool right)
    {
        if (m_IsGrounded)
        {
            anim.SetBool("Side", right);
            anim.SetTrigger("JumpSide");

            audioSource.PlayOneShot(jumpSideClip);
        }
        if (crouching)
        {
            CancelInvoke("stopCrouching");
            stopCrouching();
        }
        
        RaycastHit hitInfo;

        if (!right)
        {
            if (!Physics.Raycast((thisTransform.position + Vector3.up * 0.1f) + (Vector3.forward / 8f), Vector3.left, out hitInfo, 2f, levelLayer)
             && !Physics.Raycast((thisTransform.position + Vector3.up * 0.1f) - (Vector3.forward / 8f), Vector3.left, out hitInfo, 2f, levelLayer))
                posX = posX - stepDistance;
        }
        else
        {
            if (!Physics.Raycast((thisTransform.position + Vector3.up * 0.1f) + (Vector3.forward / 8f), Vector3.right, out hitInfo, 2f, levelLayer)
             && !Physics.Raycast((thisTransform.position + Vector3.up * 0.1f) - (Vector3.forward / 8f), Vector3.right, out hitInfo, 2f, levelLayer))
                posX = posX + stepDistance;
        }
    }

    protected bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
    
    private void RotatePlayer(float angle)
    {
        manager.selectedCharacter.transform.DOLocalRotate(new Vector3(0, angle, 0), 0.13f)
            .SetEase(Ease.Linear).SetRelative(true)
            .OnComplete(() =>
            {
                manager.selectedCharacter.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.13f)
                    .SetEase(Ease.Linear).SetRelative(false);
            });
    }

    protected virtual void Update()
    {
        if (!dead && manager.bIsPlayPressed && !manager.cameraLerp && Time.timeScale != 0)
        {
            if (Time.timeScale != 0)
            {
                time += Time.deltaTime * timeMultiplier;
                //manager.timeScore.text = (time * timeScoreMultiplier).ToString("F0");
            }
           
            CheckGroundStatus();

            if (animForward < 1f)
            {               
                anim.SetFloat("Forward", animForward);
                animForward += Time.deltaTime * 2f;
            }
            else
                anim.SetFloat("Forward", 1f);

            if (!crouching)
                anim.SetBool("OnGround", m_IsGrounded);

            if (!m_IsGrounded)            
                anim.SetFloat("Jump", m_Rigidbody.velocity.y);            

#if UNITY_ANDROID || UNITY_IOS

            //Mobile Platforms movement
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    swiped = false;
                    fp = touch.position;
                    lp = touch.position;
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    lp = touch.position;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    swiped = true;
                }
                if(!swiped)
                {
                    if ((fp.x - lp.x) > mouseAndTouchMoveDistance) // left swipe
                    {
                        swiped = true;
                        RotatePlayer(-30f);
                        MoveSide(false);
                        
                        
                    }
                    else if ((fp.x - lp.x) < -mouseAndTouchMoveDistance) // right swipe
                    {
                        swiped = true;
                        RotatePlayer(30f);
                        MoveSide(true);
                       
                    }
                    else if ((fp.y - lp.y) < -mouseAndTouchMoveDistance) // up swipe
                    {
                        if (!m_Jump)
                        {
                            swiped = true;
                            Jump();
                        }
                    }
                    else if ((fp.y - lp.y) > mouseAndTouchMoveDistance) // down swipe
                    {
                        if (m_IsGrounded && !crouching)
                        {
                            swiped = true;
                            Crouch();
                        }
                        else if (!m_IsGrounded) // Pull player down in air
                        {
                            swiped = true;
                            PullDown();
                        }
                    }
                }
            }
#else            
            //Mouse movement
            if (enableMouseMovement)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    swiped = false;
                    fp = Input.mousePosition;
                    lp = Input.mousePosition;
                }
                if (HasMouseMoved())
                {
                    lp = Input.mousePosition;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    swiped = true;
                }
                if (!swiped)
                {
                    if ((fp.x - lp.x) > mouseAndTouchMoveDistance) // left swipe
                    {
                        swiped = true;
                        MoveSide(false);
                    }
                    else if ((fp.x - lp.x) < -mouseAndTouchMoveDistance) // right swipe
                    {
                        swiped = true;
                        MoveSide(true);
                    }
                    else if ((fp.y - lp.y) < -mouseAndTouchMoveDistance) // up swipe
                    {
                        if (!m_Jump)
                        {
                            swiped = true;
                            Jump();
                        }
                    }
                    else if ((fp.y - lp.y) > mouseAndTouchMoveDistance) // down swipe
                    {
                        if (m_IsGrounded && !crouching)
                        {
                            swiped = true;
                            Crouch();
                        }
                    }
                }
            }

            //Keyboard movement
            if (enableKeyboardMovement)
            {
                float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
                float vertical = CrossPlatformInputManager.GetAxis("Vertical");

                Vector3 m_Input = new Vector2(horizontal, vertical);

                if (CrossPlatformInputManager.GetButtonDown("Horizontal") && m_Input.x < 0)
                {
                    MoveSide(false);
                }
                if (CrossPlatformInputManager.GetButtonDown("Horizontal") && m_Input.x > 0)
                {
                    MoveSide(true);
                }
                if (CrossPlatformInputManager.GetButtonDown("Vertical") && m_Input.y > 0)
                {
                    if (!m_Jump)
                    {
                        Jump();
                    }
                }
                if (CrossPlatformInputManager.GetButtonDown("Vertical") && m_Input.y < 0)
                {
                    if (m_IsGrounded && !crouching)
                    {
                        Crouch();
                    }
                }
            }
#endif

            if (m_IsGrounded)
            {
                HandleGroundedMovement(m_Jump);
            }                     
            
            
            speed += Time.deltaTime * speedModifier;
                        
            if(timeMultiplierTime > 0)
            {
                timeMultiplierTime -= Time.deltaTime;
                if (timeMultiplierTime <= 0)
                {
                    timeMultiplierTime = 0;
                    timeMultiplier = 1f;
                }                          
            }
            if (coinsMultiplierTime > 0)
            {
                coinsMultiplierTime -= Time.deltaTime;
                if (coinsMultiplierTime <= 0)
                {
                    coinsMultiplierTime = 0;
                    coinsMultiplier = 1f;
                }               
            }
            if (jumpMultiplierTime > 0)
            {
                jumpMultiplierTime -= Time.deltaTime;
                if (jumpMultiplierTime <= 0)
                {
                    jumpMultiplierTime = 0;
                    jumpMultiplier = 1f;
                }               
            }

            m_Jump = false;
        }
        if(manager.cameraLerp)
        {
            anim.SetFloat("Forward", 0.5f);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!dead && manager.bIsPlayPressed)
        {
            if(!m_IsGrounded)
            {
                HandleAirborneMovement();
            }

            if (!manager.cameraLerp)
            {
                thisTransform.position = Vector3.Lerp(thisTransform.position, new Vector3(posX, thisTransform.position.y, thisTransform.position.z), 0.15f);
               
                if (hitSpeedLoc >= speed)
                {
                    thisTransform.Translate(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    thisTransform.Translate(0, 0, hitSpeedLoc * Time.deltaTime);
                    hitSpeedLoc += Time.fixedDeltaTime * hitSpeedDecreaseSpeed;
                }
            }

            /*if (!manager.cameraLerp)
                manager.cameraTransform.position = new Vector3(manager.cameraTransform.position.x, manager.cameraTransform.position.y, transform.position.z + cameraPosition.z);
            else           
                manager.cameraTransform.position = Vector3.Lerp(manager.cameraTransform.position, new Vector3(manager.cameraTransform.position.x, manager.cameraTransform.position.y, transform.position.z + cameraPosition.z), cameraSpeed * 3f);                          

            manager.cameraTransform.position = Vector3.Lerp(manager.cameraTransform.position, new Vector3(thisTransform.position.x, thisTransform.position.y + cameraPosition.y, thisTransform.position.z + cameraPosition.z), cameraSpeed);*/
        }
        if (manager.bIsPlayPressed)
        {
            if (!manager.cameraLerp)
            {                                
                if (manager.pursuitTimeLoc > 0)
                {
                    if(!dead)
                    {
                        manager.pursuerTransform.position = Vector3.Lerp(manager.pursuerTransform.position, new Vector3(manager.pursuerTransform.position.x, manager.pursuerTransform.position.y, transform.position.z - pursuerPositionOffset), 0.4f);
                        manager.pursuerTransform.position = Vector3.Lerp(manager.pursuerTransform.position, new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - pursuerPositionOffset), 0.1f);
                    }
                    else
                    {
                        manager.pursuerTransform.position = Vector3.Lerp(manager.pursuerTransform.position, new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - (pursuerPositionOffset - 1f)), 0.1f);
                    }
                }
                else if (manager.pursuitTimeLoc < -5f)
                {
                    manager.pursuerTransform.position = Vector3.Lerp(manager.pursuerTransform.position, new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - 10.0f), 0.1f);
                }

                if(manager.pursuitTimeLoc > -5f)
                    manager.pursuitTimeLoc -= Time.fixedDeltaTime;
            }
            else
                manager.pursuerTransform.position = Vector3.Lerp(manager.pursuerTransform.position, new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z - pursuerPositionOffset), 0.1f);
        }
    }

    protected virtual void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        // 0.3f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(thisTransform.position + (Vector3.up * 0.3f), Vector3.down, out hitInfo, m_GroundCheckDistance + 0.3f, levelLayer))
        {            
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            //m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            //m_Animator.applyRootMotion = false;
        }
    }

    protected virtual void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * 9f) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }

    protected virtual void HandleGroundedMovement(bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump)
        {
            // jump!
            audioSource.PlayOneShot(jumpClip);
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower * jumpMultiplier, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_GroundCheckDistance = 0.1f;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if (other.tag == "Bonus")
            {                
                Bonus bonus = other.GetComponent<Bonus>();
                Transform effect = Instantiate(bonus.effect, thisTransform.position + Vector3.up * capsuleColliders[0].center.y, Quaternion.identity).transform;
                effect.SetParent(thisTransform);

                if (bonus.sound != null)
                {
                    audioSource.PlayOneShot(bonus.sound);
                }

                if (bonus.isCoin)
                {
                    coins += (int)(bonus.multiplier * coinsMultiplier);
                    manager.CoinTriggered();
                }
                else if (bonus.isAbility)
                {
                    manager.AbilityTriggered();
                }
                else if (bonus.isMavinMode)
                {
                    manager.MavinTriggered();
                }

                other.GetComponent<Collider>().enabled = false;
                Destroy(other.gameObject);
                
                //manager.coinsScore.text = coins.ToString();
            }
        }
    }
    

    protected virtual void GameOver()
    {
        audioSource.PlayOneShot(gameOverClip);
        
        dead = true;
        m_IsGrounded = true;
        anim.SetBool("GameOver", true);
        
        manager.Crashed();
        
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (!dead)
        {
            //GameOver
            if (other.collider.tag != "Bonus" && (other.contacts[0].normal.z < -0.8f && other.contacts[0].normal.z > -1.2f))
            {
                bool gameOver = false;
                for (int i = 0; i < other.contacts.Length; i++)
                {
                    if (other.contacts[i].thisCollider == m_Collider)
                    {
                        gameOver = true;
                        break;
                    }
                }
                if (gameOver)
                {
                    GameOver();
                    Debug.Log("GameOver");
                }
                else
                {
                    if (manager.pursuitTimeLoc > 0)
                    {
                        GameOver();
                        Debug.Log("GameOverHit");
                    }
                    else
                    {
                        manager.pursuitTimeLoc = manager.pursuitTime;
                        hitSpeedLoc = initialHitSpeed;
                        Debug.Log("LightHit");
                    }
                }              
            }
        }
    }
}
