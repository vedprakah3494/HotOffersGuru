using System;

namespace hotoffersguru.Entity.Models
{
    public abstract class Entity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}