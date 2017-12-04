﻿using System;
using NH4CookbookHelpers.Queries;
using NH4CookbookHelpers.Queries.Model;
using NHibernate;
using NH4CookbookHelpers;

namespace QueryRecipes.CollectionFilters
{
    public class Recipe : QueryRecipe
    {
        protected override void Run(ISession session)
        {
            var movie = session.Get<Movie>(1);
            var actorFilter = session
            .CreateFilter(movie.Actors,
            "WHERE Actor=:actor");
            actorFilter.SetString("actor",
            "Harrison Ford");
            var actors = actorFilter.List<ActorRole>();
            foreach (var actorRole in actors)
            {
                Console.WriteLine(
                "Harrison Ford played {0} in {1}",
                actorRole.Role,
                movie.Name);
            }
        }
    }
}
