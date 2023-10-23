using ConsoleRPG.Effects;
using ConsoleRPG.Interfaces;
using Spectre.Console;

namespace ConsoleRPG
{
	abstract class Unit
	{
		public string Name { get; set; }

		public int Level { get; set; }

		public int BaseMaxHealth { get; private set; }
		public int MaxHealth { get; set; }
		public int CurrentHealth { get; set; }

		public int MaxMana { get; set; }
		public int CurrentMana { get; set; }

		public bool IsDead { get; set; }

		public Race MyRace;

		public List<BaseEffect> CurrentBuffs { get; set; }
		public List<BaseEffect> CurrentDebuffs { get; set; }

		public Unit AddComponent(Characteristics component)
		{
			_components.Add(component);
			return this;
		}

		public T GetComponent<T>()
		{
			return (T)_components.OfType<T>().FirstOrDefault();
		}

		private List<Characteristics> _components = new List<Characteristics>();

		public Equipment Equipment = new Equipment();

		public Inventory Inventory = new Inventory();

		public Unit()
		{
			AddComponent(new StrengthCharacteristic { RealStrength = 1, Strength = 1, StrengthPerLevel = 0 });
			AddComponent(new AgilityCharacteristic { RealAgility = 1, Agility = 1, AgilityPerLevel = 0 });
			AddComponent(new IntelligenceCharacteristic { RealIntelligence = 1, Intelligence = 1, IntelligencePerLevel = 0 });
			AddComponent(new ArmorCharacteristic { RealArmor = 0, Armor = 0, ArmorPerLevel = 0 });
			AddComponent(new PhysicalDamageCharacteristic { RealPhysicalDamage = 3, PhysicalDamage = 3, PhysicalDamagePerLevel = 0 });
			AddComponent(new CriticalChanceCharacteristic { CriticalChance = 0, CriticalChancePerLevel = 0 });
			AddComponent(new CriticalDamageCharacteristic { CriticalDamage = 0, CriticalDamagePerLevel = 0 });
			AddComponent(new EvasionCharacteristic { EvasionChance = 0, EvasionPerLevel = 0 });
			AddComponent(new MissCharacteristic { MissChance = 0, MissPerLevel = 0 });
			AddComponent(new LuckCharacteristic { RealLuck = 0, Luck = 0, LuckPerLevel = 0 });
			AddComponent(new MagicAmplificationCharacteristic { Amplification = 0, AmplificationPerLevel = 0 });
			AddComponent(new ExperienceBooster { PercentBoost = 0, PercentBoostPerLevel = 0 });
			AddComponent(new SpikeCharacteristic { SpikeDamage = 0 });
			AddComponent(new VampirismCharacteristic { VampirismPercent = 0, VampirismPercentPerLevel = 0 });
			AddComponent(new HealAmplificationCharacteristic { Amplification = 0, AmplificationPerLevel = 0 });
			AddComponent(new ParryCharacteristic { ParryPercent = 0, ParryPercentPerLevel = 0 });

			AddComponent(new StatusEffectsImmunityCharacteristic { ImmunityEffects = new List<BaseEffect>() });

			CurrentBuffs = new List<BaseEffect>();
			CurrentDebuffs = new List<BaseEffect>();

			Dictionary<DamageTypes, int> elemental = new Dictionary<DamageTypes, int>();
			Dictionary<DamageTypes, int> elemental2 = new Dictionary<DamageTypes, int>();

			foreach (DamageTypes type in new DamageTypesNames().ArrayBasicElementalDamageTypes)
			{
				elemental.Add(type, 0);
				elemental2.Add(type, 0);
			}

			AddComponent(new ElementalResistanceCharacteristic { ElemResistance = elemental });
			AddComponent(new ElementalDamageCharacteristic { ElemDamage = elemental2 });
		}

		public void Resurrection()
		{
			IsDead = false;
			AnsiConsole.MarkupLine(Name + " [lime]возроджён[/]");
			HealMaxHealth();
			HealMaxMana();
		}

