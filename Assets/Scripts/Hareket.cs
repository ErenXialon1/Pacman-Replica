using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hareket : MonoBehaviour
    {
        public float speed = 8f;
        public float speedMultiplier = 1f;
        public Vector2 initialDirection;
        public LayerMask obstacleLayer;
        public Vector2 direction;


        public Vector2 nextDirection;
        public Vector3 startingPosition;
        public new Rigidbody2D rigidbody;

        // Start is called before the first frame update
        private void Awake()
        {
            this.rigidbody = GetComponent<Rigidbody2D>();
            this.startingPosition = this.transform.position;
        }
        private void Start()
        {
            ResetState();
        }
        public void ResetState()
        {
            this.speedMultiplier = 1.0f;
            this.direction = this.initialDirection;
        this.transform.position = startingPosition;
        }

        private void Update()
        {
            if(this.nextDirection != Vector2.zero)
            {
                SetDirection(this.nextDirection);
            }
        }
        private void FixedUpdate()
        {
            Vector2 position = this.rigidbody.position;
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(position + translation);
        }
        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !Occupied(direction))
            {
                this.direction = direction;
                this.nextDirection = Vector2.zero;
            }
            else
            {
                this.nextDirection = direction;
            }
        }
        public bool Occupied(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.55f, 0.0f, direction, 0.40f, this.obstacleLayer);
            return hit.collider != null;
        }
    }
