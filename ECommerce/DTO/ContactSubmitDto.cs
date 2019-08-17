using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DTO
{
    public class ContactSubmitDto
    {
        public string NameSurname { get; set; }
        public string EMail { get; set; }
        public string Message { get; set; }
    }
}
