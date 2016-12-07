namespace SpatialDimensionTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RegisteredUsers
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Username")]
        public string LoginName { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public int PermissionLevel { get; set; }
    }
}
