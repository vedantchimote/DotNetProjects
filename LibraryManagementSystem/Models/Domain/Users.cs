using Microsoft.AspNetCore.SignalR;

namespace Library.Models.Domain
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Mobile_no { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}