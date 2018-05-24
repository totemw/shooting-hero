using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int Health = 10;			//玩家的生命值
	public bool IsAlive = true;		//玩家是否存活

	//每帧执行一次，检测玩家是否存活
	private void Update () {	
		if (Health <= 0)
			IsAlive = false;
	}

	//玩家扣血函数，用于GameManager脚本中调用
	public void TakeDamage(int damage){
		Health -= damage;
		if (Health < 0) 
			Health = 0;
	}
}
