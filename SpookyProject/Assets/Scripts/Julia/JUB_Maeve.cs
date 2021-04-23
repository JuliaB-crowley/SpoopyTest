using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace character
{
    public enum DirectionAngle {North, Est, South, West}
    public class JUB_Maeve : MonoBehaviour
    {
        //collisions
        public JUB_MaeveCollisionDetector left, right, top, bottom;
        public bool collisionLeft, collisionRight, collisionTop, collisionBottom;

        //mouvements
        Rigidbody2D rigidBody;
        Controller controller;
        public float speed, rollSpeed, rollDuration, rollRecover, crouchSpeed, xVelocity, yVelocity, accelerationTime;
        Vector2 rStick, lStick, lStickNormalised, lastDirection, rollDirection, targetSpeed, currentSpeed;
        public float lastAngle;
        public DirectionAngle dirAngle;


        [SerializeField]
        bool isInRoll,  isInRecoil, isInImmunity, isInRecover, isPushingObject;
        public bool isFlashing;
        public LayerMask pushableObjects, interactibleObjects;
        public bool isCrouching;
        [SerializeField]
        bool attackMaintained, isInBuildup, isInAttack;

        //combat
        public Transform attackPoint;
        public float attackRange, attackTime;
        public Vector2 quickAtkZone, heavyAtkZone;
        public LayerMask ennemies, breakableObjects;
        public int attackDamage;
        public bool ennemyWasHitOnce;
        List<Collider2D> ennemiesHitLastTime = new List<Collider2D>();
        public float immunityTime;

        //pousser des objets
        Collider2D[] allPushableInRange, allInteractibleInRange;
        public float interactAndPushableRange;

        //HUD
        public int maxLife, currentLife, currentBonbons;
        public Text displayLife, displayBonbons;
        public Sprite[] heartSprites;
        public Image heartsDisplay;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            controller = new Controller();
            controller.Enable();
            displayBonbons.text = currentBonbons.ToString();

            AttackProfile quickAttack = new AttackProfile(1, new Vector2(1, 1), 0.1f, 0.2f, "quick");
            AttackProfile heavyAttack = new AttackProfile(3, new Vector2(2, 1), 0, 0.8f, "heavy");

            currentLife = maxLife;

            controller.MainController.Roll.performed += ctx => Roll();
            controller.MainController.Crouch.performed += ctx => Crouch();
            controller.MainController.Push.performed += ctx => PushObjects();
            controller.MainController.Interact.performed += ctx => Interact();
            //controller.MainController.Crouch.performed += ctx => isCrouching = !isCrouching;
            controller.MainController.Attack.performed += ctx => Attack(quickAttack);// Attack();
            controller.MainController.HeavyAttack.performed += ctx => Attack(heavyAttack);
        }

        // Update is called once per frame
        void Update()
        {
            Inputs();
            InteractSphere();
            PushableSphere();
            Move();

            displayLife.text = currentLife.ToString() + " / " + maxLife.ToString();
            if (currentLife > maxLife)
            {
                currentLife = maxLife;
            }

            if(currentLife <= 0)
            {
                heartsDisplay.sprite = heartSprites[0];
            }
            else 
            { 
                heartsDisplay.sprite = heartSprites[currentLife - 1];
            }
        }

        void Inputs()
        {
            lStick = controller.MainController.Move.ReadValue<Vector2>();
            lStickNormalised = lStick.normalized;

            if (lStick != Vector2.zero)
            {
                lastDirection = lStickNormalised;
                lastAngle = Vector2.Angle(Vector2.up, lastDirection);

                if(lastAngle > -45 && lastAngle < 45)
                {
                    dirAngle = DirectionAngle.North;
                }
                else if(45 < lastAngle && lastAngle < 135 && xVelocity > 0)
                {
                    dirAngle = DirectionAngle.Est;
                }
                else if(45 < lastAngle && lastAngle < 135 && xVelocity < 0)
                {
                    dirAngle = DirectionAngle.West;
                }
                else
                {
                    dirAngle = DirectionAngle.South;
                }

                //Debug.Log(lastAngle.ToString() + dirAngle.ToString());


                //animation movement on
            }
            else
            {
                //animation movement off
            }

            if (!isInRoll)
            {
                rollDirection = lastDirection;
            }
        }

        void Move()
        {
            if (!isInRoll && !isFlashing)
            {
                if (!isCrouching && !isPushingObject)
                {
                    targetSpeed = Vector2.ClampMagnitude(lStick, 1) * speed;
                }
                else
                {
                    targetSpeed = Vector2.ClampMagnitude(lStick, 1) * crouchSpeed;
                }
            }

            collisionLeft = left.isCollision;
            collisionRight = right.isCollision;
            collisionTop = top.isCollision;
            collisionBottom = bottom.isCollision;

            if (!isInRecoil && !isFlashing)
            {
                currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, targetSpeed.x, ref xVelocity, accelerationTime);
                currentSpeed.y = Mathf.SmoothDamp(currentSpeed.y, targetSpeed.y, ref yVelocity, accelerationTime);

            }

            if (collisionLeft && currentSpeed.x < 0)
            {
                currentSpeed.x = 0;
            }
            if (collisionRight && currentSpeed.x > 0)
            {
                currentSpeed.x = 0;
            }
            if (collisionTop && currentSpeed.y > 0)
            {
                currentSpeed.y = 0;
            }
            if (collisionBottom && currentSpeed.y < 0)
            {
                currentSpeed.y = 0;
            }
            if(!isFlashing)
            {
                rigidBody.velocity = currentSpeed;

            }
            else
            {
                rigidBody.velocity = Vector2.zero;
            }

        }

        void Crouch()
        {
            if(!isFlashing && !isInRoll && !isInRecover && !isPushingObject && !isInAttack)   
            {
                isCrouching = !isCrouching;
                //changement mode anim debout accroupi
                //son

                if (isCrouching)
                {
                    Debug.Log("s Crouching !");

                    //indique aux collisions détectors d'ignorer le layer crouchable 
                    //détection des ennemis baisse

                }
            }
        }

        void Roll()
        {
            if (!isInRecover && !isPushingObject && !isCrouching)
            {
                isInRoll = true;
                isInImmunity = true;
                isInRecover = true;
                StartCoroutine(RollCoroutine());
            }
        }

        IEnumerator RollCoroutine()
        {
            targetSpeed = rollDirection * rollSpeed;
            //anim roulade
            yield return new WaitForSeconds(rollDuration);
            isInRoll = false;
            isInImmunity = false;
            yield return new WaitForSeconds(rollRecover);
            isInRecover = false;
        }


        void Attack(AttackProfile attackProfile)
        {
            Vector2 attackVector = Vector2.zero;
            if (!isInRecover && !isInBuildup && !isInRoll && !isCrouching && !isPushingObject && !isFlashing)
            {
                ennemiesHitLastTime.Clear();

                switch (dirAngle)
                {
                    case DirectionAngle.North:
                        attackVector = Vector2.up;
                        break;

                    case DirectionAngle.West:
                        attackVector = Vector2.left;
                        break;

                    case DirectionAngle.Est:
                        attackVector = Vector2.right;
                        break;

                    case DirectionAngle.South:
                        attackVector = Vector2.down;
                        break;
                }
                isInBuildup = true;
                attackProfile.atkVector = attackVector;
                StartCoroutine(Buildup(attackProfile));

                Debug.Log(attackProfile.atkName);
            }
        }

        IEnumerator Buildup(AttackProfile attackProfile)
        {
            yield return new WaitForSeconds(attackProfile.atkBuildup);
            isInBuildup = false;
            isInRecover = true;
            StartCoroutine(Hit(attackProfile));
        }
        IEnumerator Hit(AttackProfile attackProfile)
        {
            Collider2D[] hitEnnemies = Physics2D.OverlapCircleAll(transform.position, attackProfile.atkZone.x, ennemies);
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackProfile.atkZone.x, breakableObjects);

            if (!ennemyWasHitOnce)
            {
                foreach (Collider2D ennemy in hitEnnemies)
                {
                    if (!ennemiesHitLastTime.Contains(ennemy))
                    {
                        Debug.Log(ennemy.bounds.extents.magnitude);
                        Vector2 ennemyDirection = ennemy.transform.position - transform.position;
                        float ennemyAngle = Vector2.Angle(attackProfile.atkVector, ennemyDirection);
                        float a = ennemyDirection.magnitude;
                        float b = ennemyDirection.magnitude;
                        float c = ennemy.bounds.extents.x * 2;
                        float additionalAngle = Mathf.Rad2Deg * Mathf.Acos(((a * a) + (b * b) - (c * c)) / (2 * (a * b)));
                        float totalAngle = attackProfile.atkZone.y + additionalAngle;
                        //Debug.Log("Additional Angle = " + additionalAngle + " / AA+AtkAngle = " + totalAngle + " / Ennemy Angle = " + ennemyAngle);
                        if (ennemyAngle <= totalAngle)
                        {
                            if (ennemy.GetComponent<JUB_EnnemyDamage>())
                            {
                                ennemy.GetComponent<JUB_EnnemyDamage>().TakeDamage(attackProfile.atkDamage);
                                Debug.Log("attack was performed");
                                ennemiesHitLastTime.Add(ennemy);

                            }

                        }
                    }
                }

            }

            foreach (Collider2D breakableObject in hitObjects)
            {
                breakableObject.GetComponent<JUB_BreakableBehavior>().Breaking();
            }

            yield return new WaitForSeconds(attackProfile.atkRecover);
            isInRecover = false;
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, quickAtkZone.x);
            Vector3 attackLength;
            attackLength = new Vector3(quickAtkZone.x, 0);
            attackLength = Quaternion.Euler(0, 0, quickAtkZone.y) * attackLength;
            Gizmos.DrawLine(transform.position, transform.position + attackLength);
            attackLength = Quaternion.Euler(0, 0, -2 * quickAtkZone.y) * attackLength;
            Gizmos.DrawLine(transform.position, transform.position + attackLength);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, heavyAtkZone.x);
            attackLength = new Vector3(heavyAtkZone.x, 0);
            attackLength = Quaternion.Euler(0, 0, heavyAtkZone.y) * attackLength;
            Gizmos.DrawLine(transform.position, transform.position + attackLength);
            attackLength = Quaternion.Euler(0, 0, -2 * heavyAtkZone.y) * attackLength;
            Gizmos.DrawLine(transform.position, transform.position + attackLength);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, interactAndPushableRange);
        }

        class AttackProfile
        {
            public AttackProfile(float damage, Vector2 zone, float buildup, float recover, string name)
            {
                atkDamage = damage;
                atkZone = zone;
                atkRecover = recover;
                atkBuildup = buildup;
                atkName = name;
            }

            public float atkDamage, atkRecover, atkBuildup;
            public Vector2 atkZone, atkVector;
            public string atkName;

            public void ChangeDamage(float changeAmount)
            {
                atkDamage += changeAmount;
            }

            public void NewDamage(float newDamageValue)
            {
                atkDamage = newDamageValue;
            }

        }

        void InteractSphere()
        {
            allInteractibleInRange = Physics2D.OverlapCircleAll(transform.position, interactAndPushableRange, interactibleObjects);
            foreach(Collider2D interactible in allInteractibleInRange)
            {
                interactible.GetComponent<JUB_InteractibleBehavior>().interactible = true;
            }
        }

        void Interact()
        {
            if(!allInteractibleInRange.Count().Equals(0) && !isInRoll && !isInAttack && !isFlashing)
            {
                if(allInteractibleInRange.Count().Equals(1))
                {
                    if(allInteractibleInRange.Count().Equals(1))
                    {
                        allInteractibleInRange[0].GetComponent<JUB_InteractibleBehavior>().interacted = true;
                    }
                    else
                    {
                        float smallestAngle = Mathf.Infinity;
                        Collider2D interactibleTarget = allInteractibleInRange[0];
                        foreach(Collider2D interactible in allInteractibleInRange)
                        {
                            Vector2 playerToInteractible = interactible.transform.position - transform.position;
                            float interactibleAngle = Vector2.Angle(lastDirection, playerToInteractible);
                            if(interactibleAngle < smallestAngle)
                            {
                                interactibleTarget = interactible;
                                smallestAngle = interactibleAngle;
                            }
                        }
                        interactibleTarget.GetComponent<JUB_InteractibleBehavior>().interacted = true;
                    }
                }
            }
        }

        void PushableSphere()
        {
            allPushableInRange = Physics2D.OverlapCircleAll(transform.position, interactAndPushableRange, pushableObjects);
            foreach(Collider2D pushable in allPushableInRange)
            {
                pushable.GetComponent<JUB_PushableBehavior>().pushable = true;
            }
        }
        void PushObjects()
        {
            if (!isFlashing && !isCrouching && !isInAttack && !isInRoll)
            {
                isPushingObject = !isPushingObject;
                if (!allPushableInRange.Count().Equals(0) && isPushingObject)
                {
                    if (allPushableInRange.Count().Equals(1))
                    {
                        allPushableInRange[0].GetComponent<JUB_PushableBehavior>().pushed = true;
                        allPushableInRange[0].GetComponent<JUB_PushableBehavior>().ManagePushing();
                    }
                    else
                    {
                        float smallestAngle = Mathf.Infinity;
                        Collider2D pushableTarget = allPushableInRange[0];
                        foreach (Collider2D pushable in allPushableInRange)
                        {
                            Vector2 playerToPushable = pushable.transform.position - transform.position;
                            float interactibleAngle = Vector2.Angle(lastDirection, playerToPushable);
                            if (interactibleAngle < smallestAngle)
                            {
                                pushableTarget = pushable;
                                smallestAngle = interactibleAngle;
                            }
                        }
                        pushableTarget.GetComponent<JUB_PushableBehavior>().pushed = true;
                        pushableTarget.GetComponent<JUB_PushableBehavior>().ManagePushing();
                    }
                }
                else if (isPushingObject == false)
                {
                    foreach (Collider2D pushable in allPushableInRange)
                    {
                        pushable.GetComponent<JUB_PushableBehavior>().pushed = false;
                        pushable.GetComponent<JUB_PushableBehavior>().ManagePushing();
                    }
                }
                Debug.Log(isPushingObject.ToString());
            }

            //inverser le booléen

            //si isPush = true mettre l'objet en parent
            //agrandir le box collider du joueur pour qu'il s'ajoute celui de l'objet

            //si !isPush = enlever le parent
            //reduire le collider du joueur à son état d'origine
        }

        //fonctions liées au HUD

        public void TakeDamages(int damages)
        {
            if (!isInImmunity)
            {
                currentLife -= damages;
                if (currentLife <= 0)
                {
                    currentLife = 0;
                    Die();
                }
                Immunity(immunityTime);
            }
        }

        public void Immunity(float immuTime)
        {
            StartCoroutine(ImmunityCoroutine(immuTime));
        }

        IEnumerator ImmunityCoroutine(float immuTime)
        {
            isInImmunity = true;
            yield return new WaitForSeconds(immuTime);
            isInImmunity = false;
        }

        public void Heal(int heal)
        {
            currentLife += heal;
            if (currentLife > maxLife)
            {
                currentLife = maxLife;
            }
        }

        public void MaxUpgrades(int upgrade)
        {
            maxLife += upgrade;
            currentLife += maxLife;
        }

        void Die()
        {
            //RIP
            //anim mort
            //respawn checkpoint
        }

        public void GainBonbons(int bonbons)
        {
            currentBonbons += bonbons;
            displayBonbons.text = currentBonbons.ToString();
        }

        public void Achat(int price)
        {
            currentBonbons -= price;
            displayBonbons.text = currentBonbons.ToString();
        }

       private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Heal"))
            {
                Heal(collision.GetComponent<RPP_CollectibleScript>().collectibleValeur);
                collision.GetComponent<RPP_CollectibleScript>().collectibleObject.SetActive(false);
            }

            if (collision.CompareTag("HealthBoost"))
            {
                MaxUpgrades(collision.GetComponent<RPP_CollectibleScript>().collectibleValeur);
                collision.GetComponent<RPP_CollectibleScript>().collectibleObject.SetActive(false);
            }

            if (collision.CompareTag("Bonbon"))
            {
                GainBonbons(collision.GetComponent<RPP_CollectibleScript>().collectibleValeur);
                collision.GetComponent<RPP_CollectibleScript>().collectibleObject.SetActive(false);
            }

            if (collision.CompareTag("DamageDealer"))
            {
                TakeDamages(collision.GetComponent<JUB_DamagingEvent>().damageAmount);
            }
        }
    }
}
