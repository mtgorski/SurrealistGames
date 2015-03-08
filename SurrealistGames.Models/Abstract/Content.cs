using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Abstract
{
    public abstract class Content
    {
        public abstract string Value { get; set; }

        public abstract int Id { get; set; }

        public int? ApprovingUserId { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? RemovingUserId { get; set; }
        public DateTime? RemovedOn { get; set; }

        public virtual bool IsApproved
        {
            get
            {
                return ApprovingUserId.HasValue || ApprovedOn.HasValue;
            }
        }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
