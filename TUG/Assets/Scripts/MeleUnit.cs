﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamagableEntity))] //no invulrable mele units for now
public class MeleUnit : BaseUnit
{


	protected override void attack (DamagableEntity enemy)
	{
		enemy.takeDmg(attackDmg);//the meles implimentation of attack
	}
	
	private void attackOnCollision(Collision other)
	{
		BaseUnit otherUnit = other.gameObject.GetComponent<BaseUnit>();// stores the base unit of what ever you hit
		DamagableEntity otherUnitDamage = other.gameObject.GetComponent<DamagableEntity>();
		if (otherUnit != null && otherUnitDamage != null)// if the unit stored exists
		{
			if (!checkTeam (otherUnit))//checks if its not on your team
			{
				attack (otherUnitDamage);//passes it to the attack function
			}
		}
	}
	
	private void OnCollisionEnter(Collision other)
	{
		attackOnCollision(other);
	}
	private void OnCollisionStay(Collision other)
	{
		attackOnCollision(other);
	}
}
