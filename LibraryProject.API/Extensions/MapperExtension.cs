﻿using AutoMapper;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Genres;
using LibraryProject.Domain.Entities;

namespace LibraryProject.API.Extensions
{
    public static class MapperExtension
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreDto>();
                //cfg.CreateMap<List<Genre>, List<GenreDto>>();
                cfg.CreateMap<Book, BookDto>().ForMember(e => e.Genres, e => e.MapFrom(t => t.BookGenres.Select(genre => genre.Genre)));
                cfg.CreateMap<Book, BookDetailsDto>().ForMember(e => e.Genres, e => e.MapFrom(t => t.BookGenres.Select(genre => genre.Genre)));
            });

            configuration.AssertConfigurationIsValid();

            services.AddTransient(_ => configuration.CreateMapper());
        }
    }
}