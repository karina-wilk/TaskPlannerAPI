using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Shared.DTO
{
    public class DutyDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public DutyDto()
        { }

        public DutyDto(Duty f)
        {
            Id = f.Id;
            Description = f.Description;
            Enabled = f.Enabled;
        }
    }
}
