﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTwoJuetengApp.Models.CustomerRelated
{
    public class CustomerReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public string CellphoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
    }
}
