// See https://aka.ms/new-console-template for more information
using  Warhammer40KDatacardGenerator;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        string command = "";
        //MakeTestCard();
        do
        {
            command = ConsoleTools.GetInput("Command? (gen/quit)");

            if (command?.ToLower() == "gen")
				GenerateNewDatacard();

		} while (command?.ToLower() != "quit");

		void GenerateNewDatacard()
		{
			Datacard newCard = new Datacard();
			newCard.Generate();

			Console.WriteLine("\n ===== Datacard generated and copied to clipboard! ===== ");
			PrintAndCopyCard(newCard);
		}

        void PrintAndCopyCard(Datacard _toPrint)
        {
			string cardString = _toPrint.GetStringForm();
			Console.WriteLine(cardString);
			Clipboard.SetText(cardString);
			Console.WriteLine("\n\n");
		}

        void MakeTestCard()
        {
            Datacard testCard = new Datacard();

            Model testModel = new Model();
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
            testCard.AddWeapon(testWeapon);

            testWeapon.name = "Bolt Pistol";
            testWeapon.isMelee = false;
            testWeapon.range = "12\"";
            testWeapon.attacks = "1";
            testWeapon.skill = "3+";
            testWeapon.strength = "4";
            testWeapon.ap = "0";
            testWeapon.damage = "1";
            testWeapon.skills = new List<string>();
            testWeapon.skills.Add("Pistol");
            testCard.AddWeapon(testWeapon);
            testCard.AddWeapon(testWeapon);

            testCard.AddAbility("Oath of Moment");
            testCard.AddAbility("Berserk Charge");

            Console.WriteLine(testCard.GetStringForm());
            Clipboard.SetText(testCard.GetStringForm());
        }
	}
}

