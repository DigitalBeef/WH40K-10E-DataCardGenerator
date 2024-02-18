// See https://aka.ms/new-console-template for more information
using  Warhammer40KDatacardGenerator;
using System;
using System.Windows.Forms;
internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        void Print(string message) //alias for brevity 
        {
            Console.WriteLine(message);
        }

        Datacard testCard = new Datacard();

        ModelStats testModel = new ModelStats();
        testModel.movement = "6\"";
        testModel.toughness = "5";
        testModel.armor = "4+";
        testModel.wounds = "3";
        testModel.leadership = "6+";
        testModel.invuln = "0";
        testCard.SetModelStats(testModel);

        Weapon testWeapon = new Weapon();
        testWeapon.name = "Astartes Chainsword";
        testWeapon.isMelee = true;
        testWeapon.attacks = "4";
        testWeapon.skill = "3+";
        testWeapon.strength = "4";
        testWeapon.ap = "-1";
        testWeapon.damage = "1";

        testCard.AddWeapon(testWeapon);

        testWeapon.name = "Bolt Pistol";
        testWeapon.isMelee = false;
        testWeapon.range = "12\"";
        testWeapon.attacks = "1";
        testWeapon.skill = "3+";
        testWeapon.strength = "4";
        testWeapon.ap = "0";
        testWeapon.damage = "1";
        testCard.AddWeapon(testWeapon);

        testCard.AddAbility("Oath of Moment");
        testCard.AddAbility("Berserk Charge");

        Print(testCard.GetStringForm());
        

        string? command = "";
        do
        {
            Print("Waiting for command");
            command = Console.ReadLine();
            if (command?.ToLower() == "generate")
            {

            }
        } while (command?.ToLower() != "quit");

        void GenerateAndCopyDataCard()
        {

        }
    }
}