		public void Heal(int health, IEntity Entity)
		{
			health = HealWithAmplification(health);

			if (CurrentHealth + health >= MaxHealth)
			{
				HealMaxHealth();
				UpdateHealMessage(health, Entity.GetName());
			}
			else
			{
				if (health > 0)
				{
					CurrentHealth += health;
					UpdateHealMessage(health, Entity.GetName());
				}
				else if (health < 0)
				{
					DealDamage(this, Math.Abs(health), DamageTypes.Holy, Entity);
				}
			}
		}

		public void HealWithoutMessage(int health)
		{
			health = HealWithAmplification(health);

			if (CurrentHealth + health > MaxHealth)
			{
				HealMaxHealth();
			}
			else
			{
				CurrentHealth += health;
				if (CurrentHealth <= 0)
				{
					Death();
				}
			}
		}

		public void HealMaxHealth()
		{
			CurrentHealth = MaxHealth;
		}

		public void HealMaxMana()
		{
			CurrentMana = MaxMana;
		}

		public int CurrentHealthPercent()
		{
			int coefficient = MaxHealth / CurrentHealth;
			return 100 / coefficient;
		}

		public void ShowMessage(string message)
		{
			AnsiConsole.MarkupLine(message);
		}

		public void TakeDamageMessage(int damage, DamageTypes damageType)
		{
			var names = new DamageTypesNames().Names;
			ShowMessage($"{Name} получил {damage} ({names[damageType]}) урона, текущее здоровье [lime]{CurrentHealth}[/]");
		}

		public void UpdateTakeDamageMessage(int damage, DamageTypes damageType, string damageDealer)
		{
			var names = new DamageTypesNames().Names;
			ShowMessage($"{Name} получил {damage} ({names[damageType]}) урона от ({damageDealer})," +
				$" текущее здоровье [lime]{CurrentHealth}[/]");
		}

		public void HealMessage(int heal)
		{
			ShowMessage($"{Name} исцелился на {heal}, текущее здоровье [lime]{CurrentHealth}[/]");
		}

		public void UpdateHealMessage(int heal, string healDealer)
		{
			ShowMessage($"{Name} исцелился на {heal} от ({healDealer}), текущее здоровье [lime]{CurrentHealth}[/]");
		}

		public void DeathMessage(int damage, DamageTypes damageType)
		{
			var names = new DamageTypesNames().Names;
			ShowMessage($"{Name} получил {damage} ({names[damageType]}) урона и [red]умер[/]");
		}

		public void UpdateDeathMessage(int damage, DamageTypes damageType, string damageDealer)
		{
			var names = new DamageTypesNames().Names;
			ShowMessage($"{Name} получил {damage} ({names[damageType]}) урона от ({damageDealer}) и [red]умер[/]");
		}

		public void DealDamage(Unit damageDealerUnit, int damage, DamageTypes damageType, params IEntity[] Entities)
		{
			BeforeAttackBehaviour(this, damageDealerUnit, ref damage, damageType);

			foreach (var entity in Entities)
				damageDealerUnit.TakeDamage(this, damage, damageType, entity);

			damageDealerUnit.CheckApplyingEffects(Entities);

			if (damageDealerUnit != this)
			{
				//damageDealerUnit.CheckApplyingEffects(Entities);
			}

			AfterAttackBehaviour(this, damage, damageType);
		}

		public void TakeDamage(Unit damageDealerUnit, int takedDamage, DamageTypes damageType, IEntity Entity)
		{
			if (!Entity.GetType().GetInterfaces().Contains(typeof(IStatusEffectEntity)))
				BeforeTakeDamageBehaviour(damageDealerUnit, ref takedDamage, damageType);

			if (takedDamage > 0)
			{
				CurrentHealth -= takedDamage;

				if (IsDie())
				{
					Death();
					//DeathMessage(takedDamage, damageType);
					UpdateDeathMessage(takedDamage, damageType, Entity.GetName());
				}
				else
				{
					//TakeDamageMessage(takedDamage, damageType);
					UpdateTakeDamageMessage(takedDamage, damageType, Entity.GetName());
					if (!Entity.GetType().GetInterfaces().Contains(typeof(IStatusEffectEntity)))
						AfterTakeDamageBehaviour(damageDealerUnit, damageType);
				}
			}
			else if (takedDamage < 0)
			{
				Heal(Math.Abs(takedDamage), Entity);
			}
		}

