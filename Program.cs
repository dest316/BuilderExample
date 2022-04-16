using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePattern
{
    public enum CoffeeBeans { Arabika, Rabusta, Liberica }
    public enum Milk { StandartMilk, OilyMilk, SoyMilk, WithoutMilk}
    public enum Sugar { StandartSugar, SynteticSugar, WithoutSugar}
    public enum Syrip { Vanilla, Strawberry, Raspberry, Banana, Chocolate, WithoutSyrip}
    
    
        
    

    public interface ICoffeeBuilder
    {
        void BuildCoffeeBeans(CoffeeBeans coffeeBeans);
        void BuildMilk(Milk milk);
        void BuildSugar(Sugar sugar);
        void BuildWater(bool withWater);
        void BuildFoam(bool withFoam);
        void BuildSyrip(Syrip syrip);
        void BuildCinnamon(bool withCinnamon);
        void BuildVolume(int volume);
    }
    public class Coffee
    {
        public CoffeeBeans coffeeBeans { get; set; }
        public Milk milk { get; set; }
        public Sugar sugar { get; set; }
        public bool withWater { get; set; }
        public bool withFoam { get; set; }
        public Syrip syrip { get; set; }
        public bool withCinnamon { get; set; }
        private int _volume;
        //Можно присвоить любое значение от 50 до 700. Даже не обязательно ровное, ведь стакан можно заполнить на четверть, или на 38%, как угодно
        //лишь бы intом было
        public int volume    
        {
            get { return _volume; }
            set 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (value < 50)
                {
                    _volume = 50;
                    Console.WriteLine(DateTime.Now.ToString() + " Предупреждение! Использован объем 50мл, так как меньше использовать запрещено");
                }
                else if (value > 700)
                {
                    _volume = 700;
                    Console.WriteLine(DateTime.Now.ToString() + " Предупреждение! Использован объем 700мл, так как стаканчиков поменьше у нас нет. И вообще, подумайте " +
                        "о своем сердце, оно вам еще пригодится");
                } 
                else { _volume = value; }
                Console.ResetColor(); 
            }
        }
        public void Drink()
        {
            Console.WriteLine("Кофе выпито: +10 к бодрости, -5 к кошельку");
        }
        public void PrintInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Состав:\n\tЗерна: {coffeeBeans}\n\tМолоко: {milk}\n\tСахар: {sugar}\n\tС водой: {withWater}\n\tС пенкой: {withFoam}\n\tС корицей: {withCinnamon}" +
                $"\n\tСироп: {syrip}\n\tВ стаканчике объемом {volume} миллилитров");
        }

    }
    /// <summary>
    /// Класс, отвечающий за создание экземпляров класса Coffee
    /// </summary>
    public class CoffeeBuilder: ICoffeeBuilder
    {
        public CoffeeBuilder()
        {
            Reset();
        }
        private Coffee coffee = new Coffee();
        public void BuildCoffeeBeans(CoffeeBeans coffeeBeans) { coffee.coffeeBeans = coffeeBeans; }
        public void BuildMilk(Milk milk) { coffee.milk = milk;}
        public void BuildCinnamon(bool withCinnamon) { coffee.withCinnamon = withCinnamon; }
        public void BuildFoam(bool withFoam) { coffee.withFoam = withFoam; }
        public void BuildSugar(Sugar sugar) { coffee.sugar = sugar; }
        public void BuildSyrip(Syrip syrip) { coffee.syrip = syrip; }
        public void BuildVolume(int volume) { coffee.volume = volume; }
        public void BuildWater(bool withWater) { coffee.withWater = withWater; }
        public void BuildAmericano()
        {
            Reset();
        }
        public void BuildCappuccino()
        {
            Reset();
            coffee.milk = Milk.StandartMilk;
            coffee.withFoam = true;
            coffee.withWater = false;
        }
        public void BuildEspresso()
        {
            Reset();
            coffee.withWater = false;
        }
        public void BuildLatte()
        {
            BuildCappuccino(); //Между этими напитками разница только в пропорциях, а в этой программе указать их нельзя:(
        }
        /// <summary>
        /// Сбрасывает парамертры возвращаемого объекта в дефолтные значения. По умолчанию готовится американо
        /// </summary>
        private void Reset()
        {
            coffee.coffeeBeans = CoffeeBeans.Arabika;
            coffee.milk = Milk.WithoutMilk;
            coffee.sugar = Sugar.WithoutSugar;
            coffee.withWater = true;
            coffee.withFoam = false;
            coffee.syrip = Syrip.WithoutSyrip;
            coffee.withCinnamon = false;
            coffee.volume = 200;
        }
        /// <summary>
        /// Возвращает созданный билдером экземпляр кофе. Приятного аппетита!
        /// </summary>
        /// <param name="withReseting"> Если true, производит сброс к дефолтным значениям, см. метод Reset(). Если false, то не производит. По умолчанию true
        /// Значение false будет разумно передавать, если необходимо создать несколько одинковых напитков, в остальных случаях лучше выполнять сброс, чтобы не запутаться</param>
        /// <returns>Возвращает экземпляр класса coffee с заранее заданными или частично/полностью дефолтными параметрами</returns>
        public Coffee Create(bool withReseting = true)
        {
            if (!withReseting) { return coffee; }
            var tmpCoffee = coffee;
            Reset();
            return tmpCoffee;
            
        }

        
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CoffeeBuilder();
            cb.BuildCoffeeBeans(CoffeeBeans.Arabika);
            cb.BuildMilk(Milk.StandartMilk);
            cb.BuildSugar(Sugar.StandartSugar);
            cb.BuildFoam(true);
            cb.BuildSyrip(Syrip.WithoutSyrip);
            cb.BuildWater(false);
            cb.BuildVolume(300);
            var firstCoffee = cb.Create();
            firstCoffee.Drink();
            cb.BuildLatte();
            cb.BuildSyrip(Syrip.Chocolate);
            cb.BuildSugar(Sugar.StandartSugar);
            firstCoffee.PrintInfo();    
        }
    }
}
