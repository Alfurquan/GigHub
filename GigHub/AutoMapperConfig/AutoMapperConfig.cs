﻿using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.AutoMapperConfig
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            MapperWrapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Genre, GenreDto>();
                cfg.CreateMap<Notification, NotificationDto>();

            });

            MapperWrapper.AssertConfigurationIsValid();
        }
    }
}