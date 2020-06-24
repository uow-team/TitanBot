using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TitanBot.Models;

namespace TitanBot.Repository
{
    public class GuildRepository
    {
        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (var db = new TitanDBContext())
            {
                var dbMembers = db.Members;
                
                foreach(var member in dbMembers)
                {
                    members.Add(new Member
                    {
                        UserName = member.UserName,
                        Points = member.Points,
                        DiscordId = member.DiscordId
                    });
                }
            }

            return members;
        }

        public bool AddMember(Member member)
        {
            int result;
            using (var db = new TitanDBContext())
            {
                db.Members.Add(member);
                result = db.SaveChanges();
            }

            return result > 0;
        }

        public bool AddRule(string text)
        {
            int result;
            using (var db = new TitanDBContext())
            {
                var count = db.Rules.Count();
                db.Rules.Add(new Rule { Text = text, Number = db.Rules.Count() + 1});
                result = db.SaveChanges();
            }

            return result > 0;
        }

        public List<Rule> GetAllRules()
        {
            List<Rule> rules = new List<Rule>();
            using (var db = new TitanDBContext())
            {
                var dbRules = db.Rules;

                foreach (var rule in dbRules)
                {
                    rules.Add(new Rule
                    {
                        Number = rule.Number,
                        Text = rule.Text
                    });
                }
            }

            return rules;
        }

        public void EditRule(Rule rule)
        {
            using (var db = new TitanDBContext())
            {
                db.Rules.SingleOrDefaultAsync<Rule>(x => x.Number == rule.Number).Result.Text = rule.Text;
                db.SaveChanges();
            }
        }
    }
}
