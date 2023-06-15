using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform indicatorPoint;

    private CounterTime counter = new CounterTime();
    private Vector3 positionJoystick = Vector3.zero;
    private bool isMoving;

    /*private void Start()
    {
        OnInit();
    }*/
    public void OnInit()
    {
        if (joystick != null)
        {
            joystick.OnPointerUp();
        }
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(indicatorPoint);
        TF.rotation = Quaternion.Euler(0, 180f, 0);
        level = LevelManager.Ins.currentLevel;
        score = 0;
        SetSize();
        targets.Clear();
        IsDead = false;
        ChangeAnim(Constant.ANIM_IDLE);
        ChangeItem();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Ins.IsState(GameState.MAINMENU))
        {
            ChangeAnim(Constant.ANIM_IDLE);
            return;
        }
        if (IsDead)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (joystick != null)
        {
            // Ấn chuột trái đặt vị trí joystick
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 viewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 joystickPosition = new Vector3(viewportPosition.x * Screen.width, viewportPosition.y * Screen.height, joystick.transform.position.z);
                if (joystickPosition.x < 780 || joystickPosition.y < 1600)
                {
                    joystick.transform.position = joystickPosition;
                }
            }
            Move();
            if (!Input.GetMouseButton(0))
            {
                counter.Execute();

            }
            // Thả chuột trái đặt lại joystick
            if (Input.GetMouseButtonUp(0))
            {
                joystick.transform.position = positionJoystick;
                StopMove();
                if (currentSkin.weapon.IsCanAttack)
                {
                    if (FindTarget() != Vector3.zero)
                    {
                        Attack();
                    }
                }
            }

            // Tìm mục tiêu tấn công
            if (!IsAttack && currentSkin.weapon.IsCanAttack)
            {
                if (FindTarget() != Vector3.zero)
                {
                    Attack();
                }
            }
        }

        /*Move();
        if (!Input.GetMouseButton(0))
        {
            counter.Execute();

        }
        if (Input.GetMouseButtonUp(0))
        {
            StopMove();
            if (currentSkin.weapon.IsCanAttack)
            {
                Attack();
            }
        }*/
    }

    // Dừng di chuyển
    public void StopMove()
    {
        isMoving = false;
        rb.velocity = Vector3.zero;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    // Di chuyển
    public void Move()
    {
        float horizontalMove = joystick.Horizontal;
        float verticalMove = joystick.Vertical;
        if (new Vector2(horizontalMove, verticalMove).magnitude > 0.1f)
        {
            ChangeAnim(Constant.ANIM_RUN);
        }
        if (verticalMove != 0)
        {
            isMoving = true;
            counter.Cancel();
            float angle = Mathf.Atan2(horizontalMove, verticalMove) * Mathf.Rad2Deg;
            rb.velocity = new Vector3(horizontalMove, 0, verticalMove) * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    // Tấn công
    public override void Attack()
    {
        base.Attack();
        Target = FindTarget();
        if (Target != Vector3.zero)
        {
            TF.LookAt(Target - (Target.y - TF.position.y) * Vector3.up);
            counter.Start(Throw, 0.4f);
        }
    }

    // Ném vũ khí
    public void Throw()
    {
        currentSkin.weapon.SetActive(false);
        currentSkin.weapon.Throw(this, Target, size);
        ResetAnim();
        Invoke("SetActiveWeapon", 1f);
    }

    // Player chết
    public override void Death()
    {
        base.Death();
        joystick.transform.position = positionJoystick;
        LevelManager.Ins.Defeat();
    }

    // Mặc trang bị
    public void ChangeItem()
    {
        ChangeSkin(UserData.Ins.playerSkin);
        ChangeWeapon(UserData.Ins.playerWeapon);
        ChangeHat(UserData.Ins.playerHat);
        ChangeAccessory(UserData.Ins.playerAccessory);
        ChangePant(UserData.Ins.playerPant);
    }

    public Joystick Joystick { get { return joystick; } set { joystick = value; } }
    public Vector3 PositionJoystick { get { return positionJoystick; } set { positionJoystick = value; } }
}
