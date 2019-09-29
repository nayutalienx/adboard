using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Comment;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Implementation
{
    public class CommentManager : ICommentManager
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAdvertRepository _advertRepository;
        private readonly IUserRepository _userRepository;

        public CommentManager(IAdvertRepository advertRepository, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _advertRepository = advertRepository;

            _commentRepository = commentRepository;

            _userRepository = userRepository;
        }

        public void AddComment(NewCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException();
            if (dto.UserId == -1)
                throw new Exception("Гости не могут оставлять комментарии");
            var advert = _advertRepository.Get(dto.AdvertId);
            if (advert == null)
                throw new Exception("Такого объявления не существует");
            if (string.IsNullOrWhiteSpace(dto.Text))
                throw new Exception("Текст обязателен");

            _commentRepository.Add(new Comment
            {
                AdvertId = dto.AdvertId,
                AuthorName = dto.AuthorName,
                Text = dto.Text
            });
        }

        public CommentDto[] GetCommentsByAdvert(long id)
        {
            var advert = _advertRepository.Get(id);
            if (advert == null)
                throw new Exception("Такого объявления не существует");
            return _commentRepository.GetCommentsByAdvert(id).Select(x => {
                return new CommentDto
                {
                    AdvertId = x.AdvertId,
                    AuthorName = x.AuthorName,
                    Text = x.Text
                };
            }).ToArray();
        }

        public void RemoveCommentsByAdvert(long id)
        {
            _commentRepository.RemoveCommentsByAdvert(id);
        }
    }
}
