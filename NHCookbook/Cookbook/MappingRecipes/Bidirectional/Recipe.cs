﻿using FluentNHibernate;
using NH4CookbookHelpers;
using NH4CookbookHelpers.Mapping;
using NH4CookbookHelpers.Mapping.Model;
using NHibernate;
using NHibernate.Cfg;

namespace MappingRecipes.Bidirectional
{
    public class Recipe : BaseMappingRecipe
    {
        protected override void Configure(Configuration cfg)
        {
            cfg.AddMappingsFromAssembly(GetType().Assembly);
        }

        protected override void AddInitialData(ISession session)
        {
            
  }
}
 }