using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Configuration.AppSettings
{
    public class EmailSettings
    {
        [Required]
        public string? Host { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Passord { get; set; }
        [Required]
        public string? From { get; set; }
        [Required]
        public EmailTemplate? Templates { get; set; }
    }

    public class EmailTemplate
    {
        [Required]
        public string? BasePath { get; set; }

        [Required]
        public string? Reminder { get; set; }
    }

}
