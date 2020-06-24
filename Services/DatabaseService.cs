using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanBot.Models;
using TitanBot.Repository;

namespace TitanBotX.Services
{
    public class DatabaseService
    {
        private GuildRepository GuildRepository { get; set; }
        public DatabaseService()
        {
            GuildRepository = GuildRepository == null ? new GuildRepository() : GuildRepository;
        }

        public string AddMember(string name, string discordId, int points = 0)
        {
            Member member = new Member()
            {
                UserName = name,
                DiscordId = discordId,
                Points = points
            };

            var result = GuildRepository.AddMember(member);

            return result ? "Successfully added member!" : "Failed to add member";
        }

        public List<Member> GetAllMembers()
        {
            var members = GuildRepository.GetAllMembers();

            return members;
        }

        public string AddRule(string text)
        {
            var result = GuildRepository.AddRule(text);

            return result ? "Successfully added rule!" : "Failed to add rule";
        }

        public List<Rule> GetAllRules()
        {
            var rules = GuildRepository.GetAllRules();

            return rules;
        }

        public void EditRule(int number, string text)
        {
            Rule rule = new Rule()
            {
                Number = number,
                Text = text
            };

            GuildRepository.EditRule(rule);
        }
    }
}
