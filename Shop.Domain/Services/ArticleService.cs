﻿using System;
using System.Collections.Generic;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository repository;

        public ArticleService(IArticleRepository repository)
        {
            this.repository = repository;
        }

        public List<Article> GetAllArticles()
        {
            return repository.GetAll();
        }
    }
}
