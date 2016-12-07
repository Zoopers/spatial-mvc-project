namespace SpatialDimensionTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        //Set all Employee Model properties according to the relevant columns in the database
        [Key]
        [DisplayName("Employee ID")] //DisplayName sets the name that will be shown whenever this column is displayed in a view
        public int EmpID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        public string EmpName { get; set; }

        [StringLength(50)]
        [DisplayName("Job title")]
        public string Job { get; set; }

        [DisplayName("Reports to:")]
        public int? Superior { get; set; } // the '?' indicates that the value can be set to null

    }
}
