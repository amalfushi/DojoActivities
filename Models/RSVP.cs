using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DojoActivities.Models
{
    public class RSVP: BaseEntity
    {
        [Key]
        public int RSVPId { get; set;}
        public int ActivityId { get; set; }
        public Activity Activity { get; set;}
        public int AttendeeId { get; set; }
        public User Attendee { get; set; }
    }

    //     public class ViewRSVP
    // {
    //     // private Regex reg;

    //     [Required(ErrorMessage="First Name is required")]
    //     [Display(Name = "First Name")]
    //     [MinLength(2, ErrorMessage="Names must be at least 2 characters")]
    //     public string FirstName { get; set; }
    // }
}