using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;
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
        private readonly IMapper _mapper;

        public CommentManager(IAdvertRepository advertRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _advertRepository = advertRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public void AddComment(NewCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException();
            if (dto.AuthorId == -1)
                throw new Exception("Гости не могут оставлять комментарии");
            var advert = _mapper.Map<Advert>(_advertRepository.Get(dto.AdvertId));
            if (advert == null)
                throw new Exception("Такого объявления не существует");
            if (string.IsNullOrWhiteSpace(dto.Text))
                throw new Exception("Текст обязателен");

            _commentRepository.Add(_mapper.Map<Comment>(dto));
            _commentRepository.SaveChanges();
        }

        public CommentDto[] GetCommentsByAdvert(AdvertDto dto)
        {
            var advert = _advertRepository.Get(dto.Id);
            if (advert == null)
                throw new Exception("Такого объявления не существует");
            return _mapper.Map<CommentDto[]>(_commentRepository.GetCommentsByAdvert(advert));
        }

        public void RemoveCommentsByAdvert(RemoveAdvertDto dto)
        {
            _commentRepository.RemoveCommentsByAdvert(_mapper.Map<Advert>(dto));
        }
    }
}
