using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitch10WcfService.DAL.Entities
{
    public class UserHasBuild
    {
        //public int UserHasBuildId { get; set; }

        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BuildId { get; set; }

        public virtual User User { get; set; }
        public virtual Build Build { get; set; }
    }
}