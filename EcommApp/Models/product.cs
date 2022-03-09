//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EcommApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.cart_items = new HashSet<cart_items>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int prod_id { get; set; }
        public string prod_name { get; set; }
        public int stock { get; set; }
        public decimal price { get; set; }
        public string prod_image { get; set; }
        public string prod_desc { get; set; }
        public string product_cat { get; set; }

        //for product image
        public HttpPostedFileBase imgFile { get; set; }
        //

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_items> cart_items { get; set; }
        public virtual product products1 { get; set; }
        public virtual product product1 { get; set; }
    }

    public enum Category
    {
        Shirt,
        Dress,
        Shorts,
        Jeans,
        Swimwear
    }
}