		public void BeforeAttackBehaviour(Unit unit, Unit damageDealer, ref int damage, DamageTypes damageType)
		{
			if (damageType != DamageTypes.Physical)
			{
				damage += (int)(damage * (GetComponent<MagicAmplificationCharacteristic>().Amplification) / 100);
				damage += GetElementalDamage(damageType);
			}

		}

		public void AfterAttackBehaviour(Unit unit, int damage, DamageTypes damageType)
		{
			if (damage > 0 && damageType == DamageTypes.Physical)
				TakeVampirism();
		}

		public void BeforeTakeDamageBehaviour(Unit damageDealer, ref int takedDamage, DamageTypes damageType)
		{
			if (damageType == DamageTypes.Physical && damageDealer.CheckMiss())
			{
				takedDamage = 0;
				return;
			}

			if (damageType == DamageTypes.Physical && CheckEvasion())
			{
				takedDamage = 0;
				return;
			}

			if (damageType == DamageTypes.Physical && CheckParry())
			{
				takedDamage = 0;
				return;
			}

			if (damageType == DamageTypes.Physical && IsCrit())
			{
				takedDamage = CalcPhysicalCritical(takedDamage);
			}

			takedDamage = CheckResistance(takedDamage, damageType);
		}

		public void AfterTakeDamageBehaviour(Unit damageDealer, DamageTypes damageType)
		{
			if (CurrentHealth <= 0)
			{
				Death();
			}
			else
			{
				if (damageType == DamageTypes.Physical && !IsDead)
				{
					TakeSpikeDamage(damageDealer);
				}
			}
		}

		public void EffectsUpdate()
		{

			foreach (var buff in CurrentBuffs.ToList())
			{
				if (buff.CurrentDuration > 0)
				{
					buff.ApplyEffect(this);
				}
				else
				{
					RemoveBuff(buff);
				}
			}

			foreach (var debuff in CurrentDebuffs.ToList())
			{
				if (debuff.CurrentDuration > 0)
				{
					debuff.ApplyEffect(this);
				}
				else
				{
					RemoveDebuff(debuff);
				}
			}
		}

		public void CheckApplyingEffects(params IEntity[] Entities)
		{
			//var items = unit.Equipment.GetAllEquipmentWithStatusEffects();

			Type interfaceType = typeof(IAppyStatusEffectEntity);
			List<IAppyStatusEffectEntity> items = new List<IAppyStatusEffectEntity>();
			foreach (var equip in Entities)
			{
				if (equip.GetType().GetInterfaces().Contains(interfaceType))
				{
					items.Add((IAppyStatusEffectEntity)equip);
				}
			}

			foreach (var item in items)
			{
				var effects = item.GetEffects();

				foreach (var effect in effects)
				{
					if (new Random().Next(1, 101) < effect.Value)
					{
						switch (effect.Key.EffectType)
						{
							case EffectType.Buff:
								if (CurrentBuffs.Contains(effect.Key))
								{
									//var buff = CurrentBuffs.FirstOrDefault(x => x == effect.Key);
									//buff.CurrentDuration += effect.Key.Duration;
									//ShowMessage($"Эффект {effect.Key.Name} продлён на {Name}");
								}
								else if (CheckImmunity(effect.Key))
								{
									ShowMessage($"{Name} имеет иммунитет к эффекту {effect.Key.Name}");
								}
								else if (!IsDie())
								{
									AddBuff(effect.Key.Clone());
								}
								break;
							case EffectType.Debuff:
								if (CurrentDebuffs.Contains(effect.Key))
								{
									//var debuff = CurrentDebuffs.FirstOrDefault(x => x == effect.Key);
									//debuff.CurrentDuration += effect.Key.Duration;
									//ShowMessage($"Эффект {effect.Key.Name} продлён на {Name}");
								}
								else if (CheckImmunity(effect.Key))
								{
									ShowMessage($"{Name} имеет иммунитет к эффекту {effect.Key.Name}");
								}
								else if (!IsDie())
								{
									AddDebuff(effect.Key.Clone());
								}
								break;
							case EffectType.Stun:
								if (CurrentBuffs.Contains(effect.Key))
								{
									//var buff = CurrentBuffs.FirstOrDefault(x => x == effect.Key);
									//buff.CurrentDuration += effect.Key.Duration;
									//ShowMessage($"Эффект {effect.Key.Name} продлён на {Name}");
								}
								else if (CheckImmunity(effect.Key))
								{
									ShowMessage($"{Name} имеет иммунитет к эффекту {effect.Key.Name}");
								}
								else if (!IsDie())
								{
									AddBuff(effect.Key.Clone());
								}
								break;
							default:
								AnsiConsole.MarkupLine("[red]Ошибка при наложении эффекта[/]");
								break;
						}
					}
				}
			}
		}

