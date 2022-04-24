using System;
using System.Configuration;
using System.Diagnostics;

namespace Players_modified
{
    abstract class BASEPlayer
    {
      protected static float AddAndSubWithMaxValue(char symbol, float value, float addition, float maxvalue)
        {
            if (symbol == '+')
            {
                value = value + addition > maxvalue ? maxvalue : value + addition;
            }
            else if (symbol == '-')
            {
                value = value - addition < maxvalue ? maxvalue : value - addition;
            }

            return value;
        }
        protected static Random randGen = new Random();
        public abstract string Name { get; set; }
        public abstract float Rating { get; set; }
        public abstract string Team { get; set; }
        public abstract int Age { get; set; }
        public abstract string Nickname { get; set; }
        public const string Defaultnickname = "Player";
        protected  float Energy = 100f;
        protected  float Tilt = 0f;
        
        public BASEPlayer()
        {
          
        }

        public abstract string Play();
        public abstract void Eat(string Food);
        public abstract void Sleep(float hours);
        public abstract void Entertainment(string type);
        public abstract string GetTiltandEnergy(bool higher);
        
        ~BASEPlayer()
        {
            System.Diagnostics.Trace.WriteLine("Вызван финализатор");
        }

    }

    class Player:BASEPlayer
    {
        public override string Name { get; set; }
        public  override float Rating { get; set; }
        public  override string Team { get; set; }
        public  override int Age { get; set; }
        public override string Nickname { get; set; }
        public Player(string name)
        {
            Name = name;
            Rating = 0;
            Team = "";
            Age = 0;
            Nickname = Defaultnickname;
        }

        public Player(string name, float rating, string team, int age)
        {
            Name = name;
            Rating = rating;
            Team = team;
            Age = age;
            Nickname = Defaultnickname;
        }

        public Player(string name, float rating, string team, int age, string nickname)
        {
            Name = name;
            Rating = rating;
            Team = team;
            Age = age;
            Nickname = nickname;
        }
        public override string Play()
        {
            Energy = BASEPlayer.AddAndSubWithMaxValue('-', Energy, 10, 0);
           
            int winchance = randGen.Next(0,100);
            if (winchance < 70)
            {
                Tilt = AddAndSubWithMaxValue('+', Tilt, 40, 100);
                return $"{winchance} Lose";
            }
            else
            {
                Tilt = AddAndSubWithMaxValue('-', Tilt, 20, 0);
                return $"{winchance} Win";
            }
        }

        public override void Eat(string Food)
        {
            switch (Food)
            {
                case "Шоколадка":
                    Energy = AddAndSubWithMaxValue('+', Energy, 20, 100);
                    break;
                case "Бургер":
                    Energy = AddAndSubWithMaxValue('+', Energy, 30, 100);
                    break;
                case "Энергетик":
                    Energy = AddAndSubWithMaxValue('+', Energy, 35, 100);
                    break;
                case "Кола":
                    Energy = AddAndSubWithMaxValue('+', Energy, 15, 100);
                    break;
                case "Пицца":
                    Energy = AddAndSubWithMaxValue('+', Energy, 35, 100);
                    break;
                default:
                    Console.WriteLine("Такой еды нет");
                    break;
            }
        }

        public override void Sleep(float hours)
        {
            if (hours >= 2 && hours < 4)
            {
                Energy = 40;
            }
            else if (hours >= 4 && hours < 6)
            {
                Energy = 60;
            }
            else if (hours >= 6 && hours < 8)
            {
                Energy = 80;
            }
            else if (hours >= 8)
            {
                Energy = 100;
            }
        }

        public override void Entertainment(string type)
        {
            switch (type)
            {
                case "Смотреть кино":
                    Tilt = AddAndSubWithMaxValue('-', Tilt, 20, 0);
                    break;
                case "Гулять с друзьями":
                    Tilt = AddAndSubWithMaxValue('-', Tilt, 40, 0);
                    break;
                case "Пить пиво":
                    Tilt = AddAndSubWithMaxValue('-', Tilt, 25, 0);
                    break;
                default:
                    Console.WriteLine("Такого развлечения нет");
                    break;
            }
        }

        public override string GetTiltandEnergy(bool higher)
        {
            if (higher)
            { 
                return $"Игрок {this.Name} {this.Nickname} имеет {this.Energy}% энергии и на {this.Tilt}% в тильте";
            }
            else
            {
                return $"На следующий день игрок {this.Name} {this.Nickname} имеет {this.Energy}% энергии и на {this.Tilt}% в тильте";
            }
        }
    }
    class Program
    {
        static void Main()
        {
            Player Player1 = new Player("Илья Киреев");
            Player Player2 = new Player("Александр Костылев",1.38f,"NA'VI",23);
            Player Player3 = new Player("Денис Шарипов",1.12f,"NA'VI",23,"electron1c");
            Player1.Nickname = "CheckWay";
            Player2.Nickname = "S1mple";
            Console.WriteLine(Player1.Play());
            Console.WriteLine(Player1.Play());
            Console.WriteLine(Player1.GetTiltandEnergy(true));
            Player1.Eat("Энергетик");
            Player1.Entertainment("Смотреть кино");
            Player1.Sleep(10);
            Console.WriteLine(Player1.GetTiltandEnergy(false));
            Console.WriteLine(Player2.Play());
            Console.WriteLine(Player2.Play());
            Console.WriteLine(Player2.Play());
            Console.WriteLine(Player2.GetTiltandEnergy(true));
            Player2.Eat("Шоколадка");
            Player2.Entertainment("Гулять с друзьями");
            Player2.Sleep(6);
            Console.WriteLine(Player2.GetTiltandEnergy(false));
            Console.WriteLine(Player3.Play());
            Console.WriteLine(Player3.Play());
            Console.WriteLine(Player3.Play());
            Console.WriteLine(Player3.Play());
            Console.WriteLine(Player3.GetTiltandEnergy(true));
            Player3.Eat("Пицца");
            Player3.Entertainment("Пить пиво");
            Player3.Sleep(2);
            Console.WriteLine(Player3.GetTiltandEnergy(false));
        }
    }
}
