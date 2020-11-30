﻿using Server.Common.Data;
using System.Collections;
using System.Collections.Generic;

namespace Server.Ghost.Characters
{
	public class CharacterSkills : IEnumerable<Skill>
	{
		public Character Parent { get; private set; }

		private List<Skill> Skills { get; set; }

		public CharacterSkills(Character parent)
			: base()
		{
			this.Parent = parent;

			this.Skills = new List<Skill>();
		}

		public void Load()
		{
			foreach (dynamic datum in new Datums("Skills").Populate("cid = '{0}'", this.Parent.ID))
			{
				this.Add(new Skill(datum));
			}
		}

		public void Save()
		{
			foreach (Skill skill in this)
			{
				skill.Save();
			}
		}

		public void Delete()
		{
			foreach (Skill skill in this)
			{
				skill.Delete();
			}
		}

		public void Add(Skill skill)
		{
			skill.Parent = this;
			this.Skills.Add(skill);
		}

		public List<Skill> getSkills()
		{
			return this.Skills;
		}

		public byte GetNextFreeSlot(byte type)
		{
			for (byte i = 0; i < 10; i++)
			{
				if (this[type, i] == null)
				{
					return i;
				}
			}

			throw new SkillFullException();
		}

		public Skill this[byte Type, byte Slot]
		{
			get
			{
				foreach (Skill Skill in this)
				{
					if (Skill.Type == Type && Skill.Slot == Slot)
					{
						return Skill;
					}
				}

				return null;
			}
		}

		public IEnumerator<Skill> GetEnumerator()
		{
			return this.Skills.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.Skills).GetEnumerator();
		}
	}
}