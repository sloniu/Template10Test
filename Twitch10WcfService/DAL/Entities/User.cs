using System.Collections.Generic;

namespace Twitch10WcfService.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<UserHasBuild> UserHasBuilds { get; set; }
    }
}