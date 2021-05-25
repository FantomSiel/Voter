using System;

namespace Voter.Dto.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Sex { get; set; }
        public string HashedPassword { get; set; }
    }
}
