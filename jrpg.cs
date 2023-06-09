using System;

class JRPG
{
    static void Main()
    {
        Console.WriteLine("Welcome to the JRPG!");

        while (true)
        {
            PlayGame();
            Console.WriteLine("Game over! Play again? (Y/N)");

            if (Console.ReadKey().Key != ConsoleKey.Y)
                break;
        }

        Console.WriteLine("Thank you for playing the JRPG!");
    }

    static void PlayGame()
    {
        Console.WriteLine("Choose your class:");
        Console.WriteLine("1. Knight");
        Console.WriteLine("2. Healer");
        Console.WriteLine("3. Rogue");
        Console.Write("Enter the number of your class: ");

        PlayerClass playerClass;

        switch (Console.ReadLine())
        {
            case "1":
                playerClass = PlayerClass.Knight;
                break;
            case "2":
                playerClass = PlayerClass.Healer;
                break;
            case "3":
                playerClass = PlayerClass.Rogue;
                break;
            default:
                Console.WriteLine("Invalid input. Defaulting to Knight.");
                playerClass = PlayerClass.Knight;
                break;
        }

        Console.WriteLine($"You have chosen the {playerClass} class.");

        Player player = new Player(playerClass);

        Console.WriteLine("A wild enemy appears!");

        Enemy enemy = GetRandomEnemy();

        Console.WriteLine($"A {enemy.Type} attacks!");

        bool gameOver = false;

        while (!gameOver)
        {
            Console.WriteLine();
            Console.WriteLine($"Player HP: {player.HP} / {player.MaxHP}");
            Console.WriteLine($"Enemy HP: {enemy.HP} / {enemy.MaxHP}");
            Console.WriteLine();
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Heal");
            Console.Write("Choose an action: ");

            switch (Console.ReadLine())
            {
                case "1":
                    player.Attack(enemy);
                    break;
                case "2":
                    player.Heal();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
            }

            if (enemy.HP <= 0)
            {
                Console.WriteLine($"You defeated the {enemy.Type}! Congratulations!");
                gameOver = true;
            }
            else
            {
                enemy.Attack(player);

                if (player.HP <= 0)
                {
                    Console.WriteLine("You were defeated. Game over!");
                    gameOver = true;
                }
            }
        }
    }

    static Enemy GetRandomEnemy()
    {
        Random random = new Random();
        int enemyType = random.Next(1, 4);

        switch (enemyType)
        {
            case 1:
                return new Enemy(EnemyType.Slime);
            case 2:
                return new Enemy(EnemyType.Skeleton);
            case 3:
                return new Enemy(EnemyType.Thief);
            default:
                return new Enemy(EnemyType.Slime);
        }
    }
}

enum PlayerClass { Knight, Healer, Rogue }

class Player
{
    public PlayerClass Class { get; private set; }
    public int MaxHP { get; private set; }
    public int HP { get; private set; }
    public int AttackPower { get; private set; }
    public int HealPower { get; private set; }

    public Player(PlayerClass playerClass)
    {
        Class = playerClass;

        switch (Class)
        {
            case PlayerClass.Knight:
                MaxHP = 100;
                AttackPower = 20;
                break;
            case PlayerClass.Healer:
                MaxHP = 80;
                HealPower = 30;
                break;
            case PlayerClass.Rogue:
                MaxHP = 70;
                AttackPower = 25;
                break;
        }

        HP = MaxHP;
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"You attack the {enemy.Type}!");
        enemy.HP -= AttackPower;
    }

    public void Heal()
    {
        Console.WriteLine("You heal yourself!");
        HP += HealPower;

        if (HP > MaxHP)
            HP = MaxHP;
    }
}

enum EnemyType { Slime, Skeleton, Thief }

class Enemy
{
    public EnemyType Type { get; private set; }
    public int MaxHP { get; private set; }
    public int HP { get; set; }
    public int AttackPower { get; private set; }

    public Enemy(EnemyType enemyType)
    {
        Type = enemyType;

        switch (Type)
        {
            case EnemyType.Slime:
                MaxHP = 50;
                AttackPower = 10;
                break;
            case EnemyType.Skeleton:
                MaxHP = 70;
                AttackPower = 15;
                break;
            case EnemyType.Thief:
                MaxHP = 60;
                AttackPower = 20;
                break;
        }

        HP = MaxHP;
    }

    public void Attack(Player player)
    {
        Console.WriteLine($"The {Type} attacks!");
        player.HP -= AttackPower;
    }
}
