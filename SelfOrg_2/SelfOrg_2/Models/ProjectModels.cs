using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
namespace SelfOrg_2.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Select> Selects { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("Project")]
    public class Project
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        //[Required]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        //public int CreatorID { get; set; }
        //public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        [ForeignKey("parent")]
        public int? Creator_UserId { get; set; }
        public virtual UserProfile parent { get; set; }
    }

    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        //[Required]
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Score { get; set; }
        public int SelectID { get; set; }
        public bool Status { get; set; }
    }

    [Table("Member")]
    public class Member
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }

    [Table("Select")]
    public class Select
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SelectId { get; set; }
        public int ItemId { get; set; }
        public int MemberId { get; set; }
        //public DateTime SelectDate { get; set; }
    }
}