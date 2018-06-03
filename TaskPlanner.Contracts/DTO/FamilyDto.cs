using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Data.Models;

namespace TaskPlanner.Shared.DTO
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public FamilyDto()
        { }

        public FamilyDto(Family f)
        {
            Id = f.Id;
            Name = f.Name;
            IsActive = f.IsActive;
        }
    }
}
