using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

	public float maxSpeed = 10f;
	public float jumpForce = 400f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float move;
	public bool jump;
	
	public int coins = 0;
	public int lifes = 3;

	public bool win = false;
    public bool loose = false;

	public spawnPointScript spawnPoint;

	Animator animator;
	Rigidbody2D body;
	spawnPointScript temporary;
	Vector2 spawn;

	void Start() {

		spawn = spawnPoint.transform.position;
		transform.position = spawn;

		animator = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
	}
	
	void Update() {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Vertical");
	}

    void FixedUpdate()
    {

        if (grounded && jump) {

            body.AddForce(new Vector2(0f, jumpForce));
        }

        if (grounded) {

            body.velocity = new Vector2(move * maxSpeed, body.velocity.y);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("Ground", grounded);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip() {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Die() {

		lifes--;

        if(lifes <= 0)
            loose = true;
        else
		    transform.position = spawn;
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		if (col.gameObject.tag == "Enemy") {
			Die();
		}
		
		if (col.gameObject.tag == "Win") {
			win = true;
		}

		if (col.gameObject.tag == "Coin") {
			coins++;
			Destroy(col.gameObject);
		}

		if (col.gameObject.name == "spawnPoint") {
			
			temporary = col.gameObject.GetComponent<spawnPointScript>();

			if(!temporary.activated) {
				spawn = temporary.transform.position;
				temporary.activated = true;
			}
		}
	}
}