		public void AddBuff(BaseEffect effect)
		{
			ShowMessage($"{Name} получил эффект {effect.Name}");
			CurrentBuffs.Add(effect);
		}

		public void RemoveBuff(BaseEffect effect)
		{
			CurrentBuffs.Remove(effect);
		}

		public void AddDebuff(BaseEffect effect)
		{
			ShowMessage($"{Name} получил эффект {effect.Name}");
			CurrentDebuffs.Add(effect);
		}

		public void RemoveDebuff(BaseEffect effect)
		{
			CurrentDebuffs.Remove(effect);
		}

		public void AddImmunity(BaseEffect effect)
		{
			GetImmunities().ImmunityEffects.Add(effect);
		}

		public void RemoveImmunity(BaseEffect effect)
		{
			GetImmunities().ImmunityEffects.Remove(effect);;
		}

		public StatusEffectsImmunityCharacteristic GetImmunities()
		{
			return GetComponent<StatusEffectsImmunityCharacteristic>();
		}

		public bool IsStunned()
		{
			foreach (var buff in CurrentBuffs)
			{
				if (buff.EffectType == EffectType.Stun)
					return true;
			}

			foreach (var debuff in CurrentDebuffs)
			{
				if (debuff.EffectType == EffectType.Stun)
					return true;
			}

			return false;
		}

		public bool CheckMiss()
		{
			int miss = GetMissChance();

			if (new Random().Next(1, 101) < miss)
			{
				AnsiConsole.MarkupLine($"{Name} [bold]промахнулся[/] атакой!");
				return true;
			}

			return false;
		}

		public bool CheckEvasion()
		{
			int miss = GetEvasionChance();

			if (new Random().Next(1, 101) < miss)
			{
				AnsiConsole.MarkupLine($"{Name} [bold]уклонился[/] от атаки!");
				return true;
			}

			return false;
		}

		public bool CheckParry()
		{
			int parry = GetParryChance();

			if (new Random().Next(1, 101) < parry)
			{
				AnsiConsole.MarkupLine($"{Name} [bold]парировал[/] атаку!");
				return true;
			}

			return false;
		}

		public int CheckResistance(int takedDamage, DamageTypes damageType)
		{
			Dictionary<DamageTypes, int> resistance = GetExistableTypeResistance();
			if (resistance.ContainsKey(damageType))
			{
				if (resistance[damageType] != 0)
				{
					double tempBlockedDamage = takedDamage * (resistance[damageType] / (double)100);
					takedDamage -= (int)tempBlockedDamage;
				}
			}

			return takedDamage;
		}

		public bool CheckImmunity(BaseEffect effect)
		{
			foreach (var item in GetImmunities().ImmunityEffects)
			{
				if (item.GetType() == item.GetType())
					return true;
			}

			return false;
		}

		public void Attack(Unit Unit, IEntity Entity)
		{
			Dictionary<DamageTypes, int> damage = GetExistableTypeDamage();
			foreach (DamageTypes type in damage.Keys)
			{
				DealDamage(Unit, damage[type], type, Entity);
			}
		}

