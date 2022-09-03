 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlFreamworkProjectWeb.Database.BaseEntity
{
    public class InharitanceEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? LastDateTime { get; set; }
        public bool DeletionStatus { get; set; }
    }
}
