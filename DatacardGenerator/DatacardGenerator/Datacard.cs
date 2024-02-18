using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warhammer40KDatacardGenerator
{ 
		struct Model
		{
			public string 
				movement,
				toughness, 
				armor, 
				wounds, 
				leadership, 
				objectiveControl,
				invuln;
		}
		struct Weapon 
		{
			public bool isMelee;

			public string
				name,
				range,
				attacks,
				skill,
				strength,
				ap,
				damage;

			public List<string> skills;
		}

	 class Datacard
	{
		public List<Weapon> RangedWeapons { get; private set; }
		public List<Weapon> MeleeWeapons { get; private set; }
		public Model Stats { get; private set; }
		public List<String> Abilities { get; private set; }

		public Datacard() 
		{
			RangedWeapons = new List<Weapon>();
			MeleeWeapons = new List<Weapon>();
			Abilities = new List<string>();
		} 

		public void SetModelStats(Model _stats)
		{
			Stats = _stats;
		}

		public void AddWeapon(Weapon _wep)
		{
			if (_wep.isMelee)
				MeleeWeapons.Add(_wep);
			else
			RangedWeapons.Add(_wep);
		}

		public void AddAbility(string _abl)
		{
			Abilities.Add(_abl);
		}

		public string GetStringForm()
		{
			string retval = ModelStatsAsString(Stats) + "\n";
			retval += GetWeaponsString() + "\n";
			if(Abilities.Count > 0)
				retval += GetAbilitiesString();
			return retval;
		}

		public string GetWeaponsString()
		{
			string retval = "";
			if(RangedWeapons.Count > 0)
			{
				retval += "\n[e85545]Ranged weapons[-]\n";
				foreach (var item in RangedWeapons)
					retval += WeaponStatsAsString(item);
			}
			if(MeleeWeapons.Count >0)
			{
				retval += "\n[e85545]Melee weapons[-]\n";
				foreach (var item in MeleeWeapons)
					retval += WeaponStatsAsString(item);
			}
			return retval;
		}

		public string GetAbilitiesString()
		{
			string retval = "[dc61ed]Abilities[-]\n";
			for (int i = 0; i < Abilities.Count; i++)
			{
				if (i > 0)
					retval += "\n";
				retval += " " + Abilities[i];
			}

			return retval;
		}

		public static string ModelStatsAsString(Model _stats)
		{
			string retval = "";
			retval += "[56f442] M   T   Sv    W    Ld   OC  Inv [-]\n";

			if (_stats.movement.Length == 2)
				_stats.movement = " " + _stats.movement;
			string padding = "";
			for (int i = _stats.movement.Length; i <= 5; i++)
				padding += " ";

			retval +=
				_stats.movement + padding +
				_stats.toughness + "   " +
				_stats.armor + "    " +
				_stats.wounds + "      " +
				_stats.leadership + "   " +
				_stats.objectiveControl + "     " +
				_stats.invuln;

			return retval;
		}

		public static string WeaponStatsAsString(Weapon _stats)
		{
			string retval = "";
			string skillLabel;

			if (_stats.isMelee)
				skillLabel = " WS:";
			else
				skillLabel = " BS:";

			retval += " [c6c930]" + _stats.name + "[-]\n";
			retval += "  ";
			if (_stats.isMelee == false)
				retval += _stats.range + " ";

			retval +=
		   "A:" + _stats.attacks +
		   skillLabel + _stats.skill +
		   " S:" + _stats.strength +
		   " AP:" + _stats.ap +
		   " D:" + _stats.damage + "\n";

			if (_stats.skills?.Count > 0)
			{
				retval += "   [7bc596][";
				for (int i = 0; i < _stats.skills.Count; i++)
				{
					if (i > 0)
						retval += ", ";

					retval += _stats.skills[i]; 
				}
				retval += "][-]\n";
			}
			
			return retval;
		}

		public void Generate()
		{
			GenerateModelStats();
			GenerateRangedWeapons();
			GenerateMeleeWeapons();
			GenerateAbilities();
		}

		private void GenerateModelStats()
		{
			Model newStats = new Model();

			newStats.movement				= ConsoleTools.GetInput("Movement?") + "\"";
			newStats.toughness				= ConsoleTools.GetInput("Toughness?");
			newStats.armor                    = ConsoleTools.GetInput("Armor save?") + "+";
			newStats.wounds                 = ConsoleTools.GetInput("Wounds?");
			newStats.leadership				= ConsoleTools.GetInput("Leadership?");
			newStats.objectiveControl		= ConsoleTools.GetInput("Objective control?");
			newStats.invuln					= ConsoleTools.GetInput("Invuln save? (0 if none)");
			if (newStats.invuln != "0")
				newStats.invuln += "+";

		   SetModelStats(newStats);
		}

		private void GenerateRangedWeapons()
		{
			while(ConsoleTools.AskYesNo("Add a ranged weapon? (Y/N)"))
			{
				Console.WriteLine("-- Making a new ranged wepaon --");
				RangedWeapons.Add(GenerateRangedWeapon());
				Console.WriteLine("\n-- Weapon Finished! --");
			}
		}

		private Weapon GenerateRangedWeapon()
		{
			Weapon newWep = new Weapon();
			newWep.isMelee = false;
			FillOutWeapon(ref newWep, "Ballistic skill?");
            return newWep;
		}

		private void GenerateMeleeWeapons()
		{
            while (ConsoleTools.AskYesNo("Add a melee weapon? (Y/N)"))
            {
                Console.WriteLine("-- Making a new melee wepaon --");
                RangedWeapons.Add(GenerateMeleeWeapon());
                Console.WriteLine("\n-- Weapon Finished! --");
            }
        }
		
		private Weapon GenerateMeleeWeapon()
		{
            Weapon newWep = new Weapon();
            newWep.isMelee = true;
			FillOutWeapon(ref newWep, "Weapon Skill?");
            return newWep;
        }

		void FillOutWeapon(ref Weapon _toFill, string _skillLabel)
		{
            _toFill.name = ConsoleTools.GetInput("Name?");

			if (_toFill.isMelee == false)
				_toFill.range = ConsoleTools.GetInput("Range?") + "\"";
			else
				_toFill.range = "Mel";

            _toFill.attacks = ConsoleTools.GetInput("Attacks?");
            _toFill.skill = ConsoleTools.GetInput(_skillLabel) + "+";
            _toFill.strength = ConsoleTools.GetInput("Strength?");
            _toFill.ap = "-" + ConsoleTools.GetInput("AP?");
            _toFill.damage = ConsoleTools.GetInput("Damage?");

            Console.WriteLine("Skills? (enter a blank line to end)");
			_toFill.skills = new List<string>();
            AddToStringListUntilEmtpyLine(_toFill.skills);
        }

		private void GenerateAbilities()
		{
			Console.WriteLine("Abilities? (Enter a blank line to end)");
			AddToStringListUntilEmtpyLine(Abilities);
		}

		private void AddToStringListUntilEmtpyLine(List<string> _list)
		{
            string input;
            while ((input = Console.ReadLine()) != "")
            {
                _list.Add(input);
            }
        }
	}
}