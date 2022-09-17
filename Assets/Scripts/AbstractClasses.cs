using UnityEngine;

public abstract class Weapon
{
    private float damage;
    public float Damage { get => damage; protected set => damage = value; }
    private float range;
    public float Range { get => range; protected set => range = value; }

    public abstract void Hit();
}

public abstract class Enemy
{
    private float health;
    public float Health { get => health; protected set => health = value; }

    private Color enemyColor;
    public Color EnemyColor { get => enemyColor; protected set => enemyColor = value; }

    private bool isDead = false;
    public bool IsDead { get => isDead; }

    public void RecieveHit(float value)
    {
        Health -= value;
        if(Health <= 0)
        {
            isDead = true;
            Death();
        }
    }

    public abstract void Death();
}

public class Fist : Weapon
{
    public Fist()
    {
        Damage = 5f;
        Range = 1f;
    }

    public override void Hit()
    {
        // hits the fist, no weapon shown

    }
}

public class Bat : Weapon
{
    public Bat()
    {
        Damage = 25f;
        Range = 3f;
    }

    public override void Hit()
    {
        // hits the bat, weapon shown

    }
}

public class RedEnemy : Enemy
{
    public RedEnemy(float heatlh)
    {
        Health = heatlh;
        EnemyColor = new Color(1, 0, 0);
    }

    public override void Death()
    {
        Debug.Log("Cringanyl");
    }
}

public class BlueEnemy : Enemy
{
    public BlueEnemy(float heatlh)
    {
        Health = heatlh;
        EnemyColor = new Color(0, 0, 1);
    }

    public override void Death()
    {
        Debug.Log("Slovil cringe");
    }
}

public abstract class EnemyFactory
{
    public abstract Enemy CreateEnemy();
    public abstract Weapon CreateWeapon();
}

public class RedEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new RedEnemy(Resources.instance.GetRandomFloat(1, 51));
    }

    public override Weapon CreateWeapon()
    {
        return new Fist();
    }
}

public class BlueEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new BlueEnemy(Resources.instance.GetRandomFloat(51, 101));
    }

    public override Weapon CreateWeapon()
    {
        return new Bat();
    }
}