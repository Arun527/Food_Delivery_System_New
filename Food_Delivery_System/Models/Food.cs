﻿using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Delivery.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        public string? FoodName { get; set; }

        public int Price { get; set; }

        public string? ImageId { get; set; }

        public  int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        public string? Type { get; set; }

        [NotMapped]
        public IFormFile? CoverPhoto { get; set; }

    }

  


}