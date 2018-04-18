﻿namespace HealthCenter.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<HealthCenter.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<HealthCenter.Domain.UserType> UserTypes { get; set; }
    }
}