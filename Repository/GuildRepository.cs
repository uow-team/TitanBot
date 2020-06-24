using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}
