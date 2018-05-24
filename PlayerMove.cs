using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float MoveSpeed = 10.0f;		//玩家移动速度
    public float RotateSpeed = 40.0f;	//玩家旋转速度
    public float JumpVelocity = 2.0f;	//玩家起跳速度

	private Animator _animator;		//玩家的Animator组件，用于控制玩家动画的播放
    private Rigidbody _rigidbody;	//玩家的刚体组件

    private float _h;				//获取玩家横轴输入
    private float _v;				//获取玩家纵轴输入
    private bool _isGrounded;		//玩家是否在地面上
    private readonly float _groundedRaycastDistance = 0.1f;	//表示向地面发射射线的射线长度

	//初始化，获取玩家组件
	private void Start () {
		_animator= GetComponent<Animator> ();	//获取玩家Animator组件
        _rigidbody = GetComponent<Rigidbody>();	//获取玩家刚体组件
    }

	//每个固定时间执行一次，用于物理模拟
	private void FixedUpdate()
    {
		//从玩家的位置垂直向下发出长度为groundedRaycastDistance的射线，返回值表示玩家是否该射线是否碰撞到物体，该句代码用于检测玩家是否在地面上
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, _groundedRaycastDistance);
        Jump(_isGrounded);	//调用跳跃函数
    }

	//跳跃函数，用于FixedUpdate()中调用
	private void Jump(bool isGround)
	{
		//当玩家按下跳跃键Space，并且玩家在地面上时执行跳跃相关函数
		if (Input.GetKey(KeyCode.Space) && isGround)
		{
			//给玩家刚体组件添加向上的作用力，以改变玩家的运动速度，改变值为jumpVelocity
			_rigidbody.AddForce(Vector3.up * JumpVelocity, ForceMode.VelocityChange);	
			_animator.SetBool("isJump", true);	//设置动画参数，将isJump布尔型参数设置为true，播放玩家跳跃动画
		}
		else if(isGround) _animator.SetBool("isJump", false);	//设置动画参数，将isJump布尔型参数设置为false，停止播放玩家跳跃动画
	}

	//每帧执行一次，用于玩家的位移与旋转
	private void Update () {
        var h = Input.GetAxisRaw("Horizontal");	//获取玩家水平轴上的输入
        var v = Input.GetAxisRaw("Vertical");		//获取玩家垂直轴上的输入
        MoveAndRotate(h, v);		//根据玩家在水平、垂直轴上的输入，调用玩家的位移与旋转函数
    }

	//玩家的位移与旋转函数
	private void MoveAndRotate(float h, float v)
    {
		//v>0表示获取玩家向前的输入，玩家以moveSpeed的速度向前运动
        if (v > 0) transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);	
		//v<0表示获取玩家向后的输入，玩家以moveSpeed的速度向后运动
        else if (v < 0) transform.Translate(-Vector3.forward * MoveSpeed * Time.deltaTime);	

		//若玩家垂直轴上有输入，则表示玩家进行位移，设置动画参数，将isMove布尔型参数设置为true，播放玩家奔跑动画
        if (v != 0.0f) _animator.SetBool("isMove", true);
		//若玩家垂直轴上无输入，则表示玩家没有位移，设置动画参数，将isMove布尔型参数设置为false，停止播放玩家奔跑动画
        else _animator.SetBool("isMove", false);

		//根据玩家水平轴的输入进行旋转，顺时针为正方向
        transform.Rotate(Vector3.up * h * RotateSpeed * Time.deltaTime);
    }

}