﻿using JobseekBerca.Models;

namespace JobseekBerca.ViewModels
{
    public class ProfileVM
    {
        public class CreateVM
        {
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? userId { get; set; }
        }
        public class GetVM
        {
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }

        }
        public class UpdateVM
        {
            public string? userId { get; set; }
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }

        }
        public class GetAllVM
        {
            public string? fullName { get; set; }
            public string? summary { get; set; }
            public Gender? gender { get; set; }
            public string? address { get; set; }
            public DateTime? birthDate { get; set; }
            public string? userId { get; set; }
        }
    }
}