﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dealership.Data.Models
{
    public class Extra : Entity
    {
        private ICollection<CarsExtras> _carsExtras;

        public Extra()
        {
            this._carsExtras = new HashSet<CarsExtras>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<CarsExtras> CarsExtras
        {
            get { return _carsExtras; }
            set
            {
                _carsExtras = value;
            }
        }
    }
}
