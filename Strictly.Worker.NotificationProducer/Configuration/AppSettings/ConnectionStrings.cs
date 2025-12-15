using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Worker.NotificationProducer.Configuration.AppSettings
{
    public class ConnectionStrings
    {
        [Required]
        public string? Redis { get; set; }
    }

}