		public void TakeSpikeDamage(Unit damageDealer)
		{
			int spikeDamage = SpikeDamage();

			if (spikeDamage > 0)
			{
				DealDamage(damageDealer, spikeDamage, DamageTypes.Physical, GetComponent<SpikeCharacteristic>());
			}
		}

		public void TakeVampirism()
		{
			double vampirism = GetComponent<VampirismCharacteristic>().VampirismPercent;

			int damage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;

			int percentConverter = 100;

			int heal = (int)(damage * (vampirism / percentConverter));

			if (heal > 0)
			{

				Heal(heal, GetComponent<VampirismCharacteristic>());
			}
		}

		public void UpgradeStats()
		{
			BaseStatsUpgrade();
			RaceUpgradeStats();
		}

		public void BaseStatsUpgrade()
		{
			StrengthUpgrade();
			AgilityUpgrade();
			IntelligenceUpdage();
		}

		public void StrengthUpgrade()
		{
			GetComponent<StrengthCharacteristic>().RealStrength += MyRace.GetComponent<StrengthCharacteristic>().StrengthPerLevel;
			int tempRealSTR = (int)GetComponent<StrengthCharacteristic>().RealStrength;
			int tempSTR = GetComponent<StrengthCharacteristic>().Strength;
			int subtracts = tempRealSTR - tempSTR;
			MaxHealth += subtracts * GetComponent<StrengthCharacteristic>().HealthPerStrength;
			GetComponent<StrengthCharacteristic>().Strength = tempRealSTR;
		}

		public void AgilityUpgrade()
		{
			GetComponent<AgilityCharacteristic>().RealAgility += MyRace.GetComponent<AgilityCharacteristic>().AgilityPerLevel;
			int tempRealAGI = (int)GetComponent<AgilityCharacteristic>().RealAgility;
			int tempAGI = GetComponent<AgilityCharacteristic>().Agility;
			int subtracts = tempRealAGI - tempAGI;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += subtracts * GetComponent<AgilityCharacteristic>().DamagePerAgility;
			GetComponent<AgilityCharacteristic>().Agility = tempRealAGI;
		}

		public void IntelligenceUpdage()
		{
			GetComponent<IntelligenceCharacteristic>().RealIntelligence += MyRace.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel;
			int tempRealINT = (int)GetComponent<IntelligenceCharacteristic>().RealIntelligence;
			int tempINT = GetComponent<IntelligenceCharacteristic>().Intelligence;
			int subtracts = tempRealINT - tempINT;
			GetComponent<MagicAmplificationCharacteristic>().Amplification +=
				subtracts * GetComponent<IntelligenceCharacteristic>().MagicAmplificationPerIntelligence;
			GetComponent<IntelligenceCharacteristic>().Intelligence = tempRealINT;
		}

