using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class UserRequest
    {
        private UserRequest() { }
        public UserRequest(string username, DateTime timestamp)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("", nameof(username));
            }

            if(timestamp == null)
            {
                throw new ArgumentNullException("", nameof(timestamp));
            }

            Key = new Guid();
            Username = username;
            Timestamp = timestamp;
        }

        [Key]
        public Guid Key { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
