using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NHibernate.Id;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;

namespace WebCheckin.Models
{
    
    public class Reservations
    {
        public virtual int RezId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string ArrivalDate { get; set; }
        public virtual string DeptDate { get; set; }
        public virtual int NoAdult { get; set; }
        public virtual int NoRoom { get; set; }
        public virtual string RezStatus { get; set; }
        public virtual string Country { get; set; }
        //public virtual string Image { get; set; }
        public virtual string ImageSrc { get; set; }
        public virtual string Phone { get; set; }

        public virtual string Email { get; set; }


    }
}