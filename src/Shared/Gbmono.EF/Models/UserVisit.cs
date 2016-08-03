using System;

namespace Gbmono.EF.Models
{
    public class UserVisit
    {
        public int UserVisitId { get; set; }

        public string UserId { get; set; }

        public int KeyId { get; set; }

        public short VisitTypeId { get; set; }

        public DateTime Created { get; set; }
    }
}