		public void RaceUpgradeStats()
		{
			if (MyRace.GetComponent<ArmorCharacteristic>() != null)
			{
				GetComponent<ArmorCharacteristic>().RealArmor += MyRace.GetComponent<ArmorCharacteristic>().ArmorPerLevel;
				int tempRealArmor = (int)GetComponent<ArmorCharacteristic>().RealArmor;
				int tempArmor = GetComponent<ArmorCharacteristic>().Armor;
				int subtracts = tempRealArmor - tempArmor;
				GetComponent<ArmorCharacteristic>().Armor += subtracts;
			}
			if (MyRace.GetComponent<PhysicalDamageCharacteristic>() != null)
			{
				GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage += MyRace.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamagePerLevel;
				int tempRealDamage = (int)GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage;
				int tempDamage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
				int subtracts = tempRealDamage - tempDamage;
				GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += subtracts;
			}
			if (MyRace.GetComponent<LuckCharacteristic>() != null)
			{
				GetComponent<LuckCharacteristic>().RealLuck += MyRace.GetComponent<LuckCharacteristic>().LuckPerLevel;
				int tempRealLuck = (int)GetComponent<LuckCharacteristic>().RealLuck;
				int tempLuck = GetComponent<LuckCharacteristic>().Luck;
				int subtracts = tempRealLuck - tempLuck;
				GetComponent<LuckCharacteristic>().Luck += (int)GetComponent<LuckCharacteristic>().RealLuck;
			}
			if (MyRace.GetComponent<MissCharacteristic>() != null)
			{
				GetComponent<MissCharacteristic>().MissChance += MyRace.GetComponent<MissCharacteristic>().MissPerLevel;
			}
			if (MyRace.GetComponent<EvasionCharacteristic>() != null)
			{
				GetComponent<EvasionCharacteristic>().EvasionChance += MyRace.GetComponent<EvasionCharacteristic>().EvasionPerLevel;
			}
			if (MyRace.GetComponent<CriticalChanceCharacteristic>() != null)
			{
				GetComponent<CriticalChanceCharacteristic>().CriticalChance += MyRace.GetComponent<CriticalChanceCharacteristic>().CriticalChancePerLevel;
			}
			if (MyRace.GetComponent<CriticalDamageCharacteristic>() != null)
			{
				GetComponent<CriticalDamageCharacteristic>().CriticalDamage += MyRace.GetComponent<CriticalDamageCharacteristic>().CriticalDamagePerLevel;
			}
			if (MyRace.GetComponent<MagicAmplificationCharacteristic>() != null)
			{
				GetComponent<MagicAmplificationCharacteristic>().Amplification += MyRace.GetComponent<MagicAmplificationCharacteristic>().AmplificationPerLevel;
			}
			if (MyRace.GetComponent<ExperienceBooster>() != null)
			{
				GetComponent<ExperienceBooster>().PercentBoost += MyRace.GetComponent<ExperienceBooster>().PercentBoostPerLevel;
			}
			if (MyRace.GetComponent<HealAmplificationCharacteristic>() != null)
			{
				GetComponent<HealAmplificationCharacteristic>().Amplification += MyRace.GetComponent<HealAmplificationCharacteristic>().AmplificationPerLevel;
			}
			if (MyRace.GetComponent<VampirismCharacteristic>() != null)
			{
				GetComponent<VampirismCharacteristic>().VampirismPercent += MyRace.GetComponent<VampirismCharacteristic>().VampirismPercentPerLevel;
			}
		}

		public Dictionary<DamageTypes, int> GetExistableTypeDamage()
		{
			Dictionary<DamageTypes, int> result = new Dictionary<DamageTypes, int>();
			Dictionary<DamageTypes, int> damageTypes = GetElementalDamage();
			result.Add(DamageTypes.Physical, GetPhysicalDamage());



			foreach (DamageTypes type in damageTypes.Keys)
			{
				result.Add(type, damageTypes[type]);
			}

			return result;
		}

		public Dictionary<DamageTypes, int> GetExistableTypeResistance()
		{
			Dictionary<DamageTypes, int> result = GetElementalResistance();

			result.Add(DamageTypes.Physical, GetArmor());

			return result;
		}

		public int GetPhysicalDamage()
		{
			return GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
		}

		public int GetArmor()
		{
			return GetComponent<ArmorCharacteristic>().Armor;
		}

		public Dictionary<DamageTypes, int> GetElementalDamage()
		{
			Dictionary<DamageTypes, int> elementalDamage = new Dictionary<DamageTypes, int>();

			foreach (DamageTypes type in GetComponent<ElementalDamageCharacteristic>().ElemDamage.Keys)
			{
				int damage = GetComponent<ElementalDamageCharacteristic>().ElemDamage[type];
				if (damage > 0)
				{
					elementalDamage.Add(type, damage);
				}
			}

			return elementalDamage;
		}

		public Dictionary<DamageTypes, int> GetTypeElementalDamage(params DamageTypes[] damageTypes)
		{
			Dictionary<DamageTypes, int> elementalDamage = new();

			foreach (DamageTypes damageType in damageTypes)
			{
				int damage = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == damageType).Value;
				if (damage > 0)
				{
					elementalDamage.Add(damageType, damage);
				}
			}

			return elementalDamage;
		}

