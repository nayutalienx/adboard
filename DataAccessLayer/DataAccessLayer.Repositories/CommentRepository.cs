﻿using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AdboardContext context) : base(context)
        {
        }

        public Comment[] GetCommentsByAdvert(Advert advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            return Entity.Where(c => c.Advert.Id == advert.Id).ToArray();
        }

        public void RemoveCommentsByAdvert(Advert advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            Entity.RemoveRange(GetCommentsByAdvert(advert));
        }
    }
}