		public Dictionary<DamageTypes, int> GetElementalResistance()
		{
			Dictionary<DamageTypes, int> elementalResistance = new Dictionary<DamageTypes, int>();

			foreach (DamageTypes type in GetComponent<ElementalResistanceCharacteristic>().ElemResistance.Keys)
			{
				int resistance = GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type];

				elementalResistance.Add(type, resistance);
			}

			return elementalResistance;
		}

		public Dictionary<DamageTypes, int> GetTypeResistance(params DamageTypes[] damageTypes)
		{
			Dictionary<DamageTypes, int> elementalResistance = new();

			foreach (DamageTypes damageType in damageTypes)
			{
				int resistance = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == damageType).Value;
				if (resistance > 0)
				{
					elementalResistance.Add(damageType, resistance);
				}
			}

			return elementalResistance;
		}

		public virtual IDamageDealerEntity[] GetDamageEntities()
		{
			Type interfaceType = typeof(IDamageDealerEntity);
			List<Weapon> items = new List<Weapon>();
			foreach (var equip in Equipment.Equip.Values)
			{
				if (equip.GetType().GetInterfaces().Contains(interfaceType))
				{
					items.Add((Weapon)equip);
				}
			}

			return items.ToArray();
		}

		//public virtual string GetDamageEntities()
		//{
		//    string firstWeapon = Equipment.Equip[EquipmentSlot.LeftHand].Name;
		//    string secondWeapon = Equipment.Equip[EquipmentSlot.RightHand].Name;
		//    string result = "";
		//    if (firstWeapon != "")
		//    {
		//        result += firstWeapon;
		//        if (secondWeapon != "")
		//        {
		//            result += $" и {secondWeapon}";
		//        }
		//    }
		//    else
		//    {
		//        result = "Кулаки";
		//    }

		//    return result;
		//}

		public int GetElementalDamage(DamageTypes type)
		{
			return GetComponent<ElementalDamageCharacteristic>().ElemDamage[type];
		}

		public double GetCriticalChance()
		{
			return GetComponent<CriticalChanceCharacteristic>().CriticalChance;
		}

		public double GetCriticalDamage()
		{
			return GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
		}

		public int GetLuck()
		{
			return GetComponent<LuckCharacteristic>().Luck;
		}

		public int GetMissChance()
		{
			return (int)GetComponent<MissCharacteristic>().MissChance;
		}

		public int GetEvasionChance()
		{
			return (int)GetComponent<EvasionCharacteristic>().EvasionChance;
		}

		public int GetParryChance()
		{
			return (int)GetComponent<ParryCharacteristic>().ParryPercent;
		}

		public bool IsCrit()
		{
			if (GetCriticalChance() >= new Random().Next(1, 101))
			{
				return true;
			}

			return false;
		}

		public double ExperienceBoost()
		{
			return GetComponent<ExperienceBooster>().PercentBoost;
		}

		public double HealAmplification()
		{
			return GetComponent<HealAmplificationCharacteristic>().Amplification;
		}

		public double VampirismPercent()
		{
			return GetComponent<VampirismCharacteristic>().VampirismPercent;
		}

		public int HealWithAmplification(int heal)
		{
			int healWithBoost = heal;
			int percentConvert = 100;
			int healAmplification = (int)(HealAmplification() / percentConvert);

			if (healAmplification > 0)
			{
				healWithBoost += healAmplification;
				AnsiConsole.MarkupLine($"[lime]Исцеление[/] усилено на [bold]{healAmplification}[/]");
			}

			return healWithBoost;
		}

		public int CalcPhysicalCritical(int damage)
		{
			int percentConvert = 100;
			return damage += (int)damage * ((int)GetCriticalDamage() / percentConvert);
		}

		public int SpikeDamage()
		{
			return GetComponent<SpikeCharacteristic>().SpikeDamage;
		}

		public void Death()
		{
			IsDead = true;
			CurrentBuffs.Clear();
			CurrentDebuffs.Clear();
		}

		public bool IsDie()
		{
			return CurrentHealth <= 0;
		}
	}
}
